namespace Vocup.Models;

public class VocabularyWordPractice
{
    public VocabularyWordPractice(VocabularyWord word)
    {
        VocabularyWord = word;
        WrongInput = string.Empty;
    }

    public VocabularyWord VocabularyWord { get; }
    public PracticeResult PracticeResult { get; set; }
    public string WrongInput { get; set; }
}
