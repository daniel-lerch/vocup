using System;
using System.Collections.Generic;
using System.Text;

namespace Vocup.Avalonia.Models
{
    public class Book
    {
        public string MotherTongue { get; set; }
        public string ForeignLanguage { get; set; }
        public PracticeMode PracticeMode { get; set; }
        public IList<Word> Words { get; set; }
    }
}
