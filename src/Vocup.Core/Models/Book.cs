using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Vocup.Models
{
    public class Book
    {
        [JsonIgnore] public Version FileVersion { get; set; }
        [JsonIgnore] public string VhrCode { get; set; }
        public string MotherTongue { get; set; }
        public string ForeignLanguage { get; set; }
        public PracticeMode PracticeMode { get; set; }
        public IList<Word> Words { get; set; }
    }
}
