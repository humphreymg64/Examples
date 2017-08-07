/*                                          */
/* Author: Matthew Humphrey                 */
/* Date  : 4/26/2016                        */
/*                                          */
/* Purpose: To encapsulate the server       */
/*          sending and recieving messages  */
/*                                          */
/*                                          */

#include "serverCommunicator.h"
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

serverCommunicator::serverCommunicator(){
	
}

void serverCommunicator::sendMsg(int sockdesc, void* m){	
	communicator::message ms = *((communicator::message*)m);
		send(sockdesc, ms.payload, 80, 0);		
	}

int serverCommunicator::recieveMsg(int fd, void *vptr, int n){
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

int serverCommunicator::connectUp(int sockdesc, struct addrinfo* myinfo, int portnum, char pn[81]){
	stringstream converter;
	converter << portnum;
	if ( sockdesc < 0 )
	{
			cout << "Error creating socket" << endl;
			exit(0);
	}

	// Set up the address record
	if ( getaddrinfo( "0.0.0.0", pn, NULL, &myinfo ) != 0 )
	{
		cout << "Error getting address" << endl;
		exit(0);
	}
	// Bind the socket to an address
	while (bind(sockdesc, myinfo->ai_addr, myinfo->ai_addrlen) < 0 )
	{
		portnum++;
		converter << portnum; 
		strcpy(pn, converter.str().c_str()); 
			if ( getaddrinfo( "0.0.0.0", pn, NULL, &myinfo ) != 0 )
			{
				cout << "Error getting address" << endl;
				exit(0);
			}
	 }
	 // Display the port number that was successful
	 cout << "Bind successful, using portnum = " << portnum << endl;

	 // Now listen to the socket
	 if ( listen(sockdesc, 1) < 0 )
	 {
			cout << "Error in listen" << endl;
			exit(0);
	 }
	// Accept a connect request and use connection, *NOT* sockdesc
	// (tcp handoff)
	cout << "Calling accept" << endl;
	return accept(sockdesc, NULL, NULL);
}
