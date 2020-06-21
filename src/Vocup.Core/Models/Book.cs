using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Vocup.Models
{
    public class Book
    {
        public Book()
        {
            Words = new List<Word>();
        }

        [JsonIgnore] public Version FileVersion { get; set; }
        [JsonIgnore] public string VhrCode { get; set; }
        public string MotherTongue { get; set; }
        public string ForeignLanguage { get; set; }
        public PracticeMode PracticeMode { get; set; }
        public List<Word> Words { get; set; }
    }
}
