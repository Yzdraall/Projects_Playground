using System;

namespace Strings
{
    class Program
    {
        static void Main(string[] args)
        {
            ///////// Verbatim string literal //////////

            //\t \n \" \\ = \
            string speech = "He said \"something\"";
            string path = "C:\\Users\\CoffeeNCode\\Desktop\\C# Course\nNew line test";
            Console.WriteLine(path);
            Console.WriteLine(speech);

            string name = "Aba";
            int age = 23;

            //@ ignores \ characters
            path = @"C:\Users\CoffeeNCode\Desktop\C# Course" + "\nNew line test";
            Console.WriteLine(path);

            name = @"Hello ""someone"""; // double "" for it to count
            Console.WriteLine(name);

            name = "Hello 'someone'";   //alternative
            Console.WriteLine(name);
        

            /////// String formatting ////// 
        
            Console.WriteLine("\n"); 
            Console.WriteLine("-----------"); 
            Console.WriteLine("\n"); 

            
            name = "Aba";

            //concatenation using +
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Age: " + age);

            Console.WriteLine();

            //concatenation with escape character
            Console.WriteLine("Name: " + name + "\nAge: " + age);
            Console.WriteLine("Your name is " + name + ", and your age is " + age);
            //hard to read    
            
            //alternatives
            Console.WriteLine("Your name is {0}, and your age is {1}", name, age);  //composite formatting
            Console.WriteLine("Name: {0}\nAge: {1}", name, age);                    
            Console.WriteLine($"Your name is {name} and your age is {age}");        //interpolation
            string test = string.Concat(" Your name is ", name, " and your age is ", age);  //don't forget the spaces
            Console.WriteLine(test);

            //using arrays (covered later)
            string[] names = new string[] {"Aba ", "Bob ", "Jim"};
            Console.WriteLine(string.Concat(names));


            /////// String Equal func /////


            Console.WriteLine("\n"); 
            Console.WriteLine("-----------"); 
            Console.WriteLine("\n"); 


            string message = "Hello";
            string compare = "Hello";


            //using .Equals() is the standard way for strings
            if (message.Equals(compare))
            {
                Console.WriteLine("Same");
            }
            else
            {
                Console.WriteLine("Diff");
            }

            //input validation example
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            //if string is not empty
            if (!name.Equals(""))
            {
                Console.WriteLine("Your name is " + name);
            }
            else
            {
                Console.WriteLine("Invalid name input");
            }



            char[] chars = new char[] { 'H', 'e', 'l', 'l', 'o' };
            
            //stored as generic object
            object newCompare = new string(chars);

            //compare str with object
            //== checks if they are the same memory address
            if (message == newCompare) 
            {
                Console.WriteLine("Same");
            }
            else
            {
                Console.WriteLine("Different"); 
            }
            // returns different

            //compare the objet's and str's content
            if (message.Equals(compare)) 
            {
                Console.WriteLine("Same");
            }
            else
            {
                Console.WriteLine("Different"); 
            }
            //returns same




            /////// Strings Loops /////////


            string message = "C# is awesome";

            //string is essentially a char[]
            //printing specific characters by index
            //Console.WriteLine(message[0]); C
            //Console.WriteLine(message[1]); #
            //Console.WriteLine(message[2]); 
            //Console.WriteLine(message[3]); i


            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
            
                //pauses execution for 100ms
                Thread.Sleep(100); 
            }

            Console.WriteLine(); //new line after loop

            Console.WriteLine(message.Contains("C")); //checks if C in message 

            //implementing ou own .Contain method
            bool contains = false;

            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] == 'C')
                {
                    contains = true;
                }
            }

            Console.WriteLine(contains);
        


            /////// IsNullOrEmpty() ////////



            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            Console.WriteLine($"Your name is {name}");

            //if (name != ""){
            //Console.WriteLine("0");
            //}

            //if (!name.Equals("")){
            //Console.WriteLine("1");
            //}

            //correct way
            //checks if string is null OR ""
            if (!string.IsNullOrEmpty(name))
            {
                //nested check example
                if (name.Equals("Aba"))
                {
                    Console.WriteLine("Correct");
                }
            }

            Console.ReadLine();


        }

    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                               