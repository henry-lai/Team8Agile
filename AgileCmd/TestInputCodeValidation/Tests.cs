using AgileCmd;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestInputCodeValidation
{
    [TestFixture]
    public class Tests
    {
        ServerData sd;
        List<DataRow> data;
        Searching s;

        [SetUp]
        public void Setup()
        {
            sd = new ServerData();
            data = sd.data;
            s = new Searching();
        }

        [Test]
        public void ConnectionTest()
        {
            CollectionAssert.IsNotEmpty(data);
        }

        [Test]
        public void SearchByCodeTest()
        {

            //Searching s = new Searching();
            /*Address ad = new Address("17 Hawkhill", "Dundee", "Angus", "DD15DL");
            DataRow dr = new DataRow("123 - Surgery", "001235", "Bill", ad, "JJA", 112536, 233.55);
            DataRow dr1 = new DataRow("556 - Opp", "001235", "Bill", ad, "JJA", 112536, 233.55);
            DataRow dr2 = new DataRow("176 - IIDA HEart", "001235", "Bill", ad, "JJA", 112536, 233.55);*/

            //var dataSet = new List<DataRow>();
            //var dataSet2 = new List<DataRow>();
            //dataSet2.Add(dr);
            //dataSet2.Add(dr1);
            /*dataSet2.Add(dr2);
            dataSet.Add(dr);
            dataSet.Add(dr1);
            dataSet.Add(dr2);*/
            List<DataRow> t = s.SearchByCode("176");
            //CollectionAssert.AreEqual(t, dataSet2);
            CollectionAssert.IsNotEmpty(t);
        }

        [Test]
        public void SortTest()
        {
            Searching s = new Searching();
            Address ad = new Address("17 Hawkhill", "Dundee", "Angus", "DD15DL");
            DataRow dr = new DataRow("123 - Surgery", "001235", "Bill", ad, "JJA", 112536, 100.55,0);
            DataRow dr1 = new DataRow("556 - Opp", "001235", "Bill", ad, "JJA", 112536, 233.55,0);
            DataRow dr2 = new DataRow("567 - IIDA HEart", "001235", "Bill", ad, "JJA", 112536, 333.55,0);

            var dataSet = new List<DataRow>();
            dataSet.Add(dr2);
            dataSet.Add(dr);
            dataSet.Add(dr1);

            var expected = new List<DataRow>();
            expected.Add(dr);
            expected.Add(dr1);
            expected.Add(dr2);

            List<DataRow> t = s.Sort(dataSet);

            CollectionAssert.AreEqual(t, expected);
        }

    }
}
