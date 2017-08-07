///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 5 - BTrees
//	File Name:		Index.cs
//	Description:	Class used to create the nodes
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
    /// the Node type that points to other nodes
    /// </summary>
    class Index :Node
    {
        public List<Node> Indexes { get; set; }

        /// <summary>
        /// Initializes a new empty instance of the <see cref="Index"/> class.
        /// </summary>
        public Index ( )
        {
            Indexes = new List<Node> ( );
            NodeSize = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Index"/> class.
        /// </summary>
        /// <param name="Size">The size of the node.</param>
        public Index (int Size)
        {
            Indexes = new List<Node> ( );
            NodeSize = Size;
        }

        /// <summary>
        /// Inserts the specified new value.
        /// </summary>
        /// <param name="newVal">The new value.</param>
        /// <returns>
        /// returns a 2 if duplicate, a 1 if the node overflows, or a zero if everthing was fine
        /// </returns>
        public byte Insert (int newVal,Node node)
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
                    Indexes.Add (node);
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
        public void sort()
        {
            Node tmpNode;
            int iTemp;

            for (int i = 1; i < Values.Count; i++)
            {
                if (Values[i-1] > Values [i])
                {
                    iTemp = Values[i];
                    tmpNode = Indexes[i];
                    Values[i] = Values[i-1];
                    Indexes[i] = Indexes[i-1];
                    Values[i-1] = iTemp;
                    Indexes[i-1] = tmpNode;
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this node's values.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this node's values.
        /// </returns>
        public override string ToString ( )
        {
            string strValues = "";                //the string that holds the values

            for (int i = 0; i < Values.Count; i++)
            {
                strValues += Values[i] + " ";
            }

            return strValues;
        }
    }
}
