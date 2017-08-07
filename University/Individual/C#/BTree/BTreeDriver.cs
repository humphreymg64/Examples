///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 5 - BTrees
//	File Name:		BTreeDriver.cs
//	Description:	Class used to run the program and display the trees
//	Course:			CSCI 2210 - Data Structures	
//	Author:			Matthew Humphrey, Humphreymg@goldmail.etsu.edu
//	Created:		Saturday, November 26, 2015
//	Copyright:		Matthew Humphrey,  2015
//
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2210_201_HumphreyMatthew_Project5
{
    /// <summary>
    /// the driver that lets the user interact with the tree
    /// </summary>
    class BTreeDriver
    {
        /// <summary>
        /// The main method
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            menu ( );
        }

        /// <summary>
        /// Creates the menu for a user interface.
        /// </summary>
        public static void menu()
        {
            string userChoice = "";     //what the user chooses on the menu
            string temp = "";           //temporary string
            int iTemp;                  //the iTemp of the nodes in the B-Tree
            bool parsed = false;         //if the expression parsed
            bool added;                 //if the value was added successfully
            BTree b = new BTree(0);     //the btree the menu holds

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;

            while (userChoice != "5")
            {
                Console.Clear();
                Console.WriteLine ("BTree Menu\n----------\n\n1.Set size of node and create B-Tree\n2.Display the B-Tree");
                Console.WriteLine ("3.Add a Value to B-Tree\n4.Find a value in the B-Tree\n5.Close the program\n\n\n\n\n");
                Console.WriteLine ("Type the number of the choice you want: ");

                userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        while (!parsed)
                        {
                            Console.Clear();
                            Console.WriteLine ("What do you want the size of the nodes to be? ");
                            temp = Console.ReadLine ( );
                            parsed = int.TryParse (temp,out iTemp);
                            b = createTree (iTemp); 
                        }
                        parsed = false;
                        break;
                    case "2":
                        Console.Clear ( );
                        b.display ( );
                        Console.WriteLine (b.Stats ( ));
                        Console.WriteLine ("\nPress enter to continue.");
                        Console.ReadLine ( );
                        break;
                    case "3":
                        while (!parsed)
                        {
                            Console.Clear();
                            Console.WriteLine ("What do you want the value to be? ");
                            temp = Console.ReadLine ( );
                            parsed = int.TryParse (temp, out iTemp);
                            added = b.addValue(iTemp);
                            if (added)
                            {
                                Console.WriteLine ("The value was added successfully.");
                            }
                            else
                            {
                                Console.WriteLine ("The value could not be added");
                            }
                            Console.WriteLine ("\nPress enter to continue.");
                            Console.ReadLine ( );
                        }
                        parsed = false;
                        break;
                    case "4":
                        while (!parsed)
                        {
                            Console.Clear();
                            Console.WriteLine ("What value do you want to find? ");
                            temp = Console.ReadLine ( );
                            parsed = int.TryParse (temp, out iTemp);
                            if (b.findLeaf (iTemp))
                            {
                                Console.WriteLine ("This is a list of the nodes that were looked through to find the value.");
                            }
                            else
                            {
                                Console.WriteLine ("This is a list of the nodes that show where the value would be if it was in the list.");
                            }
                            Console.WriteLine ("\nPress enter to continue.");
                            Console.ReadLine ( );
                        }
                        parsed = false;
                        break;
                    default:
                        break;
                } 
            }
        }

        /// <summary>
        /// Creates the tree.
        /// </summary>
        /// <param name="iTemp">The arity of the tree.</param>
        /// <returns>
        /// returns a BTree
        /// </returns>
        private static BTree createTree (int iTemp)
        {
            bool added;
            Random rand = new Random();
            BTree b = new BTree (iTemp);
            for (int i = 0; i < 500; i++)
            {
                added = b.addValue (rand.Next (1,10000));
                if (!added)
                {
                    i--;
                }
            }

            return b;
        }
        
    }
}
