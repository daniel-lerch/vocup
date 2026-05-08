using System;
using System.Linq;
using Vocup.Util;
using Xunit;

namespace Vocup.UnitTests
{
    public class SaintLagueTest
    {
        [Theory]
        [InlineData(new double[] { 0.3, 0.1, 0.0 }, 20)]
        [InlineData(new double[] { 0.1, 0.0, 0.0 }, 73)]
        [InlineData(new double[] { 345674, 25648393, 4385634, 9483463, 9376390 }, 598)]
        [InlineData(new double[] { 0.3, 0.2, 0.5 }, 0)]
        [InlineData(new double[] { 0.5 }, 5)]
        [InlineData(new double[] { }, 0)]
        [InlineData(new double[] { 6, 3, 3 }, 3)]
        [InlineData(new double[] { 1, 1, 1 }, 2)]
        public void TestCompose(double[] votes, int seats)
        {
            Party[] parties = votes.Select(x => new Party(x)).ToArray();
            int result = SaintLague.Calculate(parties, seats);
            Assert.Equal(seats, result);
            Assert.Equal(seats, parties.Sum(x => x.Seats));
        }

        [Fact]
        public void TestArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => SaintLague.Calculate(null!, 0));
            Assert.Throws<ArgumentNullException>(() => SaintLague.Calculate(new SaintLague.IParty[] { new Party(0.3), null! }, 0));
        }

        [Theory]
        [InlineData(new double[] { 0.3, 0.1, 0.0 }, -1)]
        [InlineData(new double[] { 3.0, -1.0, 0.0 }, 1)]
        [InlineData(new double[] { 0.3, 0.2, -0.5 }, 0)]
        public void TestArgumentOutOfRange(double[] votes, int seats)
        {
            Party[] parties = votes.Select(x => new Party(x)).ToArray();
            Assert.Throws<ArgumentOutOfRangeException>(() => SaintLague.Calculate(parties, seats));
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
