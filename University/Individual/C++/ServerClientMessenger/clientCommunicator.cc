#include "clientCommunicator.h"
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <netdb.h>
#include <iostream>
#include <string.h>
#include <sstream>
#include <stdio.h>
#include <errno.h>
#include <arpa/inet.h>
#include <netdb.h>
#include <fstream>
#include <pthread.h>
#include <time.h>
using namespace std;

		struct message {
   	char payload[32]; // The rest of the message
	 	int sender;			  // Who sent the message
		};

	//
	//		clientCommunicator()
	//
	//		A simple constructor
	clientCommunicator::clientCommunicator(){
		
	}

	//
	//      sendMsg()
	//      
	//      Sends a message to the logging server from connect()
	void clientCommunicator::sendMsg(int sockdesc, void* m){
		message ms = *((message*)m);
		send(sockdesc, ms.payload, 80, 0);		
	}

	int clientCommunicator::recieveMsg(int fd, void *vptr, int n){
	int   nleft;
  int   nread;
  char   *ptr;

	ptr = (char*)vptr;
	nleft = n;
	while (nleft > 0) {
		if ( (nread = read(fd, ptr, nleft)) < 0) {
			 if (errno == EINTR)
					nread = 0;      /* and call read() again */
			 else
					return(-1);
		} 
	 else if (nread == 0)
			 break;            /* EOF */

		nleft -= nread;
		ptr   += nread;
		}  	
		return(n - nleft);      /* return >= 0 */	
	}
	
	//      connect()
	//      
	//      Connects to the logging server
	void clientCommunicator::connectUp(string hostname, string portnum, int* sockdesc, string logFileName){

	 int connection;
	 char * serStr;
   struct addrinfo* myinfo;
	 message m;
	 char hn[81], pn[81];
	
   *sockdesc = socket(AF_INET, SOCK_STREAM, 0);
   if ( *sockdesc < 0 )
   {
      cout << "Error creating socket" << endl;
      exit(0);
   }

   if ( getaddrinfo( strcpy(hn,hostname.c_str()), strcpy(pn,portnum.c_str()), NULL, &myinfo) != 0 )
   {
      cout << "Error getting address" << endl;
      exit(0);
   }
	 
	// Attempt to connect to the host
   connection = connect(*sockdesc, myinfo->ai_addr, myinfo->ai_addrlen);
   if ( connection < 0 )
   {
      cout << "Error in connect " << connection << hn << pn << endl;
      exit(0);
   }
	 strcpy(m.payload,logFileName.c_str());
	 sendMsg(*sockdesc, (void*)&m);
}

	