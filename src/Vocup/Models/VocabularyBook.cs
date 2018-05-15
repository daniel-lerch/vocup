using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocup.Models
{
    public class VocabularyBook
    {
        public VocabularyBook()
        {
            Words = new List<VocabularyWord>();
        }

        List<VocabularyWord> Words { get; }
    }
}
