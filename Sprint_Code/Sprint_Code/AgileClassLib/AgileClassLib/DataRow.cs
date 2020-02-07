using System;
using System.Collections.Generic;
using System.Text;

namespace AgileCmd
{
    // holds hostipital details and location coordinates
    public class DataRow
    {
        public string definition { get; set; }
        public string providerID { get; set; }
        public string providerName { get; set; }
        public string reference { get; set; }
        public long totalDischarge { get; set; }
        public double cost { get; set; }
        public Address address { get; set; }
        public double distanceFromUser { get; set; }
        public int costRank { get; set; }
        public int distanceRank { get; set; }
        public int score { get; set; }
        public List<string> label = new  List<string>();
        private Address add;
        private int discharge;
        private Random random;

        public LatLng cords { get; set; }

        public DataRow(string definition, string providerID, string providerName, Address address, string reference, long totalDischarge, double cost, double distanceFromUser)
        {
            this.cost = cost;
            this.totalDischarge = totalDischarge;
            this.reference = reference;
            this.providerName = providerName;
            this.providerID = providerID;
            this.definition = definition;
            this.address = address;
            this.distanceFromUser = distanceFromUser;

        }

        public DataRow()
        {

        }

        public DataRow(string definition, string providerID, string providerName, Address add, string reference, int discharge, double cost, Random random)
        {
            this.definition = definition;
            this.providerID = providerID;
            this.providerName = providerName;
            this.add = add;
            this.reference = reference;
            this.discharge = discharge;
            this.cost = cost;
            this.random = random;
        }

        // set appropiate labels for the hospital
        public string CombineLabel() {
            string retVal = "";
            foreach (string x in label) {
                retVal += x;
                // not finished yet
            }
            return retVal;
        }

        public void SetCords(string c) {
            cords = new LatLng();
            string[] Vals = c.Split(':');
            cords.Latitude = Convert.ToDouble(Vals[0]);
            cords.Longitude = Convert.ToDouble(Vals[1]);

        }

    }
}
