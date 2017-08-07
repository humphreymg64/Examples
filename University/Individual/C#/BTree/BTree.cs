///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 5 - BTrees
//	File Name:		BTree.cs
//	Description:	Class used to create the btrees
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
    /// used to hold and create BTrees
    /// </summary>
    class BTree
    {
        public int Count { get; set; }              //the total number of nodes
        public int IndexCount { get; set; }         //the total number of indexes
        public int LeafCount { get; set; }          //the total number of leaves
        public int NodeSize { get; set; }           //the max size of all the nodes
        public Index Root { get; set; }             //the root node
        public Stack<Node> stack { get; set; }      //a stack that holds where a list goes and where its bee
        public int maxDepth { get; set; }           //the deepest depth ever achieved
        public double avgFull { get; set; }         //the average fullness of the nodes
        public int depth { get; set; }              //the current depth of the nodes

        /// <summary>
        /// Initializes a new instance of the <see cref="BTree"/> class.
        /// </summary>
        /// <param name="n">The size of the node.</param>
        public BTree (int n)
        {
            NodeSize = n;
            maxDepth = 0;
            avgFull = 0;
            IndexCount++;
            Count++;
            LeafCount++;
            Count++;
            Root = new Index (NodeSize);
            stack = new Stack<Node> ( );
            Root.Insert (9999, new Leaf(NodeSize));
        }

        /// <summary>
        /// Finds the depth.
        /// </summary>
        /// <returns>
        /// the current depth
        /// </returns>
        private int findDepth ()
        {
            if (stack.Count != 0)
            {
                return (stack.Count - 1); 
            }
            return 0;
        }

        /// <summary>
        /// Displays this BTree.
        /// </summary>
        public void display()
        {
            display (Root);
        }

        /// <summary>
        /// Displays the specified node.
        /// </summary>
        /// <param name="n">The node.</param>
        /// <exception cref="Exception">Unexpected node type</exception>
        public void display(Node n)
        {
            Console.WriteLine (Stats (n));
            if (n is Index)
            {
                Index temp = (Index)n;
                depth++;
                for (int i = 0; i < temp.Indexes.Count; i++)
                {
                    stack.Push (temp.Indexes[i]);
                }
            }

            else if (n is Leaf)
            {
            }

            else
            {
                throw new Exception ("Unexpected node type");
            }


            if (stack.Count > 0)
            {

                display (stack.Pop ( ));

            }
            if (n is Index)
            {
                depth--;
            }
        }

        /// <summary>
        /// Statses of the specified node.
        /// </summary>
        /// <param name="n">The node to get the stats of.</param>
        /// <returns>
        /// a string that describes the node
        /// </returns>
        /// <exception cref="Exception">Unexpected node type</exception>
        private string Stats (Node n)
        {
            string stats ="";       //holds the info to return
            double fullness;        //%full the node is

            if (n.Values.Count != 0)
            {
                fullness = ((double)n.Values.Count / (double)NodeSize) * 100; 
            }
            else
            {
                fullness = 0;
            }

            if (n is Index)
            {
                stats += "Node Type: Index";
                stats += "\nNumber of Values: " + n.Values.Count;
                stats += " (" + fullness + "% full)";
                stats += "\nValues: \n" + n.ToString ( );
                stats += "\nLevel of index in BTree: " + depth;
                if (depth > maxDepth)
                {
                    maxDepth = depth;
                }
            }

            else if (n is Leaf)
            {
                stats += "Node Type: Leaf";
                stats += "\nNumber of Values: " + n.Values.Count;
                stats += " (" + fullness + "% full)\n";
                stats += "Values: \n" + n.ToString ( );
                avgFull += fullness;
            }

            else
            {
                throw new Exception ("Unexpected node type");
            }
            stats += "\n";

            return stats;
        }

        /// <summary>
        /// Statses of the instance.
        /// </summary>
        /// <returns>
        /// gives overall stats of the BTree
        /// </returns>
        public string Stats()
        {
            string stats = "";       //holds the info to return

            avgFull /= LeafCount;

            stats += "\nNumber of total nodes: " + Count;
            stats += "\nNumber of leaf nodes: " + LeafCount;
            stats += " and they are an average of " + avgFull + "% full.\n";
            stats += "Number of index node: " + IndexCount;
            stats += "\nThe maximum depth of the tree is: " + maxDepth;

            return stats;
        }

        public bool findLeaf(int n)
        {
            Leaf temp =  findValue (Root, n);
            if (!(temp is Leaf))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Finds the value.
        /// </summary>
        /// <param name="n">The node to start the search at.</param>
        /// <param name="val">The value to search for.</param>
        /// <returns>
        /// the leaf the value was found at or null if it could not be found
        /// </returns>
        private Leaf findValue(Node n, int val)
        {
            for (int i = 0; i < n.Values.Count; i++)
            {
                if (n.Values[i] >= val)
                {
                    if (n is Index)
                    {
                        Index temp = (Index)n;
                        stack.Push (temp.Indexes[i]);
                        Console.WriteLine (Stats (n));
                        return findValue (temp.Indexes[i], val);
                    }
                    else if (n is Leaf)
                    {
                        if (n.Values[i] == val)
                        {
                            Console.WriteLine (Stats (n));
                            return (Leaf)n;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Finds the leaf of a value.
        /// </summary>
        /// <param name="start">The start of the search.</param>
        /// <param name="val">The value to search.</param>
        /// <returns>
        /// a leaf where val should be
        /// </returns>
        private Leaf findLeaf (Node start, int val)
        {
            if (start is Index)
            {
                for (int i = 0; i < start.Values.Count; i++)
                {
                    if (val >= start.Values[i] )
                    {
                        return findLeaf (((Index)start).Indexes[i], val);
                    }
                  
                }
                return findLeaf(((Index)start).Indexes[((Index)start).Indexes.Count-1], val);
            }
            else
            {
                return (Leaf)start;
            }
        }

        /// <summary>
        /// Adds the value.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>
        /// a boolean that tells if the addition was successful
        /// </returns>
        public bool addValue (int n)
        {
            Count++;
            Leaf temp = findLeaf (Root, n);
                byte code = temp.Insert (n);
                if (code == 0)
                {
                    return true;
                }
                else if (code == 1)
                {
                    SplitLeaf (temp, n);
                    return true;
                }
                else if (code == 2)
                {
                    return false;
                }
                return false; 
            
        }

        /// <summary>
        /// Splits the leaf.
        /// </summary>
        /// <param name="leaf">The leaf to split.</param>
        /// <param name="n">The value to add.</param>
        public void SplitLeaf(Leaf leaf, int n)
        {

            Leaf newLeaf = new Leaf(NodeSize);       //the leaf to hold half the values
            int Max;                         //the value to be added to the index
            Index parent = findParentNode (Root, leaf);

            if (parent.Indexes.Count == NodeSize)
            {
                SplitIndex (parent);
                SplitLeaf (leaf, n);
                return;
            }
            else
            {
                LeafCount++;
                for (int i = leaf.Values.Count-1; i > leaf.Values.Count/2; i--)
                {
                    newLeaf.Insert(leaf.Values[i]);
                    leaf.Values.RemoveAt (i);
                }
                Max = newLeaf.Values.Max ( );
                if ((Max-leaf.Values.Max()) < NodeSize)
                {
                    Max = leaf.Values.Max ( ) + NodeSize;
                }
                parent.Insert (Max, newLeaf);
                return;
            }

        }

        /// <summary>
        /// Splits the index.
        /// </summary>
        /// <param name="node">The index to split.</param>
        private void SplitIndex (Index node)
        {
            Index newIndex = new Index (NodeSize);       //the leaf to hold half the values
            int Max;                           //the value to be added to the index
            Index parent = findParentNode (Root, node);

            if(parent.Indexes.Count != NodeSize && node != Root)
            {
                IndexCount++;
                for (int i = node.Values.Count-1; i > node.Values.Count / 2; i--)
                {
                    newIndex.Insert(node.Values[i],node.Indexes[i]);
                    node.Indexes.RemoveAt (i);
                    node.Values.RemoveAt (i);
                }
                Max = newIndex.Values.Max ( );
                if ((Max - node.Values.Max ( )) < NodeSize)
                {
                    Max = node.Values.Max ( ) + NodeSize;
                }
                parent.Insert (Max, newIndex);
                return;
            }
            else if (node == Root)
            {
                IndexCount++;
                for (int i = node.Values.Count-1; i > node.Values.Count / 2; i--)
                {
                    newIndex.Insert (node.Values[i], node.Indexes[i]);
                    node.Indexes.RemoveAt (i);
                    node.Values.RemoveAt (i);
                }
                Max = newIndex.Values.Max ( );
                Index newRoot = new Index (NodeSize);
                if ((Max - node.Values.Max ( )) < NodeSize)
                {
                    Max = node.Values.Max ( ) + NodeSize;
                }
                newRoot.Insert (Max, newIndex);
                Max = node.Values.Max ( );
                newRoot.Insert (Max, Root);
                Root = newRoot;
                return;
            }
            else
            {
                SplitIndex (parent);
                IndexCount++;
                for (int i = node.Values.Count - 1; i > node.Values.Count / 2; i--)
                {
                    newIndex.Insert (node.Values[i], node.Indexes[i]);
                    node.Indexes.RemoveAt (i);
                    node.Values.RemoveAt (i);
                }
                Max = newIndex.Values.Max ( );
                parent.Insert (Max, newIndex);
                return;
            }

        }

        /// <summary>
        /// Finds the parent node.
        /// </summary>
        /// <param name="start">The node to start at.</param>
        /// <param name="lookFor">The node to look for.</param>
        /// <returns>
        /// the parent node
        /// </returns>
        private Index findParentNode (Index start, Node lookFor)
        {
            if (start.Indexes.IndexOf(lookFor) != -1 || start.Indexes[start.Indexes.Count-1] is Leaf)
            {
                return start;
            }
            else
            {
                return findParentNode ((Index)start.Indexes[start.Indexes.Count-1], lookFor);
            }
      
        }
    }
}
