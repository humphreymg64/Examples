///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Sim with Queues and PQ's
//	File Name:		SimDriver.cs
//	Description:	The driver for the simulation
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
    /// The driver class for the simulation. Handles the menus.
    /// </summary>
    class SimDriver
    {
        /// <summary>
        /// The main method that runs the program.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main (string[] args)
        {
            int numReg=0;
            int numHours=0;
            int numWindows=0;
            double expectedDur=0.0;
            bool cont = true;
            int input;
            ConventionRegistration cReg;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Title = "Line Simulator";
            Console.InputEncoding = Encoding.ASCII;

            while(cont == true)
            {
                Console.Clear ( );
                displayMenu ( );
                input = Convert.ToInt32(Console.ReadLine ( ));

                switch (input)
                {
                    case 1:
                        Console.Clear ( );
                        Console.WriteLine ("\nEnter Number of Registrants: ");
                        numReg = Convert.ToInt32 (Console.ReadLine ( ));
                        break;
                    case 2:
                        Console.Clear ( );
                        Console.WriteLine ("\nEnter Number of Operating Hours: ");
                        numHours = Convert.ToInt32 (Console.ReadLine ( ));
                        break;
                    case 3:
                        Console.Clear ( );
                        Console.WriteLine ("\nEnter number of windows: ");
                        numWindows = Convert.ToInt32 (Console.ReadLine ( ));
                        break;
                    case 4:
                        Console.Clear ( );
                        Console.WriteLine ("\nSet Expected Registration Duration: ");
                        expectedDur = Convert.ToDouble (Console.ReadLine ( ));
                        break;
                    case 5:

                        if (numHours != 0&&numWindows != 0&&numReg != 0)
                        {
                            cReg = new ConventionRegistration (numReg, numWindows, expectedDur, numHours);
                            cReg.RunSimulation ( ); 
                        }
                        else
                        {
                            Console.WriteLine ("More information is needed to run the simulation.");
                        }
                        PressEnterToContinue ( );
                        Console.Read ( );
                        break;
                    case 6:
                        Console.Clear ( );
                        Console.WriteLine ("\nThank you for using the Registration Simulation.");
                        Thread.Sleep (3000);
                        cont = false;
                        Environment.Exit (1);
                        break;
                    default:
                        Console.Clear ( );
                        Console.WriteLine ("\n!!!INVALID INPUT!!!");
                        PressEnterToContinue();
                        Console.Read ( );
                        break;
                }


            }
        }

        /// <summary>
        /// Waits for the user to press enter.
        /// </summary>
        private static void PressEnterToContinue ( )
        {
            Console.WriteLine ("Press Enter to return to the menu");
            Console.Read ( );
        }

        /// <summary>
        /// Displays the menu.
        /// </summary>
        public static void displayMenu ( )
        {
            string strMenu = "\n\t***Simulation Menu***\n\n";
            strMenu += "\n1. Set the Number of Registrants";
            strMenu += "\n2. Set the number of hours of operation";
            strMenu += "\n3. Set the number of windows";
            strMenu += "\n4. Set the expected checkout duration";
            strMenu += "\n5. Run the Simulation";
            strMenu += "\n6. End Program";
            strMenu += "\n\nType the number of your choice from the menu: ";
            Console.WriteLine (strMenu);
        }//end displayMenu()

       
    }


}
