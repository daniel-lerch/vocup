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

        public List<Synonym> MotherTongue { get; set; }
        public List<Synonym> ForeignLanguage { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}
