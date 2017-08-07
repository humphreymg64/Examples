/*                                          */
/* Author: Matthew Humphrey                 */
/* Date  : 4/26/2016                        */
/*                                          */
/* Purpose: Encapsulates the thread safe    */
/*          enqueue and dequeue methods.    */
/*                                          */
/*                                          */

#include "SafeQueue.h"

struct message {
   char payload[32]; // The rest of the message
	 int sender;			 // Who sent the message
};

queue <message> messageQueue; //the queue for the threads to talk.
pthread_mutex_t lock; //the mutex lock for critical regions 
sem_t sem;            //semphore used for waiting

//      SafeQueue()
//      
//      Constructor that initializes the semaphores and mutexes
SafeQueue::SafeQueue(){
    sem_init(&sem, 0, 0);
	  pthread_mutex_init(&lock,NULL);
}

//
//      enqueue(message)
//      
//      Adds the message m to the queue
//      Returns nothing
void SafeQueue::enqueue(void* ms){
	message msg = *((message*)ms);
	pthread_mutex_lock(&lock);
  messageQueue.push(msg);
  sem_post(&sem);
  pthread_mutex_unlock(&lock);
}

//
//      dequeue()
//      
//      Removes the top item from the queue
//      Returns the item at the front of the queue
void* SafeQueue::dequeue(){
	message msg;
	sem_wait(&sem);
  pthread_mutex_lock(&lock);
  msg = messageQueue.front();
  messageQueue.pop();
  pthread_mutex_unlock(&lock);
	return (void*)&msg;
}
