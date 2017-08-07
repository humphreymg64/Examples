//Created by: Matthew Humphrey

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingProject1MatthewHumphrey
{
    class IPConverterDriver
    {
        /// <summary>
        /// Creates a clean table for each IP entered
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main (string[] args)
        {
            int[] iIPs = new int[4]; //the ip quatrains to be evaluated
            char cClass = 'X';     //holds the network type
            string strIP;   //the string IP
            string strChoice = "";   //the choice the user enters
            string[] strIPs = new string[4];    //the ip quatrains


            for (int i = 0; i < 4; i++)
            {
                iIPs[i] = -1; 
            }
            while (iIPs[0] == -1 && iIPs[1] == -1 && iIPs[2] == -1 && iIPs[3] == -1)
            {

                Console.WriteLine ("Enter an IP to evaluate.");
                strIP = Console.ReadLine ( );
                try
                {
                    strIPs = strIP.Split('.');
                    for (int i = 0; i < 4; i++)
                    {
                        iIPs[i] = int.Parse (strIPs[i]);
                    }
                    if(iIPs[0] < 0 && iIPs[1] < 0 && iIPs[2] < 0 && iIPs[4] < 0)
                    {
                        throw new InvalidException ( );
                    }

                    else if (iIPs[0] >223 && iIPs[1] > 255 && iIPs[2] > 255 && iIPs[4] > 255)
                    {
                        throw new InvalidException();

                    }

                    else if (iIPs[0] < 192)
                    {
                        if (iIPs[0] < 128)
                        {
                            cClass = 'A';
                        }
                        else
                        {
                            cClass = 'B';
                        }
                    }

                    else
                    {
                        cClass = 'C';
                    }
                    
                    if (cClass == 'A')
                    {
                        Console.WriteLine ("IP: {0}\nClass: {1}\nDefault Subnet Mask: 255.0.0.0\n"
                                            +"Size: 2^24\nNetwork Address: {2}.0.0.0\nBroadCast Address: {2}.255.255.255"+
                                            "\nFirst Usable IP: {2}.0.0.1\nLast Usable IP: {2}.255.255.254", strIP, cClass, iIPs[0]);
                        Console.WriteLine ("\nPress enter to continue.");
                        Console.ReadLine ( );
                    }
                    else if (cClass == 'B')
                    {
                        Console.WriteLine ("IP: {0}\nClass: {1}\nDefault Subnet Mask: 255.255.0.0\n"
                                            + "Size: 2^16\nNetwork Address: {2}.{3}.0.0\nBroadCast Address: {2}.{3}.255.255" +
                                            "\nFirst Usable IP: {2}.{3}.0.1\nLast Usable IP: {2}.{3}.255.254",
                                            strIP, cClass, iIPs[0],iIPs[1]);
                        Console.WriteLine ("\nPress enter to continue.");
                        Console.ReadLine ( );
                    }
                    else if (cClass == 'C')
                    {
                        Console.WriteLine ("IP: {0}\nClass: {1}\nDefault Subnet Mask: 255.255.255.0\n"
                                            + "Size: 2^8\nNetwork Address: {2}.{3}.{4}.0\nBroadCast Address: {2}.{3}.{4}.255" +
                                            "\nFirst Usable IP: {2}.{3}.{4}.1\nLast Usable IP: {2}.{3}.{4}.254",
                                            strIP, cClass, iIPs[0], iIPs[1],iIPs[2]);
                        Console.WriteLine ("\nPress enter to continue.");
                        Console.ReadLine ( );
                    }
                    else
                    {
                        Console.WriteLine ("Something went wrong");
                    }

                    strChoice = "";

                    while (strChoice == "")
                    {
                        Console.WriteLine ("Would you like to enter another IP?\n(y/n)");
                        strChoice = Console.ReadLine ( );
                        if (strChoice.Equals("y")|| strChoice.Equals ("Y") ||
                            strChoice.Equals ("yes") || strChoice.Equals ("Yes") || strChoice.Equals ("YES"))
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                iIPs[i] = -1;
                            }

                        }
                        else if (strChoice.Equals ("n") || strChoice.Equals ("N") ||
                            strChoice.Equals ("no") || strChoice.Equals ("No") || strChoice.Equals ("NO"))
                        {

                        }

                        else
                        {
                            strChoice = "";
                            Console.WriteLine ("That was not valid\nTry again\n");

                        }
                    }

                }
                catch (Exception)
                {

                    Console.WriteLine ("That was not valid.\nPlease try again.\n");
                    for (int i = 0; i < 4; i++)
                    {
                        iIPs[i] = -1;
                    }

                }
            }
            
        }
        public class InvalidException : Exception
        {
            public InvalidException ( ) { }
            public InvalidException (string message) : base (message) { }
            public InvalidException (string message, Exception inner) : base (message, inner) { }
            protected InvalidException (
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base (info, context)
            { }
        }
    }
}
