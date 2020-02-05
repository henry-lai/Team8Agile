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

        [Test]
        public async System.Threading.Tasks.Task mapInitVoidTestAsync()
        {
            //await BingMap.mapInit("");

            Assert.That((Assert.ThrowsAsync<Exception>(async () => await BingMap.mapInit(""))).Message, Is.EqualTo("No Query or Address value specified."));
        }

        [Test]
        public async System.Threading.Tasks.Task mapInitMisspellTestAsync()
        {
            await BingMap.mapInit("Dunde");
            String expected = BingMap.Answer;

            StringAssert.Contains("56.46126937866211 : -2.967600107192993", expected);
        }
    }
}