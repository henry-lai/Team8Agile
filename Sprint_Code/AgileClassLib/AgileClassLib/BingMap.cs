using BingMapsRESTToolkit;
using System;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace LocationsAndRouting
{
    public class BingMap
    {
        private static String answer;
        String lng, lat;

        public static string Answer { get => answer; set => answer = value; }
        public string Lng { get => lng; set => lng = value; }
        public string Lat { get => lat; set => lat = value; }

        public static async System.Threading.Tasks.Task mapInit(String query)
        {
            //Console.WriteLine("Hello World!");

            //Create a request.
            var request = new GeocodeRequest()
            {
                Query = query,
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
                /*String lng = result.Point.Coordinates[0].ToString();
                String lat = result.Point.Coordinates[1].ToString();*/

                //Console.WriteLine(lng, lat);

                //Do something with the result.

                Answer = result.Point.Coordinates[0] + " : " + result.Point.Coordinates[1];
            }

        }

        

        public void Main(String[] args)
        {

        }
    }
}