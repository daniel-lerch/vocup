using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocup.Util
{
    /// <summary>
    /// This class implements the Sainte-Laguë/Webster method for parliamentary seat composition.
    /// </summary>
    /// <remarks>
    /// C# port from https://github.com/juliuste/sainte-lague (slightly modified)
    /// </remarks>
    public static class SaintLague
    {
        public static int[] Calculate(double[] votes, int seats)
        {
            int[] results = new int[votes.Length];

            double voteSum = CalculateSum(votes);
            double divisor = voteSum / seats;
            double low = divisor * 2d;
            double high = divisor * 0.5;

            int result = CalculateSeats(votes, divisor, results);

            while (result != seats)
            {
                if (result < seats) low = divisor;
                else high = divisor;
                divisor = (low + high) / 2d;
                result = CalculateSeats(votes, divisor, results);
            }

            return results;
        }

        private static int CalculateSeats(double[] votes, double divisor, int[] results)
        {
            int result = 0;
            for (int i = 0; i < votes.Length; i++)
            {
                int rounded = (int)Math.Round(votes[i] / divisor);
                results[i] = rounded;
                result += rounded;
            }
            return result;
        }

        private static double CalculateSum(double[] votes)
        {
            double result = 0;
            foreach (double item in votes)
            {
                result += item;
            }
            return result;
        }
    }

    public static class SaintLague2
    {
        public interface IParty
        {
            double Votes { get; }
            int Seats { get; set; }
        }

        public static void Calculate(List<IParty> parties, int seats)
        {
            if (parties == null) throw new ArgumentNullException(nameof(parties));
            if (seats < 0) throw new ArgumentOutOfRangeException(nameof(seats), seats, "The number of seats must not be negative");
            if (parties.Any(x => x.Votes < 0)) throw new ArgumentOutOfRangeException(nameof(parties), "The count of votes must not be negative");
            double sum = parties.Sum(x => x.Votes);
            if (sum == 0 || seats == 0) return;
        }
    }
}
