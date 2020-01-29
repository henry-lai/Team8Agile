using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileCmd
{
    public class Display
    {

        //public List<DataRow> data = new List<DataRow>();
        //public List<string> wrongdata = new List<string>();


        

        //public void populateDataTest()
       // {

      //      Address Address1 = new Address("Big Street", "LargeTown", "BIG", "534 453");
    //        DataRow line1 = new DataRow("023 - Heart Transplant", "10001", "SOUTHEAST ALBAMA MEDICAL CENTER", Address1, "AL - Dothan", 28, 25000.00, 1000);
  //          this.data.Add(line1);

//            Address Address2 = new Address("Big Street", "LargeTown", "BIG", "534 453");
        //    DataRow line2 = new DataRow("023 - Heart Transplant", "10001", "SOUTHEAST ALBAMA MEDICAL CENTER", Address1, "AL - Dothan", 28, 40000.00, 2);
         //   this.data.Add(line2);

            //Address Address3 = new Address("Big Street", "LargeTown", "BIG", "534 453");
          //  DataRow line3 = new DataRow("023 - Heart Transplant", "10001", "SOUTHEAST ALBAMA MEDICAL CENTER", Address1, "AL - Dothan", 28, 2000.00, 350);
        //    this.data.Add(line3);

      //      Address Address4 = new Address("Big Street", "LargeTown", "BIG", "534 453");
    //        DataRow line4 = new DataRow("023 - Heart Transplant", "10001", "SOUTHEAST ALBAMA MEDICAL CENTER", Address1, "AL - Dothan", 28, 2500000.0, 10000);
  //          this.data.Add(line4);
             
           // Address Address5 = new Address("Big Street", "LargeTown", "BIG", "534 453");
         //   DataRow line5 = new DataRow("023 - Heart Transplant", "10001", "SOUTHEAST ALBAMA MEDICAL CENTER", Address1, "AL - Dothan", 28, 256000.00, 4678);
       //     this.data.Add(line5);

     //       Address Address6 = new Address("Big Street", "LargeTown", "BIG", "534 453");
   //         DataRow line6 = new DataRow("023 - Heart Transplant", "10001", "SOUTHEAST ALBAMA MEDICAL CENTER", Address1, "AL - Dothan", 28, 2570000.00, 2.5);
 //           this.data.Add(line6);

            
 //       }





       public void populateData(List<DataRow> newList) {


            //listReceived = Search();

            List<DataRow> tempList = new List<DataRow>();
            tempList = newList;
            if (validateList(newList) == true)
            {
                Console.WriteLine("Recieved Valid List");
            }
            else if(validateList(newList) == false)
            {
                newList = tempList;
                Console.WriteLine("Recieved InValid List");
            }
        }
        

        public bool validateList(List<DataRow> listToValidate)
        {


            if(listToValidate.Count < 1)
            {
                return false;
                Console.WriteLine("Empty List");
            }

          //  for (int i = 0; i < listToValidate.Count; i++)
          //  {
          //      Console.WriteLine(listToValidate[i].GetType().FullName);
          //      if (!((listToValidate[i].GetType().FullName) == "AgileCmd.DataRow"))
           //     {
           //         Console.WriteLine("Not Working");
           //     return false;
           //     }
          //  }
            return true;
        }


        public void displayFullList(List<DataRow> listToDisplay)
        {
            //populate display list with data given from search
            Console.WriteLine("Definition - Provider ID - Provider Name - Street - City - State - Zip Code - Reference - Total Discharge - Cost - Distance From User");
            for (int i = 0; i < listToDisplay.Count; i++)
            {
                displayLine(listToDisplay[i]);
            }

        }

        public DataRow findCheapest(List<DataRow> listToCheck)
        {
            DataRow min = new DataRow();
            min.cost = 100000000000;
            for (int i = 0; i < listToCheck.Count; i++)
            {
                if(listToCheck[i].cost < min.cost)
                {
                    listToCheck[i].label.Add("Cheapest");
                    min = listToCheck[i];
                    for (int j = 0; j < listToCheck.Count; i++)
                    {
                        if (!(listToCheck[i].Equals(listToCheck[j])))
                        {
                            listToCheck[j].label.Remove("Cheapest");
                            break;
                        }
                    }
                }
            }
            return min;
        }

        public DataRow findSmallestDistance(List<DataRow> listToCheck)
        {
            DataRow min = new DataRow();
            min.distanceFromUser = 1000000000000;
            for (int i = 0; i < listToCheck.Count; i++)
            {
                if (listToCheck[i].distanceFromUser < min.distanceFromUser)
                {
                    listToCheck[i].label.Add("Closest");
                    min = listToCheck[i];
                    for (int j = 0; j < listToCheck.Count; j++)
                    {
                        if (!(listToCheck[i].Equals(listToCheck[j])))
                        {
                            listToCheck[j].label.Remove("Closest");
                            break;
                        }
                    }
                }

            }
            return min;
        }

        public List<DataRow> sortCost(List<DataRow> listToSort)
        {
            Console.WriteLine("THIS IS SORTED LIST COST");

            IEnumerable<DataRow> query = listToSort.OrderBy(DataRow => DataRow.cost);

            foreach(DataRow datarow in query)
            {
                Console.WriteLine("{0} - {1}", datarow.cost, datarow.distanceFromUser);
            }

            listToSort = query.ToList<DataRow>();
            return listToSort;

        }

        public List<DataRow> sortDistance(List<DataRow> listToSort)
        {
            Console.WriteLine("THIS IS SORTED LIST DISTANCE");

            IEnumerable<DataRow> query = listToSort.OrderBy(DataRow => DataRow.distanceFromUser);

            foreach (DataRow datarow in query)
            {
                Console.WriteLine("{0} - {1}", datarow.distanceFromUser, datarow.cost);
            }

            listToSort = query.ToList<DataRow>();
            return listToSort;
        }

        public List<DataRow> sortRank(List<DataRow> listToSort)
        {

            listToSort = setScore(listToSort);
            Console.WriteLine("THIS IS SORTED LIST RANKING");

            IEnumerable<DataRow> query = listToSort.OrderBy(DataRow => DataRow.score);

            foreach (DataRow datarow in query)
            {
                Console.WriteLine("{0} - {1} - {2}", datarow.score, datarow.cost, datarow.distanceFromUser);
            }

            listToSort = query.ToList<DataRow>();
            return listToSort;
        }


        public List<DataRow> setScore(List<DataRow> listToSort) {
            listToSort = setDistanceRanking(listToSort);
            listToSort = setCostRanking(listToSort);
            for (int i = 0; i < listToSort.Count; i++)
            {
                listToSort[i].score = listToSort[i].costRank * listToSort[i].distanceRank;
            }
            return listToSort;
        }


        public DataRow findBest(List<DataRow> listToSort)
        {
            setCostRanking(listToSort);
            setDistanceRanking(listToSort);

            DataRow best = new DataRow();
            best.score = 0;
            best.costRank = 0;

            for (int i = 0; i < listToSort.Count; i++)
            {
                listToSort[i].score = listToSort[i].costRank * listToSort[i].distanceRank;
                if(listToSort[i].score > best.score)
                {
                    listToSort[i].label.Add("Best");
                    best = listToSort[i];
                    //label
                }
                if(listToSort[i].score == best.score)
                {
                    if (listToSort[i].costRank > best.costRank)
                    {
                        listToSort[i].label.Add("Best");
                        best = listToSort[i];
                        for (int j = 0; j < listToSort.Count; i++)
                        {
                            if (!(listToSort[i].Equals(listToSort[j])))
                            {
                                listToSort[j].label.Remove("Best");
                                break;
                            }
                        }
                    }
                }
            }
                return best;
        }

        public List<DataRow> setCostRanking(List<DataRow> listToSort)
        {
            listToSort = sortCost(listToSort);

            for (int i = 1; i < listToSort.Count+1; i++)
            {
                listToSort[i-1].costRank = (listToSort.Count) - (i-1);
            }
            return listToSort;
        }

        public List<DataRow> setDistanceRanking(List<DataRow> listToSort)
        {
            listToSort = sortDistance(listToSort);

            for (int i = 1; i < listToSort.Count + 1; i++)
            {
                listToSort[i - 1].distanceRank = (listToSort.Count) - (i - 1);
            }
            return listToSort;
        }

        public void displayClosest(List<DataRow> listToCheck)
        {
            Console.WriteLine("This is the Closest Entry");
            displayLine(findSmallestDistance(listToCheck));
        }

        public void displayBest(List<DataRow> listToSort)
        {
            Console.WriteLine("This is the Best Entry");
            displayLine(findBest(listToSort));
        //    Console.WriteLine(data[2].cost);

        }

        public void displayCheapest(List<DataRow> listToShow)
        {
            Console.WriteLine("This is the Cheapest Entry");
            displayLine(findCheapest(listToShow));
        }


        private void displayLine(DataRow data)
        {
            Console.Write(data.definition + " - ");
            Console.Write(data.providerID + " - ");
            Console.Write(data.providerName + " - ");
            Console.Write(data.address.Street + " - ");
            Console.Write(data.address.City + " - ");
            Console.Write(data.address.ZipCode + " - ");
            Console.Write(data.address.State + " - ");
            Console.Write(data.reference + " - ");
            Console.Write(data.totalDischarge + " - ");
            Console.Write(data.cost + " - ");
            Console.Write(data.distanceFromUser + "\n");
        }

    }
}
