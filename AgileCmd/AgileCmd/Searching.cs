using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AgileCmd
{

    public class Searching
    {
  
        /* alter so that it just passes the search string to the stored proc
         *db reader code exe's stored proc which returns search results
         * */
        public List<DataRow> SearchByCode(String searchTerm, List<DataRow> data)  // remove passed in data list 
        {
            Console.WriteLine(data.Count);
            List<DataRow> searchResult = new List<DataRow>();
            foreach (var x in data)
            {
                if (Regex.IsMatch(x.Definition, searchTerm, RegexOptions.IgnoreCase))
                {
                    searchResult.Add(x);
                }
            }
            return searchResult;
        }

        public List<DataRow> Sort(List<DataRow> data)
        {
            var sortedList = data.OrderBy(si => si.Cost).ToList();
            return sortedList;
        }

    }


}
