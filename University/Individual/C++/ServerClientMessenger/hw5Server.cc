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
#include <pthread.h>
#include <errno.h>
#include "log.h"
using namespace std;

// Function declarations
// readn( ): reads n bytes from file descriptor fd
//    From Stevens, Unix Network Programming
int readn(int fd, void *vptr, int n);

// Thread function to handle one request
void* handleRequest( void* arg );

//
//      openLog()
//      
//      Opens the logFile
void openLog(log* logFile, string logFileName){
		
	if (logFileName == "-1"){ // -1 means that there was no log file given
      logFile->setLogfileName("log.txt");
    }
    else {
      logFile->setLogfileName(logFileName);
    }  
	
    logFile->open();
    logFile->writeLogRecord("BEGIN");
	
}

//
//      logProcess()
//      
//      Does what the log process needs to do
//      Returns nothing, but kills the process
void logProcess(log * logFile, string line){
  logFile->writeLogRecord(line); 
}

int main(int argc, char** argv )
{
   int sockdesc;
   struct addrinfo* myinfo;
   int portnum;
	 char pn[81];
   char buffer[40];
   int connection;
   int value;
   pthread_t thrd;
  stringstream converter;

	if (argc < 3){
		cout << "Usage hw5Server -p portnumber" << endl;
		exit(0);
	}
  char c = getopt(argc, argv, "p:");
  portnum = atoi(optarg);
	converter << portnum;
  strcpy(pn, converter.str().c_str()); 
	
   // Use AF_UNIX for unix pathnames instead
   // Use SOCK_DGRAM for UDP datagrams
   sockdesc = socket(AF_INET, SOCK_STREAM, 0);
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

   // Forever loop - kill manually
   // Accept a connect request and use connection, *NOT* sockdesc
   // (tcp handoff)
   while(true){
      cout << "Calling accept" << endl;
      connection = accept(sockdesc, NULL, NULL);
      while ( connection < 0 )
      {
         cout << "Error in accept" << endl;
         exit(0);
      }
         // Create a thread to handle the request
         // Show the connection number (your process's FILE
         // descriptor) for fun - the client could care less
	      cout << "Calling pthread_create with connection = " 
              << connection << endl;
        pthread_create(&thrd, NULL, handleRequest, (void *)&connection);
	      cout << "After create" << endl;
				pthread_exit(0);
	}
  return 0;
}

// Thread function - handles one client connection
void* handleRequest( void* arg )
{
   int connection;
   int value;
   char buffer[81];
	 string str = "-1";
	 bool fileNameSet = false;
	 log* logFile = new log();

   pthread_detach( pthread_self() );
   connection = *((int*)arg);
   // Show the actual FILE descriptor used
   cout << "Server thread, connection = " << connection << endl;
   // Receive a message
   // Note: the next two lines are alternate methods for reading from
   // the socket.
   //value = recv(connection, buffer, 20, 0);
   //value = read(connection, buffer, 80);
   value = readn(connection, (void*)buffer, 80);
   if ( value < 0 )
   {
      cout << "Error on recv" << endl;
      exit(0);
   }
   else if ( value == 0 )
   {
      cout << "End of transmission" << endl;
      exit(0);
   }

	str = buffer;
  openLog(logFile, str);
	
   // Continue until the message is "quit"
   while ( strcmp(buffer, "q") != 0  && strcmp(buffer, "Q") != 0) {
      // Get the next message
      value = readn(connection, (void*)buffer, 80);
		  str = buffer;		
      if ( value < 0 )
      {
         cout << "Error on recv" << endl;
         exit(0);
      }
		  else if(value == 0){
				break;
			}

	    cout << "Message received: " << buffer << endl;
		  //send(connection, buffer, 80, 0);
		 	logProcess(logFile,str);
   } // while
   // Close the socket
	
   logFile->writeLogRecord("END");
   logFile->close();
   close(connection);
   pthread_exit(0);
}

/* Adapted from Stevens, Unix Network Programming, v.1 */
/* include readn */
/* Read "n" bytes from a descriptor. */
int readn(int fd, void *vptr, int n)
{
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
      } else if (nread == 0)
         break;            /* EOF */

      nleft -= nread;
      ptr   += nread;
   }
   return(n - nleft);      /* return >= 0 */
}
/* end readn */