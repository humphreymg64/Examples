/* Author: Matthew Humphrey                 */
/* Date  : 4/26/2016                        */
/*                                          */
/* Purpose: To encapsulate the client       */
/*          sending and recieving messages  */
/*                                          */
/*                                          */

#ifndef CLIENTCOMMUNICATOR_H
#define CLIENTCOMMUNICATOR_H

#include "communicator.h"

using namespace std;

//	Class communicator
//	The abstract class from which others will inherit
class clientCommunicator:public communicator {
		
public:
	
		struct message {
   	char payload[32]; // The rest of the message
	 	int sender;			  // Who sent the message
		};
	
	//
	//		clientCommunicator()
	//
	//		A simple constructor
	clientCommunicator();
	
	//
	//      send()
	//      
	//      Sends a message to the logging server
	
	void sendMsg(int sockdesc, void* m);
	
	int recieveMsg(int fd, void *vptr, int n);
	
	//      connect()
	//      
	//      Connects to the logging server
	void connectUp(string hostname, string portnum, int* sockdesc, string logFileName);
};

#endif
