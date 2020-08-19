using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Vocup.IO;
using Vocup.Models;

namespace Vocup.Core.UnitTests.IO
{
    [TestClass]
    public class BookStorageTests
    {
        [TestMethod]
        public async Task TestReadVhf1()
        {
            Book book = await new BookStorage().ReadBookAsync(Path.Join("Resources", "Year 11.vhf")).ConfigureAwait(false);

            Assert.AreEqual(new Version(1, 0), book.FileVersion);
            Assert.AreEqual(113, book.Words.Count);
        }

        [TestMethod]
        public async Task TestReadVhf2()
        {
            Book book = await new BookStorage().ReadBookAsync(Path.Join("Resources", "Year 12.vhf")).ConfigureAwait(false);

            Assert.AreEqual(new Version(2, 0), book.FileVersion);
            Assert.AreEqual(1, book.Words.Count);
            Assert.AreEqual(2, book.Words[0].MotherTongue.Count);
            Assert.AreEqual(2, book.Words[0].ForeignLanguage.Count);
        }

        [TestMethod]
        public async Task TestWriteReadVhf1()
        {
            Book sample = GenerateSampleBook(new Version(1, 0));
            sample.VhrCode = "o5xqm7rdg6y9fecs9ykuuckv";
            var storage = new BookStorage { VhrPath = Path.GetTempPath() };
            string path = Path.Combine(Path.GetTempPath(), "Vocup_751e0198-5ed8-439d-9041-efb3d594c400.vhf");
            await storage.WriteBookAsync(path, sample).ConfigureAwait(false);

            Book book = await storage.ReadBookAsync(path).ConfigureAwait(false);
            File.Delete(path);
            File.Delete(Path.Combine(Path.GetTempPath(), "o5xqm7rdg6y9fecs9ykuuckv.vhr"));
            Assert.AreEqual(new Version(1, 0), book.FileVersion);
            BookAssert.AreEqual(sample, book);
        }

        [TestMethod]
        public async Task TestWriteReadVhf2()
        {
            Book sample = GenerateSampleBook(new Version(2, 0));
            var storage = new BookStorage();
            string path = Path.Combine(Path.GetTempPath(), "Vocup_d3afa6cf-a041-489f-8f39-aea5ed1c0ec5.vhf");
            await storage.WriteBookAsync(path, sample).ConfigureAwait(false);

            Book book = await storage.ReadBookAsync(path).ConfigureAwait(false);
            File.Delete(path);
            Assert.AreEqual(new Version(2, 0), book.FileVersion);
            BookAssert.AreEqual(sample, book);
        }

        private Book GenerateSampleBook(Version fileVersion)
        {
            var book = new Book { FileVersion = fileVersion };
            book.MotherTongue = "Deutsch";
            book.ForeignLanguage = "Englisch";
            book.PracticeMode = PracticeMode.AskForForeignLanguage;

            var word1 = new Word();
            word1.MotherTongue.Add(new Synonym { Value = "Katze" });
            word1.ForeignLanguage.Add(new Synonym { Value = "cat" });

            var word2 = new Word();
            var pratice2 = new List<Practice>
            {
                new Practice
                {
                    // Vocup v1 file formats do not support seconds or timezones of practices
                    Date = new DateTimeOffset(2020, 8, 19, 16, 17, 17, default),
                    Result = PracticeResult.Correct
                } 
            };
            word2.MotherTongue.Add(new Synonym { Value = "Farbe" });
            word2.ForeignLanguage.Add(new Synonym { Value = "colour", Practices = pratice2 });
            word2.ForeignLanguage.Add(new Synonym { Value = "color", Practices = pratice2 });

            book.Words.Add(word1);
            book.Words.Add(word2);
            return book;
        }
    }
}
