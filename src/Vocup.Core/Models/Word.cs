using System;
using System.Collections.Generic;
using System.Text;

namespace Vocup.Models
{
    public class Word
    {
        public Word()
        {
            MotherTongue = new List<Synonym>();
            ForeignLanguage = new List<Synonym>();
        }

        public IList<Synonym> MotherTongue { get; }
        public IList<Synonym> ForeignLanguage { get; }
        public DateTimeOffset CreationDate { get; set; }
    }
}
