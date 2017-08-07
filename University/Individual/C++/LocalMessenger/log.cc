/*                                          */
/* Author: Matthew Humphrey                 */
/* Date  : 1/26/2016                        */
/*                                          */
/* Purpose: Read up to two files and put    */
/*          them in a log file.             */
/*                                          */
/*                                          */

#include "log.h"

log::log(){
  log::setLogfileName("log.txt");
}

log::log (char* lname){
  setLogfileName(lname);
}

log::log (string lname){
  setLogfileName(lname);
}

int log::open(){
  char cname[MAX_LOG_STRING];
  strcpy(cname,logfilename.c_str());
  logF.open(cname,fstream::out|fstream::app|fstream::in);
  return 0;
}

int log::close(){
  logF.close();
  return 0;
}

void log::setLogfileName(string cname){
 logfilename = cname; 
}

int log::writeLogRecord(string s){
  if(!logF.is_open()){
    open();
  }
  logF << getTimeStamp();
  logF << s << endl;
  return 0;
}

string log::getTimeStamp(){
  time_t nu; 
  time(&nu);
  return ctime(&nu);
}
