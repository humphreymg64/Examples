///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Sim with Queues and PQ's
//	File Name:		ConventionRegistration.cs
//	Description:	Class used to simulate the lines
//	Course:			CSCI 2210 - Data Structures	
//	Author:			Jarred Wininger, Winingerj@goldmail.etsu.edu ; Matthew Humphrey, Humphreymg@goldmail.etsu.edu
//	Created:		Monday, November 16, 2015
//	Copyright:		Jarred Wininger, Matthew Humphrey,  2015
//
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proj4
{
    /// <summary>
    /// The class that does the simulating
    /// </summary>
    class ConventionRegistration
    {

        private static int numRegistrants;              //number of registrants at event               
        private static int numberOfEvents;              //number of events processed
        private static int numberEnter;                 //the amount of enter events that have occured
        private static int numberExit;                  //the amount of exit events that have occured
        private static double expectedDuration;         //the amount of time it is expected to take from the front of the line
        private static int hoursOpen;                   //the amount of hours the lines will be open
        private static int longestLine;                 //the length of the longest line so far
        private static TimeSpan shortest;               //shortest registration time
        private static TimeSpan avg;                    //average registration time
        private static List<Registrant> registrants;    //list of registrants
        private static List<Queue<Registrant>> windows; //list of registration windows
        private static PriorityQueue PQ;                //Priority Queue for Events 
        private static TimeSpan longest;                //longest registration time
        private static DateTime timeRegistrationStarts; //time that registration begins

        public static Random r = new Random ( );        //generates random numbers

        /// <summary>
        /// Initializes a new empty instance of the <see cref="ConventionRegistration"/> class.
        /// </summary>
        public ConventionRegistration ( )
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConventionRegistration"/> class.
        /// </summary>
        /// <param name="nRegistrants">The expected amount of registrants.</param>
        /// <param name="numWindows">The number of windows.</param>
        /// <param name="expectedTime">The expected amount of time at the front.</param>
        /// <param name="hours">The hours the lines are open.</param>
        public ConventionRegistration (int nRegistrants, int numWindows, double expectedTime, int hours)
        {
            numRegistrants = NumberGenerators.Poisson (nRegistrants);           //set number of registrants
            expectedDuration = expectedTime;                //set expected time for registration
            windows = new List<Queue<Registrant>> ( );      //create new list of registration windows

            //create and add registration windows to list
            for (int i = 0; i < numWindows; i++)
            {
                Queue<Registrant> line = new Queue<Registrant> ( );
                windows.Add (line);
            }//end for loop

            PQ = new PriorityQueue (nRegistrants * 2);

            //Registration starts at 8 AM 
            timeRegistrationStarts = new DateTime (DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);

            hoursOpen = hours;
            numberOfEvents = 0;
            numberExit = 0;
            numberEnter = 0;
            longestLine = 0;
            shortest = new TimeSpan (999,59,59);
            longest = new TimeSpan (0, 0, 0);

        }

        /// <summary>
        /// Runs the simulation.
        /// </summary>
        public void RunSimulation ( )
        {
            int linePos;
            //generate registrants list
            GenerateRegistrants ( );

            while (!PQ.IsEmpty ( ))
            {
                if (PQ.GetMin ( ).type == EVENTTYPE.ARRIVAL)
                {
                    //find Next Available Line
                    linePos = pickLine ( );

                    registrants[findRegistrant (PQ.GetMin ( ).ID)].windowNum = linePos;
                    windows[linePos].Enqueue (registrants[findRegistrant (PQ.GetMin ( ).ID)]);
                    if (windows[linePos].Count == 1)
                    {
                        GenerateExit (PQ.GetMin ( ).ID);
                    }
                    if (windows[linePos].Count > longestLine)
                    {
                        longestLine = windows[linePos].Count;
                    }
                    numberEnter++;
                    numberOfEvents++;
                    displayLines ( );
                    PQ.RemoveMin ( );
                    //Thread.Sleep (500);
                }
                else
                {
                    linePos = registrants[findRegistrant (PQ.GetMin ( ).ID)].windowNum;
                    TimeSpan duration = (PQ.GetMin ( ).eventTime) - (registrants[findRegistrant (PQ.GetMin ( ).ID)].serviceTime);
                    if (duration > longest)
                    {
                        longest = duration;
                    }
                    if (duration < shortest)
                    {
                        shortest = duration;
                    }
                    avg += duration;
                    windows[linePos].Dequeue ( );

                    numberExit++;
                    numberOfEvents++;
                    displayLines ( );
                    //Thread.Sleep (500);
                    removeReg (linePos);
                }


            }
            DisplayFinal ( );
        }
        /// <summary>
        /// Semi-graphicly displays the lines on screen.
        /// </summary>
        public void displayLines ( )
        {
            Console.Clear ( );
            string[] output = new string[windows.Count ( )];
            int lineSize = 0;
            Console.WriteLine ("\t\tRegistration Windows\n\t\t--------------------");
            for (int i = 0; i < windows.Count ( ); i++)
            {
                lineSize = windows[i].Count ( );
                output[i] += "\nWindow " + i + ": ";

                for (int j = 0; j < lineSize; j++)
                {
                    output[i] += "☺ ";
                }
                Console.WriteLine (output[i]);

            }
            Console.WriteLine ("\n\nTotal number of events so far: " + numberOfEvents);
            Console.WriteLine ("Number of Enter Events: " + numberEnter + "  Number of Exit Events: " + numberExit);
            Console.WriteLine ("Total Registrants: " + numRegistrants);
            Console.WriteLine ("The longest line so far was " + longestLine + " people long.");

        }

        /// <summary>
        /// Calculates the average wait time.
        /// </summary>
        private static void CalcAvg ( )
        {
           avg = TimeSpan.FromTicks(avg.Ticks / registrants.Count);
        }

        /// <summary>
        /// Displays the results of the simulation.
        /// </summary>
        private static void DisplayFinal()
        {
            CalcAvg ( );
            Console.WriteLine ("\nThe average wait time for " + numRegistrants + " registrants was  " + avg);
            Console.WriteLine ("The minimum wait time was " + shortest + ", and the maximum wait time was " + longest);
        }

        /// <summary>
        /// Removes the next registrant.
        /// </summary>
        /// <param name="linePos">The index of the line.</param>
        public void removeReg(int linePos)
        {
            if (windows[linePos].Count != 0)
            {
                GenerateExit ( windows[linePos].Peek ( ).ID); 
            }

            PQ.RemoveMin ( );

        }

        /// <summary>
        /// Finds a registrant.
        /// </summary>
        /// <param name="ID">The identifier of a registrant.</param>
        /// <returns>
        /// The first index the registrant was found at, or a -1 if no registrant was found
        /// </returns>
        public static int findRegistrant (int ID)
        {
            int regPos = -1;

            for (int i = 0; i < numRegistrants; i++)
            {
                if (registrants[i].ID == ID)
                {
                    regPos = i;
                    break;
                }//endif
            }//endFor

            return regPos;
        }//end findRegistrant(INT)

        /// <summary>
        /// Picks the shortest line.
        /// </summary>
        /// <returns>
        /// The index of the shortest line
        /// </returns>
        public static int pickLine ( )
        {
            //assume first line is shortest
            int lineNum = 0;

            for (int i = 0; i < windows.Count ( ); i++)
            {
                if (windows[i].Count ( ) < windows[lineNum].Count ( ))
                {
                    lineNum = i;
                }
            }
            return lineNum;
        }//end pickLine()

        /// <summary>
        /// Generates the registrants.
        /// </summary>
        public static void GenerateRegistrants ( )
        {
            TimeSpan start;
            Registrant reg;
            registrants = new List<Registrant> ( );


            for (int registrant = 1; registrant <= numRegistrants; registrant++)
            {
                DateTime enterReg = timeRegistrationStarts;
                //time registrant enters registration event
                start = new TimeSpan (0, r.Next (hoursOpen * 60), 0);
                
                enterReg = enterReg.Add (start);
              

                reg = new Registrant (registrant, 0, enterReg);
                registrants.Add (reg);

                PQ.Insert (new Evnt (EVENTTYPE.ARRIVAL, enterReg, registrant));



            }//end For Loop

        }//endGenerateRegistrants()

        /// <summary>
        /// Generates the departure time for a registrant at the front of a line.
        /// </summary>
        /// <param name="ID">The identifier of the registrant.</param>
        public static void GenerateExit (int ID)
        {
            DateTime eventTime = registrants[findRegistrant (ID)].serviceTime;    //How long the event will take
            TimeSpan serviceDuration = new TimeSpan (0, (int)(1 + NumberGenerators.NegExp (expectedDuration)), (int)(30 + NumberGenerators.NegExp(expectedDuration)));    //how long they will be at the front of the line
            
            eventTime = eventTime.Add (serviceDuration);

            PQ.Insert (new Evnt (EVENTTYPE.DEPARTURE, eventTime, ID));
        }

    }
}
