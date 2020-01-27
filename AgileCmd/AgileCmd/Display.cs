using System;
using System.Collections.Generic;
using System.Text;

namespace AgileCmd
{
    class Display
    {

        public List<DataRow> data = new List<DataRow>();



        public void populateDataTest()
        {

            Address Address1 = new Address("1", "1", "1", "1");
            DataRow line1 = new DataRow("def1", "001", "guy1", Address1, "0001", 1, 1);
            this.data.Add(line1);

            Address Address2 = new Address("2", "2", "2", "2");
            DataRow line2 = new DataRow("def2", "002", "guy2", Address1, "0002", 2, 2);
            this.data.Add(line2);

            Address Address3 = new Address("3", "3", "3", "3");
            DataRow line3 = new DataRow("def3", "003", "guy3", Address3, "0003", 3, 3);
            this.data.Add(line3);

            Address Address4 = new Address("4", "4", "4", "4");
            DataRow line4 = new DataRow("def4", "004", "guy4", Address4, "0004", 4, 4);
            this.data.Add(line4);

            Address Address5 = new Address("5", "5", "5", "5");
            DataRow line5 = new DataRow("def5", "005", "guy5", Address5, "0005", 5, 5);
            this.data.Add(line5);

            Address Address6 = new Address("6", "6", "6", "6");
            DataRow line6 = new DataRow("def6", "006", "guy6", Address6, "0006", 6, 6);
            this.data.Add(line6);
        }



        public void displayList()
        {


            // first loop loops through the list
            // second loop
            for (int i = 0; i < data.Count; i++)
            {
                Console.WriteLine(this.data[i].Definition);
                Console.WriteLine(this.data[i].ProviderID);
                Console.WriteLine(this.data[i].ProviderName);
                Console.WriteLine(this.data[i].Address.Street);
                Console.WriteLine(this.data[i].Address.City);
                Console.WriteLine(this.data[i].Address.State);
                Console.WriteLine(this.data[i].Address.ZipCode);
                Console.WriteLine(this.data[i].Reference);
                Console.WriteLine(this.data[i].TotalDischarge);
                Console.WriteLine(this.data[i].Cost);
                Console.WriteLine("##############################");
            }

        }


    }
}
