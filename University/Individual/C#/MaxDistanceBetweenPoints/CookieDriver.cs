///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 2
//	File Name:		CookieDriver.cs
//	Description:	To find the most points in quadrant 1 and 3 if the vertical line is in a worst case spot.
//	Course:			CSCI 3230 - Algorithms	
//	Author:			Matthew Humphrey, Humphreymg@goldmail.etsu.edu
//	Created:		Saturday, March 12 2016
//	Copyright:		Matthew Humphrey,  2016
//
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2MatthewHumphrey
{
    /// <summary>
    /// calculates the most amount of points that can be in quadrants 1 and 3 in a worst case senario
    /// </summary>
    class CookieDriver
    {
        /// <summary>
        /// the main method that takes care of all input and output
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int numTestCases = int.Parse(Console.ReadLine()); //the number of cases to test
            int numberOfValues;                //the number of values in a case
            Stopwatch sw = new Stopwatch();    //keeps track of time
            List<double> x;                    //the sorted list of x's
            List<double> tempX;                //unsorted list of x's
            List<double> y;                    //list of y's based on sort of x's
            List<double> tempY;                //unsorted list of y's
            List<double> distX;                //list of sorted distinct x's
            List<string> values;               //the string forms of the points
            List<string> point;                //sepearated strings of the points
            string instring;                   //the incoming string
            int temp;                          //used to temporarily hold values
            double dTemp;                      //double used for temp holding values
            List<double> max = new List<double>(numTestCases);    //the max's of each test case
            sw.Start();         

            for (int j = 0; j < numTestCases; j++)      //goes through each test case
            {
                numberOfValues = int.Parse(Console.ReadLine());
                distX = new List<double>(numberOfValues);
                x = new List<double>(numberOfValues);
                y = new List<double>(numberOfValues);
                tempX = new List<double>(numberOfValues);
                tempY = new List<double>(numberOfValues);
                values = new List<string>(numberOfValues);
                point = new List<string>(2);

                for (int i = 0; i < numberOfValues; i++)    //gets the points into their unsorted lists
                {
                    instring = Console.ReadLine();
                    values.Add(instring);
                    point.AddRange(values[i].Split(' ').ToList());
                    double.TryParse(point[0], out dTemp);
                    tempX.Add(dTemp);
                    double.TryParse(point[1], out dTemp);
                    tempY.Add(dTemp);
                    point.Clear();
                }
                for (int i = 0; tempX.Count > 0; i++)    //sorts the lists based on the x's
                {
                    temp = tempX.IndexOf(tempX.Min());
                    x.Add(tempX[temp]);
                    y.Add(tempY[temp]);
                    tempX.RemoveAt(temp);
                    tempY.RemoveAt(temp);
                }
                tempX.AddRange(x.Distinct().ToList());
                for (int i = 0; i < tempX.Count / 2.0; i++)
                {
                    //distX.Add(tempX[i]);
                    distX.Add(tempX[i] + .5);
                }

                max.Add(findVertical(x, y, distX));
            }

            Console.WriteLine(sw.Elapsed);
            foreach (int i in max)          //writes each of the max's
            {
                Console.WriteLine(i);
            }
            Console.ReadLine();

        }

        /// <summary>
        /// Tests each vertical line for the lowest max.
        /// </summary>
        /// <param name="x">The x values.</param>
        /// <param name="y">The y values.</param>
        /// <returns>
        /// The lowest possible max
        /// </returns>
        private static double findVertical(List<double> x, List<double> y, List<double> distX)
        {
            double vertical;                                   //the value of the vertical line
            int max = 0;                                 //the smallest maximum number of points so far
            int temp;                                          //the value retrived from testing horizontals
            List<double> distY = new List<double>(y.Count);    //a list of distinct items from the y values
            List<double> copyY = new List<double>(y.Count);    //a copy of the y values
            
            copyY.AddRange(y.Distinct().ToList());
            distY.Add(copyY.Min() - .5);
            for (int i = 0; i < copyY.Count; i++)
            {
                distY.Add(copyY[i]);
                distY.Add(copyY[i] + .5);
            }

            for (int i = 0; i < distX.Count ; i++)     //goes through each of the remain placements of the vertical
            {
                vertical = distX[i];
                temp = findHorizonal(x, y, distY, vertical, max);
                //Console.WriteLine(temp);
                if (temp >= max)
                {
                    max = temp;
                }

            }
            return max;
        }

        /// <summary>
        /// Tests each possible horizontal line to see where the best max would be.
        /// </summary>
        /// <param name="x">The x values.</param>
        /// <param name="y">The y values.</param>
        /// <param name="sortedY">The y values in order.</param>
        /// <param name="vertical">The vertical divider.</param>
        /// <param name="prev">The current max.</param>
        /// <returns>
        /// The max points that could be in quadrants 1 and 3 with the defined vertical
        /// </returns>
        private static int findHorizonal(List<double> x, List<double> y, List<double> sortedY, double vertical, int prev)
        {
            double horizontal; //the horizontal divider to test
            int max = x.Count;       //the most points so far
            int temp;          //the points found for the current dividers
            
            for (int i = 0; i < sortedY.Count;i++)   //used to go through each possible position of the horizontal
            {
                horizontal = sortedY[i];
                temp = calcPnts(x, y, vertical, horizontal, prev);
                if (temp < max)
                {
                    //Console.WriteLine(tempRem + " : " + temp + " : " + vertical + " : " + horizontal); //used for debugging
                    
                    max = temp;
                }
            }
            return max;
        }

        /// <summary>
        /// Calculates the amount of points caught with current horizontal and vertical.
        /// </summary>
        /// <param name="x">The x values.</param>
        /// <param name="y">The y values.</param>
        /// <param name="vertical">The vertical dividing line.</param>
        /// <param name="horizontal">The horizontal dividing line.</param>
        /// <param name="low">The lowest max so far.</param>
        /// <returns>
        /// the number of points in quadrants 1 and 3
        /// </returns>
        private static int calcPnts(List<double> x, List<double> y, double vertical, double horizontal, int low)
        {
            int points = 0;   //the number of points caught so far
            int i = 0;        //the indexer for the for loops. Declared here so that it can be used in both loops
            //remain = x.Count;   //amount remaining
            
            try
            {
                for (;x[i] < vertical; i++)     //go through every point before the x's cross the vertical
                {
                    if (y[i] < horizontal)      //if the point is in quadrant 3
                    {
                        points += 1;
                        //if (points > low)       //if there are more points than the lowest amount recorded so far
                        //{
                        //    remain = remain - points;
                        //    return points;
                        //}
                    }
                }
            }
            catch (ArgumentOutOfRangeException) //this exception should only be thrown if the vertical
            {                                   //line was past all the points

              //  remain = remain - points;
                return points;
            }
            while (x[i] == vertical)            //if the i is currently sitting on the vertical
            {
                //remain--;
                i += 1;
                if (i == x.Count)
                {
                //    remain = remain - points;
                    return points;
                }
            }
            for (; i < y.Count; i++)            //go through every point after the crossing of the vertical
            {
                if (y[i] > horizontal)          //check if in quadrant 1
                {
                    points += 1;
                    //if (points > low)           //if the current amount is already too much
                    //{
                    //    remain = remain - points;
                    //    return points;
                    //}
                }
            }
         //   remain = remain - points;
            return points;
        }
    }
}