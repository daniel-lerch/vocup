using System;
using System.Collections.Generic;
using System.Text;

namespace Vocup.Models
{
    public class Synonym
    {
        public Synonym()
        {
            Flags = new List<string>();
            Practices = new List<Practice>();
        }

        public string Value { get; set; }
        public IList<string> Flags { get; }
        public IList<Practice> Practices { get; }
    }
}
