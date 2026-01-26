using System;

namespace ConditionnalOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            int age = 10;

            //classic way ;
            if (age >= 0)
            {
                Console.WriteLine("Valid");
            }
            else
            {
                Console.WriteLine("Invalid");
            }

            //but here     condition ?  true   :  false
            string result = age >= 0 ? "Valid" : "Invalid";
            
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }

namespace FormattingAndParsing
{
    class Program
    {
        static void Main(string[] args)
        {
            double value = 1000.0 / 12.34D;
            Console.WriteLine(string.Format("{0:0.00}", value)); //displays "value" with 3 decimals rouded up but still stocks it as 81.10...
            Console.WriteLine(string.Format("{0:0}", value)); //displays "value" with 0 decimals rounded up
            Console.WriteLine(string.Format("{0:0.#}", value)); //displays "value" rounded up to the 0th dcecimal unless it's zeroes, it keeps going on

            // currency adds the money symbol of your country + separators
            Console.WriteLine(value.ToString("C")); 
            
            //fixed point rounded to 3 decimals number of decimals
            Console.WriteLine(value.ToString("F3")); 
            
            //percentage (*100 + "%")
            Console.WriteLine(value.ToString("P")); 
            
            //custom format
            Console.WriteLine(value.ToString("0.00"));


        }
    }
}
}