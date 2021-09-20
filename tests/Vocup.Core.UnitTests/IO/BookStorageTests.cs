using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vocup.IO;
using Vocup.Models;

namespace Vocup.Core.UnitTests.IO
{
    [TestClass]
    public class BookStorageTests
    {
        private readonly BookStorage bookStorage = new BookStorage();
        private readonly string tempPath = Path.GetTempPath();

        [TestMethod]
        public async Task TestReadVhf1()
        {
            var storage = new BookStorage();
            Book book = await storage.ReadBookAsync(Path.Join("Resources", "Year 11.vhf"), "Resources").ConfigureAwait(false);

            Assert.AreEqual(BookFileFormat.Vhf_1_0, book.Serializer!.FileFormat);
            Assert.AreEqual("Deutsch", book.MotherTongue);
            Assert.AreEqual("Englisch", book.ForeignLanguage);
            Assert.AreEqual(PracticeMode.AskForForeignLanguage, book.PracticeMode);
            Assert.AreEqual(113, book.Words.Count);
            Assert.IsTrue(book.Words.Any(word => word.ForeignLanguage.Any(synonym => synonym.Practices.Count > 0)));
        }

        [TestMethod]
        public async Task TestReadVhf2()
        {
            Book book = await new BookStorage().ReadBookAsync(Path.Join("Resources", "Year 12.vhf"), tempPath).ConfigureAwait(false);

            Assert.AreEqual(BookFileFormat.Vhf_2_0, book.Serializer!.FileFormat);
            Assert.AreEqual("Deutsch", book.MotherTongue);
            Assert.AreEqual("Englisch", book.ForeignLanguage);
            Assert.AreEqual(PracticeMode.AskForForeignLanguage, book.PracticeMode);
            Assert.AreEqual(1, book.Words.Count);
            Assert.AreEqual(2, book.Words[0].MotherTongue.Count);
            Assert.AreEqual(2, book.Words[0].ForeignLanguage.Count);
        }

        [TestMethod]
        public async Task TestWriteReadVhf1()
        {
            Book sample = GenerateSampleBook(BookFileFormat.Vhf_1_0);
            sample.VhrCode = "o5xqm7rdg6y9fecs9ykuuckv";
            var storage = new BookStorage();
            string path = Path.Combine(Path.GetTempPath(), "Vocup_751e0198-5ed8-439d-9041-efb3d594c400.vhf");
            await storage.WriteBookAsync(path, sample, tempPath).ConfigureAwait(false);

            Book book = await storage.ReadBookAsync(path, tempPath).ConfigureAwait(false);
            File.Delete(path);
            File.Delete(Path.Combine(Path.GetTempPath(), "o5xqm7rdg6y9fecs9ykuuckv.vhr"));
            Assert.AreEqual(BookFileFormat.Vhf_1_0, book.Serializer!.FileFormat);
            BookAssert.AreEqual(sample, book);
        }

        [TestMethod]
        public async Task TestWriteReadVhf2()
        {
            Book sample = GenerateSampleBook(BookFileFormat.Vhf_2_0);
            var storage = new BookStorage();
            string path = Path.Combine(Path.GetTempPath(), "Vocup_d3afa6cf-a041-489f-8f39-aea5ed1c0ec5.vhf");
            await storage.WriteBookAsync(path, sample, tempPath).ConfigureAwait(false);

            Book book = await storage.ReadBookAsync(path, tempPath).ConfigureAwait(false);
            File.Delete(path);
            Assert.AreEqual(BookFileFormat.Vhf_2_0, book.Serializer!.FileFormat);
            BookAssert.AreEqual(sample, book);
        }

        private Book GenerateSampleBook(BookFileFormat fileFormat)
        {
            var pratice2 = new List<Practice>
            {
                new Practice
                {
                    // Vocup v1 file formats do not support seconds or timezones of practices
                    Date = new DateTimeOffset(2020, 8, 19, 16, 17, 17, default),
                    Result = PracticeResult.Correct
                }
            };

            return new Book("Deutsch", "Englisch", new List<Word>
            {
                new Word(new List<Synonym> { new Synonym("Katze") }, new List<Synonym> { new Synonym("cat") }),
                new Word(new List<Synonym> { new Synonym("Farbe") }, new List<Synonym>
                {
                    new Synonym("colour", new List<string>(), pratice2),
                    new Synonym("color", new List<string>(), pratice2)
                })
            })
            {
                PracticeMode = PracticeMode.AskForForeignLanguage,
                Serializer = bookStorage.GetSerializer(fileFormat)
            };
        }
    }
}
