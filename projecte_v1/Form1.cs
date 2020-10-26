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
        int PORT = 9010;
        int HeEntrat = 0; // utilitzare aquesta vriable per evitar l'error d'intentar entrar 2 cops.

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

                // Nos desconectamos
                this.BackColor = Color.Gray;
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
            
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
        {
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

                string missatge = "1/" + NomUsuari.Text + "/" + Contrasenya.Text; // Volem connectar el usuari => missatge 1/NomUsuari/Contrasenya
                MessageBox.Show(missatge);
                // Enviamos al servidor el nombre tecleado ==> això es per enviar la consulta
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(missatge);
                server.Send(msg);

                //Recibimos la respuesta del servidor ==> aixo es per llegir la resposta (la resposta tmb es un vector de bytes
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                missatge = Encoding.ASCII.GetString(msg2).Split('\0')[0];// li posem el split pq volem que ens el parteixi per el indicador de final de string
                MessageBox.Show(missatge);
                byte[] msg3;
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
            else
                MessageBox.Show("Ja has entrat");
            

            
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

    }
}
