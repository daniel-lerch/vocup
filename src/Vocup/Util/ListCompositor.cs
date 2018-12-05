using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocup.Util
{
    public class ListCompositor<T>
    {
        private List<ItemSource> sources;

        public ListCompositor()
        {
            sources = new List<ItemSource>();
        }

        public void AddSource(IList<T> data, double votes)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (votes < 0)
                throw new ArgumentOutOfRangeException(nameof(votes), votes, "Must not be negative");

            sources.Add(new ItemSource(data, votes));
        }

        public List<T> ToList(int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count), count, "Must not be negative");
            int dataCount = sources.Sum(x => x.Data.Count);
            if (count > dataCount) throw new ArgumentOutOfRangeException(nameof(count), count, $"There are only {dataCount} elements available");
            
            // 1. Round and count items
            int result = SaintLague.Calculate(sources, count);
            List<T> final = new List<T>();
            Stack<int> remove = new Stack<int>();

            // 2. While one of the sources has not enough items
            bool found;
            do
            {
                found = false;

                for (int i = 0; i < sources.Count; i++)
                {
                    if (sources[i].Seats > sources[i].Data.Count)
                    {
                        ItemSource source = sources[i];
                        final.AddRange(source.Data); // add all items to final list
                        remove.Push(i);
                        count -= source.Data.Count; // substract added items from count
                        found = true;
                    }
                }

                while (remove.Count > 0)
                    sources.RemoveAt(remove.Pop()); // remove source after iteration

                // Round again and see if the list fits this time
                result = SaintLague.Calculate(sources, count);

            } while (found);

            // 3. Get necessary count and add to result list
            foreach (ItemSource source in sources)
            {
                final.AddRange(source.Data.Take(source.Seats));
            }

            // 4. Mix result list
            final.Shuffle();

            return final;
        }

        private class ItemSource : SaintLague.IParty
        {
            public ItemSource(IList<T> data, double votes)
            {
                Data = data;
                Votes = votes;
            }

            public IList<T> Data { get; }
            public double Votes { get; set; }
            public int Seats { get; set; }
        }
    }
}
