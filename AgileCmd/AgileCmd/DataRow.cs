using System;
using System.Collections.Generic;
using System.Text;

namespace AgileCmd
{
    public class DataRow
    {
        private string definition;
        private string providerID;
        private string providerName;
        private string reference;
        private long totalDischarge;
        private double cost;
        private Address address;

        public DataRow(string definition, string providerID, string providerName, Address address, string reference, long totalDischarge, double cost)
        {
            Cost = cost;
            TotalDischarge = totalDischarge;
            Reference = reference;
            ProviderName = providerName;
            ProviderID = providerID;
            Definition = definition;
            Address = address;
        }

        public double Cost { get => cost; set => cost = value; }
        public long TotalDischarge { get => totalDischarge; set => totalDischarge = value; }
        public string Reference { get => reference; set => reference = value; }
        public string ProviderName { get => providerName; set => providerName = value; }
        public string ProviderID { get => providerID; set => providerID = value; }
        public string Definition { get => definition; set => definition = value; }
        internal Address Address { get => address; set => address = value; }
    }
}
