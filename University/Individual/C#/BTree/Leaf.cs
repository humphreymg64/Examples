///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 5 - BTrees
//	File Name:		Leaf.cs
//	Description:	Class used to create the leaf nodes
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
    /// A type of node used for storing values
    /// </summary>
    class Leaf : Node
    {

        /// <summary>
        /// Initializes a new empty instance of the <see cref="Leaf"/> class.
        /// </summary>
        public Leaf ()
        {
            NodeSize = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Leaf"/> class.
        /// </summary>
        /// <param name="Size">The size of the node.</param>
        public Leaf (int Size)
        {
            NodeSize = Size;
        }

        /// <summary>
        /// Inserts the specified new value.
        /// </summary>
        /// <param name="newVal">The new value.</param>
        /// <returns>
        /// returns a 2 if duplicate, a 1 if the node overflows, or a zero if everthing was fine
        /// </returns>
        public byte Insert(int newVal)
        {
            byte code = 0;      //returns a 2 if duplicate, a 1 if the node overflows, or a zero if everthing was fine

                if (!Values.Contains (newVal))
                {
                    if (Values.Count == NodeSize)
                    {
                        code = 1;
                    }
                    else
                    {
                        Values.Add (newVal);
                    }
                }
                else
                {
                    code = 2;
                } 
            
            sort ( );

            return code;
        }

        /// <summary>
        /// Sorts the nodes this index points to.
        /// </summary>
        public void sort ( )
        {
            int iTemp;

            for (int i = 1; i < Values.Count; i++)
            {
                if (Values[i-1] > Values[i])
                {
                    iTemp = Values[i ];
                    Values[i ] = Values[i-1];
                    Values[i-1] = iTemp;
                }
            }
        }
    }
}
