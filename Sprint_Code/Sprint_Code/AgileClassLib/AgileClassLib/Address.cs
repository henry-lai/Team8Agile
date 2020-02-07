using System;
using System.Collections.Generic;
using System.Text;

namespace AgileCmd
{
    public class Address
    {
        private string street;
        private string city;
        private string state;
        private string zipCode;

        public Address(string street, string city, string state, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public string Street { get => street; set => street = value; }
        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
        public string ZipCode { get => zipCode; set => zipCode = value; }

        // return full address
        public string DisplayAddress() {
            return Street + ", " + city + ", " + state + ", " + zipCode;
        }
    }
}
