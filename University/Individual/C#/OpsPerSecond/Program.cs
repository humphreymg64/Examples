using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsPerSecond
{
    class Program
    {
        static void Main(string[] args)
        {
            char cChoice = '0';
            long opsPerSecond = 0;
            while (cChoice != '1')
            {
                Console.WriteLine("How many operations per second can it do?");
                opsPerSecond = long.Parse(Console.ReadLine());
                Console.WriteLine("1 Second\n------------------------\nN: " + opsPerSecond);
                Console.WriteLine("N^2: " + Math.Sqrt(opsPerSecond) + "\nN^3: " + Math.Pow(opsPerSecond,.3333333333333333));
                Console.WriteLine("2^N: " + Math.Log(opsPerSecond,2) + "\nN^4: " + Math.Pow(opsPerSecond, .25));
                Console.WriteLine("10800 Second\n------------------------\nN: " + opsPerSecond*10800);
                Console.WriteLine("N^2: " + Math.Sqrt(opsPerSecond * 10800) + "\nN^3: " + Math.Pow(opsPerSecond * 10800, .3333333333333333));
                Console.WriteLine("2^N: " + Math.Log(opsPerSecond * 10800, 2) + "\nN^4: " + Math.Pow(opsPerSecond * 10800, .25));
                Console.WriteLine("259200 Second\n------------------------\nN: " + opsPerSecond * 259200);
                Console.WriteLine("N^2: " + Math.Sqrt(opsPerSecond * 259200) + "\nN^3: " + Math.Pow(opsPerSecond * 259200, .3333333333333333));
                Console.WriteLine("2^N: " + Math.Log(opsPerSecond * 259200, 2) + "\nN^4: " + Math.Pow(opsPerSecond * 259200, .25));
                Console.WriteLine("23328000 Second\n------------------------\nN: " + opsPerSecond * 23328000);
                Console.WriteLine("N^2: " + Math.Sqrt(opsPerSecond * 23328000) + "\nN^3: " + Math.Pow(opsPerSecond * 23328000, .3333333333333333));
                Console.WriteLine("2^N: " + Math.Log(opsPerSecond * 23328000, 2) + "\nN^4: " + Math.Pow(opsPerSecond * 23328000, .25));
                Console.WriteLine("94608000 Second\n------------------------\nN: " + opsPerSecond * 94608000);
                Console.WriteLine("N^2: " + Math.Sqrt(opsPerSecond * 94608000) + "\nN^3: " + Math.Pow(opsPerSecond * 94608000, .3333333333333333));
                Console.WriteLine("2^N: " + Math.Log(opsPerSecond * 94608000, 2) + "\nN^4: " + Math.Pow(opsPerSecond * 94608000, .25));
                Console.WriteLine("100000000000 Second\n------------------------\nN: " + opsPerSecond * 100000000000);
                Console.WriteLine("N^2: " + Math.Sqrt(opsPerSecond * 100000000000) + "\nN^3: " + Math.Pow(opsPerSecond * 100000000000, .3333333333333333));
                Console.WriteLine("2^N: " + Math.Log(opsPerSecond * 100000000000, 2) + "\nN^4: " + Math.Pow(opsPerSecond * 100000000000, .25));
                Console.WriteLine("60 Second\n------------------------\nN: " + opsPerSecond * 60);
                Console.WriteLine("N^2: " + Math.Sqrt(opsPerSecond * 60) + "\nN^3: " + Math.Pow(opsPerSecond * 60, .3333333333333333));
                Console.WriteLine("2^N: " + Math.Log(opsPerSecond * 60, 2) + "\nN^4: " + Math.Pow(opsPerSecond * 60, .25));

                Console.WriteLine("Would you like to enter another number?\n1 for no...");
                cChoice = Console.ReadLine().ToCharArray()[1];
            }
        }
    }
}
