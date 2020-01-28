using System;
using System.Collections.Generic;

namespace AgileCmd
{
    class Program
    {
        static void Main(string[] args)
        {
  
            string searchTerm = "par";
            Validate validate = new Validate();
            if (validate.validateCode(searchTerm))
            {
                searchTerm = validate.cleanInput;
                Searching search = new Searching();
                List<DataRow> data = search.SearchByCode(searchTerm);

                Display test = new Display();
                test.populateDataTest();
                test.displayList();
            }
            else { Console.WriteLine("Invalid input"); }
           
        }
    }
}
