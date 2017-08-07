///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 1
//	File Name:		PermutationDriver.cs
//	Description:	To find the distance of the shortest path of a list of points.
//	Course:			CSCI 3230 - Algorithms	
//	Author:			Matthew Humphrey, Humphreymg@goldmail.etsu.edu
//	Created:		Wednesday, February 10 2016
//	Copyright:		Matthew Humphrey,  2016
//
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Project1MatthewHumphrey
{
    /// <summary>
    /// the class to create the permutations
    /// </summary>
    class PermutationDriver
    {

        static int numberOfValues = int.Parse(Console.ReadLine());
        static List<int> x = new List<int>(numberOfValues+2);
        static List<int> y = new List<int>(numberOfValues+2);

        /// <summary>
        /// The method that runs the method to create the permuatations and outputs the results.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            List <int> order = new List<int>(numberOfValues);
            List<string> values = new List<string>();
            double[,] distances = new double[numberOfValues,numberOfValues+1];
            Stopwatch sw = new Stopwatch();
            String instring;
            int temp;
            double shortest;

            sw.Start();

            x.Add(0);
            y.Add(0);

            for (int i = 0; i < numberOfValues; i++)
            {
                instring = Console.ReadLine();
                values.AddRange(instring.Split(' ').ToList());
                int.TryParse(values[0], out temp);
                x.Add(temp);
                int.TryParse(values[1], out temp);
                y.Add(temp);
                values.Clear();
            }


            for (int i = 0; i < numberOfValues; i++)
            {
                for (int j = i + 1; j < numberOfValues + 1; j++)
                {
                    distances[i, j] = distance(x[i], y[i], x[j], y[j]);  
                }
            }

            shortest = permutations(distances);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            Console.WriteLine("The shortest distance was: " + shortest);
            Console.ReadLine();
            
        }

        /// <summary>
        /// Creates and tests each item.
        /// </summary>
        /// <param name="distances">The distances between each item.</param>
        /// <returns>
        /// the shortest distance
        /// </returns>
        public static double permutations(double[,] distances)
        {
            List<int> finishedNums = new List<int>(20); //numbers that have already been at the front
            int front = numberOfValues - 2;    //where the order starts looking to organize the numbers
            HashSet<int> numbers = new HashSet<int>();  //the numbers that it has to organize
            int frontNum;           //the number at order[front]
            int trueFront = front - 1;  //the farthest back the front has ever been
            List<int> order = new List<int>(front + 1); //the current permutation
            double distance;    //the distance for this order
            int temp;   //a temporary variable for different uses
            int oldFirst = 1;   //the number that used to be at the front
            int max;    //the highest number in order
            double shortest = 9999999;  //the shortest distance so far

            for (int i = 0; i < numberOfValues; i++)
            {
                order.Add(i + 1);
            }
            
            max = order.Max();

            while (order[0] < ((order.Count / 2) + 1))
            {
                if (finishedNums.Contains(order[order.Count -1]))
                {
                    distance = calcPermutation(order, shortest, distances);
                    if (distance < shortest)
                    {
                        shortest = distance;
                    } 
                }
                numbers.Clear();
                for (int i = order.Count - 1; i > front; i--)
                {
                    numbers.Add(order[i]);
                }

                if (oldFirst != order[0])
                {
                    finishedNums.Add(oldFirst);
                    oldFirst = order[0];
                }
                frontNum = order[front];
                if (front == order.Count - 2)
                {
                    temp = order[order.Count - 1];
                    order[order.Count - 1] = frontNum;
                    order[front] = temp;
                    numbers.Add(temp);
                    if (order[front - 1] > numbers.Max())
                    {
                        front = trueFront;
                    }
                    else
                    {
                        front--;
                    }
                }
                else if (front == trueFront)
                {
                    if (frontNum != max)
                    {
                        if (frontNum < numbers.Min() || front > order.Count - 5)
                        {
                            for (int i = 0; i < numbers.Count; i++)
                            {
                                if (numbers.ElementAt(i) > frontNum && frontNum == order[front])
                                {
                                    order[front] = numbers.ElementAt(i);
                                }
                                else if (numbers.ElementAt(i) > frontNum && numbers.ElementAt(i) < order[front])
                                {
                                    order[front] = numbers.ElementAt(i);
                                }
                            }
                            numbers.Remove(order[front]);
                            numbers.Add(frontNum);
                            for (int i = front + 1; i < order.Count; i++)
                            {
                                order[i] = numbers.Min();
                                numbers.Remove(numbers.Min());
                            }
                            front = order.Count - 2; 
                        }
                        else
                        {
                            front += 1;
                        }
                    }
                    else
                    {
                        front--;
                        trueFront = front;
                        numbers.Add(frontNum);
                        frontNum = order[front];
                        order[front] = numbers.Min();
                        numbers.Remove(order[front]);
                        numbers.Add(frontNum);
                        for (int i = front + 1; i < order.Count; i++)
                        {
                            order[i] = numbers.Min();
                            numbers.Remove(numbers.Min());
                        }
                        front = order.Count - 2;
                    }
                }
                else if (order[front] == max)
                {
                    front--;
                    if (front < trueFront)
                    {
                        trueFront = front;
                    }
                    numbers.Add(frontNum);
                    numbers.Add(order[front]);
                    order[front] += 1; 
                    temp = order[front];
                    numbers.Remove(temp);
                    for (int i = front + 1; i < order.Count; i++)
                    {
                        order[i] = numbers.Min();
                        numbers.Remove(order[i]);
                    }
                    front = order.Count - 2;
                }
                else
                {
                    if (frontNum < numbers.Max() || front < order.Count - 4)
                    {
                        if (order[front+1] == numbers.Max()) 
                        {
                            for (int i = 0; i < numbers.Count; i++)
                            {
                                if (numbers.ElementAt(i) > frontNum && frontNum == order[front])
                                {
                                    order[front] = numbers.ElementAt(i);
                                }
                                else if (numbers.ElementAt(i) > frontNum && numbers.ElementAt(i) < order[front])
                                {
                                    order[front] = numbers.ElementAt(i);
                                }
                            }

                            numbers.Remove(order[front]);
                            numbers.Add(frontNum);
                            for (int i = front + 1; i < order.Count; i++)
                            {
                                order[i] = numbers.Min();
                                numbers.Remove(numbers.Min());
                            }
                            front = order.Count - 2;
                        }

                        else
                        {
                            front += 1;
                        }
                    }
                    else
                    {
                        front += 1;
                    }
                }
                
            }
            return shortest;    
        }

        /// <summary>
        /// Calculates the permutation.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="shortest">The shortest distance so far.</param>
        /// <param name="distances">The distances.</param>
        /// <returns>
        /// the current distance
        /// </returns>
        private static double calcPermutation(List<int> order,double shortest,double[,] distances)
        {
            
            double distance = distances[0, order[0]];           //distance of this permuatation
            distance += distances[0, order[order.Count - 1]];   
            
            for (int i = 0; i < numberOfValues - 1; i++)
            {
                if (distance < shortest && i + 1 != order.Count)
                {
                    if (order[i] < order[i+1])
                    {
                        distance += distances[order[i], order[i + 1]];  
                    }
                    else
                    {
                        distance += distances[order[i + 1], order[i]];
                    }
                }
                else if (distance > shortest)
                {
                    break;
                }
            }
            
            return distance;
        }

        /// <summary>
        /// Calculates a distance.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <returns>
        /// The distance between (x1,y1) and (x2,y2)
        /// </returns>
        public static double distance(int x1, int y1, int x2, int y2)
        {
            double dist;    //the value to be returned
            dist = x2 - x1;
            dist = Math.Pow(dist, 2);
            dist += Math.Pow(y2 - y1, 2);
            dist = Math.Sqrt(dist);

            return dist;
        }
    }
}
