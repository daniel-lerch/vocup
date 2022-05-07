﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vocup.IO;
using Vocup.Models;
using Xunit;

namespace Vocup.Core.UnitTests.IO
{
    public class BookStorageTests
    {
        private readonly BookStorage bookStorage = new BookStorage();
        private readonly string tempPath = Path.GetTempPath();

        [Fact]
        public async Task TestReadVhf1()
        {
            var storage = new BookStorage();
            Book book = await storage.ReadBookAsync(Path.Join("Resources", "Year 11.vhf"), "Resources").ConfigureAwait(false);

            Assert.Equal(BookFileFormat.Vhf_1_0, book.Serializer!.FileFormat);
            Assert.Equal("Deutsch", book.MotherTongue);
            Assert.Equal("Englisch", book.ForeignLanguage);
            Assert.Equal(PracticeMode.AskForForeignLanguage, book.PracticeMode);
            Assert.Equal(113, book.Words.Count);
            Assert.Contains(book.Words, word => word.ForeignLanguage.Any(synonym => synonym.Practices.Count > 0));
        }

        [Fact]
        public async Task TestReadVhf2()
        {
            Book book = await new BookStorage().ReadBookAsync(Path.Join("Resources", "Year 12.vhf"), tempPath).ConfigureAwait(false);

            Assert.Equal(BookFileFormat.Vhf_2_0, book.Serializer!.FileFormat);
            Assert.Equal("Deutsch", book.MotherTongue);
            Assert.Equal("Englisch", book.ForeignLanguage);
            Assert.Equal(PracticeMode.AskForForeignLanguage, book.PracticeMode);
            Assert.Single(book.Words);
            Assert.Equal(2, book.Words[0].MotherTongue.Count);
            Assert.Equal(2, book.Words[0].ForeignLanguage.Count);
        }

        [Fact]
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
            Assert.Equal(BookFileFormat.Vhf_1_0, book.Serializer!.FileFormat);
            BookAssert.Equal(sample, book);
        }

        [Fact]
        public async Task TestWriteReadVhf2()
        {
            Book sample = GenerateSampleBook(BookFileFormat.Vhf_2_0);
            var storage = new BookStorage();
            string path = Path.Combine(Path.GetTempPath(), "Vocup_d3afa6cf-a041-489f-8f39-aea5ed1c0ec5.vhf");
            await storage.WriteBookAsync(path, sample, tempPath).ConfigureAwait(false);

            Book book = await storage.ReadBookAsync(path, tempPath).ConfigureAwait(false);
            File.Delete(path);
            Assert.Equal(BookFileFormat.Vhf_2_0, book.Serializer!.FileFormat);
            BookAssert.Equal(sample, book);
        }

        private Book GenerateSampleBook(BookFileFormat fileFormat)
        {
            var pratice2 = new ObservableCollection<Practice>
            {
                new Practice
                {
                    // Vocup v1 file formats do not support seconds or timezones of practices
                    Date = new DateTimeOffset(2020, 8, 19, 16, 17, 17, default),
                    Result = PracticeResult.Correct
                }
            };

            return new Book("Deutsch", "Englisch", new ObservableCollection<Word>
            {
                new Word(new ObservableCollection<Synonym> { new Synonym("Katze") }, new ObservableCollection<Synonym> { new Synonym("cat") }),
                new Word(new ObservableCollection<Synonym> { new Synonym("Farbe") }, new ObservableCollection<Synonym>
                {
                    new Synonym("colour", new ObservableCollection<string>(), pratice2),
                    new Synonym("color", new ObservableCollection<string>(), pratice2)
                })
            })
            {
                PracticeMode = PracticeMode.AskForForeignLanguage,
                Serializer = bookStorage.GetSerializer(fileFormat)
            };
        }
    }
}
