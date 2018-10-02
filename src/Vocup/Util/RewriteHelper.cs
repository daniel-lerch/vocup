using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocup.Util
{
    public class RewriteHelper
    {
        private readonly string[] destinationItems;
        private IList<string> sourceItems;

        public RewriteHelper(params string[] destinationItems)
        {
            this.destinationItems = destinationItems;
            sourceItems = new List<string>();
            SourceItems = new ReadOnlyCollection<string>(sourceItems);
        }

        public IReadOnlyList<string> SourceItems { get; }

        public string Rewrite(string source)
        {
            foreach (string sourceItem in source.Split('\t'))
            {
                sourceItems.Add(sourceItem);
            }

            return string.Join("\t", destinationItems);
        }
    }
}
