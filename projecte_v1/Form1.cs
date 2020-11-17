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

namespace WindowsFormsApplication1
{
    public partial class form_inicial : Form
    {
        Socket server;
<<<<<<< HEAD
        int PORT = 9010;
        int HeEntrat = 0; // utilitzare aquesta vriable per evitar l'error d'intentar entrar 2 cops.
=======
        int PORT = 9060;
        string IP = "192.168.56.102";
        int HeEntrat = 0; // utilitzare aquesta variable per evitar l'error d'intentar entrar 2 cops.
        string[] Usuaris;
>>>>>>> dev-v3

        public form_inicial()
        {
            InitializeComponent();
        }
        public void Tancar_connexio()
        {
            if (HeEntrat != 0)
            {
                //Mensaje de desconexión
                string mensaje = "0/";
                HeEntrat = 0;

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
<<<<<<< HEAD

=======
                ObtenirConectats();
>>>>>>> dev-v3
                // Nos desconectamos
                this.BackColor = Color.Gray;
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
<<<<<<< HEAD
            
        }

        public void NomGuanyadorsPartida(string ID_Partida)
        {   // Demana al servidor el nom dels dos jugadors que van guanyar una partida
            // Protocol 3/ID_Partida

            string mensaje = "3/" + ID_Partida;
            MessageBox.Show(mensaje);
            // Envia al servidor la ID de la partida
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Rebem la resposta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show("Els jugadors que van guanyar la partida són: " + mensaje);
        }

        public void PartidaMaximPuntsJugador(string NomJugador) 
        {   // Demana al servidor el maxim de punts que ha fet un jugador a les seves partides
            // Protocol 4/NomJugador

            string mensaje = "4/" + NomJugador;
            MessageBox.Show(mensaje);
            //Envia al servidor el nom del jugador
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Rebem la resposta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show("La partida on el jugador " + NomJugador + " ha fet el màxim de punts és: " + mensaje);
        }

        public void PersonesQueNoHanGuanyat()
        {   // Demana al servidor el nom dels jugadors que no han guanyat cap partida
            // Protocol 5/

            string mensaje = "5/";
            // Nomes enviem el numero del protocol
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Rebem la resposta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            if (mensaje == "0")
                MessageBox.Show("No se han encontrado datos");
            else
                MessageBox.Show("Els jugadors que no han guanyat cap partida son: " + mensaje);
        }


        private void BotoEntrar_Click(object sender, EventArgs e)
=======

        }

        public void ObtenirConectats()
        {
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            string missatge = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            Usuaris = missatge.Split(',');
            if (Usuaris.Length > 1)
            {
                LlistaConectats.RowCount = Int32.Parse(Usuaris[0]);
                LlistaConectats.ColumnHeadersVisible = false;
                LlistaConectats.RowHeadersVisible = false;
                LlistaConectats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                int i = 1;
                int j = 0;
                while (i < Usuaris.Length)
                {
                    LlistaConectats[0, j].Value = Usuaris[i];
                    i = i + 1;
                    j = j + 1;
                }
            }
            else
            {
                LlistaConectats.Rows.Clear();
            }
        }
        public void Entrar()
>>>>>>> dev-v3
        {
            if (HeEntrat != 1)
            {
                HeEntrat = 1;
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
<<<<<<< HEAD
                IPAddress direc = IPAddress.Parse("192.168.56.102");
=======
                IPAddress direc = IPAddress.Parse(IP);
>>>>>>> dev-v3
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
<<<<<<< HEAD
                MessageBox.Show(missatge);
=======
>>>>>>> dev-v3
                // Enviamos al servidor el nombre tecleado ==> això es per enviar la consulta
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(missatge);
                server.Send(msg);

                //Recibimos la respuesta del servidor ==> aixo es per llegir la resposta (la resposta tmb es un vector de bytes
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                missatge = Encoding.ASCII.GetString(msg2).Split('\0')[0];// li posem el split pq volem que ens el parteixi per el indicador de final de string
<<<<<<< HEAD
                MessageBox.Show(missatge);
                byte[] msg3;
=======
>>>>>>> dev-v3
                if (Convert.ToInt32(missatge) == 1)
                {
                    MessageBox.Show("Existeix l'usuari i coincideix la contrasenya");
                    this.BackColor = Color.Lime;
<<<<<<< HEAD
=======
                    ObtenirConectats();

>>>>>>> dev-v3
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
            else
                MessageBox.Show("Ja has entrat");
<<<<<<< HEAD
            

            
        }

=======


        }
        public void Registrar()
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

                //Recibimos la respuesta del servidor ==> aixo es per llegir la resposta (la resposta tmb es un vector de bytes
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                missatge = Encoding.ASCII.GetString(msg2).Split('\0')[0];// li posem el split pq volem que ens el parteixi per el indicador de final de string
                if (Convert.ToInt32(missatge) == 1)
                {
                    MessageBox.Show("Usuari Registrat i connectat.");
                    this.BackColor = Color.Lime;
                    ObtenirConectats();
                }
                else if (Convert.ToInt32(missatge) == 2)
                {
                    MessageBox.Show("Ja existeix aquest usuari.");
                    Tancar_connexio();
                }
            }
        }
        public int NumeroServeis()
        {
            //Demanar numero de serveis realitzats
            string mensaje = "6/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            return Convert.ToInt32(mensaje);
        }

        public void NomGuanyadorsPartida(string ID_Partida)
        {   // Demana al servidor el nom dels dos jugadors que van guanyar una partida
            // Protocol 3/ID_Partida

            string mensaje = "3/" + ID_Partida;
            // Envia al servidor la ID de la partida
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Rebem la resposta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            if (mensaje == "-1")
            {
                MessageBox.Show("No s'ha obtingut resultat d'aquesta partida.");

            }
            else
            {
                MessageBox.Show("Els jugadors que van guanyar la partida són: " + mensaje);
            }
        }

        public void PartidaMaximPuntsJugador(string NomJugador)
        {   // Demana al servidor el maxim de punts que ha fet un jugador a les seves partides
            // Protocol 4/NomJugador

            string mensaje = "4/" + NomJugador;
            //Envia al servidor el nom del jugador
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Rebem la resposta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            if (mensaje == "-1")
            {
                MessageBox.Show("No s'ha obtingut resultat d'aquest jugador.");

            }
            else
            {
                MessageBox.Show("La partida on el jugador " + NomJugador + " ha fet el màxim de punts és: " + mensaje);
            }
            
        }

        public void PersonesQueNoHanGuanyat()
        {   // Demana al servidor el nom dels jugadors que no han guanyat cap partida
            // Protocol 5/

            string mensaje = "5/";
            // Nomes enviem el numero del protocol
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Rebem la resposta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            if (mensaje == "0")
                MessageBox.Show("No se han encontrado datos");
            else
                MessageBox.Show("Els jugadors que no han guanyat cap partida son: " + mensaje);
        }


        private void BotoEntrar_Click(object sender, EventArgs e)
        {
            Entrar();
        }

>>>>>>> dev-v3
        private void Desconnectar_Click(object sender, EventArgs e)
        {
            Tancar_connexio();
        }

        private void EnviarPeticionButton_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            
=======

>>>>>>> dev-v3
            if (DonamNomGuanyadorsPartidaButton.Checked)
            {
                if (ID_Partida_TextBox.Text == "")
                {
                    MessageBox.Show("No has escrit cap partida");
                }
                else
                {
                    //Enviar petició
                    NomGuanyadorsPartida(ID_Partida_TextBox.Text);
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
                    PartidaMaximPuntsJugador(Nom_Jugador_TextBox.Text);
<<<<<<< HEAD
                    
=======

>>>>>>> dev-v3
                }
            }
            else if (DonamPersonesQueNoHanGuanyatButton.Checked)
            {
                //Enviar petició
                PersonesQueNoHanGuanyat();
            }
            else
            {
                MessageBox.Show("Selecciona una consulta");
            }
        }

        private void RegistrarUsuari_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            if (HeEntrat != 1)
            {
                HeEntrat = 1;
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
                IPAddress direc = IPAddress.Parse("192.168.56.102");
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

                //Recibimos la respuesta del servidor ==> aixo es per llegir la resposta (la resposta tmb es un vector de bytes
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                missatge = Encoding.ASCII.GetString(msg2).Split('\0')[0];// li posem el split pq volem que ens el parteixi per el indicador de final de string
                MessageBox.Show(missatge);
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
        }

=======
            Registrar();
        }

        private void serveis_Click(object sender, EventArgs e)
        {
            if (HeEntrat != 0)
            {
                int num = NumeroServeis();
                count_lbl.Text = "Nº de serveis: " + num;
            }
            else
            {
                MessageBox.Show("Cal que et connectis primer.");
            }

        }

        private void MostrarUsuarisConectats_Click(object sender, EventArgs e)
        {
            //Demanar numero de serveis realitzats
            string mensaje = "7/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show("Els usuaris conectats son: " + mensaje);
        }

        private void form_inicial_Load(object sender, EventArgs e)
        {
        }

        private void MostrarUsuarisConectats_Click_1(object sender, EventArgs e)
        {
            string missatge = "7/";
            // Enviamos al servidor el nombre tecleado ==> això es per enviar la consulta
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(missatge);
            server.Send(msg);
            ObtenirConectats();
        }
>>>>>>> dev-v3
    }
}
