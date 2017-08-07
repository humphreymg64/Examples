///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Sim with Queues and PQ's
//	File Name:		PriorityQueue.cs
//	Description:	A priority queue class. Taken directly from Dr. Don Bailes' slides.
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
    /// A priority queue implemented through a binary heap. All code from Dr.Bailes' powerpoint slides
    /// </summary>
    class PriorityQueue
    {
        private Evnt[] regEvnts;    //container for MaxHeap Priority Queue
        public int HeapSize { get; set; }   //current size of the heap

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        public PriorityQueue (int size)
        {
            regEvnts = new Evnt[size];
            HeapSize = 0;
        }
        /// <summary>
        /// Gets the minimum.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Heap is empty.</exception>
        public Evnt GetMin ( )
        {
            if (IsEmpty ( ))
                throw new Exception ("Heap is empty.");
            else
                return regEvnts[0];
        }
        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty ( )
        {
            return (HeapSize == 0);
        }

        /// <summary>
        /// Inserts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="System.Exception">Heap's underlying storage is overflow</exception>
        public void Insert (Evnt value)
        {
            if (HeapSize == regEvnts.Length)
            {
                throw new Exception ("Heap's underlying storage is overflow");
            }
            else
            {
                HeapSize++;
                regEvnts[HeapSize - 1] = value;
                siftUp (HeapSize - 1);
            }
        }

        /// <summary>
        /// Sifts up.
        /// </summary>
        /// <param name="nodeIndex">Index of the node.</param>
        private void siftUp(int nodeIndex)
        {
            int parentIndex;
            Evnt tmp;
            if (nodeIndex !=0)
            {
                parentIndex = GetParentIndex (nodeIndex);
                if(regEvnts[parentIndex] > regEvnts[nodeIndex])
                {
                    tmp = regEvnts[parentIndex];
                    regEvnts[parentIndex] = regEvnts[nodeIndex];
                    regEvnts[nodeIndex] = tmp;
                    siftUp (parentIndex);
                }
            }
        }

        /// <summary>
        /// Removes the minimum.
        /// </summary>
        /// <exception cref="System.Exception">Heap is empty</exception>
        public void RemoveMin()
        {
            if (IsEmpty ( ))
                throw new Exception ("Heap is empty");
            else
            {
                regEvnts[0] = regEvnts[HeapSize - 1];
                HeapSize--;
                if (HeapSize > 0)
                    siftDown (0);
            }
        }

        /// <summary>
        /// Sifts down.
        /// </summary>
        /// <param name="nodeIndex">Index of the node.</param>
        private void siftDown(int nodeIndex)
        {
            int leftChildIndex, rightChildIndex, minIndex;
            Evnt tmp;
            leftChildIndex = GetLeftChildIndex (nodeIndex);
            rightChildIndex = GetRightChildIndex (nodeIndex);

            if(rightChildIndex >= HeapSize)
            {
                if (leftChildIndex >= HeapSize)
                    return;
                else
                    minIndex = leftChildIndex;
            }
            else
            {
                if (regEvnts[leftChildIndex] <= regEvnts[rightChildIndex])
                    minIndex = leftChildIndex;
                else
                    minIndex = rightChildIndex;
            }
            if(regEvnts[nodeIndex] > regEvnts[minIndex])
            {
                tmp = regEvnts[minIndex];
                regEvnts[minIndex] = regEvnts[nodeIndex];
                regEvnts[nodeIndex] = tmp;
                siftDown (minIndex);
            }

        }

        /// <summary>
        /// Gets the index of the left child.
        /// </summary>
        /// <param name="nodeIndex">Index of the node.</param>
        /// <returns></returns>
        private int GetLeftChildIndex (int nodeIndex)
        {
            return 2 * nodeIndex + 1;
        }

        /// <summary>
        /// Gets the index of the right child.
        /// </summary>
        /// <param name="nodeIndex">Index of the node.</param>
        /// <returns></returns>
        private int GetRightChildIndex (int nodeIndex)
        {
            return 2 * nodeIndex + 2;
        }

        /// <summary>
        /// Gets the index of the parent.
        /// </summary>
        /// <param name="nodeIndex">Index of the node.</param>
        /// <returns></returns>
        private int GetParentIndex (int nodeIndex)
        {
            return (nodeIndex - 1) / 2;
        }

    }
}
