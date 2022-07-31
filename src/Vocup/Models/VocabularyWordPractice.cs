namespace Vocup.Models;

public class VocabularyWordPractice
{
    public VocabularyWordPractice(Word word)
    {
        VocabularyWord = word;
        WrongInput = string.Empty;
    }

    public Word VocabularyWord { get; }
    public PracticeResult PracticeResult { get; set; }
    public string WrongInput { get; set; }
}
