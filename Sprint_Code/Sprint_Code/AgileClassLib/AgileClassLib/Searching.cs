using System;
using System.Collections.Generic;
using System.Linq;

namespace AgileCmd
{
    // search from database
    public class Searching
    {   
        // take search term and filter conditions. Returns search result
        public List<DataRow> SearchByCode(String searchTerm, Dictionary<string,double> Filters)  // remove passed in data list 
        {
            List<DataRow> searchResult = new List<DataRow>();
            ServerData server = new ServerData();

            searchResult = server.ReadDatabase(searchTerm, Filters);

            

            return searchResult;
        }

        // sort a list by hospital cost
        public List<DataRow> Sort(List<DataRow> data)
        {
            var sortedList = data.OrderBy(si => si.cost).ToList();
            return sortedList;
        }

    }
}
