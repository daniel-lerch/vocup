using Vocup.Models.Legacy;

namespace Vocup.Models;

public class VocabularyWordPractice
{
    public VocabularyWordPractice(IVocabularyWord word)
    {
        VocabularyWord = word;
        WrongInput = string.Empty;
    }

    public IVocabularyWord VocabularyWord { get; }
    public PracticeResult PracticeResult { get; set; }
    public string WrongInput { get; set; }
}
