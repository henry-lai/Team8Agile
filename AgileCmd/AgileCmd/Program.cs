using System;

namespace AgileCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Display test = new Display();
            test.populateDataTest();
            test.displayList();
        }
    }
}
