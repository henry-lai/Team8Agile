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
            //map.setCoordinate();
            String expected = BingMap.Answer;

            StringAssert.Contains("56.46126937866211 : -2.967600107192993", expected);
        }
    }
}