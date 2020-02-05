using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileCmd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AgileGUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;

        [BindProperty(SupportsGet = true)]
        public string UseCurrLocation { get; set; }

        [BindProperty(SupportsGet = true)]
        public string LocationString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CostFrom { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CostTo { get; set; }

        [BindProperty(SupportsGet = true)]
        public string DistanceAway { get; set; }

        public static string UserInput = "";

        public static string UseCurrLocation = "";

        //public static string LocChoice = "";

        public static List<Dictionary<string, string>> Data; //= new List<Dictionary<string, string>>();
        public static List<DataRow> RankedResults;  //= new List<DataRow>();

        public static bool DataFound = true;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            
        }

        public void OnGet()
        {

            UserInput = SearchString;

            if (UserInput != null)
            {
                Data = new List<Dictionary<string, string>>();
                RankedResults  = new List<DataRow>();
                //runs the input validation method
                Validate validate = new Validate();
                if (validate.validateCode(UserInput))
                {
                    UserInput = validate.cleanInput;
                    Searching search = new Searching();
                    List<DataRow> data = search.SearchByCode(UserInput);

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
            // if userInput not empty then

                // call
                // validate
                // search db
                // create dataRow results
                // calculate distance
                // calculate best
                // calc closest
                // calc cheapest
                // display results
            UseCurrLocation = UserLocation;
            if(UseCurrLocation.Length == 0)
            {
                Console.WriteLine("Please enter location");
            }
            else
            {
                BingMap.mapInit(UseCurrLocation);
            }



        }
    }
}
