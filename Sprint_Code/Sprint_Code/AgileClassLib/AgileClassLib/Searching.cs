using System;
using System.Collections.Generic;
using System.Linq;

namespace AgileCmd
{
    public class Searching
    {   
        public List<DataRow> SearchByCode(String searchTerm)  // remove passed in data list 
        {
            List<DataRow> searchResult = new List<DataRow>();
            ServerData server = new ServerData();

            searchResult = server.ReadDatabase(searchTerm);

            return searchResult;
        }

        public List<DataRow> Sort(List<DataRow> data)
        {
            var sortedList = data.OrderBy(si => si.cost).ToList();
            return sortedList;
        }

    }
}
