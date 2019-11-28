using System;
using System.Collections.Generic;
using System.Text;

namespace Vocup.Avalonia.Models
{
    public class Word
    {
        public IList<Synonym> MotherTongueWords { get; set; }
        public IList<Synonym> ForeignLanguageWords { get; set; }
        public IList<Practice> PracticeHistory { get; set; }
    }
}
