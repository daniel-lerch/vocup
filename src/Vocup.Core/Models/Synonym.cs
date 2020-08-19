using System.Collections.Generic;

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
        public List<string> Flags { get; set; }
        public List<Practice> Practices { get; set; }
    }
}
