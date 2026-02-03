using System;

namespace ExceptionExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            string invalidInput = "abc";

            try
            {
                //this line crashes = input not a number
                int num = Convert.ToInt32(invalidInput); 
                Console.WriteLine(num);
            }
            catch (Exception ex)
            {
                //catching the error so program doesn't stop
                Console.WriteLine("Error: " + ex.Message);
            }

            
            
            int resultNumber;
            
            //test with valid number
            if(CustomTryParse("123", out resultNumber))
            {
                Console.WriteLine($"Success! Number is: {resultNumber}");
            }
            else
            {
                Console.WriteLine("Conversion failed");
            }

            //Test with invalid text
            if(CustomTryParse("Hello", out resultNumber))
            {
                Console.WriteLine($"Success! Number is: {resultNumber}");
            }
            else
            {
                Console.WriteLine("Conversion failed");
            }

            Console.ReadLine();
        }

        

        static bool CustomTryParse(string s, out int result)
        {
            try
            {
                //try to convert using standard Parse
                result = int.Parse(s);
                return true; //if it works
            }
            catch (Exception)
            {
                result = 0;
                return false; //return false to say failure
            }
        }
    }
}