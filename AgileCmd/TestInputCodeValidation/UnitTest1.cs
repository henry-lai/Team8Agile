using NUnit.Framework;
using TestInputValidation;

namespace TestInputValidation
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Validate test1 = new Validate();
            test1.displayuserMsg();

            //string correctInput = "123";

            //string twoDigitInput = "22";

            //string noInput = " ";

            string charInput = "123$$$%%%";


            //var correctTest = test1.validateCode(correctInput);
            //Assert.IsTrue(correctTest);

            //var twoDigitTest = test1.validateCode(twoDigitInput);
            //Assert.IsFalse(twoDigitTest);

            //var noInputTest = test1.validateCode(noInput);
            //Assert.IsFalse(noInputTest);

            var charInputTest = test1.CleanUserInput(charInput);
            Assert.IsTrue(charInputTest);

        }
    }
}