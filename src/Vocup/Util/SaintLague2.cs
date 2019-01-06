using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocup.Util
{
    public static class SaintLague2
    {
        public interface IParty
        {
            double Votes { get; }
            int Seats { get; set; }
        }

        private class PartyGroup : IParty
        {
            public double Votes { get; }
            public int Seats { get; set; }
            public List<IParty> Parties { get; }

            public PartyGroup(List<IParty> parties)
            {
                Votes = parties.Sum(x => x.Votes);
                Parties = parties;
            }
        }

        public static int Distribute(IParty[] parties, int seats)
        {
            IReadOnlyList<IParty> grouped = GroupParties(parties);
            do
            {
                grouped = GroupParties(parties);
            } while (true);
           
        }

        private static IReadOnlyList<IParty> GroupParties(IReadOnlyList<IParty> parties)
        {
            List<IParty> groups = new List<IParty>();

            for (int i = 0; i < parties.Count; i++)
            {
                bool found = false;

                for (int j = 0; j < i; j++) // skip this item if there is an existing groups
                {
                    if (parties[i].Votes == parties[j].Votes)
                    {
                        found = true;
                        break;
                    }
                }

                if (found) break;

                PartyGroup group = new PartyGroup(new List<IParty>() { parties[i] });

                for (int j = i + 1; j < parties.Count; j++) // check for duplicates and make groups
                {
                    if (parties[i].Votes == parties[j].Votes)
                    {
                        group.Parties.Add(parties[j]);
                    }
                }

                groups.Add(group);
            }

            return groups;
        }

        public static void InternalDistribute()
        {

        }
    }
}
