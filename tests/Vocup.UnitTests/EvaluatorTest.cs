using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Assert.AreEqual(PracticeResult.Correct, evaluator.GetResult(new[] { input }, new[] { result }));
        }
    }
}
