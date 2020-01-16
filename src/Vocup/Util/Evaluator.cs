using System;
using System.Linq;
using Vocup.Models;

namespace Vocup.Util
{
    public class Evaluator
    {
        public bool TolerateNoSynonym { get; set; }
        public bool TolerateWhiteSpace { get; set; }
        public bool ToleratePunctuationMark { get; set; }
        public bool TolerateSpecialChar { get; set; }
        public bool TolerateArticle { get; set; }

        public PracticeResult GetResult(string[] inputs, string[] results)
        {
            int missed = 0;

            foreach (string result in results)
            {
                if (!inputs.Contains(result))
                    missed++;
            }

            if (missed == 0)
                return PracticeResult.Correct;
            else if (missed < results.Length && TolerateNoSynonym)
                return PracticeResult.PartlyCorrect;

            string[] simplifiedInputs = inputs.Select(x => SimplifyText(x)).ToArray();
            string[] simplifiedResults = results.Select(x => SimplifyText(x)).ToArray();

            missed = 0;

            foreach (string result in simplifiedResults)
            {
                if (!simplifiedInputs.Contains(result))
                    missed++;
            }

            if (missed == 0)
                return PracticeResult.PartlyCorrect;

            int correctCount = 0;

            foreach (string result in results)
            {
                if (result.ContainsAny(",;"))
                {
                    bool found = false;

                    foreach (string input in inputs)
                    {
                        if (GetPartitialResult(input, result) == PracticeResult.Correct)
                            found = true;
                    }

                    if (found) correctCount++;
                }
            }

            if (correctCount == results.Length)
                return PracticeResult.Correct;
            else
                return PracticeResult.Wrong;
        }

        private PracticeResult GetPartitialResult(string input, string result)
        {
            string[] keywords = result.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in keywords)
            {
                if (!input.Contains(item))
                    return PracticeResult.Wrong;
            }

            return PracticeResult.Correct;
        }

        /// <summary>
        /// Returns the uppercase input simplified with all acitvated rules.
        /// </summary>
        private string SimplifyText(string text)
        {
            // Remove white space
            if (TolerateWhiteSpace)
            {
                text = text.Replace(" ", "");
            }

            // Remove punctuation marks
            if (ToleratePunctuationMark)
            {
                text = text.Replace(",", "");
                text = text.Replace(".", "");
                text = text.Replace(";", "");
                text = text.Replace("-", "");
                text = text.Replace("!", "");
                text = text.Replace("?", "");
                text = text.Replace("'", "");
                text = text.Replace("\\", "");
                text = text.Replace("/", "");
                text = text.Replace("(", "");
                text = text.Replace(")", "");
            }

            // Replace special chars
            if (TolerateSpecialChar)
            {
                text = text.Replace("ä", "a");
                text = text.Replace("ö", "o");
                text = text.Replace("ü", "u");
                text = text.Replace("ß", "ss");

                text = text.Replace("à", "a");
                text = text.Replace("â", "a");
                text = text.Replace("ă", "a");
                text = text.Replace("æ", "oe");
                text = text.Replace("ç", "c");
                text = text.Replace("é", "e");
                text = text.Replace("è", "e");
                text = text.Replace("ê", "e");
                text = text.Replace("ë", "e");
                text = text.Replace("ï", "i");
                text = text.Replace("î", "i");
                text = text.Replace("ì", "i");
                text = text.Replace("í", "i");
                text = text.Replace("ñ", "n");
                text = text.Replace("ô", "o");
                text = text.Replace("ò", "o");
                text = text.Replace("ó", "o");
                text = text.Replace("œ", "oe");
                text = text.Replace("ş", "s");
                text = text.Replace("ţ", "t");
                text = text.Replace("ù", "u");
                text = text.Replace("ú", "u");
                text = text.Replace("û", "u");
                text = text.Replace("ÿ", "y");

                text = text.Replace("ª", "");
                text = text.Replace("º", "");
                text = text.Replace("¡", "");
                text = text.Replace("¿", "");

            }

            // Remove articles
            if (TolerateArticle)
            {
                // German
                text = text.Replace("der", "");
                text = text.Replace("die", "");
                text = text.Replace("das", "");
                text = text.Replace("des", "");
                text = text.Replace("dem", "");
                text = text.Replace("den", "");
                text = text.Replace("ein", "");
                text = text.Replace("eine", "");
                text = text.Replace("eines", "");
                text = text.Replace("einer", "");
                text = text.Replace("einem", "");
                text = text.Replace("einen", "");

                // French
                text = text.Replace("un", "");
                text = text.Replace("une", "");
                text = text.Replace("le", "");
                text = text.Replace("la", "");
                text = text.Replace("les", "");
                text = text.Replace("l'", "");

                // English
                text = text.Replace("a", "");
                text = text.Replace("an", "");
                text = text.Replace("that", "");
                text = text.Replace("the", "");

                // Italian
                text = text.Replace("il", "");
                text = text.Replace("i", "");
                text = text.Replace("lo", "");
                text = text.Replace("gli", "");
                text = text.Replace("uno", "");
                text = text.Replace("una", "");
                text = text.Replace("un'", "");

                // Spanish
                text = text.Replace("el", "");
                text = text.Replace("los", "");
                text = text.Replace("las", "");
                text = text.Replace("unos", "");
                text = text.Replace("unas", "");
                text = text.Replace("lo", "");
                text = text.Replace("otro", "");
                text = text.Replace("medio", "");
            }

            text = text.ToUpper();
            return text;
        }
    }
}
