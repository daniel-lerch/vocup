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

        public void AddSource(IList<T> data, double representation)
        {
            if (representation <= 0)
                throw new ArgumentOutOfRangeException(nameof(representation));

            sources.Add(new ItemSource(data, representation));
        }

        public List<T> ToList(int count)
        {
            // 1. Round and count items
            int[] result = SaintLague.Calculate(sources.Select(x => x.Representation).ToArray(), count);
            List<T> final = new List<T>();
            Stack<int> remove = new Stack<int>();

            // 2. While one of the sources has not enough items
            bool found;
            do
            {
                found = false;

                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i] > sources[i].Data.Count)
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
                result = SaintLague.Calculate(sources.Select(x => x.Representation).ToArray(), count);

            } while (found);

            // 3. Get necessary count and add to result list
            for (int i = 0; i < result.Length; i++)
            {
                final.AddRange(sources[i].Data.Take(result[i]));
            }

            // 4. Mix result list
            final.Shuffle();

            return final;
        }

        private class ItemSource
        {
            public ItemSource(IList<T> data, double representation)
            {
                Data = data;
                Representation = representation;
            }

            public IList<T> Data { get; }
            public double Representation { get; set; }
        }
    }
}
