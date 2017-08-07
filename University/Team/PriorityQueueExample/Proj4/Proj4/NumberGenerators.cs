///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Sim with Queues and PQ's
//	File Name:		NumberGenerators.cs
//	Description:	Class for generating random numbers in different distrubutions
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
    /// used to hold random number generators of a specific distribution
    /// </summary>
    class NumberGenerators
    {
        public static Random rand = new Random ( );    //used to generate random numbers

        /// <summary>
        /// Gets a completly random interger.
        /// </summary>
        /// <returns>
        /// A random interger
        /// </returns>
        public static int Uniform()
        {
            return rand.Next ( );
        }

        /// <summary>
        /// Generates a number of the Poisson distribution.
        /// Code taken from Dr. Bails lecture notes.
        /// </summary>
        /// <param name="ExpectedValue">The expected value that the distribution is based on.</param>
        /// <returns>
        /// A number of the poisson distribution
        /// </returns>
        public static int Poisson (double ExpectedValue)
        {
            double dLimit = -ExpectedValue;     //when the count will stop
            double dSum = Math.Log (rand.NextDouble ( ));   //the sum counting up to dLimit

            int Count;      //used to hold the value to be returned
            for (Count = 0; dSum > dLimit; Count++)
            {
                dSum += Math.Log (rand.NextDouble ( ));
            }

            return Count;
        }

        /// <summary>
        /// Generates a number of the Negative Exponential distribution.
        /// Code taken from Dr. Bails lecture notes.
        /// </summary>
        /// <param name="ExpectedValue">The expected value that the distribution is based on.</param>
        /// <returns>
        /// A number of the negative exponential distribution
        /// </returns>
        public static double NegExp (double ExpectedValue)
        {
            return -ExpectedValue * Math.Log (rand.NextDouble ( ), Math.E);
        }
    }
}
