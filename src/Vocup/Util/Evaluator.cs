using System;
using System.Collections.Generic;
using Vocup.Models;

namespace Vocup.Util
{
    public class Evaluator
    {
        public bool OptionalExpressions { get; set; }
        public bool TolerateNoSynonym { get; set; }
        public bool TolerateWhiteSpace { get; set; }
        public bool ToleratePunctuationMark { get; set; }
        public bool TolerateSpecialChar { get; set; }
        public bool TolerateArticle { get; set; }

        // Evaluating vocabulary words requires multiple steps:
        //
        // Question 1: Does an input contain multiple synonyms?
        // Question 2: Does a synonym contain optional expressions?
        // Question 3: Is a synonym at least partly correct?

        public PracticeResult GetResult(string[] results, string[] inputs)
        {
            if (results is null) throw new ArgumentNullException(nameof(results));
            if (results.Length < 1) throw new ArgumentOutOfRangeException(nameof(results));
            if (inputs is null) throw new ArgumentNullException(nameof(inputs));
            if (inputs.Length < 1) throw new ArgumentOutOfRangeException(nameof(inputs));

            (PracticeResult bestResult, PracticeResult worstResult) = EvaluatePartialResults(results, inputs);

            return TolerateNoSynonym && worstResult == PracticeResult.Wrong && bestResult == PracticeResult.Correct
                ? PracticeResult.PartlyCorrect
                : worstResult;
        }

        private (PracticeResult bestResult, PracticeResult worstResult) EvaluatePartialResults(string[] results, string[] inputs)
        {
            var bestResult = PracticeResult.Wrong;
            var worstResult = PracticeResult.Correct;

            foreach (string result in results)
            {
                var bestMatch = PracticeResult.Wrong;

                foreach (string input in inputs)
                {
                    PracticeResult match = EvaluateOptionalExpressions(result, input);

                    if (match != PracticeResult.Correct)
                    {
                        string[] partialResults = result.SplitAndTrim(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] partialInputs = input.SplitAndTrim(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

                        // If the synonym contains separators which match with the input in term of count
                        // we evaluate each part of the input as a synonym and check if we get a better score

                        if (partialResults.Length == partialInputs.Length && partialResults.Length > 1)
                        {
                            (_, PracticeResult partialMatch) = EvaluatePartialResults(partialResults, partialInputs);
                            if (partialMatch > match) match = partialMatch;
                        }
                    }

                    if (match > bestMatch) bestMatch = match;
                }

                if (bestMatch > bestResult) bestResult = bestMatch;
                if (bestMatch < worstResult) worstResult = bestMatch;
            }

            return (bestResult, worstResult);
        }

        private PracticeResult EvaluateOptionalExpressions(string result, string input)
        {
            if (!OptionalExpressions) return EvaluateToleratedMistakes(result, input);

            List<Segment> segments = GetSegments(result, out int optionalCount);

            if (segments.Count == 1) return EvaluateToleratedMistakes(result, input);

            // Evaluating optional expressions range by range would be efficient but difficult
            // because it's not possible to match result and input indices for partly correct statements.
            // The alternative is to compute all possible combinations which are as many as  3 to the power
            // of n where n is the number of optional segments.

            string[] candidiates = new string[(int)Math.Pow(3, optionalCount)];
            int optionalIndex = -1;

            for (int i = 0; i < segments.Count; i++)
            {
                Segment seg = segments[i];
                var variations = new string[3];
                variations[0] = result.Substring(seg.Start, seg.End - seg.Start + 1);

                if (segments[i].Optional)
                {
                    if (seg.Space == 0)
                        variations[1] = result.Substring(seg.Start + 1, seg.End - seg.Start - 1);
                    else if (seg.Space == -1)
                        variations[1] = ' ' + result.Substring(seg.Start + 2, seg.End - seg.Start - 2);
                    else if (seg.Space == 1)
                        variations[1] = result.Substring(seg.Start + 1, seg.End - seg.Start - 2) + ' ';

                    variations[2] = string.Empty;
                    optionalIndex++;
                }

                for (int k = 0; k < candidiates.Length; k++)
                {
                    int variation = seg.Optional
                        ? k / (int)Math.Pow(3, optionalCount - 1 - optionalIndex) % 3
                        : 0;

                    if (i == 0)
                        candidiates[k] = variations[variation];
                    else
                        candidiates[k] += variations[variation];
                }
            }

            // Iterate through all possible candidates and return the result of the best match.

            PracticeResult bestMatch = PracticeResult.Wrong;

            foreach (string candidate in candidiates)
            {
                PracticeResult match = EvaluateToleratedMistakes(candidate, input);
                if (match > bestMatch) bestMatch = match;
            }

            return bestMatch;
        }

        private List<Segment> GetSegments(string result, out int optionalCount)
        {
            var segments = new List<Segment>();
            optionalCount = 0;
            bool isOptional = false;
            int previousStart = 0;
            int optionalStart = -1;
            int optionalSpace = 0;
            int lastSegmentEnd = -1;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == '(')
                {
                    // Nested parentheses or a non whitespace character in front of an opening parenthesis
                    // will not be treated as the start of an optional expression.

                    if (!isOptional)
                    {
                        if (i == 0)
                        {
                            optionalStart = i;
                        }
                        else if (result[i - 1] == ' ')
                        {
                            optionalStart = i - 1;
                            optionalSpace = -1;
                        }
                    }

                    isOptional = true;
                }
                else if (result[i] == ')')
                {
                    // A leading closing parenthesis or a non whitespace character after a closing
                    // parenthesis will not be treated as the end of an optional expression.

                    bool end;
                    if (optionalStart != -1 && ((end = i == result.Length - 1) || result[i + 1] == ' '))
                    {
                        // If there are characters in front of an optional segment, it must be mandatory.
                        if (optionalStart - 1 >= previousStart)
                        {
                            segments.Add(new Segment(previousStart, optionalStart - 1, false, 0));
                        }

                        int tail = 0;

                        if (!end)
                        {
                            if (optionalSpace != -1) tail = optionalSpace = 1;

                            // After an optional segment must follow a mandatory segment.
                            previousStart = i + tail + 1;
                        }

                        segments.Add(new Segment(optionalStart, i + tail, true, optionalSpace));
                        optionalCount++;
                        lastSegmentEnd = i + tail;
                    }

                    optionalStart = -1;
                    optionalSpace = 0;
                    isOptional = false;
                }
            }

            if (lastSegmentEnd != result.Length - 1)
            {
                segments.Add(new Segment(previousStart, result.Length - 1, false, 0));
            }

            return segments;
        }

        private PracticeResult EvaluateToleratedMistakes(string result, string input)
        {
            if (result.Equals(input, StringComparison.Ordinal))
            {
                return PracticeResult.Correct;
            }
            else if (SimplifyText(result).Equals(SimplifyText(input), StringComparison.Ordinal))
            {
                return PracticeResult.PartlyCorrect;
            }
            else
            {
                return PracticeResult.Wrong;
            }
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
                text = text.Replace("æ", "ae");
                text = text.Replace("ç", "c");
                text = text.Replace("č", "c");
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
                text = text.Replace("š", "s");
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

        private readonly struct Segment
        {
            public Segment(int start, int end, bool optional, int space)
            {
                Start = start;
                End = end;
                Optional = optional;
                Space = space;
            }

            public int Start { get; }
            public int End { get; }
            public bool Optional { get; }
            public int Space { get; }
        }
    }
}
