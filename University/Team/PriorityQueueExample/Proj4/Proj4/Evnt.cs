///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Sim with Queues and PQ's
//	File Name:		Evnt.cs
//	Description:	Class for arrival and departure events
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
using System.Threading.Tasks;

namespace Proj4
{
    enum EVENTTYPE { ARRIVAL, DEPARTURE };  //Enum values for Arrival and Departure events

    /// <summary>
    /// Evnt Class holds type of event, registrant, and event time.
    /// </summary>
    class Evnt : IComparable
    {
        public int ID;                     //Registrant's ID 
        public DateTime eventTime;         //time event occurs
        public EVENTTYPE type;             //arrival/departure event
        public Registrant owner;           //the registrant the event belongs to


        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="owner">Registrant to generate event for</param>
        public Evnt (Registrant owner)
        {
            ID = -1;                        //default ID = -1
            eventTime = DateTime.Now;       //default eventTime is Current time
            type = EVENTTYPE.ARRIVAL;       //default event is Arrival 
            this.owner = owner;             //owner is owner 


        }//end Event(Registrant) 

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="e"></param>
        public Evnt (Evnt e)
        {
            this.ID = e.ID;
            this.eventTime = e.eventTime;
            this.type = e.type;


        }//end Evnt(Evnt)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"> type of event</param>
        /// <param name="eventTime">time event occured</param>
        /// <param name="ID">ID of registrant</param>
        public Evnt (EVENTTYPE type, DateTime eventTime, int ID)
        {
            this.type = type;
            this.eventTime = eventTime;
            this.ID = ID;
        }//end Event(EVENTTYPE,INT)


        #endregion

        /// <summary>
        /// Operator to compare if Event occurs before or after another
        /// </summary>
        /// <param name="e1">first event </param>
        /// <param name="e2">second event</param>
        /// <returns>returns true or false</returns>
        public static bool operator <(Evnt e1, Evnt e2)
        {
            return (e1.CompareTo (e2) < 0);
        }

        /// <summary>
        /// Operator to compare if Event occurs before or after another
        /// </summary>
        /// <param name="e1">first event </param>
        /// <param name="e2">second event</param>
        /// <returns>returns true or false</returns>
        public static bool operator >(Evnt e1, Evnt e2)
        {
            return (e1.CompareTo (e2) > 0);
        }

        /// <summary>
        /// Operator to compare if Events are >=
        /// </summary>
        /// <param name="e1">first event </param>
        /// <param name="e2">second event</param>
        /// <returns>returns true or false</returns>
        public static bool operator >=(Evnt e1, Evnt e2)
        {
            bool result = true;

            if (e1 < e2)
            {
                result = false;
            }
            return result;

        }

        /// <summary>
        /// Operator to compare if Event is <=
        /// </summary>
        /// <param name="e1">first event </param>
        /// <param name="e2">second event</param>
        /// <returns>returns true or false</returns>
        public static bool operator <=(Evnt e1, Evnt e2)
        {
            bool result = true;

            if (e1 > e2)
            {
                result = false;
            }

            return result;

        }

        /// <summary>
        /// To string to display Event information
        /// </summary>
        /// <returns> Returns formatted string</returns>
        public override string ToString ( )
        {
            string str = "";
            str += String.Format ("Registrant {0}", ID);
            str += type;
            str += string.Format (" at {0}", eventTime.ToShortTimeString ( ));
            return str;
        }

        /// <summary>
        /// Compares Evnt Objects
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo (object obj)
        {
            if (!(obj is Evnt))
                throw new NotImplementedException ( );

            Evnt e = (Evnt)obj;
            return (DateTime.Compare (this.eventTime, e.eventTime));
        }

    }
}
