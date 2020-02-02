using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Test
{
    [TestClass]
    public class MainTest
    {
        private readonly FormulaParser mParser;

        public MainTest()
        {
            mParser = new FormulaParser();
        }

        [TestMethod]
        public void AdditionOne()
        {
            var result = mParser.Perform("1 + 1");
            Assert.AreEqual(1d + 1d, result);
        }

        [TestMethod]
        public void SubstractionOne()
        {
            var result = mParser.Perform("5 - 1");
            Assert.AreEqual(5d - 1d, result);
        }

        [TestMethod]
        public void MultiplicationOne()
        {
            var result = mParser.Perform("4 * 6");
            Assert.AreEqual(4d * 6d, result);
        }

        [TestMethod]
        public void DivisionOne()
        {
            var result = mParser.Perform("45 / 9");
            Assert.AreEqual(45d / 9d, result);
        }

        [TestMethod]
        public void AdditionTwo()
        {
            var result = mParser.Perform("5 + 5 + 11 + 56");
            Assert.AreEqual(5d + 5d + 11d + 56d, result);
        }

        [TestMethod]
        public void AdditionThree()
        {
            var result = mParser.Perform("5.41 + 11.89 + 56.01");
            Assert.AreEqual(5.41 + 11.89 + 56.01, result);
        }

        [TestMethod]
        public void FormulaOne()
        {
            var result = mParser.Perform("4 + 5 * 11");
            Assert.AreEqual(4d + 5d * 11d, result);
        }

        [TestMethod]
        public void FormulaTwo()
        {
            var result = mParser.Perform("( 4 + 5 ) * 11");
            Assert.AreEqual((4d + 5d) * 11d, result);
        }

        [TestMethod]
        public void FormulaThree()
        {
            var result = mParser.Perform("10 - ( 3 + 2 * ( 6 - 4 ) )");
            Assert.AreEqual(10d - (3d + 2d * (6d - 4d)), result);
        }

        [TestMethod]
        public void FormulaFour()
        {
            var result = mParser.Perform("( 4 / 2 ) - ( 5 * ( 11 - 8 ) )");
            Assert.AreEqual((4d / 2d) - (5d * (11d - 8d)), result);
        }

        [TestMethod]
        public void FormulaFive()
        {
            var result = mParser.Perform("45.5 + 51.7 * 2 - ( 7 % 4 * ( 10 / 4 ) )");
            Assert.AreEqual(45.5 + 51.7 * 2d - (7d % 4d * (10d / 4d)), result);
        }

        [TestMethod]
        public void FormulaSix()
        {
            var result = mParser.Perform("2.2 + 4.4 / 2 - ( 3 + 2 * 3 + 5 / 5 )");
            Assert.AreEqual(2.2 + 4.4 / 2d - (3d + 2d * 3d + 5d / 5d), result);
        }
    }
}
