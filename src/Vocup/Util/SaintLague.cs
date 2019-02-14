using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

            for (int i = 1; ; i++)
            {
                int result = CalculateSeats(parties, divisor);
                if (result == seats)
                    return result;
                if (i >= 32) // Stop after 32 iterations if we still have no result
                    break; // Prevent further changes as we won't check their result 

                if (result < seats)
                    low = divisor;
                else
                    high = divisor;
                divisor = (low + high) / 2d;
            }

            // This code will randomly assign the remaining seats which can't be calculated due to arithmetic conflicts.
            List<IParty> conflicts = new List<IParty>(parties.Where(p => GetSeats(p, low) != GetSeats(p, high)));
            conflicts.Shuffle();

            int baseSeats = CalculateSeats(parties, low);
            if (baseSeats > seats || baseSeats + conflicts.Count < seats)
                throw new NotSupportedException($"Mathematically distributed {baseSeats} of {seats} seats with {conflicts.Count} conflicts." +
                    $"{Environment.NewLine}The number of distributed seats must not be higher than the expected result" +
                    $"and the number of conflicts has to be higher than the number of missing distributions.");

            for (int i = 0; i < seats - baseSeats; i++)
            {
                conflicts[i].Seats++;
            }

            return seats;
        }

        private static int CalculateSeats(IEnumerable<IParty> parties, double divisor)
        {
            int result = 0;
            foreach (IParty party in parties)
            {
                result += party.Seats = GetSeats(party, divisor);
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetSeats(IParty party, double divisor)
        {
            return (int)Math.Round(party.Votes / divisor);
        }
    }
}
