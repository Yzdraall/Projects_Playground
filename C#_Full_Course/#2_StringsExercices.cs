using System;

namespace StringsExercices 
{
    class Program 
    {
        static void Main(string[] args)   
        {
            //string reverser test -> tset
            
            Console.WriteLine("Enter your message");
            string message = Console.ReadLine(); 

            for (int i = 0; i < message.Length;  i++)
            {
                Console.Write(message[i]); //forward
            }
            Console.WriteLine();
            for (int i = 1; i <= message.Length; i++)
            {
                Console.Write(message[message.Length - i]);
            }


            //password checker

            Console.WriteLine("Set up a password");
            string set_password = Console.ReadLine();
            Console.WriteLine("Password stored");
            
            Console.WriteLine("What's your password ?");
            string typed_password = Console.ReadLine();


            if (!string.IsNullOrEmpty(set_password) && !string.IsNullOrEmpty(typed_password))
            {
                if (set_password.Equals(typed_password))
                {
                    Console.WriteLine("Password match");
                }
                else
                {
                    Console.WriteLine("Passwords do not match");
                }
            }
            else
            {
                Console.WriteLine("Please enter a password");
            }
           Console.ReadLine();
        }

    }
}