using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vocup.Util;

namespace Vocup.UnitTests
{
    [TestClass]
    public class SaintLagueTest
    {
        [DataTestMethod]
        [DataRow(new double[] { 0.3, 0.1, 0.0 }, 20)]
        [DataRow(new double[] { 3.0, 0.0, 0.0 }, 1)]
        [DataRow(new double[] { 0.3, 0.2, 0.5 }, 0)]
        public void TestCompose(double[] votes, int seats)
        {
            //Party[] parties = votes.Select(x => new Party(x)).ToArray();
            int[] result = SaintLague.Calculate(votes, seats);
            Assert.AreEqual(seats, result.Sum());
        }

        [TestMethod]
        public void TestArgumentNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => SaintLague.Calculate(null, 0));
        }

        [DataTestMethod]
        [DataRow(new double[] { 0.3, 0.1, 0.0 }, -1)]
        [DataRow(new double[] { 3.0, -1.0, 0.0 }, 1)]
        [DataRow(new double[] { 0.3, 0.2, -0.5 }, 0)]
        public void TestArgumentOutOfRange(double[] votes, int seats)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => SaintLague.Calculate(votes, seats));
        }

        private class Party : SaintLague2.IParty
        {
            public Party(double votes)
            {
                Votes = votes;
            }

            public double Votes { get; set; }
            public int Seats { get; set; }
        }
    }
}
