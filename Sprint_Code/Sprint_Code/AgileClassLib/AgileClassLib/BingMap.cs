using BingMapsRESTToolkit;
using System;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace LocationsAndRouting
{

    // handle bing map functions and requests
    public class BingMap
    {
        private static string answer;
        public static double lng, lat;
        private static string currentLoc;

        public static string Answer { get => answer; set => answer = value; }
        public static double Lng { get => lng; set => lng = value; }
        public static double Lat { get => lat; set => lat = value; }
        public static string CurrentLoc { get => currentLoc; set => currentLoc = value; }

        // search for coordinates using given query
        public static async System.Threading.Tasks.Task<string> mapInit(String query)
        {
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
                Answer = result.Point.Coordinates[0] + " : " + result.Point.Coordinates[1];
                lat = result.Point.Coordinates[0];
                lng = result.Point.Coordinates[1];
            }
            return Answer;

        }
        


        // Calculate distance between two coordinates
        public double HaversineDistance(LatLng pos1, LatLng pos2)
        {
            double R = 3960; // used to calculate the distance in miles
            double lat = ToRadians((pos2.Latitude - pos1.Latitude));
            double lng = ToRadians((pos2.Longitude - pos1.Longitude));
            double h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(ToRadians(pos1.Latitude)) * Math.Cos(ToRadians(pos2.Latitude)) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            double h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));

            double distance = Math.Round(R * h2);

            return distance;
        }



        // convert degree to radian
        public static double ToRadians(double val)
        {
            return (Math.PI / 180) * val;
        }



    }
}




/// <summary>
/// Specifies a Latitude / Longitude point.
/// </summary>
public class LatLng
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public LatLng()
    {
    }

    public LatLng(double lat, double lng)
    {
        this.Latitude = lat;
        this.Longitude = lng;
    }
}





