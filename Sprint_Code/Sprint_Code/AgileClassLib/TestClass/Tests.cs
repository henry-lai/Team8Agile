using NUnit.Framework;
using LocationsAndRouting;
using System;

namespace TestClass
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async System.Threading.Tasks.Task mapInitTestAsync()
        {
            BingMap map = new BingMap();
            await BingMap.mapInit("Dundee");
            String expected = BingMap.Answer;

            StringAssert.Contains("56.46126937866211 : -2.967600107192993", expected);
        }

        [Test]
        public async System.Threading.Tasks.Task mapInitVoidTestAsync()
        {
            Assert.That((Assert.ThrowsAsync<Exception>(async () => await BingMap.mapInit(""))).Message, Is.EqualTo("No Query or Address value specified."));
        }

        [Test]
        public async System.Threading.Tasks.Task mapInitMisspellTestAsync()
        {
            await BingMap.mapInit("Dunde");
            String expected = BingMap.Answer;

            StringAssert.Contains("56.46126937866211 : -2.967600107192993", expected);
        }

        [Test]
        public void HaversineDistanceTest()
        {
            BingMap m = new BingMap();
            //Dundee 56.46913, -2.97489
            //Glasgow 55.86515, -4.25763
            LatLng dundee = new LatLng(56.46913, -2.97489);
            LatLng glasgow = new LatLng(55.86515, -4.25763);


            Assert.That(m.HaversineDistance(dundee, glasgow), Is.EqualTo(64).Within(5.0));
        }
    }
}