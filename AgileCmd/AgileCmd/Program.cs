using System;
using System.Collections.Generic;

namespace AgileCmd
{
    class Program
    {
        static void Main(string[] args)
        {
  
            string searchTerm = "023";
            Validate validate = new Validate();
            if (validate.validateCode(searchTerm))
            {
                searchTerm = validate.cleanInput;
                Searching search = new Searching();
                List<DataRow> data = search.SearchByCode(searchTerm);

                Display test = new Display();
                test.populateData(data);
                test.displayFullList();
                Console.WriteLine("************************************************************************************"); Console.WriteLine("************************************************************************************");
                Console.WriteLine("************************************************************************************");

                Console.WriteLine("************************************************************************************");
                Console.WriteLine("************************************************************************************");

                test.displayCheapest(data);
            }
            else { Console.WriteLine("Invalid input"); }
           
        }
    }
}
