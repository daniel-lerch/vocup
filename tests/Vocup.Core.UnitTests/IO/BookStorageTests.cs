using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            var storage = new BookStorage();
            string path = Path.Combine(Path.GetTempPath(), "Vocup_751e0198-5ed8-439d-9041-efb3d594c400.vhf");
            await storage.WriteBookAsync(path, sample).ConfigureAwait(false);

            Book book = await storage.ReadBookAsync(path).ConfigureAwait(false);
            File.Delete(path);
            Assert.AreEqual(new Version(1, 0), book.FileVersion);
            AssertSampleBook(book);
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
            AssertSampleBook(book);
        }

        private Book GenerateSampleBook(Version fileVersion)
        {
            var book = new Book { FileVersion = fileVersion };
            book.MotherTongue = "Deutsch";
            book.ForeignLanguage = "Englisch";
            var word1 = new Word();
            word1.MotherTongue.Add(new Synonym { Value = "Farbe" });
            word1.ForeignLanguage.Add(new Synonym { Value = "colour" });
            word1.ForeignLanguage.Add(new Synonym { Value = "color" });
            book.Words.Add(word1);
            return book;
        }

        private void AssertSampleBook(Book book)
        {
            Assert.IsNotNull(book);
            Assert.AreEqual("Deutsch", book.MotherTongue);
            Assert.AreEqual("Englisch", book.ForeignLanguage);
            Assert.AreEqual(1, book.Words.Count);
            Assert.AreEqual(1, book.Words[0].MotherTongue.Count);
            Assert.AreEqual(2, book.Words[0].ForeignLanguage.Count);
            Assert.AreEqual("Farbe", book.Words[0].MotherTongue[0].Value);
            Assert.AreEqual("colour", book.Words[0].ForeignLanguage[0].Value);
            Assert.AreEqual("color", book.Words[0].ForeignLanguage[1].Value);
        }
    }
}
