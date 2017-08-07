/////////////////////////////////////////////////////////////////////
//
//Solution/Project: 2210-201-HumphreyMatthew-Project1
//File Name:        CreditCardDriver.cs
//Description:      Holds the menus and handles data entry
//Course:           CSCI 2210 - Data Structures
//Author:           Matthew Humphrey, humphreymg@goldmail.etsu.edu
//Created:          Sunday, September 13, 2015
//Copyright:        Matthew Humphrey, 2015
//
////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardProgram
{
    /// <summary>
    /// The Driver class that runs the program
    /// </summary>
    class CreditCardDriver
    {


        [STAThread]

        /// <summary>
        /// The main method which creates the menu
        /// </summary>
        static void Main (string[] args)
        {
            menu ( );
        }

        /// <summary>
        /// The method that holds the menus and handles data entry
        /// </summary>
        static void menu ()
        {
            int iChoice;        //records the user's choices on the menu
            int iIndex;         //used to pick an index choosen by the user
            string strIndex;    //holds the user's choosen index value
            string strAnyway;   //holds if the user wants to use something anyway
            string strChoice;   //records the user's choices on the menus
            string strName;     //holds the card holder's name
            string strCardNum;  //holds the card number
            string strEmail;    //holds the card holder's email
            string strPhone;    //holds the card holder's phone number
            string strDate;     //holds the experation date on the card
            string strAdd = "";     //holds what the user entered when they were asked to save
            string strSearch = "";    //holds what the user entered when asked what to search by

            CreditCard cc; //a card checker used to check if what the user has entered is possible
            CreditCardList ccl = new CreditCardList ( );    //a list to hold all the cards

            iChoice = 0;

            //while used to only exit the program when the user enters a 2
            while(iChoice != 8)
            {
                iChoice = 0;

                Console.WriteLine ("\nEnter the number of the action you would like to do." +
                                        "\n1.Enter Credit Card Info\n2.Load a file of Cards\n" +
                                        "3.Search for cards\n4.Save the current List\n5.Display" +
                                        " all cards\n6.Sort by card number\n7.Remove Card\n8.Exit");
                strChoice = Console.ReadLine ( );

                //while used to check that the user's choice was valid
                while (iChoice < 1 || iChoice > 8)
                {

                    //used a try catch because parsing can throw an exception
                    try
                    {
                        iChoice = int.Parse (strChoice);
                    }//end try
                    catch (Exception)
                    {
                        Console.WriteLine ("That was not valid");
                    }//end catch

                    if (iChoice < 1 || iChoice > 8)
                    {
                        Console.WriteLine ("That was not a valid choice.\nTry again.");
                        Console.WriteLine ("\nEnter the number of the action you would like to do." +
                                                "\n1.Enter Credit Card Info\n2.Load a file of Cards\n" +
                                                "3.Search for cards\n4.Save the current List\n5.Display" +
                                                " all cards\n6.Sort by card number\n7.Remove Card\n8.Exit");
                        strChoice = Console.ReadLine ( );
                    }

                }//end while (iChoice < 1 || iChoice > 2)

                if (iChoice == 1)
                {

                    Console.WriteLine ("Enter the card holder's name.");
                    strName = Console.ReadLine ( );

                    strAnyway = "9";

                    //reenter card number
                    while (!CreditCard.NameCheck (strName) && !strAnyway.Equals ("1"))
                    {
                        Console.WriteLine ("The name, {0}, is not valid.\nWould you like to use it anyway?\n1.Yes" +
                                            "\n2.No", strName);
                        strAnyway = Console.ReadLine ( );
                        if (strAnyway.Equals ("2"))
                        {
                            Console.WriteLine ("Enter the card holder's name.");
                            strName = Console.ReadLine ( );
                        }
                        else if (strAnyway.Equals ("1"))
                        {

                        }
                        else
                        {
                            Console.WriteLine ("That was not valid.\n");
                        }
                    }//end while
                    strAnyway = "9";

                    Console.WriteLine ("Enter the card number?");
                    strCardNum = Console.ReadLine ( );
                    //reenter card number
                    while (!CreditCard.CardNumCheck (strCardNum) && !strAnyway.Equals ("1"))
                    {
                        Console.WriteLine ("The card number, {0}, is not valid.\nWould you like to use it anyway?\n1.Yes" +
                                            "\n2.No", strCardNum);
                        strAnyway = Console.ReadLine ( );
                        if (strAnyway.Equals ("2"))
                        {
                            Console.WriteLine ("Enter the card number?");
                            strCardNum = Console.ReadLine ( );
                        }
                        else if (strAnyway.Equals ("1"))
                        {

                        }
                        else
                        {
                            Console.WriteLine ("That was not valid.\n");
                        }
                    }//end while
                    strAnyway = "9";

                    Console.WriteLine ("Enter the card's experation date in the form mm/yyyy?");
                    strDate = Console.ReadLine ( );
                    
                    //reenter date
                    while (!CreditCard.DateCheck (strDate) && !strAnyway.Equals ("1"))
                    {
                        Console.WriteLine ("The date, {0}, has passed or is not realistic"+ 
                                            ".\nWould you like to use it anyway?\n1.Yes" +
                                            "\n2.No", strDate);
                        strAnyway = Console.ReadLine ( );
                        if (strAnyway.Equals ("2"))
                        {
                            Console.WriteLine ("Enter the card's experation date.");
                            strDate = Console.ReadLine ( );
                        }
                        else if (strAnyway.Equals ("1"))
                        {
                        }
                        else
                        {
                            Console.WriteLine ("That was not valid.\n");
                        }
                        
                    }//end while
                    strAnyway = "9";

                    Console.WriteLine (@"Enter the card holder's phone number in the form (###)###-####?");
                    strPhone = Console.ReadLine ( );

                    //reenter phone number
                    while (!CreditCard.PhoneCheck (strPhone) && !strAnyway.Equals ("1"))
                    {
                        Console.WriteLine ("The phone number, {0}, was not valid.\nWould you like to use it anyway?\n1.Yes" +
                                            "\n2.No", strPhone);
                        strAnyway = Console.ReadLine ( );
                        if (strAnyway.Equals ("2"))
                        {
                            Console.WriteLine ("Enter the card holder's phone number.");
                            strPhone = Console.ReadLine ( );
                        }
                        else if (strAnyway.Equals ("1"))
                        {
                        }
                        else
                        {
                            Console.WriteLine ("That was not valid.\n");
                        }
                        
                    }//end while
                    strAnyway = "9";

                    Console.WriteLine ("Enter the card holder's email?");
                    strEmail = Console.ReadLine ( );
                    //reenter email
                    while (!CreditCard.EmailCheck(strEmail) && !strAnyway.Equals ("1"))
                    {
                        Console.WriteLine ("The email, {0}, was not valid.\nWould you like to use it anyway?\n1.Yes" +
                                            "\n2.No", strEmail);
                        strAnyway = Console.ReadLine ( );
                        if (strAnyway.Equals ("2"))
                        {
                            Console.WriteLine ("Enter the card holder's email.");
                            strEmail = Console.ReadLine ( );
                        }
                        else if (strAnyway.Equals ("1"))
                        {

                        }
                        else
                        {
                            Console.WriteLine ("That was not valid.\n");
                        }
                    }//end while
                    
                    cc = new CreditCard (strName, strCardNum, strPhone, strEmail, strDate);

                    strAnyway = "9";

                    while (!strAdd.Equals("2") && !strAdd.Equals("1"))
                    {
                        Console.WriteLine (cc.ToString ( ));
                        Console.WriteLine ("\nDo you want to add this card to the list?\n1.Yes\n2.No");
                        strAdd = Console.ReadLine ( );
                        if (strAdd.Equals ("1"))
                        {
                            ccl = ccl + cc;
                        }

                        else if (strAdd.Equals ("2"))
                        {

                        }

                        else
                        {
                            Console.WriteLine ("That was not valid");
                        } 
                    }

                    strAdd = "";
                }//end if(iChoice ==1)

                else if(iChoice == 2)
                {
                    ccl.Load ( );
                }

                else if(iChoice == 3)
                {
                    Console.WriteLine ("What would you like to search for?\n1.Card Holder Name\n" +
                                        "2.Card Number\n3.Search for all valid cards\n4.Search by index number");
                    strSearch = Console.ReadLine ( );
                    if (strSearch.Equals("1"))
                    {
                        Console.WriteLine ("Enter the full name of the person you want to search for.");
                        strSearch = Console.ReadLine ( );
                        ccl.SearchForName (strSearch);

                        if (ccl.SearchForName (strSearch).Count < 1)
                        {
                            Console.WriteLine ("The name {0} was not found.", strSearch);
                        }

                        else
                        {
                            for (int i = 0; i < ccl.SearchForName (strSearch).Count; i++)
                            {
                                Console.WriteLine (ccl[ccl.SearchForName (strSearch)[i]].ToString ( ));
                            }
                        }
                    }
                    else if (strSearch.Equals("2"))
                    {
                        Console.WriteLine ("Enter the card number of the card you want to search for.");
                        strSearch = Console.ReadLine ( );
                        if (ccl[strSearch].Count == 0)
                        {
                            Console.WriteLine ("The number {0} was not found.", strSearch);
                        }

                        else
                        {
                            for (int i = 0; i < ccl[strSearch].Count; i++)
                            {
                                Console.WriteLine (ccl[(ccl[strSearch])[i]].ToString ( ));
                                Console.WriteLine ("\nPress ENTER to continue");
                                Console.ReadLine ( );
                            }
                        }
                    }
                    else if (strSearch.Equals ("3"))
                    {
                        if (ccl.FindValid ( ).Count < 1)
                        {
                            Console.WriteLine ("None of the cards are valid");
                        }

                        else
                        {
                            for (int i = 0; i < ccl.FindValid ( ).Count; i++)
                            {
                                Console.WriteLine (ccl[ccl.FindValid ( )[i]].ToString ( ));
                                if (i % 5 == 3 || i == (ccl.FindValid().Count - 1))
                                {
                                    Console.WriteLine ("\nPress ENTER to continue");
                                    Console.ReadLine ( );
                                }
                            }
                        }
                    }
                    else if (strSearch.Equals ("4"))
                    {
                        iIndex = -1;
                        while (iIndex == -1)
                        {
                            Console.WriteLine ("What is the index number of the card you want to search for?");
                            strIndex = Console.ReadLine ( );
                            try
                            {
                                iIndex = int.Parse (strIndex);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine ("That was not valid.\nTry again.\n");
                            }
                        }

                        if (iIndex < ccl.Count() && iIndex > -1)
                        {
                            Console.WriteLine (ccl[iIndex]);
                            Console.WriteLine ("\nPress ENTER to continue");
                            Console.ReadLine ( ); 
                        }

                        else
                        {
                            Console.WriteLine ("That index was not found.");
                        }
                    }
                }//end else if(iChoice = 3)

                else if(iChoice == 4)
                {
                    ccl.Save();
                }

                else if (iChoice == 5)
                {
                    if (ccl.Count() > 0)
                    {
                        for (int i = 0; i < ccl.Count ( ); i++)
                        {
                            Console.WriteLine ("\n\nIndex number: " + i);
                            Console.Write (ccl[i].ToString());
                            if (i % 5 == 4 || i == (ccl.Count() - 1))
                            {
                                Console.WriteLine ("\nPress ENTER to continue");
                                Console.ReadLine( );
                            }
                        } 
                    }

                    else
                    {
                        Console.WriteLine ("There are no records to display.");
                    }
                }//end else if(iChoice == 5)

                else if (iChoice == 6)
                {
                    ccl.sort ( );
                }

                else if (iChoice == 7)
                {
                    iIndex = -1;
                    while (iIndex == -1)
                    {
                        Console.WriteLine ("What is the index number of the card you want to delete?");
                        strIndex = Console.ReadLine ( );
                        try
                        {
                            iIndex = int.Parse (strIndex);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine ("That was not valid.\nTry again.\n");
                        }
                    }

                    while (!strChoice.Equals("1") && !strChoice.Equals("2"))
                    {
                        Console.WriteLine (ccl[iIndex]);
                        Console.WriteLine ("Is this the card you want to delete?\n1.Yes\n2.No");
                        strChoice = Console.ReadLine ( );
                        if (strChoice.Equals ("1"))
                        {
                            Console.WriteLine (ccl[iIndex]);
                            ccl = ccl - ccl[iIndex];
                        }
                        else if (!strChoice.Equals("2"))
                        {
                            Console.WriteLine ("That was not valid.\nTry again");
                        }
                    }

                    strChoice = "7";
                
                }//end else if(iChoice == 7)
            }//end while(iChoice != 8)
        }//end main
    }//end CreditCardDriver class
    
}

