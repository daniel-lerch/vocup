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
    /// C# port from https://github.com/juliuste/sainte-lague in an object orientated manner.
    /// </remarks>
    public static class SaintLague
    {
        public interface IParty
        {
            double Votes { get; }
            int Seats { get; set; }
        }

        public static int Calculate(IEnumerable<IParty> parties, int seats)
        {
            if (parties == null) throw new ArgumentNullException(nameof(parties));
            if (seats < 0) throw new ArgumentOutOfRangeException(nameof(seats), seats, "The number of seats must not be negative");

            foreach (IParty party in parties)
            {
                if (party == null) throw new ArgumentNullException(nameof(parties), "Elements in parties enumerable must not be null");
                if (party.Votes < 0) throw new ArgumentOutOfRangeException(nameof(parties), "The count of votes must not be negative");
            }

            double sum = parties.Sum(x => x.Votes);
            if (sum == 0 || seats == 0) return 0;
            double divisor = sum / seats;
            double low = divisor * 2d;
            double high = divisor * 0.5;

            int result = CalculateSeats(parties, divisor);

            while (result != seats)
            {
                if (result < seats) low = divisor;
                else high = divisor;
                divisor = (low + high) / 2d;
                result = CalculateSeats(parties, divisor);
            }

            return result;
        }

        private static int CalculateSeats(IEnumerable<IParty> parties, double divisor)
        {
            int result = 0;
            foreach (IParty party in parties)
            {
                result += party.Seats = (int)Math.Round(party.Votes / divisor);
            }
            return result;
        }
    }
}
