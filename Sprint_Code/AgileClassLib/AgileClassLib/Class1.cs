using BingMapsRESTToolkit;
using System;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace LocationsAndRouting
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Create a request.
            var request = new GeocodeRequest()
            {
                Query = "perth road",
                IncludeIso2 = true,
                IncludeNeighborhood = true,
                MaxResults = 25,
                BingMapsKey = "AhewTfDZhjGWEe05u_jLLaohk57F6NYQPLI52p4-jaVEniGZOqIZ-XVaigQLsvoF"
            };

            //Process the request by using the ServiceManager.
            var response = await ServiceManager.GetResponseAsync(request);

            if (response != null &&
                response.ResourceSets != null &&
                response.ResourceSets.Length > 0 &&
                response.ResourceSets[0].Resources != null &&
                response.ResourceSets[0].Resources.Length > 0)
            {
                var result = response.ResourceSets[0].Resources[0] as BingMapsRESTToolkit.Location;

                //Do something with the result.

                Console.WriteLine(result.Point.Coordinates[0] + " : " + result.Point.Coordinates[1]);
            }

        }
    }
}