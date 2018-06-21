using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocup.Util
{
    public class RewriteHelper<TSrc, TDest>
    {
        private readonly TDest[] destinationItems;
        private IList<TSrc> sourceItems;

        public RewriteHelper(params TDest[] destinationItems)
        {
            this.destinationItems = destinationItems;
            sourceItems = new List<TSrc>();
            SourceItems = new ReadOnlyCollection<TSrc>(sourceItems);
        }

        public IReadOnlyList<TSrc> SourceItems { get; }

        public TDest Rewrite(TSrc sourceItem)
        {
            int index = sourceItems.Count;
            sourceItems.Add(sourceItem);
            return index < destinationItems.Length ? destinationItems[index] : default(TDest);
        }
    }
}
