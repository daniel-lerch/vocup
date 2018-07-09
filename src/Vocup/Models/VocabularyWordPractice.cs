using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocup.Models
{
    public class VocabularyWordPractice
    {
        public VocabularyWordPractice(VocabularyWord word)
        {
            VocabularyWord = word;
        }

        public VocabularyWord VocabularyWord { get; }
        public PracticeResult PracticeResult { get; set; }
        public string WrongInput { get; set; }
    }
}