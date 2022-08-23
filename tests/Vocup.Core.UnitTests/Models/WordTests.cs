using System;
using Vocup.Models;
using Xunit;

namespace Vocup.Core.UnitTests.Models;

public class WordTests
{
    [Fact]
    public void TestPracticeState()
    {
        Word word = new(new[] { new Synonym("Katze") }, new[] { new Synonym("cat") });
        Assert.Equal(0, word.MotherTonguePracticeState);
        Assert.Equal(0, word.ForeignLanguagePracticeState);

        word.ForeignLanguage[0].Practices.Add(new Practice { Date = DateTimeOffset.Now, Result = PracticeResult2.Wrong });
        Assert.Equal(0, word.MotherTonguePracticeState);
        Assert.Equal(1, word.ForeignLanguagePracticeState);

        word.ForeignLanguage[0].Practices.Add(new Practice { Date = DateTimeOffset.Now, Result = PracticeResult2.Correct });
        Assert.Equal(0, word.MotherTonguePracticeState);
        Assert.Equal(2, word.ForeignLanguagePracticeState);

        word.MotherTongue[0].Practices.Add(new Practice { Date = DateTimeOffset.Now, Result = PracticeResult2.Correct });
        Assert.Equal(2, word.MotherTonguePracticeState);
        Assert.Equal(2, word.ForeignLanguagePracticeState);
    }
}
