/*                                          */
/* Author: Matthew Humphrey                 */
/* Date  : 1/26/2016                        */
/*                                          */
/* Purpose: Create a set of robots and move */
/*          them around a grid using threads*/
/*          and multiprocessing.            */
/*                                          */

#include <unistd.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <netdb.h>
#include <errno.h>
#include <iostream>
#include <fstream>
#include <pthread.h>
#include <sstream>
#include <string.h>
#include <stdlib.h>
#include <time.h>
#include "SafeQueue.h"
#include "log.h"
#include "clientCommunicator.h"
using namespace std;

struct message {
   char payload[32]; // The rest of the message
	 int sender;			 // Who sent the message
};

struct world {
  int robos;       	// the number of robots
  int X;        		//the maximum x
  int Y;        		//the maximum Y
	string hostname;	//the host where the logger will be opened since the setup methods return world structs
	string portnum;		//the port where the logger will be opened since the setup methods return world structs
};

struct robot {
  int id;       //the robot's number
  int X;        //the robot's x
  int Y;        //the robot's Y
};

struct roboProcessInfo{
  world w;    //the world information the robot needs
  robot robo; //the robot's own information
};

clientCommunicator* cc = new clientCommunicator();		//used to connect and send messages to the server
SafeQueue queues[6];			//the queues that will send messages from the robots to the main process
roboProcessInfo rpis[5];  //given to each thread to know their positions and their world

//      storeFile()
//      
//      Writes a from a file (readFile)
//      Returns the next line from readFile.
string storeFile(fstream& readFile){
  string line;
  
  readFile.peek(); //sets the eof bit if it is empty
  
  if (!readFile.eof() && !readFile.fail()){
    getline(readFile, line);
    if(line.empty()){
      return " ";
    }
  }
  return line;
}

//
//      storeFile()
//      
//      Gets user input.
//      Returns the user's input as a string.
string storeUserInput(){
    string input;
  
    cout << "Please enter the next robot movement command." << endl;
    getline(cin, input);
    if(input.empty()){
      cout << "That was an empty string." << endl;
      return " ";
    }
    else{
      return input;
    }
}

//
//      checkPipe()
//      
//      Checks if a pipe opened correctly
//      Returns an error message to the screen if something went wrong
bool checkPipe(int pipeToCheck[2]){
  if( pipe(pipeToCheck) == -1){
    cout << "A pipe failed to open." << endl;
    return false;
  }
  return true;
}

//
//      moveRobo()
//      
//      Moves a robot
//      Returns a string about the robot's current position
robot moveRobo(message m, roboProcessInfo rpi){
  char * toks;
  toks = strtok(m.payload, " ");
  
  if(strcmp(toks, "M") == 0 || strcmp(toks, "m") == 0){   //if it is a movement command
    toks = strtok(NULL, " ");
    if(toks == NULL){
      cout << "Bad command." << endl;
      return rpi.robo;
    }
    else if(atoi(toks) == rpi.robo.id){
      toks = strtok(NULL, " ");
      if(toks == NULL){
      cout << "Bad command." << endl;
      return rpi.robo;
      }
      else if(strcmp(toks, "N") == 0 || strcmp(toks, "n") == 0){
        rpi.robo.Y++;
      }
      else if(strcmp(toks, "S") == 0 || strcmp(toks, "s") == 0){
        rpi.robo.Y--;
      }
      else if(strcmp(toks, "E") == 0 || strcmp(toks, "e") == 0){
        rpi.robo.X++;
      }
      else if(strcmp(toks, "W") == 0 || strcmp(toks, "w") == 0){
        rpi.robo.X--;
      }
      else{
        cout << "A robot could not understand the direction " << toks << endl;
      }
    }
    else{
      cout << "A robot got the someone else's message" << endl;
    }
  }
  
  else if(strcmp(toks, "Q") == 0 || strcmp(toks, "q") == 0){  //If the command asks to end the program
    exit(0);
  }
  
  if(rpi.robo.X > rpi.w.X){ //Check that the robot has not left the world
    rpi.robo.X = rpi.w.X;
  }
  else if(rpi.robo.X < 0){
    rpi.robo.X = 0;
  }
  
  if(rpi.robo.Y < 0){
    rpi.robo.Y = 0;
  }
  else if(rpi.robo.Y > rpi.w.Y){
    rpi.robo.Y = rpi.w.Y;
  }
  
  return rpi.robo;
}

//
//      roboProcess()
//      
//      Does what the robot processes need to do
//      Returns nothing, but kills the thread
void* roboProcess(void* rpiVoid){
  message msg;       //the message used to store things off the queue and put things on it
  stringstream converter;   //used to create the output by combining strings and ints
  roboProcessInfo rpi = *((roboProcessInfo*)rpiVoid); //used to change rpiVoid from a void pointer
  
  do{
    converter.str("");    //clears out the converter
    msg = *((message*)queues[rpi.robo.id].dequeue());
  
    rpi.robo.id++; //since the user will enter robot ids numbered starting at one
    rpi.robo = moveRobo(msg, rpi);
    cout << "Robot " << rpi.robo.id << " got the message " << msg.payload << endl;
    converter << "P " << rpi.robo.id << " " << rpi.robo.X << " " << rpi.robo.Y; 
    strcpy(msg.payload, converter.str().c_str());
    rpi.robo.id--;  //since the id is used internally starting at zero

    queues[5].enqueue((void*)&msg);
  
  }while(strcmp(msg.payload, "Q") != 0 || strcmp(msg.payload, "q") != 0);
  pthread_exit(0);
}

//
//      getRoboNum()
//      
//      Figures out which robot a message should go to
//      Returns the robot process id a message was meant for
int getRoboNum(message m, world w){
  char * toks;
  if(strcmp(m.payload," ") == 0){
    return -2;  //this means that the string was empty.
  }
  toks = strtok(m.payload, " ");
  int roboNum = 0;
  if(strcmp(toks, "M") == 0 || strcmp(toks, "m") == 0){   //if it is a movement command
    toks = strtok(NULL, " ");
    if(toks == NULL){
      return -1; //this indicates that it does not understand the robot number
    }
  }
  else{
      return -1; //this indicates that it does not understand the robot numbernumber
  }
  roboNum = atoi(toks) - 1;
  if (roboNum > w.robos - 1 || roboNum < 0){
    return -1; //this indicates that it does not understand the robot number
  }
  else{
    return roboNum;
  }
}

//
//      killChildren()
//      
//      Should kill all of main's children and the parent
//      Returns nothing
void killChildren(int numChildren, int sockdesc){
  message m;  //message to deliver the 'q' statement
	char* serStr;
	
  strcpy(m.payload, "Q");
  for(int i = 0;i < numChildren; i++){
    queues[i].enqueue((void*)&m);
  }
  exit(0);
}

//
//      writeToRobo()
//      
//      Give a robot a value, log it, and get it returned.
//      Returns nothing
void writeToRobo(message msg, world w, int sockdesc){
  int roboNum = getRoboNum(msg,w);
	char* serStr;
  
  if(strcmp(msg.payload, "Q") == 0 || strcmp(msg.payload, "q") == 0){
		cc->sendMsg(sockdesc,(void*)&msg);
    killChildren(w.robos, sockdesc);
  }
  else if(roboNum == -1){
    cout << "That robot does not exist or none was specified." << endl;
  }
  else if(roboNum == -2){//the string was empty
  }
  else{
    cc->sendMsg(sockdesc,(void*)&msg);
  	//read(sockdesc, serStr, 80);
    //cout << "Client gets back " << serStr << endl;
		queues[roboNum].enqueue((void*)&msg);
	  msg = *((message*)queues[5].dequeue());
    cc->sendMsg(sockdesc,(void*)&msg);
    //read(sockdesc, serStr, 80);
    //cout << "Client gets back " << serStr << endl;
		cout << "Parent recieved " << msg.payload << endl;
  }
} 

//
//      setup()
//      
//      Gets uses the setupfile info to create a world structure
//      Returns the world with the line appended to it
world setup(message setupLine, world w){
  char * toks; 
  string checker = setupLine.payload;
	
	 if(checker.empty()){
    return w;
  }
	
	toks = strtok(setupLine.payload, " ");
	
  if(strcmp(toks, "R") == 0 || strcmp(toks, "r") == 0){
    toks = strtok(NULL, " ");
    if(toks == NULL){
      cout << "Bad command." << endl;
      return w;
    }
    w.robos = atoi(toks);
    if(w.robos > 5 || w.robos < 0){
      w.robos = 0;
    }
  }
  
  else if(strcmp(toks, "X") == 0 || strcmp(toks, "x") == 0){
    toks = strtok(NULL, " ");
    if(toks == NULL){
      cout << "Bad command." << endl;
      return w;
    }
    w.X = atoi(toks);
    if(w.X < 0){
      w.X = 1;
    }
  }
  
  else if(strcmp(toks, "Y") == 0 || strcmp(toks, "y") == 0){
    toks = strtok(NULL, " ");
    if(toks == NULL){
      cout << "Bad command." << endl;
      return w;
    }
    w.Y = atoi(toks);
    if(w.Y < 0){
      w.Y = 1;
    }
  }
	
	else{
		
    if(strcmp(w.hostname.c_str(),"none") == 0 ){
			w.hostname = setupLine.payload;
		}
		else{
			w.portnum = setupLine.payload;
		}
	}
  
  return w;
}

//
//      setupNoFile()
//      
//      Gets setupfile info from the user to create a world structure
//      Returns the world with the user created line appended to it
world setupNoFile(world w){
  message mess;
  char * toks;
  string enteredString;
  
  cout << "Please enter the amount of robots, a dimension for the world, a port number or a host address." << endl;
  getline(cin, enteredString);
  if(enteredString.empty()){
    cout << "That was an empty string." << endl;
    return w;
  }
  strcpy(mess.payload, enteredString.c_str());
  toks = strtok(mess.payload, " ");
    
  if(strcmp(toks, "R") == 0 || strcmp(toks, "r") == 0){
    toks = strtok(NULL, " ");
    if(toks == NULL){
      cout << "Bad command." << endl;
      return w;
    }
    w.robos = atoi(toks);
    if(w.robos > 5 || w.robos < 0){
      cout << "Too many or too few robots were entered." <<endl;
      w.robos = 0;
    }
    cout << "Robots: " << w.robos << endl;
  }
  
  else if(strcmp(toks, "X") == 0 || strcmp(toks, "x") == 0){
    toks = strtok(NULL, " ");
    if(toks == NULL){
      cout << "Bad command." << endl;
      return w;
    }
    w.X = atoi(toks);
    if(w.X < 0){
      w.X = 0;
    }
    cout << "X: " << w.X << endl;
  }
  
  else if(strcmp(toks, "Y") == 0 || strcmp(toks, "y") == 0){
    toks = strtok(NULL, " ");
    if(toks == NULL){
      cout << "Bad command." << endl;
      return w;
    }
    w.Y = atoi(toks);
    if(w.Y < 0){
      w.Y = 0;
    }
    cout << "Y: " << w.Y << endl;
  }
  
  else{
    if(strcmp(w.hostname.c_str(),"none") == 0 ){
			w.hostname = mess.payload;
		}
		else{
			w.portnum = mess.payload;
		}
  }
  
  return w;
}


//
//      createRobos()
//      
//      Creates the robot processes and runs them
//      Returns the number of robots created
void createRobos(world &roboWorld){
  pthread_t threads[roboWorld.robos];
  
  for(int i = 0; i < roboWorld.robos; i++){ //create the robots
    roboProcessInfo rpi;
    rpis[i].robo.X = 0;
    rpis[i].robo.Y = 0;
    rpis[i].w.robos = roboWorld.robos;
    rpis[i].w.X = roboWorld.X;
    rpis[i].w.Y = roboWorld.Y;
    rpis[i].robo.id = i;
    pthread_create(&threads[i], NULL, roboProcess, (void*)&rpis[i]);
  }
}

//
//      flagS()
//      
//      Opens the setupfile
//      Returns the setupfile
bool flagS(char c, fstream& setupfile){
  try {
    setupfile.open(optarg,fstream::in|fstream::out);
    return true;
  }
  catch (ifstream::failure e) {
    cout << "Problem opening the setup file." << endl;
    return false;
  }
}

//
//      flagC()
//      
//      Opens the commandfile
//      Returns the commandfile
bool flagC(char c, fstream& commandfile){
  try {
    commandfile.open(optarg,fstream::in|fstream::out);
    return true;  
  }
  catch (ifstream::failure e) {
    return false;
  }
}

//
//      main()
//
//      Does everything described in the purpose.
//
int main( int argc, char** argv )
{
  world roboWorld;
  fstream setupfile, commandfile;
  char c;
  int my_id = 1, errorcodes, i, sockdesc;
  bool hasCmd = false;
  bool hasSetup = false;
  string input;
  string logFileName = "-1";
  message line;
  
  while ( (c = getopt(argc, argv, "s:c:l:")) != -1 ) {//check what kind of arguments were given
      switch (c) {
        case 's': hasSetup = flagS(c, setupfile);
                  break; 
        case 'c': hasCmd = flagC(c, commandfile);
                  break; 
        case 'l': logFileName = optarg;
                  break;
        default:  exit(0);
      }; 
   }
	roboWorld.robos = 0;
  roboWorld.X = 0;
  roboWorld.Y = 0;
	roboWorld.hostname = "none";
	roboWorld.portnum = "none";
  if(!hasSetup){  //if there was no setup file included
    while(!(roboWorld.X > 0 && roboWorld.Y > 0 && roboWorld.robos > 0 &&
						roboWorld.portnum != "none" && roboWorld.hostname != "none")){
      roboWorld = setupNoFile(roboWorld);
    }
  }
  else{
    while (!setupfile.fail() && !(roboWorld.X > 0 && roboWorld.Y > 0 && roboWorld.robos > 0 &&
																	roboWorld.portnum != "none" && roboWorld.hostname != "none")){  //setup the world
      strcpy(line.payload, storeFile(setupfile).c_str());
      roboWorld = setup(line,roboWorld);
    }
    setupfile.close();
  }
  cout << "Robos: " << roboWorld.robos << "     X: " << roboWorld.X << "\tY: " << roboWorld.Y <<endl;
	cout << "Logger set to try to open at: " << roboWorld.hostname << ":" << roboWorld.portnum << endl;
  createRobos(roboWorld);
	cc->connectUp(roboWorld.hostname, roboWorld.portnum, &sockdesc, logFileName);
  
	while (!commandfile.eof() && !commandfile.fail() && hasCmd
        && (strcmp(line.payload, "Q") != 0 && strcmp(line.payload, "q") != 0)){  //do the command file instructions
    
		strcpy(line.payload, storeFile(commandfile).c_str());
    writeToRobo(line, roboWorld, sockdesc);
  }
  commandfile.close();
  
  while (!hasCmd && (strcmp(line.payload, "Q") != 0 && strcmp(line.payload, "q") != 0)){  //get user input
    strcpy(line.payload, storeUserInput().c_str());
    writeToRobo(line, roboWorld, sockdesc);
  }
  close(sockdesc);
  return 0; 
}