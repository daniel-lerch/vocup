using System;
using Vocup.Models;
using Vocup.Settings;
using Xunit;

namespace Vocup.Core.UnitTests.Models;

public class WordTests
{
    [Fact]
    public void TestPracticeState()
    {
        IVocupSettings settings = new DefaultSettings(string.Empty);
        Book book = new("Deutsch", "Englisch");
        Word word = new(new[] { new Synonym("Katze", settings) }, new[] { new Synonym("cat", settings) }, book, settings);
        
        book.PracticeMode = PracticeMode.AskForForeignLanguage;
        Assert.Equal(PracticeState.Unpracticed, word.PracticeState.PracticeState);
        book.PracticeMode = PracticeMode.AskForMotherTongue;
        Assert.Equal(PracticeState.Unpracticed, word.PracticeState.PracticeState);

        word.ForeignLanguage[0].Practices.Add(new Practice(DateTimeOffset.Now, PracticeResult2.Wrong));

        book.PracticeMode = PracticeMode.AskForForeignLanguage;
        Assert.Equal(PracticeState.WronglyPracticed, word.PracticeState.PracticeState);
        book.PracticeMode = PracticeMode.AskForMotherTongue;
        Assert.Equal(PracticeState.Unpracticed, word.PracticeState.PracticeState);

        word.ForeignLanguage[0].Practices.Add(new Practice(DateTimeOffset.Now, PracticeResult2.Correct));

        book.PracticeMode = PracticeMode.AskForForeignLanguage;
        Assert.Equal(PracticeState.CorrectlyPracticed, word.PracticeState.PracticeState);
        book.PracticeMode = PracticeMode.AskForMotherTongue;
        Assert.Equal(PracticeState.Unpracticed, word.PracticeState.PracticeState);

        word.MotherTongue[0].Practices.Add(new Practice(DateTimeOffset.Now, PracticeResult2.Correct));

        book.PracticeMode = PracticeMode.AskForForeignLanguage;
        Assert.Equal(PracticeState.CorrectlyPracticed, word.PracticeState.PracticeState);
        book.PracticeMode = PracticeMode.AskForMotherTongue;
        Assert.Equal(PracticeState.CorrectlyPracticed, word.PracticeState.PracticeState);
    }
}
