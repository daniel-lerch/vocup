using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Vocup.Models;

namespace Vocup.Core.UnitTests
{
    public static class BookAssert
    {
        public static void AreEqual(Book expected, Book actual)
        {
            Assert.AreEqual(expected.MotherTongue, actual.MotherTongue);
            Assert.AreEqual(expected.ForeignLanguage, actual.ForeignLanguage);
            Assert.AreEqual(expected.Words.Count, actual.Words.Count);

            for (int i = 0; i < expected.Words.Count; i++)
            {
                WordsEqual(expected.Words[i], actual.Words[i], expected.FileVersion);
            }
        }

        public static void WordsEqual(Word expected, Word actual, Version fileVersion)
        {
            Assert.AreEqual(expected.MotherTongue.Count, actual.MotherTongue.Count);
            Assert.AreEqual(expected.ForeignLanguage.Count, actual.ForeignLanguage.Count);
            Assert.AreEqual(expected.CreationDate, actual.CreationDate);

            for (int i = 0; i < expected.MotherTongue.Count; i++)
            {
                SynonymsEqual(expected.MotherTongue[i], actual.MotherTongue[i], fileVersion);
            }

            for (int i = 0; i < expected.ForeignLanguage.Count; i++)
            {
                SynonymsEqual(expected.ForeignLanguage[i], actual.ForeignLanguage[i], fileVersion);
            }
        }

        public static void SynonymsEqual(Synonym expected, Synonym actual, Version fileVersion)
        {
            Assert.AreEqual(expected.Value, actual.Value);
            CollectionAssert.AreEqual(expected.Flags, actual.Flags);
            Assert.AreEqual(expected.Practices.Count, actual.Practices.Count);

            for (int i = 0; i < expected.Practices.Count; i++)
            {
                PracticesEqual(expected.Practices[i], actual.Practices[i], fileVersion);
            }
        }

        public static void PracticesEqual(Practice expected, Practice actual, Version fileVersion)
        {
            Assert.AreEqual(expected.Result, actual.Result);
            if (fileVersion == new Version(1, 0))
            {
                var expectedDate = new DateTime(expected.Date.Year, expected.Date.Month, expected.Date.Day, expected.Date.Hour, expected.Date.Minute, 0);
                var actualDate = new DateTime(actual.Date.Year, actual.Date.Month, actual.Date.Day, actual.Date.Hour, actual.Date.Minute, 0);

                Assert.AreEqual(expectedDate, actualDate);
            }
            else
            {
                Assert.AreEqual(expected.Date, actual.Date);
            }
        }
    }
}
