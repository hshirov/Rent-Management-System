using NUnit.Framework;
using Services.Common;

namespace UnitTests.Services.Tests.Common.Tests
{
    [TestFixture]
    public class StringManipulationTests
    {
        [Test]
        public void NormalizeName_Changes_Letters()
        {
            string input = "joHn";
            string exprectedResult = "John";

            string result = StringManipulation.NormalizeName(input);

            Assert.That(result == exprectedResult);
        }
    }
}
