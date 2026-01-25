using System;   //imports

namespace HelloWorld    //comme un package qu'on crée
{
    class Program   //class name (C# = POO)
    {
        static void Main(string[] args)    //if __name__ == __main__
        {
            // print
            Console.WriteLine("Hello World");
            
            //empeche la console de se fermer immédiatement attends qu'on appuie sur 'rentrée'
            Console.ReadLine(); 
        }
    }
}
