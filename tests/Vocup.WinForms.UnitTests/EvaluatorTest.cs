using Vocup.Models;
using Vocup.Util;
using Xunit;

namespace Vocup.UnitTests;

public class EvaluatorTest
{
    private readonly Evaluator evaluator;

    public EvaluatorTest()
    {
        evaluator = new Evaluator
        {
            OptionalExpressions = true,
            TolerateArticle = false,
            TolerateNoSynonym = false,
            ToleratePunctuationMark = false,
            TolerateSpecialChar = false,
            TolerateWhiteSpace = false
        };
    }

    [Fact]
    public void TestSynonymOrder()
    {
        var results = new[] { "anxiety", "fear" };
        var inputs = new[] { "fear", "anxiety" };

        Assert.Equal(PracticeResult.Correct, evaluator.GetResult(results, inputs));
    }

    [Theory]
    [InlineData("anxiety,fear", "fear, anxiety")]
    [InlineData("anxiety, fear", "fear,anxiety")]
    [InlineData("anxiety; fear", "fear; anxiety")]
    public void TestInlineSynonymOrder(string result, string input)
    {
        Assert.Equal(PracticeResult.Correct, evaluator.GetResult([result], [input]));
    }

    [Theory]
    [InlineData("(to) walk", "(to) walk")]                     // original
    [InlineData("(to) walk", "to walk")]                       // long form
    [InlineData("(to) walk", "walk")]                          // short form
    [InlineData("(to) culminate (in)", "culminate (in)")]      // half short, half original
    [InlineData("(to) crave (for) sth", "crave sth")]          // short form (intermediate)
    [InlineData("(to) crave (for) sth", "to crave (for) sth")] // half long, half original
    public void TestOptionalExpressions(string result, string input)
    {
        Assert.Equal(PracticeResult.Correct, evaluator.GetResult([result], [input]));
    }

    [Theory]
    [InlineData(PracticeResult.Correct, "upset (about sth)/(that)", "upset (about sth)/(that)")]
    [InlineData(PracticeResult.Wrong, "upset (about sth)/(that)", "upset about sth/that")]
    [InlineData(PracticeResult.Wrong, "upset (about sth)/(that)", "upset about sth/")]
    [InlineData(PracticeResult.Wrong, "upset (about sth)/(that)", "upset /that")]
    [InlineData(PracticeResult.Wrong, "upset (about sth)/(that)", "upset/")]
    public void TestOptionalEdgeCases(PracticeResult expected, string result, string input)
    {
        Assert.Equal(expected, evaluator.GetResult([result], [input]));
    }
}
