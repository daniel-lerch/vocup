using System;
using Vocup.Models;
using Xunit;

namespace Vocup.Core.UnitTests.Models;

public class BookTests
{
    [Fact]
    public void TestUnpracticed()
    {
        Book book = new("Deutsch", "Englisch");
        Assert.Equal(0, book.Unpracticed);

        book.Words.Add(new Word(new[] { new Synonym("Katze") }, new[] { new Synonym("cat") }));
        Assert.Equal(1, book.Unpracticed);

        book.Words[0].ForeignLanguage[0].Practices.Add(new Practice { Date = DateTimeOffset.Now, Result = PracticeResult2.Wrong });
        Assert.Equal(0, book.Unpracticed);

        book.PracticeMode = PracticeMode.AskForMotherTongue;
        Assert.Equal(1, book.Unpracticed);
    }
}
