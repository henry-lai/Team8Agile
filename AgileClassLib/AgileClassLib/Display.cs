using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileCmd
{
    public class Display
    {
      
       public void populateData(List<DataRow> newList) {


            //listReceived = Search();

            List<DataRow> tempList = new List<DataRow>();
            tempList = newList;
            if (validateList(newList) == true)
            {
            //    Console.WriteLine("Recieved Valid List");
            }
            else if(validateList(newList) == false)
            {
                newList = tempList;
              //  Console.WriteLine("Recieved InValid List");
            }
        }
        

        public bool validateList(List<DataRow> listToValidate)
        {


            if(listToValidate.Count < 1)
            {
                return false;
              //  Console.WriteLine("Empty List");
            }

         
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
                 //   listToCheck[i].
                    min = listToCheck[i];
                  
                }
            }
            min.label.Add("Cheapest");
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
                   
                    min = listToCheck[i];
                   
                }

            }
            min.label.Add("Closest");
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
                    
                    best = listToSort[i];
                    //label
                }
                if(listToSort[i].score == best.score)
                {
                    if (listToSort[i].costRank > best.costRank)
                    {
                       
                        best = listToSort[i];
                       
                    }
                }
            }
            best.label.Add("Best");
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
