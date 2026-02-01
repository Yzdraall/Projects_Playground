using System;
using System.Collections.Generic;

namespace Parameters
{
    class Program
    {
        static void Main(string[] args)
        {
            //standard parameters
            int firstNum = ReadInt("Enter first number: ");
            int secondNum = ReadInt("Enter second number: ");
            
            //passing variables as arguments
            int result = Add(firstNum, secondNum);
            Console.WriteLine(result);

            //using the string return function with parameter
            string name = ReadString("Enter your name");
            int age = ReadInt("Enter your age: ");
            string details = UserDetails(name,  age);
            Console.WriteLine(details);


            //optional parameters
            int resultOpt1 = AddOptional(10, 20); 
            Console.WriteLine(resultOpt1);

            //overrides default value
            int resultOpt2 = AddOptional(10, 20, 50);
            Console.WriteLine(resultOpt2);


            //order doesn't matter if we name 
            PrintDetails(age: 23, name: "Aba", address: "Street 1");
            PrintDetails(address: "Street 2", name: "Test", age: 30);


            List<string> shoppingList = new List<string> 
            {
                "Coffee", "Milk"
            };

            //built-in method
            Console.WriteLine(shoppingList.IndexOf("Milk"));

            //manual search logic inside Main
            int index = -1;

            for (int i = 0; i < shoppingList.Count; i++)
            {
                //checking match case-insensitive
                if (shoppingList[i].ToLower().Equals("milk"))
                {
                    index = i;
                }
            }



            //built-in TryParse example
            if (int.TryParse("123", out int parsedNum))
            {
                Console.WriteLine(parsedNum);
            }

            //custom method with out
            if (FindInList("Milk", out int foundIndex))
            {
                Console.WriteLine($"Found at index: {foundIndex}");
            }


            int value = 10;
            //pass by value
            Assign(value);
            Console.WriteLine(value); //stil 10

            //pass by reference
            AssignRef(ref value);
            Console.WriteLine(value); //now 20


            string myName = "Aba";
            Console.WriteLine($"Before: {myName}");

            ChangeName(ref myName, "Bob");
            
            Console.WriteLine($"After: {myName}");

            Console.ReadLine();
        }



        //--- definitions ---

        //helper to read int
        static int ReadInt(string message)
        {
            Console.Write(message);
            return Convert.ToInt32(Console.ReadLine());
        }

        //helper to read string
        static string ReadString(string message)
        {
            Console.Write($"{message}: ");
            return Console.ReadLine();
        }

        //standard add
        static int Add(int a, int b)
        {
            return a + b;
        }

        //optional parameter c has default value 0
        static int AddOptional(int a, int b, int c = 0)
        {
            return a + b + c;
        }

        static string UserDetails(string name, int age)
        {
           return $"My name is {name} and i'm {age}";
        }


        //named arguments example
        static void PrintDetails(string name, int age, string address)
        {
            Console.WriteLine($"Name: {name} - Age: {age} - Address: {address}");
        }



        //out parameter example
        static bool FindInList(string item, out int index)
        {
            List<string> shoppingList = new List<string> { "Coffee", "Milk" };
            index = shoppingList.IndexOf(item);
            
            return index > -1;
        }

        static bool ChangeName(ref string name, string newName)
        {
            //Checks if the new name is not empty
            if (!string.IsNullOrEmpty(newName))
            {
                name = newName;
                return true;
            }
            return false;
        }


        //pass by value
        static void Assign(int num)
        {
            num = 20;
        }

        //pass by reference
        static void AssignRef(ref int num)
        {
            num = 20;
        }

        
    }
}