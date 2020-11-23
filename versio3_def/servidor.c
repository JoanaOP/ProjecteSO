#include <stdio.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <string.h>
#include <mysql.h>
#include <pthread.h>

//Declaració variables 
int contador_serveis;
int numSockets;
int sockets[100]; //vector de sockets on anirem guardant el sock_conn de cada client
int i;

//Estructura necessària per l'accès excluent
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

// FUNCIONS
typedef struct{
	char Usuari[20];
	int Socket;
}Usuari;

typedef struct{
	Usuari UsuarisConectats[100];
	int num;
}LlistaConectats;

LlistaConectats llista;

void AfegirUsuariConectat(LlistaConectats *llista, char nom[20], int socket){
	int i = 0;
	int trobat = 0;
	while (i<llista->num && trobat==0)
	{
		if (strcmp(llista->UsuarisConectats[i].Usuari,nom)==0)
		{
			trobat=1;
		}
		i = i+1;
	}
	if (trobat==0)
	{
		if (llista->num==-1){
			llista->num = 0;
		};
		llista->UsuarisConectats[llista->num].Socket = socket;
		strcpy(llista->UsuarisConectats[llista->num].Usuari,nom);
		llista->num = llista->num + 1;
	}
	return;
}

void EliminarUsuariConectat(LlistaConectats* llista, char nom[20]){
	int i=0;
	int trobat=0;
	while (i<llista->num && trobat==0)
	{
		if (strcmp(llista->UsuarisConectats[i].Usuari,nom)==0)
		{
			trobat = 1;
			strcpy(llista->UsuarisConectats[i].Usuari,"");
			llista->UsuarisConectats[i].Socket = -10;
		}
		else
			i = i+1;
	}
	return;
}

int ObtenerSocket (LlistaConectats* llista, char nom[20]){
	int i = 0;
	int trobat = 0;
	while (i<llista->num && trobat ==0)
	{
		if (strcmp(llista->UsuarisConectats[i].Usuari,nom)==0)
		{
			trobat=1;
			return llista->UsuarisConectats[i].Socket;
		}
		i = i+1;
	}
	if (trobat==0)
	{
		return -1;
	}
}

void CadenaLlistaConectats (LlistaConectats* llista, char UsuarisConectats[512]){
	//funcio que crea un string amb la llista dels usuaris conectats separats per , i el numero d'usuaris davant (ex 3,Jordi,Nuria,Joana)
	int i = 0;
	int sumatori=0;
	char res1[50];
	res1[0] = '\0';
	while (i<llista->num)
	{
		if (llista->UsuarisConectats[i].Socket!=-10)
		{
			sprintf(res1,"%s,%s",res1,llista->UsuarisConectats[i].Usuari);
			sumatori = sumatori + 1;
		}
		i=i+1;
	}
	sprintf(UsuarisConectats,"%d%s",sumatori,res1);
	printf("%s\n",UsuarisConectats);
	return;
}

int PertanyUsuari(MYSQL *conn, char nomusuari[20]) // =1 si l'usuari està a la llista
{
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
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
int ComprovarContrasenya(MYSQL *conn, char respuesta[512],char contrasenya[20], char nomusuari[20]) // =1 si l'usuari que tenim té la contrasenya que s'escriu
{
	MYSQL_RES *resultado;
	MYSQL_ROW row;	
	char consulta[120];
	
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
int NumeroTotalUsuari(MYSQL *conn, char respuesta[512]) // ens retornara el num total d'usuaris, utilit per assiganr ID
{
	MYSQL_RES *resultado; 
	MYSQL_ROW row;
	char consulta[80];
	
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

void RegistrarUsuari(MYSQL *conn, char respuesta[512], char nomusuari[20],char contrasenya[80], int ID) // =1 es que s'ha registrat correctament; = 0 es qu el'usuari ja existia
{
	char consulta_nova[80];
	consulta_nova[0] = '\0';
	MYSQL_RES *resultado; 
	sprintf(consulta_nova,"INSERT INTO JUGADOR VALUES(%d,'%s','%s')",ID,nomusuari,contrasenya);
	int err = mysql_query (conn, consulta_nova);
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


void NomGuanyadorsPartida (MYSQL *conn, char respuesta[512], char* nombre){ //Nuria
	
	MYSQL_RES *resultado; 
	MYSQL_ROW row;
	char consulta[300];
	int err;
	
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
	{
		strcpy(respuesta,"-1");
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

void PartidaMaximsPunts (MYSQL* conn, char respuesta[512], char* nombre){ //Jordi
	
	MYSQL_RES *resultado; 
	MYSQL_ROW row;
	int err;
	char consulta[300];
	
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
	{
		strcpy(respuesta,"-1");
		printf ("No se han obtenido datos en la consulta\n");
	}
	else
	{
		// El resultado debe ser una matriz con una sola fila
		// y una columna que contiene el ID de la partida
		strcpy(respuesta, row[0]);
		printf ("ID de la partida que ha ganado con mas puntos: %s\n", row[0]);
	}
	return;
	mysql_close (conn);
	exit(0);
}

void PersonaQueNoHaGuanyat(MYSQL *conn, char respuesta[512]){ //Joana
	
	MYSQL_RES *resultado; 
	MYSQL_ROW row;
	int err;
	
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

void InicialitzarConexio(MYSQL *conn){
		
	//Creamos una conexion al servidor MYSQL 
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//inicializar la conexion
	conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "T2_BBDDJuego",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
}

void InicialitzarSocket(int sock_listen, int PORT){
	
	struct sockaddr_in serv_adr;
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
	
	
}


void RecibirConexion(int sock_conn, int sock_listen){
	printf ("Escuchando\n");
	sock_conn = accept(sock_listen, NULL, NULL);
	printf ("He recibido conexion\n");
	
};


void EnviarLlistaConectatsNotificacio(int numSockets, int sockets[100], char UsuarisConectats[512]){
	// Funció per enviar la notificació amb la llista de usuaris conectats dins d'una string
	// Aqui preparamos una notificacion
	char notificacio[512];
	printf("Usuaris conectats: %s\n",UsuarisConectats);
	sprintf(notificacio,"7/%s",UsuarisConectats);// afegir el 7/ pk el client sapiga que esta rebent
	printf("Notificacio llista conectats: %s\n",notificacio);
	// usamos el vector de sockets para enviar la notificacion a todos los clientes conectados
	int j;
	for (j=0;j<numSockets;j++){//numConectados nos indica quantos sockets hay, es decir quantos conectados (en nuestro caso la i del vector de sockets)
		write(sockets[j],notificacio,strlen(notificacio));//enviamos a través de cada socket la notificacion
		
	}
	
}


void *AtenderCliente(void *socket){
	
	MYSQL *conn;
	int err;
	
	//InicialitzarConexio(conn);
	
	//Creamos una conexion al servidor MYSQL 
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//inicializar la conexion
	conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "T2_BBDDJuego",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	// REBRE PETICIONS
	
	int sock_conn;
	int *s;
	s=(int *) socket;
	sock_conn = *s;
	
	char peticion[512];

	int ret;
	
	char nomusuari[20];
	char contrasenya[20];
	char ID_Partida[20];
	char NomJugador[20];
	
	//recibir i ejecutar peticion
	
	int terminar =0; //para terminar la conexion con el boton desconexion
	
	while (terminar ==0)
	{
		
		char respuesta[512];
		respuesta[0] = '\0';
		char UsuarisConectats[512];
		UsuarisConectats[0]='\0';
		
		// Ahora recibimos la petici?n
		ret=read(sock_conn, peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		printf ("Peticion: %s\n",peticion);
		
		
		// vamos a ver que quieren
		char *p = strtok( peticion, "/");
		int codigo =  atoi (p);
		// Ya tenemos el c?digo de la petici?n
		
		printf ("CODI %d\n",codigo);
		if (codigo == 1) //entrar amb un usuari que ja existeix
		{
			p = strtok( NULL, "/");
			strcpy(nomusuari,p);
			p = strtok( NULL, "/");
			strcpy (contrasenya, p);
			//Ja tenem el nom del usuari, hem de mirar si coinicdeix amb un de la base de dades ==> fem una funcio per a aixó
			if (PertanyUsuari(conn, nomusuari) == 1){
				if (ComprovarContrasenya(conn, respuesta,contrasenya, nomusuari) == 1){
					strcpy(respuesta, "1/1");
					write (sock_conn,respuesta, strlen(respuesta));
					AfegirUsuariConectat(&llista,nomusuari,sock_conn);
					CadenaLlistaConectats(&llista,UsuarisConectats);
					EnviarLlistaConectatsNotificacio(numSockets,sockets,UsuarisConectats);
				}
				else{
					strcpy(respuesta, "1/2");
					write (sock_conn,respuesta, strlen(respuesta));
				}
				
			}
			
			else{
				strcpy(respuesta, "1/3");
				write (sock_conn,respuesta, strlen(respuesta));
			}
			
		}
		else if (codigo == 2){
			int ID;
			p = strtok( NULL, "/");
			strcpy(nomusuari,p);
			p = strtok( NULL, "/");
			strcpy (contrasenya, p);
			if (PertanyUsuari(conn, nomusuari) == 1){
				strcpy(respuesta, "2/2");
				write (sock_conn,respuesta, strlen(respuesta));
			}
			else{
				ID = NumeroTotalUsuari(conn, respuesta) ;
				ID = ID + 1;
				printf("El ID es %d\n",ID);
				printf("Nomusuari: %s\n",nomusuari);
				RegistrarUsuari(conn, respuesta, nomusuari, contrasenya, ID);
				printf("%s\n",respuesta);
				strcpy(respuesta, "2/1");
				printf("%s\n",respuesta);
				write (sock_conn,respuesta, strlen(respuesta));
				AfegirUsuariConectat(&llista,nomusuari,sock_conn);
				CadenaLlistaConectats(&llista,UsuarisConectats);
				EnviarLlistaConectatsNotificacio(numSockets,sockets,UsuarisConectats);
			}
			
		}
		else if (codigo == 3)
		{
			p = strtok( NULL, "/");
			strcpy (ID_Partida, p);
			NomGuanyadorsPartida (conn, respuesta, ID_Partida);
			sprintf(respuesta,"3/%s",respuesta);
			printf("Resposta codi 3: %s",respuesta);
			write (sock_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo == 4)
		{
			p = strtok( NULL, "/");
			strcpy (NomJugador, p);
			PartidaMaximsPunts(conn, respuesta, NomJugador);
			sprintf(respuesta,"4/%s",respuesta);
			printf("Resposta codi 4: %s",respuesta);
			write (sock_conn,respuesta, strlen(respuesta));
		}
		else if (codigo == 5)
		{
			PersonaQueNoHaGuanyat(conn, respuesta);
			sprintf(respuesta,"5/%s",respuesta);
			printf("Resposta codi 5: %s",respuesta);
			write (sock_conn,respuesta, strlen(respuesta));				
		}
		//ja no hi ha peticio 6, ara es una notifiacio
		// ja no hi ha peticio 7, ara es una notificacio
		else if (codigo == 0)
		{
			terminar =1;
			p = strtok( NULL, "/");
			strcpy (nomusuari, p);
			EliminarUsuariConectat(&llista,nomusuari);
			CadenaLlistaConectats(&llista,UsuarisConectats);
			EnviarLlistaConectatsNotificacio(numSockets,sockets,UsuarisConectats);
		}
		if (codigo!=0)
			printf("Esperando consulta\n");
		if ((codigo == 3) || (codigo == 4) || (codigo == 5)) // connectar, registrar-se, desconnectar-se i demanar numero de serveis no els contem.
		{
			pthread_mutex_lock( &mutex);
			contador_serveis = contador_serveis +1;
			pthread_mutex_unlock( &mutex);
			//notificar a todos los clientes conectados los servicios realizados
			char char_contador_serveis[20];
			sprintf(char_contador_serveis,"6/%d",contador_serveis);
			int j;
			for(j=0;j<i;j++){
				write(sockets[j],char_contador_serveis,strlen(char_contador_serveis));
			}
			
		}		
		
	}
	// Se acabo el servicio para este cliente
	close(sock_conn); 
	
}



int main(int argc, char *argv[])
{
	int PORT = 50054; //en tenim 3! 
	contador_serveis = 0;
	
	//InicialitzarSocket(int sock_listen, int PORT);
	llista.num=0;
	//INICIEM ELS SOCKETS
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	
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
	//Socket en el que vamos a estar escuchando i La cola de peticiones pendientes no podra ser superior a 3
	
	
	pthread_t thread[100]; //vector on guardarem el identificador de thread de cada client
	//recibir conexion i peticiones
	// Bucle infinito
	for (;;){
		
		//RecibirConexion(sock_conn,sock_listen);
		printf("Escoltant\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);		
		printf("He rebut conexió\n");
		
		sockets[numSockets] = sock_conn;
		
		pthread_create(&thread[numSockets], NULL, AtenderCliente, &sockets[numSockets]);
		numSockets = numSockets+1;
		i=i+1;
	}
}

