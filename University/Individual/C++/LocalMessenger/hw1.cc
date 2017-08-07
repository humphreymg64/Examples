/*                                          */
/* Author: Matthew Humphrey                 */
/* Date  : 1/26/2016                        */
/*                                          */
/* Purpose: Read up to two files and put    */
/*          them in a log file.             */
/*                                          */
/*                                          */

#include <iostream>
#include <fstream>
#include <sstream>
#include <string.h>
#include <stdlib.h>
#include <time.h>
#include "log.h"
using namespace std;

struct message {
   int  from;        // Who sent this: 0 == parent, 1 == first robot, etc.
   char payload[32]; // The rest of the message
};

struct world {
  int robos;       // the number of robots
  int X;        //the maximum x
  int Y;        //the maximum Y
};

struct robot {
  int id;       //the robot's number
  int X;        //the robot's x
  int Y;        //the robot's Y
};

int logPipe[2];
int roboPipes[5][2];
int pipeBack[2];

//
//      readPipe()
//      
//      reads the indicated pipe
//      Returns a value that may indicate failure.
message readPipe(message m, int pipeToRead[2]){
  //if(
    read(pipeToRead[0], (char*)&m, sizeof(m));
                //) == 0){
    return m;
  //}
  //else{
    //exit(0);
  //}
}

//
//      writePipe()
//      
//      writes to an indicated pipe
//      Returns a value that may indicate failure.
int writePipe(message m, int pipeToWrite[2]){
  //if(
    write(pipeToWrite[1], (char*)&m, sizeof(m));
      //){
    return 0;
  //}
  //else{
  //  exit(0);
  //}   
}

//
//      checkForArgs()
//      
//      Checks that there were arguments sent to hw1.
//      
void checkForArgs(int argc){
     if ( argc < 2 ) {
      cout << "Usage: hw1 -s setupfile -c [commandfile] -l [logfile]" << endl;
      exit(0);
   } 
}

//
//      storeFile()
//      
//      Writes a from a file (readFile) to another file (logFile).
//      Returns the last line from readFile.
string storeFile(fstream& readFile){
  string line;
  
  readFile.peek(); //sets the eof bit if it is empty
  
  if (!readFile.eof() && !readFile.fail()){
    getline(readFile, line);
  }
  return line;
}

//
//      storeFile()
//      
//      Gets user input and stores it to a file (logFile).
//      Returns the last line from readFile.
string storeUserInput(){
    string input;
    cout << "Please enter the next command." << endl;
    getline(cin, input);
    return input;
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
//      logProcess()
//      
//      Does what the log process needs to do
//      Returns nothing, but kills the process
void logProcess(log * logFile){
  message line;
  line = readPipe(line, logPipe);
  logFile->writeLogRecord(line.payload); 
  if(strcmp(line.payload, "Q") == 0){
    logFile->writeLogRecord("END");
    logFile->close();
    exit(0);
  }
}

//
//      moveRobo()
//      
//      Moves a robot
//      Returns a string about the robot's current position
robot moveRobo(message m, robot robo, world w){
    char * toks = strtok(m.payload, " ");
  
  if(strcmp(toks, "M") == 0){   //if it is a movement command
    toks = strtok(NULL, " ");
    if(atoi(toks) == robo.id){
      toks = strtok(NULL, " ");
      if(strcmp(toks, "N") == 0){
        robo.Y++;
      }
      else if(strcmp(toks, "S") == 0){
        robo.Y--;
      }
      else if(strcmp(toks, "E") == 0){
        robo.X++;
      }
      else if(strcmp(toks, "W") == 0){
        robo.X--;
      }
      else{
        cout << "A robot could not understand the direction " << toks << endl;
      }
    }
    else{
      cout << "A robot got the someone else's message" << endl;
    }
  }
  
  else if(strcmp(toks, "P") == 0){  //If the command ask for the robot's position
    toks = strtok(NULL, " ");
    if(atoi(toks) == robo.id){
      return robo;
    }
  }
  
  else if(strcmp(toks, "Q") == 0){  //If the command ask for the robot's position
    exit(0);
  }
  
  else{   //Throw the robot back to 0,0 if it got a bad command
    robo.X = 0;
    robo.Y = 0;
  }
  
  if(robo.X > w.X){ //Check that the robot has not left the world
    robo.X = w.X;
  }
  else if(robo.X < 0){
    robo.X = 0;
  }
  
  if(robo.Y < 0){
    robo.Y = 0;
  }
  else if(robo.Y > w.Y){
    robo.Y = w.Y;
  }
  
  return robo;
}


//
//      roboProcess()
//      
//      Does what the robot processes need to do
//      Returns nothing, but kills the process
robot roboProcess(world w, int id, robot me){
  message msg;
  string position;
  stringstream converter;
  
  me.id = (id * -1) - 2;
  msg = readPipe(msg, roboPipes[me.id]);
  me.id++;
  me = moveRobo(msg, me, w);
 
  if(strcmp(msg.payload, "Q") == 0){
    exit(0);
  }
  
  cout << "Robot " << me.id << " got the message " << msg.payload << endl;
  msg.from = me.id;
  converter << "P " << me.id << " " << me.X << " " << me.Y; 
  strcpy(msg.payload, converter.str().c_str());
  writePipe(msg, pipeBack);
  
  return me;
}

//
//      getRoboNum()
//      
//      Figures out which robot a message should go to
//      Returns the robot process id a message was meant for
int getRoboNum(message m){
  char * toks = strtok(m.payload, " ");
  
  if(strcmp(toks, "M") == 0){   //if it is a movement command
    toks = strtok(NULL, " ");
  }
  else{
    strcpy(toks, "1");
  }
  return atoi(toks) - 1;
}

//
//      killChildren()
//      
//      Should kill all of main's children
//      Returns nothing
void killChildren(int numChildren){
  message m;
  strcpy(m.payload, "Q");
  m.from = -1;
  writePipe(m, logPipe);
  for(int i = 0; i < numChildren; i++){
    writePipe(m, roboPipes[i]);
  }
  exit(0);
}

//
//      writeToRobo()
//      
//      Give a robot a value, log it, and get it returned.
//      Returns nothing
void writeToRobo(message line, int lastId){
  if(strcmp(line.payload, "Q") == 0){
    killChildren(lastId);
  }
  else{
    writePipe(line, logPipe);  
    writePipe(line, roboPipes[ getRoboNum( line ) ] ); 
    line = readPipe(line, pipeBack);
    writePipe(line, logPipe);  
    cout << "Parent recieved " << line.payload << endl;
  }
}

//
//      setup()
//      
//      Gets uses the setupfile info to create a world structure
//      Returns the world with the line appended to it
world setup(message setupLine, world w){
  char * toks = strtok(setupLine.payload, " ");
  
  if(strcmp(toks, "R") == 0){
    toks = strtok(NULL, " ");
    w.robos = atoi(toks);
    if(w.robos > 5 || w.robos < 0){
      w.robos = 0;
    }
  }
  
  else if(strcmp(toks, "X") == 0){
    toks = strtok(NULL, " ");
    w.X = atoi(toks);
    if(w.X < 0){
      w.X = 0;
    }
  }
  
  else if(strcmp(toks, "Y") == 0){
    toks = strtok(NULL, " ");
    w.Y = atoi(toks);
    if(w.Y < 0){
      w.Y = 0;
    }
  }
  
  else{
    w.robos = -1;
    w.X = -1;
    w.Y = -1;
  }
  
  return w;
}

//
//      createRobos()
//      
//      Creates the robot processes and runs them
//      Returns the number of robots created
int createRobos(robot robo, world roboWorld){
  int my_id, i;
  for(i = 0; i < roboWorld.robos; i++){ //create the robots
    if(checkPipe( roboPipes[i] ) ){
      my_id = fork();
      if(my_id > 0){
        my_id = (i * -1) - 2;
      }
      else if(my_id == -1){
        killChildren(i);
      }
      if(my_id < -1){
        robo.X = 0;
        robo.Y = 0;
      }
      while(my_id < -1){
        robo = roboProcess(roboWorld, my_id, robo);
      }
    }
  }
  return i;
}

//
//      flagS()
//      
//      Opens the setupfile
//      Returns the setupfile
void flagS(char c, fstream& setupfile){
  try {
    setupfile.open(optarg,fstream::in|fstream::out);
  }
  catch (ifstream::failure e) {
    cout << "Problem opening the setup file." << endl;
    exit(0);
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
  log* logFile;
  robot robo;
  fstream setupfile, commandfile;
  char c;
  int my_id, errorcodes, i;
  bool hasCmd = false;
  string input;
  message line;
  logFile = new log();
  
  checkForArgs(argc);

  while ( (c = getopt(argc, argv, "s:c:l:")) != -1 ) {//check what kind of arguments were given
      switch (c) {
        case 's': flagS(c, setupfile);
                  break; 
        case 'c': hasCmd = flagC(c, commandfile);
                  break; 
        case 'l': logFile->setLogfileName(optarg);
                  break;
        default:  exit(0);
      }; 
   } 
  
  if( checkPipe( logPipe) ){ //checks if the pipe opens properly
    my_id = fork();
  
    if(my_id != 0){ //if it is not th parent
      my_id = -7; //indicates that it is the log process
    }
    else if(my_id == -1){ //if something went wrong
      return 0;
    }
    else{         //close the logFile for the parent process
      logFile->close();
    }
  }
  
  if(my_id == -7){    //begin the logging
    logFile->writeLogRecord("BEGIN");
  }
  while(my_id == -7){ //read lines and put them into the log file
    logProcess(logFile);
  }
  
  while (!setupfile.eof() && !setupfile.fail()){  //setup the world
    strcpy(line.payload, storeFile(setupfile).c_str());
    line.from = my_id;
    errorcodes = writePipe(line, logPipe);
    roboWorld = setup(line,roboWorld);
  }
  setupfile.close();
  
  if(!checkPipe(pipeBack)){//open the pipeback pipe before creating the robots
    cout<<"Main cannot open the pipeback pipe" <<endl;
    killChildren(0);
  }
  
  i = createRobos(robo, roboWorld);
  
  while (!commandfile.eof() && !commandfile.fail() && hasCmd){  //do the command file instructions
    strcpy(line.payload, storeFile(commandfile).c_str());
    line.from = my_id;
    writeToRobo(line, i);
  }
  commandfile.close();
  
  while (!hasCmd && strcmp(line.payload, "Q") != 0){  //get user input
    strcpy(line.payload, storeUserInput().c_str());
    line.from = my_id;
    writeToRobo(line, i);
  }
  
  return 0; 
}