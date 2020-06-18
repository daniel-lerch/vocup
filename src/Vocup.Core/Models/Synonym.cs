using System;
using System.Collections.Generic;
using System.Text;

namespace Vocup.Models
{
    public class Synonym
    {
        public string Value { get; set; }
        public IList<string> Flags { get; set; }
        public IList<Practice> Practices { get; set; }
    }
}
