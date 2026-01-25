using System;

namespace Loops
{
    class Program
    {
        static void Main(string[] args)
        {
            //for (specific number of times) (var; stop condit°; increment)
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(i);
            }

            //while (condit°)
            int j = 0;
            while (j < 5)
            {
                Console.WriteLine(j);
                j++; //increment dor no inf loops
            }
            
        }
    }
}