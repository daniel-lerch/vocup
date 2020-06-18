using System;
using System.Collections.Generic;
using System.Text;

namespace Vocup.Models
{
    public class Word
    {
        public IList<Synonym> MotherTongue { get; set; }
        public IList<Synonym> ForeignLanguage { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}
