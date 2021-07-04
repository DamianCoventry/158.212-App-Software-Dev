using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RugbyModel.Test
{
    [TestClass]
    public class DateDiffAsYearsTest
    {
        [TestMethod]
        public void WorksCorrectlyWhenDiffGreaterThan100Years()
        {
            Assert.AreEqual(150, Utility.DateDiffAsYears(new DateTime(2000, 6, 20), new DateTime(2150, 8, 10)));
        }

        [TestMethod]
        public void WorksCorrectlyWhenDiffGreaterThan10Years()
        {
            Assert.AreEqual(12, Utility.DateDiffAsYears(new DateTime(2000, 2, 20), new DateTime(2012, 9, 10)));
        }

        [TestMethod]
        public void WorksCorrectlyWhenDiffIs13Months()
        {
            Assert.AreEqual(1, Utility.DateDiffAsYears(new DateTime(2000, 7, 20), new DateTime(2001, 8, 20)));
        }

        [TestMethod]
        public void WorksCorrectlyWhenDiffIs12Months()
        {
            Assert.AreEqual(1, Utility.DateDiffAsYears(new DateTime(2000, 6, 20), new DateTime(2001, 6, 20)));
        }

        [TestMethod]
        public void WorksCorrectlyWhenDiffIs11Months()
        {
            Assert.AreEqual(0, Utility.DateDiffAsYears(new DateTime(2000, 2, 20), new DateTime(2000, 11, 20)));
        }

        [TestMethod]
        public void WorksCorrectlyWhenDiffIs32Days()
        {
            Assert.AreEqual(0, Utility.DateDiffAsYears(new DateTime(2203, 7, 1), new DateTime(2203, 8, 2)));
        }

        [TestMethod]
        public void WorksCorrectlyWhenDiffIs31Days()
        {
            Assert.AreEqual(0, Utility.DateDiffAsYears(new DateTime(2001, 7, 1), new DateTime(2001, 8, 1)));
        }

        [TestMethod]
        public void WorksCorrectlyWhenDiffIs30Days()
        {
            Assert.AreEqual(0, Utility.DateDiffAsYears(new DateTime(2001, 7, 1), new DateTime(2001, 7, 31)));
        }

        [TestMethod]
        public void WorksCorrectlyWhenDiffIs1Day()
        {
            Assert.AreEqual(0, Utility.DateDiffAsYears(new DateTime(2001, 7, 1), new DateTime(2001, 7, 2)));
        }

        [TestMethod]
        public void WorksCorrectlyWhenDiffIs0Days()
        {
            Assert.AreEqual(0, Utility.DateDiffAsYears(new DateTime(2000, 7, 1), new DateTime(2000, 7, 1)));
        }

        [TestMethod]
        public void WorksCorrectlyWhenDiffIsMinus1Day()
        {
            Assert.AreEqual(0, Utility.DateDiffAsYears(new DateTime(2000, 7, 2), new DateTime(2001, 7, 1)));
        }

        [TestMethod]
        public void WorksCorrectlyWhenDiffIsMinus1Month()
        {
            Assert.AreEqual(0, Utility.DateDiffAsYears(new DateTime(2000, 8, 1), new DateTime(2001, 7, 1)));
        }

        [TestMethod]
        public void WorksCorrectlyWhenDiffIsMinus1Year()
        {
            Assert.AreEqual(-1, Utility.DateDiffAsYears(new DateTime(2001, 6, 2), new DateTime(2000, 7, 1)));
        }

        [TestMethod]
        public void WorksCorrectlyWhenDiffIsMinus5Years()
        {
            Assert.AreEqual(-5, Utility.DateDiffAsYears(new DateTime(2005, 7, 1), new DateTime(2000, 7, 1)));
        }
    }
}
