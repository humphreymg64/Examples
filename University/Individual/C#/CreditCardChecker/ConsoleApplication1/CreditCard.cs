/////////////////////////////////////////////////////////////////////
//
//Solution/Project: 2210-201-HumphreyMatthew-Project1
//File Name:        CreditCard.cs
//Description:      Holds and Checks credit card info
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CreditCardProgram
{

    /// <summary>
    /// The class the holds the information for a credit card
    /// </summary>
    class CreditCard : IComparable<CreditCard>,IEquatable<CreditCard>
    {
        public bool blnNumReal { get; set; }    //holds wheather or not the card number could be a real number
        public bool blnPhoneReal { get; set; }  //holds wheather or not the phone number could be real
        public bool blnEmailReal { get; set; }  //holds wheather or not the email could be real
        public bool blnDateValid { get; set; }  //true if the date given is before the current date
        private string strCardNum { get; set; }   //holds the string of the card number to be checked
        public string strPhone { get; set; }    //holds the phone number to be verified
        public string strEmail { get; set; }    //holds the email to be verified
        public string strName { get; set; }     //holds the card holder's name
        public string strDate { get; set; }     //holds the cards experation date
        public CardType type { get; set; }      //holds the card type

        /// <summary>
        /// An empty constructor for the credit card class.
        /// </summary>
        public CreditCard ()
        {
            strCardNum = "None Entered";
            strPhone = "None Entered";
            strEmail = "None Entered";
            strName = "None Enetered";
            blnEmailReal = false;
            blnNumReal = false;
            blnPhoneReal = false;
            blnDateValid = false;
            type = CardType.INVALID;
        }//end CreditCard()

        /// <summary>
        /// The constructor that intializes all the variables in the class.
        /// </summary>
        /// <param name="strName">Name of the string.</param>
        /// <param name="strCardNum">The string card number.</param>
        /// <param name="strPhone">The string phone.</param>
        /// <param name="strEmail">The string email.</param>
        /// <param name="strDate">The string date.</param>
        public CreditCard (string strName, string strCardNum, string strPhone, string strEmail, string strDate)
        {
            this.strName = strName;
            this.strPhone = strPhone;
            this.strEmail = strEmail;
            this.strCardNum = strCardNum;
            this.strDate = strDate;
            blnNumReal = CardNumCheck (strCardNum);
            blnPhoneReal = PhoneCheck (strPhone);
            blnEmailReal = EmailCheck (strEmail);
            blnDateValid = DateCheck (strDate);
            type = TypePicker (strCardNum);

        }//end CreditCard(string,string,string,string,string)

        /// <summary>
        /// Copy constructor for the CreditCard class.
        /// </summary>
        /// <param name="card">The card to be copied.</param>
        public CreditCard (CreditCard card)
        {
            this.strCardNum = card.strCardNum;
            this.strDate = card.strDate;
            this.strEmail = card.strEmail;
            this.strName = card.strName;
            this.strPhone = card.strPhone;
            this.type = card.type;
            this.blnDateValid = card.blnDateValid;
            this.blnEmailReal = card.blnEmailReal;
            this.blnNumReal = card.blnNumReal;
            this.blnPhoneReal = card.blnPhoneReal;
        }

        /// <summary>
        /// Decides the card's type based on the first four digits
        /// </summary>
        /// <param name="strCardNum">The string card number.</param>
        /// <returns>
        /// Returns the decided card type
        /// </returns>
        public CardType TypePicker (string strCardNum)
        {
            int iFirstNums;         //holds the first four numbers of the card
            CardType pickedType;    //the type this method picks to return

            //initalize variables
            iFirstNums = 0;
            pickedType = CardType.INVALID;

            if (blnNumReal)
            {
                try
                {
                    iFirstNums = int.Parse (strCardNum.Substring (0, 4));
                }//end try

                catch (Exception e)
                {
                    Console.WriteLine (e.Message);
                }//end catch

                if (iFirstNums < 3800 && iFirstNums > 3699)
                {
                    pickedType = CardType.AMERICAN_EXPRESS;
                }//end if

                else if (iFirstNums < 3500 && iFirstNums > 3399)
                {
                    pickedType = CardType.AMERICAN_EXPRESS;
                }//end else if

                else if (iFirstNums < 5000 && iFirstNums > 3999)
                {
                    pickedType = CardType.VISA;
                }//end else if

                else if (iFirstNums < 5600 && iFirstNums > 5099)
                {
                    pickedType = CardType.MASTERCARD;
                }//end else if

                else if (iFirstNums < 6600 && iFirstNums > 6499)
                {
                    pickedType = CardType.DISCOVER;
                }//end else if

                else if (iFirstNums < 6450 && iFirstNums > 6439)
                {
                    pickedType = CardType.DISCOVER;
                }//end else if

                else if (iFirstNums == 6011)
                {
                    pickedType = CardType.DISCOVER;
                }//end else if

                else
                {
                    pickedType = CardType.OTHER;
                }//end else

            }//end if(blnNumReal)

            if (CardType.INVALID == pickedType)
            {
                blnNumReal = false;
            }//end if

            return pickedType;
        }//end TypePicker(string) CardType

        /// <summary>
        /// Checks if the experation date is a date and that the date has not passed.
        /// </summary>
        /// <param name="strDate">The string date.</param>
        /// <returns>
        /// Returns a bln that shows wheather or not the date could be real
        /// </returns>
        static public bool DateCheck (string strDate)
        {
            bool blnDateCheck;     //the value to be returned
            int iCurrentMonth;     //holds the current month
            int iCurrentYear;	   //holds the current year
            int iCardMonth;     //holds the card's month
            int iCardYear;	   //holds the card's year


            DateTime current = DateTime.Today;
            Regex dateFullPattern = new Regex (@"(\d{2})/(\d{4})");
            Regex dateYearPattern = new Regex (@"/(\d{4})");
            Regex dateMonthPattern = new Regex (@"(\d{2})/");
            Match dateFullMatch = dateFullPattern.Match (strDate);
            Match dateMonthMatch = dateMonthPattern.Match (strDate);
            Match dateYearMatch = dateYearPattern.Match (strDate);

            iCardMonth = -1;
            iCardYear = -1;

            blnDateCheck = dateFullMatch.Success;

            if (blnDateCheck)
            {
                iCurrentMonth = current.Month;
                iCurrentYear = current.Year;

                try
                {
                    iCardYear = int.Parse (strDate.Substring (3, 4));
                    iCardMonth = int.Parse (strDate.Substring (0, 2));
                }//end try
                catch (Exception e)
                {
                    blnDateCheck = false;
                    Console.WriteLine (e.Message);
                }//end catch
                
                //check if the card's year has passed
                if (iCurrentYear >= iCardYear)
                {
                    //check if the card's month has passed
                    if (iCurrentMonth >= iCardMonth)
                    {
                        blnDateCheck = false;
                    }//end if
                }//end if

                if (iCardMonth > 12)
                {
                    blnDateCheck = false;
                }//end if
            }//end if(blnCheckDate)

            return blnDateCheck;
        }//end DateCheck(string) bool

        /// <summary>
        /// Checks that the email is in the correct form
        /// </summary>
        /// <param name="strEmail">The string email.</param>
        /// <returns></returns>
        static public bool EmailCheck (string strEmail)
        {
            bool blnEmailCheck;     //the value to be returned

            Regex emailPattern = new Regex (@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)
                                              |(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})");
            Match emailMatch = emailPattern.Match (strEmail);

            blnEmailCheck = emailMatch.Success;

            return blnEmailCheck;
        }//end EmailCheck(string) bool

        /// <summary>
        /// Checks that the phone number is in the correct form
        /// </summary>
        /// <param name="strPhone">The string phone.</param>
        /// <returns>
        /// Returns a bln that shows wheather or not the phone number was in the correct form
        /// </returns>
        static public bool PhoneCheck (string strPhone)
        {
            bool blnPhoneCheck;     //the value to be returned

            Regex phonePattern = new Regex (@"\((\d{3})\)\s*(\d{3}(-|\s*)\d{4})");
            Match phoneMatch = phonePattern.Match (strPhone);

            blnPhoneCheck = phoneMatch.Success;

            return blnPhoneCheck;
        }//end PhoneCheck(string) bool

        /// <summary>
        /// Checks that the card number is the correct number of digits and that it matches luhn's algorithm
        /// </summary>
        /// <param name="strCardNum">The string card number.</param>
        /// <returns>
        /// Returns a bln that shows wheather or not the card number could be real
        /// </returns>
        static public bool CardNumCheck (string strCardNum)
        {
            bool blnNumReal = false;        //the value to be returned. shows if the value has been shown to be real
            decimal[] dCardNumbers;    //holds the values of each indivdual number
            decimal dSum = 0;     //the sum of each of the numbers in the card number except the check digit
            char[] cBadChars = new char[2]; //all the chars to be removed

            cBadChars[0] = '-';
            cBadChars[1] = ' ';

            while (strCardNum.LastIndexOfAny(cBadChars) != -1)
            {
               strCardNum = strCardNum.Substring(strCardNum.LastIndexOfAny(cBadChars) + 1,1) +
                    strCardNum.Substring(0,strCardNum.LastIndexOfAny(cBadChars)-1); 
            }
            
            //check that it is the right length. Also proves that the length is not null
            if (strCardNum.Length > 19 || strCardNum.Length < 12)
            {
                blnNumReal = false;
            }//end if
            
            else
            {
                dCardNumbers = new decimal[strCardNum.Length];

                //in a try in case the user entered letters
                try
                {
                    for (int i = 0; i < strCardNum.Length; i++)
                    {
                        dCardNumbers[i] = decimal.Parse (strCardNum.Substring (i, 1));
                    }//end for
                }//end try

                catch (Exception e)
                {
                    blnNumReal = false;
                    Console.WriteLine (e.Message);
                }//end catch

                for (int i = (strCardNum.Length - 2); i < 0; i -= 2)
                {
                    dCardNumbers[i] = (dCardNumbers[i] * 2);

                    if (dCardNumbers[i] >= 10)
                    {
                        dCardNumbers[i] -= 10;
                        dCardNumbers[i]++;
                    }//end if
                }//end for

                for (int i = 0; i > dCardNumbers.Length; i++)
                {
                    dSum += dCardNumbers[i];
                }//end for
                
                if (dSum % 10 == 0)
                {
                    blnNumReal = true;
                }//end if
            }//end else
            
            return blnNumReal;
        }//end CardNumCheck(string)

        public string AllInfo()
        {
            string strInfo; //the information to be returned

            strInfo = strName + '|' + strPhone + '|' + strEmail + '|' + strCardNum + '|' + strDate;

            return strInfo;
        }

        /// <summary>
        /// Checks the name for validity
        /// </summary>
        /// <param name="strName">The name to check.</param>
        /// <returns>
        /// true if the name is not empty
        /// </returns>
        static public bool NameCheck(string strName)
        {
            bool blnNameReal = true;   //the value to return if the name is real

            if (string.IsNullOrEmpty(strName))
            {
                blnNameReal = false;

            }

            return blnNameReal;
        }

        /// <summary>
        /// Compares two CreditCards by their Card Numbers
        /// </summary>
        /// <param name="Card">The card to be compared.</param>
        /// <returns></returns>
        public int CompareTo (CreditCard Card)
        {
            int iReturnNum;

            if(this.strCardNum.CompareTo(Card.strCardNum) < 0)
            {
                iReturnNum = -1;
            }

            if (this.strCardNum.CompareTo (Card.strCardNum) > 0)
            {
                iReturnNum = 1;
            }

            else
            {
                iReturnNum = 0;
            }

            return iReturnNum;
        }

        /// <summary>
        /// Compares the names of the card holders. Uses string compare to methods to decie
        /// </summary>
        /// <param name="strName">Name of the string.</param>
        /// <returns></returns>
        public int CompareTo (string strName)
        {
            int iReturnNum;

            iReturnNum = this.strName.CompareTo (strName);

            return iReturnNum;
        }

        /// <summary>
        /// Returns a string that displays all the cards information.
        /// </summary>
        /// <returns>
        /// Returns a string that displays all the cards information.
        /// </returns>
        override
        public string ToString ( )
        {
            return String.Format ("\nName: {0}\nCard Number: {1}\nType: {2}\nDate: {3}\nPhone: {4}\nEmail: {5}",strName,
                                    strCardNum, type, strDate, strPhone, strEmail);
        }//end ToString()

        /// <summary>
        /// Compares cards for equality.
        /// </summary>
        /// <param name="card">The card to be compared.</param>
        /// <returns>
        /// returns a true if they are equal
        /// </returns>
        public bool Equals (CreditCard card)
        {
            bool[] blnResults = new bool[5];    //holds all the results from the comparisons
            bool blnFinalResult = false;        //holds the sum of the results

            blnResults[0] = this.strCardNum.Equals (card.strCardNum);
            blnResults[1] = this.strDate.Equals (card.strDate);
            blnResults[2] = this.strEmail.Equals (card.strEmail);
            blnResults[3] = this.strName.Equals (card.strName);
            blnResults[4] = this.strPhone.Equals (card.strPhone);

            if(blnResults[0] && blnResults[1] && blnResults[2] && blnResults[3] && blnResults[4])
            {
                blnFinalResult = true;
            }

            return blnFinalResult;
        }
    }//end CreditCard class
}

