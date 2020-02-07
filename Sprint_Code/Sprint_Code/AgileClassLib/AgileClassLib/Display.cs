using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileCmd
{
    public class Display
    {
        // check the list is empty or not
        public bool validateList(List<DataRow> listToValidate)
        {
            if(listToValidate.Count < 1)
            {
                return false;
            }
            return true;
        }

        // display the full list
        public void displayFullList(List<DataRow> listToDisplay)
        {
            //populate display list with data given from search
            Console.WriteLine("Definition - Provider ID - Provider Name - Street - City - State - Zip Code - Reference - Total Discharge - Cost - Distance From User");
            for (int i = 0; i < listToDisplay.Count; i++)
            {
                displayLine(listToDisplay[i]);
            }

        }

        // find the cheapest hospital
        public DataRow findCheapest(List<DataRow> listToCheck)
        {
            DataRow min = new DataRow();
            min.cost = 100000000000;  // set very big value so that we can find the smallest value
            for (int i = 0; i < listToCheck.Count; i++)
            {
                if(listToCheck[i].cost < min.cost)
                {
                    min = listToCheck[i];
                }
            }
            min.label.Add("Cheapest"); // set the Cheapest label on the hospital
            return min;
        }

        // find the closest hospital from the user
        public DataRow findSmallestDistance(List<DataRow> listToCheck)
        {
            DataRow min = new DataRow();
            min.distanceFromUser = 1000000000000;  // set very big value so that we can find the smallest value
            for (int i = 0; i < listToCheck.Count; i++)
            {
                if (listToCheck[i].distanceFromUser < min.distanceFromUser)
                {
                    min = listToCheck[i];
                }

            }
            min.label.Add("Closest"); // set the Closest label on the hospital
            return min;
        }

        // sort hospital by cost and return the sorted list
        public List<DataRow> sortCost(List<DataRow> listToSort)
        {
            IEnumerable<DataRow> query = listToSort.OrderBy(DataRow => DataRow.cost);

            foreach(DataRow datarow in query)
            {
                Console.WriteLine("{0} - {1}", datarow.cost, datarow.distanceFromUser);
            }

            listToSort = query.ToList<DataRow>();
            return listToSort;

        }

        // sort hospital by distance and return the sorted list
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

        // sort hospital in a rank according to distance and cost and return the sorted list
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

        // set score for each hospital
        public List<DataRow> setScore(List<DataRow> listToSort) {
            listToSort = setDistanceRanking(listToSort);
            listToSort = setCostRanking(listToSort);
            for (int i = 0; i < listToSort.Count; i++)
            {
                listToSort[i].score = listToSort[i].costRank * listToSort[i].distanceRank;
            }
            return listToSort;
        }

        // find the best hospital, based on the score
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
                }
                if(listToSort[i].score == best.score)
                {
                    if (listToSort[i].costRank > best.costRank)
                    {
                        best = listToSort[i];
                    }
                }
            }
            best.label.Add("Best"); // set the Best label for the hospital
            return best;
        }

        // ranking hospital according to the cost and return the sorted list
        public List<DataRow> setCostRanking(List<DataRow> listToSort)
        {
            listToSort = sortCost(listToSort);

            for (int i = 1; i < listToSort.Count+1; i++)
            {
                listToSort[i-1].costRank = (listToSort.Count) - (i-1);
            }
            return listToSort;
        }

        // ranking hospital according to the distance and return the sorted list
        public List<DataRow> setDistanceRanking(List<DataRow> listToSort)
        {
            listToSort = sortDistance(listToSort);

            for (int i = 1; i < listToSort.Count + 1; i++)
            {
                listToSort[i - 1].distanceRank = (listToSort.Count) - (i - 1);
            }
            return listToSort;
        }

        // display the closest hospital
        public void displayClosest(List<DataRow> listToCheck)
        {
            Console.WriteLine("This is the Closest Entry");
            displayLine(findSmallestDistance(listToCheck));
        }

        // display the best hospital
        public void displayBest(List<DataRow> listToSort)
        {
            Console.WriteLine("This is the Best Entry");
            displayLine(findBest(listToSort));
        }

        // display the cheapest hospital
        public void displayCheapest(List<DataRow> listToShow)
        {
            Console.WriteLine("This is the Cheapest Entry");
            displayLine(findCheapest(listToShow));
        }

        // testing display on the cmd version of the program
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
