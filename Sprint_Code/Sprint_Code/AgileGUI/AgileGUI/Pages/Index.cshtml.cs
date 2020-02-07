using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileCmd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using LocationsAndRouting;

namespace AgileGUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchStringDesc { get; set; }

        // Requires using Microsoft.AspNetCore.Mvc.Rendering;

        [BindProperty(SupportsGet = true)]
        public string UserLocation { get; set; }

        [BindProperty(SupportsGet = true)]
        public string UseCurrLocation { get; set; }

        [BindProperty(SupportsGet = true)]
        public string LocationString { get; set; }


        [BindProperty(SupportsGet = true)]
        public string CostTo { get; set; }

        [BindProperty(SupportsGet = true)]
        public string DistanceAway { get; set; }

        public static string UserInput = "";
        public static double MaxCost { get; set; }
        public static double MaxDistance { get; set; }


        public static string useCurrLocation = "";



        public static bool TwoBoxes = false;
        public static bool ValidEntry = false;

        //public static string LocChoice = "";

        public static List<Dictionary<string, string>> Data; //= new List<Dictionary<string, string>>();
        public static List<DataRow> RankedResults;  //= new List<DataRow>();

        public static bool DataFound = true;

        public Dictionary<string, double> Filters { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

        }

        public async Task OnGet()
        {
            Task<string> UserCords;
            UserInput = null;
            Filters = new Dictionary<string, double>();
            UseCurrLocation = UserLocation;

            if (CostTo != null)
            {
                MaxCost = Convert.ToDouble(CostTo);
            }
            else
            {
                MaxCost = Double.MaxValue;
            }
            Filters.Add("MaxCost", MaxCost);

            if (DistanceAway != null)
            {
                MaxDistance = Convert.ToDouble(DistanceAway);
            }
            else 
            {
                MaxDistance = Double.MaxValue;
            }
            Filters.Add("MaxDistance", MaxDistance);

            if ((SearchString != null) && (SearchStringDesc != null))
            {
                TwoBoxes = true;
            }
            if ((SearchString == null) && ValidateEntry(SearchStringDesc, "DESC"))
            {

                UserInput = SearchStringDesc;
                TwoBoxes = false;
            }
            else if ((SearchStringDesc == null) && ValidateEntry(SearchString, "CODE"))
            {

                UserInput = SearchString;
                TwoBoxes = false;
            }

            if ((UserInput != null) && (!TwoBoxes))
            {
                
                Data = new List<Dictionary<string, string>>();
                RankedResults = new List<DataRow>();
                //runs the input validation method
                Validate validate = new Validate();
                if (validate.validateCode(UserInput))
                {
                    UserInput = validate.cleanInput;
                    Searching search = new Searching();

                    if (UseCurrLocation.Length == 0)
                    {
                        // Console.WriteLine("Please enter location");
                        // change to put message on screeen and don't search

                        UserCords = BingMap.mapInit("Glasgow");
                        await UserCords;

                    }
                    else
                    {
                        UserCords  = BingMap.mapInit(UseCurrLocation);
                        await UserCords;
                    }
                    DataRow y = new DataRow();
                    y.SetCords(UserCords.Result);

                    List<DataRow> data = search.SearchByCode(UserInput, Filters);
                    List<DataRow> ToBeRemoved = new List<DataRow>();
                    foreach (DataRow dt in data)
                    {
                        Task<string> cords = BingMap.mapInit(dt.address.Street + ", " + dt.address.City + "," + dt.address.State);
                        await cords;
                        dt.SetCords(cords.Result);

                        dt.distanceFromUser = new BingMap().HaversineDistance(y.cords, dt.cords);

                        if (dt.distanceFromUser > Filters["MaxDistance"]) { ToBeRemoved.Add(dt); }

                    }

                    foreach (DataRow dt in ToBeRemoved) {
                        data.Remove(dt);
                    }

                    if (data.Count == 0)
                    {
                        //error message here
                        DataFound = false;

                    }
                    else
                    {

                        DataFound = true;

                        Display dis = new Display();
                        // find cheap
                        // find closest
                        // find best 

                        // filter data based on filters on GUI


                        RankedResults.Add(dis.findCheapest(data));
                        RankedResults.Add(dis.findSmallestDistance(data));
                        dis.setScore(data);
                        RankedResults.Add(dis.findBest(data));

                        foreach (var x in data)
                        {
                            //This dictionary represents a row
                            Dictionary<string, string> dict = new Dictionary<string, string>();

                            //populates the row
                            dict.Add("Label", x.CombineLabel());
                            dict.Add("Description", x.definition);
                            dict.Add("Name", x.providerName);
                            dict.Add("Address", x.address.Street);
                            dict.Add("Zip", x.address.ZipCode);
                            dict.Add("State", x.address.State);
                            dict.Add("City", x.address.City);
                            dict.Add("Cost", x.cost.ToString());
                            dict.Add("Distance", x.distanceFromUser.ToString());
                            dict.Add("Score", x.score.ToString());

                            //Adds the row to the list
                            Data.Add(dict);
                        }
                    }

                }

                
            }
            else
            {
                UserInput = null;
            }

        }

        bool ValidateEntry(string entry, string EntryType)
        {
            bool result = true;
            if (entry != null)
            {

                switch (EntryType)
                {
                    case "CODE":
                        int i = 0;

                        result = int.TryParse(entry, out i);
                        break;
                    case "DESC":

                        result = !entry.Any(char.IsDigit);

                        break;

                    default:
                        // error msg
                        break;
                }
            }
            ValidEntry = result;
            return result;

        }

    }
}
