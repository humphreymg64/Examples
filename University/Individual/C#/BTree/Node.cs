///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 5 - BTrees
//	File Name:		Node.cs
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
    /// The class to create nodes in the btree
    /// </summary>
    class Node
    {
        public List<int> Values { get; set; }       //holds the values in the node
        public int NodeSize { get; set; }           //holds the maximum size of the node

        /// <summary>
        /// Initializes a new empty instance of the <see cref="Node"/> class.
        /// </summary>
        public Node ()
        {
            Values = new List<int> ( );
            NodeSize = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="size">The size the node can hold.</param>
        public Node (int size)
        {
            Values = new List<int> ( );
            NodeSize = size;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this node's values.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this node's values.
        /// </returns>
        public override string ToString()
        {
            string strValues = "";                //the string that holds the values

            for (int i = 0; i < Values.Count; i++)
            {
                strValues += Values[i] + " ";
            }

            return strValues;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="node1">The node1.</param>
        /// <param name="node2">The node2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator == (Node node1, Node node2)
        {
            bool equal = true;     //if the two are equal
            if (node1.Values.Count != node2.Values.Count)
            {
                return false;
            }
            for (int i = 0; i < node1.Values.Count; i++)
            {
                if (node1.Values[i] != node2.Values[i])
                {
                    return false;
                }
            }
            return equal;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="node1">The node1.</param>
        /// <param name="node2">The node2.</param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        public static bool operator !=(Node node1, Node node2)
        {
            bool equal = false;     //if the two are equal
            if (node1.Values.Count != node2.Values.Count)
            {
                return true;
            }
            for (int i = 0; i < node1.Values.Count; i++)
            {
                if (node1.Values[i] != node2.Values[i])
                {
                    return true;
                }
            }
            return equal;
        }
    }
}
