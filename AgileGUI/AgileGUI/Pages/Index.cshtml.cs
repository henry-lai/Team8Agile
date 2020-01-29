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

        public static string LocChoice = "";
        
        static Dictionary<string, string> temp = new Dictionary<string, string>() { { "Label", "best" },
                { "Description", "desc" },
                { "Name", "hello" },
                { "Address", "the street" },
                { "Zip", "12345" },
                { "Cost", "12345678" },
                { "Distance", "123456" },
                { "Score", "123" }
            };
        public static List<Dictionary<string, string>> Data = new List<Dictionary<string, string>>();


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            
        }

        public void OnGet()
        {

            UserInput = SearchString;

            //runs the input validation method
            Validate validate = new Validate();
            if (validate.validateCode(UserInput))
            {
                UserInput = validate.cleanInput;
                Searching search = new Searching();
                List<DataRow> data = search.SearchByCode(UserInput);

               
                
                foreach (var x in data)
                {
                    //This dictionary represents a row
                    Dictionary<string, string> dict = new Dictionary<string, string>();

                    //populates the row
                    dict.Add("Label", "0");
                    dict.Add("Description", x.definition );
                    dict.Add("Name", x.providerName);
                    dict.Add("Address", x.address.Street);
                    dict.Add("Zip", x.address.ZipCode);
                    dict.Add("Cost", x.cost.ToString());
                    dict.Add("Distance", x.distanceFromUser.ToString());
                    dict.Add("Score", x.score.ToString());
                    
                    //Adds the row to the list
                    Data.Add(dict);
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



        }
    }
}
