/////////////////////////////////////////////////////////////////////
//
//Solution/Project: 2210-201-HumphreyMatthew-Project2
//File Name:        CardType.cs
//Description:      Holds each of the CardType enum possiblities
//Course:           CSCI 2210 - Data Structures
//Author:           Matthew Humphrey, humphreymg@goldmail.etsu.edu
//Created:          Sunday, September 28, 2015
//Copyright:        Matthew Humphrey, 2015
//
////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreditCardProgram
{
    /// <summary>
    /// The class that creates a list of cards and uses them
    /// </summary>
    class CreditCardList
    {
        public bool SaveNeeded { get; set; }   //holds wheather or not the list has ben edited
        public CreditCard this[int index]   //used to do indexing
        {
            get
            {
               
               if (index < Count() && index > -1)
                {
                   return CCL[index]; 
                }
               else
                {
                   return null;
                }
            }
            set
            {
                if (Count ( ) < index && index > 0)
                 {
                    try
                    {
                        CCL[index] = new CreditCard (value);
                    }
                    catch (Exception)
                    {
                        System.Console.Write ("Could not set value");
                    }
                 }
            }
        }

        public List<int> this[string strNum]   //used to do indexing
        {
            get
            {
                List<int> indexes = new List<int>(Count());

                indexes.Add (CCL.BinarySearch (new CreditCard ("", strNum, "", "", "")));

                
                return indexes;
            }            
        }


        private List<CreditCard> CCL = new List<CreditCard>(5); //the list that holds all the cards

        /// <summary>
        /// A no arguments constructor for the CreditCardList.
        /// </summary>
        public CreditCardList ()
        {
            SaveNeeded = false;
        }

        /// <summary>
        /// Constructor with an initial card.
        /// </summary>
        /// <param name="Card">A card that will be added to the list when it is created.</param>
        public CreditCardList (CreditCard Card)
        {
            CCL.Add (Card);
            SaveNeeded = false;
        }

        /// <summary>
        /// A constructor that loads an inital list.
        /// </summary>
        /// <param name="FileName">Name of the file to load.</param>
        public CreditCardList (String FileName)
        {
            Load (FileName);
            SaveNeeded = false;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="CardList">The card list to be copied.</param>
        public CreditCardList (CreditCardList CardList)
        {
            if (CardList.CCL.Count != 0)
            {
                for (int i = 0; i < CardList.CCL.Count; i++)
                {
                    CCL.Add (new CreditCard (CardList.CCL[i]));
                } 
            }

            this.SaveNeeded = CardList.SaveNeeded;
        }

        /// <summary>
        /// Add new items to the List with the + operator.
        /// </summary>
        /// <param name="CCL">The CCL that will be added to.</param>
        /// <param name="Card">The card that will be added.</param>
        /// <returns>
        /// The a CreditCardList that is the same as CCL with card added.
        /// </returns>
        static public CreditCardList operator+ (CreditCardList CCL, CreditCard Card)
        {
            CreditCardList CardList = new CreditCardList (CCL); //creates a new card list to be returned
            CardList.CCL.Add (Card);
            CardList.SaveNeeded = true;

            return CardList;
        }

        /// <summary>
        /// Removes an item to the List with the - operator.
        /// </summary>
        /// <param name="CCL">The CCL that will be removed to.</param>
        /// <param name="Card">The card that will be removed.</param>
        /// <returns>
        /// The a CreditCardList that is the same as CCL with card removed.
        /// </returns>
        static public CreditCardList operator- (CreditCardList CCL, CreditCard Card)
        {
            CreditCardList CardList = new CreditCardList (CCL); //creates a new card list to be returned
            Console.WriteLine (CardList.CCL.Remove (Card));
            CardList.SaveNeeded = true;

            return CardList;
        }

        /// <summary>
        /// Counts the amount of cards in CCL.
        /// </summary>
        /// <returns>
        /// the amount of cards in CCL
        /// </returns>
        public int Count()
        {
            int Count;  //holds the count to be returned
            Count = CCL.Count ( );
            return Count;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>
        /// if it was successful
        /// </returns>
        public bool Save()
        {
            bool blnSucessful = true;       //returns true if the method suceeded

            SaveFileDialog dlg = new SaveFileDialog ( );        //used to find somewhere to save the file
            StreamWriter writer = null;     //used to write the file to be saved

            dlg.Filter = "text files|*.txt;*.text|all files|*.*";
            dlg.InitialDirectory = @"C:\Users\Matthew\Documents\Visual Studio 2015\Projects\2210Projects\ConsoleApplication1\testfiles";
            dlg.Title = "Select the file of cards you want to load";
            
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    writer = new StreamWriter (new FileStream (dlg.FileName, FileMode.Create, FileAccess.Write));
                    for (int i = 0; i < Count ( ); i++)
                    {
                        writer.WriteLine (CCL[i].AllInfo());
                    }
                }
                catch (Exception)
                {
                    blnSucessful = false;
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close ( );
                    }
                }
            }
            else
            {
                blnSucessful = false;
            }

            SaveNeeded = false;
            return blnSucessful;
        }

        /// <summary>
        /// Loads at a specified file path.
        /// </summary>
        /// <param name="strFileName">Name of the file path.</param>
        /// <returns>
        /// if it was successful or not
        /// </returns>
        public bool Load(string strFileName)
        {
            string[] fields;        //used to hold all the fields for the credit card classes
            bool blnSucessful = true;   //returns true if the method loaded sucessfully

            StreamReader reader = null;        //used to read files

            try
            {
                reader = new StreamReader (strFileName);
                while (reader.Peek() != 0)
                {
                    fields = reader.ReadLine ( ).Split ('|');
                    CCL.Add (new CreditCard (fields[0], fields[3], fields[1], fields[2], fields[4]));
                }
            }
            catch (Exception)
            {
                blnSucessful = false;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close ( );
                }
            }
            return blnSucessful;
        }

        /// <summary>
        /// Loads a CCL from a dialog box.
        /// </summary>
        /// <returns>
        /// if it was successful or not
        /// </returns>
        public bool Load()
        {
            string[] fields;        //holds all the fields from each line of the file
            bool blnSucessful = true;       //returns true if the files were loaded sucessfully

            OpenFileDialog dlg = new OpenFileDialog ( );    //used to open files to be loaded
            StreamReader reader = null;         //used to read lines from the file

            dlg.Filter = "text files|*.txt;*.text|all files|*.*";
            dlg.InitialDirectory = @"C:\Users\Matthew\Documents\Visual Studio 2015\Projects\2210Projects\ConsoleApplication1\testfiles";
            dlg.Title = "Select the file of cards you want to load";

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    reader = new StreamReader (dlg.OpenFile ( ));
                    while (reader.Peek ( ) != 0)
                    {
                        fields = reader.ReadLine ( ).Split ('|');
                        CCL.Add (new CreditCard (fields[0], fields[3], fields[1], fields[2], fields[4]));
                    }
                }
                catch (Exception)
                {
                    blnSucessful = false;
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close ( );
                    }
                }
            }
            else
            {
                blnSucessful = false;
            }

            return blnSucessful;
        }

        /// <summary>
        /// Sorts this instance.
        /// </summary>
        public void sort()
        {
            CCL.Sort ( );
            SaveNeeded = true;
        }

        /// <summary>
        /// Searches for a name.
        /// </summary>
        /// <param name="strName">Name of the string to search for.</param>
        /// <returns>
        /// the index values the name was found at
        /// </returns>
        public List<int> SearchForName(string strName)
        {
            List<int> iIndexes = new List<int>(CCL.Count);  //used to hold indexes of cards with the specified name

            for (int i = 0; i < Count(); i++)
            {
                if (CCL[i].CompareTo (strName) == 0)
                {
                    iIndexes.Add (i);
                }
            }

            return iIndexes;
        }

        /// <summary>
        /// Finds all the valid cards.
        /// </summary>
        /// <returns>
        /// the index values of the valid cards
        /// </returns>
        public List<int> FindValid()
        {
            List<int> iIndexes = new List<int> (CCL.Count); //used to hold indexes of valid cards

            for (int i = 0; i < Count(); i++)
            {
                if (CCL[i].blnDateValid && CCL[i].blnEmailReal && CCL[i].blnNumReal && CCL[i].blnPhoneReal)
                {
                    iIndexes.Add (i);
                }
            }

            return iIndexes;
        }

        
    }
}
