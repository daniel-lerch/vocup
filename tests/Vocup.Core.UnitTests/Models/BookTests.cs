using System;
using Vocup.Models;
using Vocup.Settings;
using Xunit;

namespace Vocup.Core.UnitTests.Models;

public class BookTests
{
    [Fact]
    public void TestUnpracticed()
    {
        IVocupSettings settings = new DefaultSettings(string.Empty);

        Book book = new("Deutsch", "Englisch");
        Assert.Equal(0, book.PracticeState.Unpracticed);

        book.Words.Add(new Word(new[] { new Synonym("Katze", settings) }, new[] { new Synonym("cat", settings) }, book, settings));
        Assert.Equal(1, book.PracticeState.Unpracticed);

        book.Words[0].ForeignLanguage[0].Practices.Add(new Practice(DateTimeOffset.Now, PracticeResult2.Wrong));
        Assert.Equal(0, book.PracticeState.Unpracticed);

        book.PracticeMode = PracticeMode.AskForMotherTongue;
        Assert.Equal(1, book.PracticeState.Unpracticed);
    }
}
