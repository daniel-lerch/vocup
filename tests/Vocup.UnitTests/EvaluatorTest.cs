using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vocup.Models;
using Vocup.Util;

namespace Vocup.UnitTests
{
    [TestClass]
    public class EvaluatorTest
    {
        private Evaluator evaluator;

        [TestInitialize]
        public void Initialize()
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

        [TestMethod]
        public void TestSynonymOrder()
        {
            var results = new[] { "anxiety", "fear" };
            var inputs = new[] { "fear", "anxiety" };

            Assert.AreEqual(PracticeResult.Correct, evaluator.GetResult(results, inputs));
        }

        [DataTestMethod]
        [DataRow("anxiety,fear", "fear, anxiety")]
        [DataRow("anxiety, fear", "fear,anxiety")]
        [DataRow("anxiety; fear", "fear; anxiety")]
        public void TestInlineSynonymOrder(string result, string input)
        {
            Assert.AreEqual(PracticeResult.Correct, evaluator.GetResult(new[] { result }, new[] { input }));
        }

        [DataTestMethod]
        [DataRow("(to) walk", "(to) walk")]                     // original
        [DataRow("(to) walk", "to walk")]                       // long form
        [DataRow("(to) walk", "walk")]                          // short form
        [DataRow("(to) culminate (in)", "culminate (in)")]      // half short, half original
        [DataRow("(to) crave (for) sth", "crave sth")]          // short form (intermediate)
        [DataRow("(to) crave (for) sth", "to crave (for) sth")] // half long, half original
        public void TestOptionalExpressions(string result, string input)
        {
            Assert.AreEqual(PracticeResult.Correct, evaluator.GetResult(new[] { result }, new[] { input }));
        }

        [DataTestMethod]
        [DataRow(PracticeResult.Correct, "upset (about sth)/(that)", "upset (about sth)/(that)")]
        [DataRow(PracticeResult.Wrong, "upset (about sth)/(that)", "upset about sth/that")]
        [DataRow(PracticeResult.Wrong, "upset (about sth)/(that)", "upset about sth/")]
        [DataRow(PracticeResult.Wrong, "upset (about sth)/(that)", "upset /that")]
        [DataRow(PracticeResult.Wrong, "upset (about sth)/(that)", "upset/")]
        public void TestOptionalEdgeCases(PracticeResult expected, string result, string input)
        {
            Assert.AreEqual(expected, evaluator.GetResult(new[] { result }, new[] { input }));
        }
    }
}
