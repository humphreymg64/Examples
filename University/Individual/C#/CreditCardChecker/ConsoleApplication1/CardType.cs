/////////////////////////////////////////////////////////////////////
//
//Solution/Project: 2210-201-HumphreyMatthew-Project1
//File Name:        CardType.cs
//Description:      Holds each of the CardType enum possiblities
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
    /// the type of the card
    /// </summary>
    enum CardType
    {
        INVALID,
        VISA,
        MASTERCARD,
        AMERICAN_EXPRESS,
        DISCOVER,
        OTHER
    }
}
