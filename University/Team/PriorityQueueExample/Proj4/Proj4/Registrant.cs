
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Sim with Queues and PQ's
//	File Name:		Registrant.cs
//	Description:	Class to create a registrant
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
    /// <summary>
    /// A class to identify registrants
    /// </summary>
    class Registrant
    {
        public int ID { get; set; }     //the unique id for each registrant
        public int windowNum { get; set; }  //the line the registrant is in
        public DateTime serviceTime { get; set; }   //amount of time to get through line

        /// <summary>
        /// Initializes a new default instance of the <see cref="Registrant"/> class.
        /// </summary>
        public Registrant ()
        {
            ID = 0;
            windowNum = 0;
            serviceTime = new DateTime (DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);
        }//end Registrant() 

        /// <summary>
        /// Initializes a new instance of the <see cref="Registrant"/> class.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <param name="windowNum">The window number.</param>
        /// <param name="serviceTime">The service time.</param>
        public Registrant (int ID, int windowNum, DateTime serviceTime)
        {
            this.ID = ID;
            this.windowNum = windowNum;
            this.serviceTime = serviceTime;
        }//end Registrant(INT,INT,DateTime)        
        
        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="r1">The first registrant.</param>
        /// <param name="r2">The second registrant.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Registrant r1, Registrant r2)
        {
            bool bEqual = false;
            if(r1.ID == r2.ID)
            {
                bEqual = true;
            }
            return bEqual;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="r1">The first registrant.</param>
        /// <param name="r2">The second registrant.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Registrant r1, Registrant r2)
        {
            bool bNotEqual = false;
            if (r1.ID != r2.ID)
            {
                bNotEqual = true;
            }
            return bNotEqual;

        }

    }
}
