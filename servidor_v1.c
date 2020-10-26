#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>

// FUNCIONS

int PertanyUsuari(MYSQL *conn, MYSQL_RES *resultado, MYSQL_ROW row, char nomusuari[20]) // =1 si l'usuari està a la llista
{
	char consulta[80];
	strcpy (consulta,"SELECT JUGADOR.USERNAME FROM (JUGADOR) WHERE JUGADOR.USERNAME = '");
	strcat (consulta, nomusuari);
	strcat (consulta, "'");
	
	int err = mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
		return 0; // pq no s'han obtingut dades per tant no hi ha l'usuari
	
	else
		return 1;
	
	mysql_close (conn);
	exit(0);
}
int ComprovarContrasenya(MYSQL *conn, MYSQL_RES *resultado, MYSQL_ROW row, char respuesta[512],char contrasenya[20], char nomusuari[20], char consulta[80]) // =1 si l'usuari que tenim té la contrasenya que s'escriu
{
	strcpy (consulta,"SELECT JUGADOR.USERNAME FROM (JUGADOR) WHERE JUGADOR.USERNAME = '");
	strcat (consulta, nomusuari);
	strcat (consulta, "' AND JUGADOR.PASWORD = '");
	strcat (consulta, contrasenya);
	strcat (consulta, "'");
	
		
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		return 0; // pq no s'han obtingut dades per tant no hi ha l'usuari
		strcpy(respuesta, "2");
	}
	else{
		return 1;
		strcpy(respuesta, "1");
	}
	
	mysql_close (conn);
	exit(0);
}
int NumeroTotalUsuari(MYSQL *conn, MYSQL_RES *resultado, MYSQL_ROW row, char respuesta[512], char* consulta) // ens retornara el num total d'usuaris, utilit per assiganr ID
{
	int numTot = 0;
	strcpy (consulta,"SELECT * FROM (JUGADOR)");
	
	int err = mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
		return 0;
	else
	{
		while (row !=NULL) {
			numTot = numTot +1;
			row = mysql_fetch_row (resultado);
		}
		return numTot;
	}
	strcpy(respuesta, "numTot");
	mysql_close (conn);
	exit(0);
}

void RegistrarUsuari(MYSQL *conn, MYSQL_RES *resultado, char* consulta, char respuesta[512], char nomusuari[20],char contrasenya[80], int ID) // =1 es que s'ha registrat correctament; = 0 es qu el'usuari ja existia
{
	strcpy (consulta,"INSERT INTO JUGADOR VALUES (");
	char ID_string[1];
	sprintf(ID_string, "%d", ID);
	strcat (consulta, ID_string);
	strcat (consulta, ",'");
	strcat (consulta, nomusuari);
	strcat (consulta, "','");
	strcat (consulta, contrasenya);
	strcat (consulta, "')");
	
	int err = mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	strcpy(respuesta, "1");
	return;	
	mysql_close (conn);
	exit(0);
}


void NomGuanyadorsPartida (MYSQL *conn, MYSQL_RES *resultado, MYSQL_ROW row, char* consulta, int err, char respuesta[512], char* nombre){ //Nuria
	strcpy (consulta,"SELECT JUGADOR.USERNAME FROM (JUGADOR,PARTIDA,PARTICIPACION) WHERE PARTIDA.ID = '");
	strcat (consulta, nombre);
	strcat (consulta,"'AND PARTICIPACION.ID_P = PARTIDA.ID AND PARTICIPACION.NOMEQUIP = PARTIDA.EQUIPGUANYADOR AND PARTICIPACION.ID_J = JUGADOR.ID");
	
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	
	row = mysql_fetch_row (resultado);
	printf("Estoy dentro de la funcion\n");
	if (row == NULL)
		printf ("No se han obtenido datos en la consulta\n");
	else{
		strcpy(respuesta,row[0]);
		row = mysql_fetch_row (resultado);
		while (row !=NULL) 
		{
			
			sprintf (respuesta,"%s,%s", respuesta, row[0]);
			row = mysql_fetch_row (resultado);
		}
	}
	return;
	mysql_close (conn);
	exit(0);
}

int PartidaMaximsPunts (MYSQL* conn, MYSQL_RES* resultado, MYSQL_ROW row, char* consulta, int err, char respuesta[512], char* nombre){ //Jordi
	
	strcpy (consulta,"SELECT DISTINCT PARTIDA.ID FROM (JUGADOR,PARTICIPACION,PARTIDA) WHERE PARTICIPACION.PUNTS IN ( SELECT DISTINCT MAX(PARTICIPACION.PUNTS) FROM (JUGADOR,PARTICIPACION) WHERE JUGADOR.USERNAME = '"); 
	strcat (consulta, nombre);
	strcat (consulta,"' AND JUGADOR.ID = PARTICIPACION.ID_J) AND PARTICIPACION.ID_P = PARTIDA.ID;");
	// hacemos la consulta 
	err=mysql_query (conn, consulta); 
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//recogemos el resultado de la consulta
	resultado = mysql_store_result (conn); 
	row = mysql_fetch_row (resultado);
	if (row == NULL)
		printf ("No se han obtenido datos en la consulta\n");
	else
		// El resultado debe ser una matriz con una sola fila
		// y una columna que contiene el ID de la partida
		printf ("ID de la partida que ha ganado con mas puntos: %s\n", row[0]);
	int puntos = atoi(row[0]);
	return puntos;
	mysql_close (conn);
	exit(0);
}

void PersonaQueNoHaGuanyat(MYSQL *conn, MYSQL_RES *resultado, MYSQL_ROW row, int err, char respuesta[512]){ //Joana
	err=mysql_query (conn, "SELECT JUGADOR.USERNAME FROM (JUGADOR,PARTIDA,PARTICIPACION) WHERE PARTICIPACION.NOMEQUIP NOT IN (SELECT PARTIDA.EQUIPGUANYADOR FROM PARTIDA) AND PARTICIPACION.ID_J=JUGADOR.ID;");
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	
	row = mysql_fetch_row (resultado);
	
	if (row == NULL){
		printf ("No se han obtenido datos en la consulta\n");
	}
	else{
		strcpy(respuesta,row[0]);
		row = mysql_fetch_row (resultado);
		while (row !=NULL) 
		{
			
			sprintf (respuesta,"%s,%s", respuesta, row[0]);
			row = mysql_fetch_row (resultado);
		}
	}
	return;
	mysql_close (conn);
	exit(0);
}

int main(int argc, char *argv[])
{
	int PORT = 9010;
	MYSQL *conn;
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado1;
	MYSQL_RES *resultado2;
	MYSQL_RES *resultado;
	MYSQL_ROW row1;
	MYSQL_ROW row2;
	MYSQL_ROW row;
	int puntos;
	char nombre[10];
	char consulta [80];
	//Creamos una conexion al servidor MYSQL 
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//inicializar la conexion
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "bd",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//INICIEM ELS SOCKETS
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(PORT);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	int i;
	// Bucle infinito
	for (;;){
		printf ("Escuchando\n");
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
		int terminar =0;
		// Entramos en un bucle para atender todas las peticiones de este cliente
		//hasta que se desconecte
		while (terminar ==0)
		{
			char respuesta[512]="";
			// Ahora recibimos la petici?n
			ret=read(sock_conn,peticion, sizeof(peticion));
			printf ("Recibido\n");
			
			// Tenemos que a?adirle la marca de fin de string 
			// para que no escriba lo que hay despues en el buffer
			peticion[ret]='\0';
			
			printf ("Peticion: %s\n",peticion);
			
			// vamos a ver que quieren
			char *p = strtok( peticion, "/");
			int codigo =  atoi (p);
			// Ya tenemos el c?digo de la petici?n
			char nomusuari[20];
			char contrasenya[20];
			char ID_Partida[20];
			char NomJugador[20];
			
			printf ("CODI %d\n",codigo);
			if (codigo == 1) //entrar amb un usuari que ja existeix
			{
				p = strtok( NULL, "/");
				strcpy(nomusuari,p);
				p = strtok( NULL, "/");
				strcpy (contrasenya, p);
				//Ja tenem el nom del usuari, hem de mirar si coinicdeix amb un de la base de dades ==> fem una funcio per a aixó
				if (PertanyUsuari(conn, resultado, row, nomusuari) == 1){
					if (ComprovarContrasenya(conn, resultado, row, respuesta,contrasenya, nomusuari, consulta) == 1){
						strcpy(respuesta, "1");
						write (sock_conn,respuesta, strlen(respuesta));
					}
					else{
						strcpy(respuesta, "2");
						write (sock_conn,respuesta, strlen(respuesta));
					}
				}
					
				else{
					strcpy(respuesta, "3");
					write (sock_conn,respuesta, strlen(respuesta));
				}
			}
			else if (codigo == 2){
				int ID;
				p = strtok( NULL, "/");
				strcpy(nomusuari,p);
				p = strtok( NULL, "/");
				strcpy (contrasenya, p);
				if (PertanyUsuari(conn, resultado, row, nomusuari) == 1){
					strcpy(respuesta, "2");
					write (sock_conn,respuesta, strlen(respuesta));
				}
				else{
					ID = NumeroTotalUsuari(conn, resultado, row, respuesta, consulta) ;
					ID = ID + 1;
					printf("El ID es %d\n",ID);
					RegistrarUsuari(conn, resultado, consulta, respuesta, nomusuari, contrasenya, ID);
					strcpy(respuesta, "1");// AQUI ES ON FALLA =(
					write (sock_conn,respuesta, strlen(respuesta));
					
				}
				
			}
			else if (codigo == 3)
			{
				p = strtok( NULL, "/");
				strcpy (ID_Partida, p);
				NomGuanyadorsPartida (conn, resultado, row, consulta, err, respuesta, ID_Partida);
				write (sock_conn,respuesta, strlen(respuesta));
			}
			
			else if (codigo == 4)
			{
				p = strtok( NULL, "/");
				strcpy (NomJugador, p);
				int partida = PartidaMaximsPunts(conn, resultado, row, consulta, err, respuesta, NomJugador);
				sprintf(respuesta, "%d", partida);
				write (sock_conn,respuesta, strlen(respuesta));
			}
			else if (codigo == 5)
			{
				PersonaQueNoHaGuanyat(conn, resultado, row, err, respuesta);
				write (sock_conn,respuesta, strlen(respuesta));				
			}
			else if ( codigo == 0){
				terminar =1;
			}
			if (codigo!=0)
				printf("Esperando consulta\n");
		
		}
		// Se acabo el servicio para este cliente
		close(sock_conn); 
	}
}

