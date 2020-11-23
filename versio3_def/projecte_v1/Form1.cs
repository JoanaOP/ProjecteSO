using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class form_inicial : Form
    {
        Socket server;
        int PORT = 50054;
        string IP = "147.83.117.22";
        int HeEntrat = 0; // utilitzare aquesta variable per evitar l'error d'intentar entrar 2 cops.
        
        string NomJugador; //per guardar el nom del jugador de la peticio "Donam la partida del jugador amb els maxims punts"
        Thread atendre; // objecte de la classe thread

        public form_inicial()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; // Necessari per a poder accedir als elements del form desde un thread diferent
        }

        private void AtendreServidor()
        {
            while (true) //bucle infinit
            {
                //Rebem missatge del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                // veiem quin codi de resposta hem rebut
                string[] parts = Encoding.ASCII.GetString(msg2).Split('/');
                int codi = Convert.ToInt32(parts[0]);
                string missatge = parts[1].Split('\0')[0]; // Guardem la resposta

                switch (codi)
                {
                    case 1: // Log in
                        EntrarResposta(missatge);
                        break;
                    case 2: // Registrar
                        RegistrarResposta(missatge);
                        break;
                    case 3: // Nom guanyadors partida
                        NomGuanyadorsPartidaResposta(missatge);
                        break;
                    case 4: // Partida maxim punts
                        PartidaMaximPuntsJugadorResposta(missatge, NomJugador);
                        break;
                    case 5: // Persones que no ha guanyat
                        PersonesQueNoHaGuanyatResposta(missatge);
                        break;
                    case 6: // Numero de serveis
                        NumeroServeisResposta(missatge);
                        break;
                    case 7: // Notificacio llista de conectats
                        ObtenirConectatsResposta(missatge);
                       // MessageBox.Show(missatge);
                        break;
                }
            }
        }

        public void Tancar_connexio()
        {
            if (HeEntrat != 0)
            {
                //Mensaje de desconexión
                string mensaje = "0/" + NomUsuari.Text + "/";
                HeEntrat = 0;

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                // Nos desconectamos
                atendre.Abort(); // tanquem el thread
                this.BackColor = Color.Gray;
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }

        }

     


        public void ObtenirConectatsResposta(string missatge)
        {
            try
            {
                string[] Usuaris;
                Usuaris = missatge.Split(',');
                if (Usuaris.Length > 1)
                {
                  //  LlistaConectats.RowCount = Int32.Parse(Usuaris[0]);
                    LlistaConectats.Rows.Clear();
                    LlistaConectats.ColumnHeadersVisible = false;
                    LlistaConectats.RowHeadersVisible = false;
                    LlistaConectats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    int i = 1;
                    int j = 0;
                    while (i < Usuaris.Length)
                    {
                        
                        LlistaConectats.Rows.Add(Usuaris[i]);
                        //LlistaConectats[0, j].Value = Usuaris[i];
                        i = i + 1;
                        j = j + 1;
                    }
                }
                else
                {
                   LlistaConectats.Rows.Clear();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("El missatge es incorrecte");
            }
        }
        public void EntrarPeticio()
        {
            
            if (HeEntrat != 1)
            {
                HeEntrat = 1;
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
                IPAddress direc = IPAddress.Parse(IP);
                IPEndPoint ipep = new IPEndPoint(direc, PORT);


                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    server.Connect(ipep);//Intentamos conectar el socket


                }
                catch (SocketException ex)
                {
                    //Si hay excepcion imprimimos error y salimos del programa con return 
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }

                string missatge = "1/" + NomUsuari.Text + "/" + Contrasenya.Text; // Volem connectar el usuari => missatge 1/NomUsuari/Contrasenya
                // Enviamos al servidor el nombre tecleado ==> això es per enviar la consulta
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(missatge);
                server.Send(msg);

                
            }
            else
                MessageBox.Show("Ja has entrat");


        }

        public void EntrarResposta(string missatge)
        {
            // Funció que processa la resposta a Entrar
            
            
            if (Convert.ToInt32(missatge) == 1)
            {
                MessageBox.Show("Existeix l'usuari i coincideix la contrasenya");
                this.BackColor = Color.Lime;                

            }
            else if (Convert.ToInt32(missatge) == 2)
            {
                MessageBox.Show("Existeix l'usuari però no coincideix la contrasenya");
                Tancar_connexio();
            }
            else if (Convert.ToInt32(missatge) == 3)
            {
                MessageBox.Show("No existeix l'usuari");
                //Mensaje de desconexión
                Tancar_connexio();
            }
        }

        public void RegistrarPeticio()
        {
            if (HeEntrat != 1)
            {
                HeEntrat = 1;
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
                IPAddress direc = IPAddress.Parse(IP);
                IPEndPoint ipep = new IPEndPoint(direc, PORT);


                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    server.Connect(ipep);//Intentamos conectar el socket


                }
                catch (SocketException ex)
                {
                    //Si hay excepcion imprimimos error y salimos del programa con return 
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }

                string missatge = "2/" + NomUsuari.Text + "/" + Contrasenya.Text; // Volem connectar el usuari => missatge 1/NomUsuari/Contrasenya
                MessageBox.Show(missatge);
                // Enviamos al servidor el nombre tecleado ==> això es per enviar la consulta
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(missatge);
                server.Send(msg);

                
            }
        }

        public void RegistrarResposta(string missatge)
        {
            // Funcio que processa la resposta a la peticio de registrarse

            
            if (Convert.ToInt32(missatge) == 1)
            {
                MessageBox.Show("Usuari Registrat i connectat.");
                this.BackColor = Color.Lime;                
            }
            else if (Convert.ToInt32(missatge) == 2)
            {
                MessageBox.Show("Ja existeix aquest usuari.");
                Tancar_connexio();
            }
        }

        public void NumeroServeisResposta(string missatge)
        {
            if (HeEntrat != 0)
            {
                int num = Convert.ToInt32(missatge);
                count_lbl.Text = "Nº de serveis: " + num;
            }
            else
            {
                MessageBox.Show("Cal que et connectis primer.");
            }        
            
        }

        public void NomGuanyadorsPartidaPeticio(string ID_Partida)
        {   // Demana al servidor el nom dels dos jugadors que van guanyar una partida
            // Protocol 3/ID_Partida

            string mensaje = "3/" + ID_Partida;
            // Envia al servidor la ID de la partida
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            
        }

        public void NomGuanyadorsPartidaResposta(string missatge)
        {
            
            if (missatge == "-1")
            {
                MessageBox.Show("No s'ha obtingut resultat d'aquesta partida.");

            }
            else
            {
                MessageBox.Show("Els jugadors que van guanyar la partida són: " + missatge);
            }
        }

        public void PartidaMaximPuntsJugadorPeticio(string NomJugador)
        {   // Demana al servidor el maxim de punts que ha fet un jugador a les seves partides
            // Protocol 4/NomJugador

            string mensaje = "4/" + NomJugador;
            //Envia al servidor el nom del jugador
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);                      
            
        }

        public void PartidaMaximPuntsJugadorResposta(string missatge,string NomJugador)
        {            
            if (missatge == "-1")
            {
                MessageBox.Show("No s'ha obtingut resultat d'aquest jugador.");
            }
            else
            {
                MessageBox.Show("La partida on el jugador " + NomJugador + " ha fet el màxim de punts és: " + missatge);
            }
        }

        public void PersonesQueNoHanGuanyatPeticio()
        {   // Demana al servidor el nom dels jugadors que no han guanyat cap partida
            // Protocol 5/

            string mensaje = "5/";
            // Nomes enviem el numero del protocol
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        public void PersonesQueNoHaGuanyatResposta(string missatge)
        {            
            if (missatge == "0")
                MessageBox.Show("No se han encontrado datos");
            else
                MessageBox.Show("Els jugadors que no han guanyat cap partida son: " + missatge);
        }

        private void Desconnectar_Click(object sender, EventArgs e)
        {
            Tancar_connexio();
            
        }

        private void EnviarPeticionButton_Click(object sender, EventArgs e)
        {

            if (DonamNomGuanyadorsPartidaButton.Checked)
            {
                if (ID_Partida_TextBox.Text == "")
                {
                    MessageBox.Show("No has escrit cap partida");
                }
                else
                {
                    //Enviar petició
                    NomGuanyadorsPartidaPeticio(ID_Partida_TextBox.Text);
                    
                }
            }

            else if (DonamPartidaMaximPuntsJugadorButton.Checked)
            {
                if (Nom_Jugador_TextBox.Text == "")
                {
                    MessageBox.Show("No has escrit cap nom");
                }
                else
                {
                    //Enviar petició
                    NomJugador = Nom_Jugador_TextBox.Text;
                    PartidaMaximPuntsJugadorPeticio(NomJugador);

                }
            }
            else if (DonamPersonesQueNoHanGuanyatButton.Checked)
            {
                //Enviar petició
                PersonesQueNoHanGuanyatPeticio();
            }
            else
            {
                MessageBox.Show("Selecciona una consulta");
            }
        }

        private void BotoEntrar_Click(object sender, EventArgs e)
        {
            EntrarPeticio();

            // Posar en marxa el thread que atendrà al servidor
            ThreadStart ts = delegate { AtendreServidor(); };
            atendre = new Thread(ts);
            atendre.Start();
        }

        private void RegistrarUsuari_Click(object sender, EventArgs e)
        {
            RegistrarPeticio();

            // Posar en marxa el thread que atendrà al servidor
            ThreadStart ts = delegate { AtendreServidor(); };
            atendre = new Thread(ts);
            atendre.Start(); 
        }

        private void form_inicial_Load(object sender, EventArgs e)
        {
            //LlistaConectats.RowCount = 10;
            LlistaConectats.ColumnCount = 1;
        }

        
    }
}
