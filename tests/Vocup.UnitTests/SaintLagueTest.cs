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
        [DataRow(new double[] { 0.1, 0.0, 0.0 }, 73)]
        [DataRow(new double[] { 345674, 25648393, 4385634, 9483463, 9376390 }, 598)]
        [DataRow(new double[] { 0.3, 0.2, 0.5 }, 0)]
        [DataRow(new double[] { 0.5 }, 5)]
        [DataRow(new double[] { }, 0)]
        public void TestCompose(double[] votes, int seats)
        {
            Party[] parties = votes.Select(x => new Party(x)).ToArray();
            int result = SaintLague.Calculate(parties, seats);
            Assert.AreEqual(seats, result);
            Assert.AreEqual(seats, parties.Sum(x => x.Seats));
        }

        [TestMethod]
        public void TestArgumentNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => SaintLague.Calculate(null, 0));
            Assert.ThrowsException<ArgumentNullException>(() => SaintLague.Calculate(new SaintLague.IParty[] { new Party(0.3), null }, 0));
        }

        [DataTestMethod]
        [DataRow(new double[] { 0.3, 0.1, 0.0 }, -1)]
        [DataRow(new double[] { 3.0, -1.0, 0.0 }, 1)]
        [DataRow(new double[] { 0.3, 0.2, -0.5 }, 0)]
        public void TestArgumentOutOfRange(double[] votes, int seats)
        {
            Party[] parties = votes.Select(x => new Party(x)).ToArray();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => SaintLague.Calculate(parties, seats));
        }

        private class Party : SaintLague.IParty
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
