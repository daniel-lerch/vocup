using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vocup.IO;
using Vocup.Models;
using Vocup.Settings;
using Xunit;

namespace Vocup.Core.UnitTests.IO;

public class BookStorageTests
{
    private readonly string tempPath;
    private readonly IVocupSettings settings;
    private readonly BookStorage bookStorage;

    public BookStorageTests()
    {
        tempPath = Path.GetTempPath();
        settings = new DefaultSettings("Resources");
        bookStorage = new BookStorage(settings);
    }

    [Fact]
    public void TestEquals()
    {
        Book book1 = GenerateSampleVhf2Book();
        Book book2 = GenerateSampleVhf2Book();
        Assert.True(book1.Equals(book2));
        Assert.Equal(book1, book2);
    }

    [Fact]
    public async Task TestReadVhf1()
    {
        await using BookContext bookContext =
            await bookStorage.OpenAsync(Path.Join("Resources", "Year 11.vhf"), "Resources").ConfigureAwait(false);

        Book book = bookContext.Book;
        Assert.NotNull(bookContext.FileFormat);
        Assert.Equal("Deutsch", book.MotherTongue);
        Assert.Equal("Englisch", book.ForeignLanguage);
        Assert.Equal(PracticeMode.AskForForeignLanguage, book.PracticeMode);
        Assert.Equal(113, book.Words.Count);
        Assert.Contains(book.Words, word => word.ForeignLanguage.Any(synonym => synonym.Practices.Count > 0));
    }

    [Fact]
    public async Task TestReadVhf2()
    {
        await using BookContext bookContext =
            await bookStorage.OpenAsync(Path.Join("Resources", "Year 12.vhf"), tempPath).ConfigureAwait(false);

        Book book = bookContext.Book;
        Assert.NotNull(bookContext.FileFormat);
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
        Book expected = GenerateSampleVhf1Book();
        string path = Path.Combine(Path.GetTempPath(), "Vocup_751e0198-5ed8-439d-9041-efb3d594c400.vhf");

        await using (BookContext sampleContext = new(expected, BookFileFormat.Vhf1, settings)
        {
            VhrCode = "o5xqm7rdg6y9fecs9ykuuckv",
            FileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.Read)
        })
            await bookStorage.SaveAsync(sampleContext, tempPath).ConfigureAwait(false);

        await using (BookContext bookContext = await bookStorage.OpenAsync(path, tempPath).ConfigureAwait(false))
        {
            Assert.Equal(BookFileFormat.Vhf1, bookContext.FileFormat);
            Assert.Equal("o5xqm7rdg6y9fecs9ykuuckv", bookContext.VhrCode);

            Assert.Equal(expected, bookContext.Book);
        }

        File.Delete(path);
        File.Delete(Path.Combine(Path.GetTempPath(), "o5xqm7rdg6y9fecs9ykuuckv.vhr"));
    }

    [Fact]
    public async Task TestWriteReadVhf2()
    {
        Book expected = GenerateSampleVhf2Book();
        string path = Path.Combine(Path.GetTempPath(), "Vocup_d3afa6cf-a041-489f-8f39-aea5ed1c0ec5.vhf");

        await using (BookContext sampleContext = new(expected, BookFileFormat.Vhf2, settings)
        {
            FileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.Read)
        })
            await bookStorage.SaveAsync(sampleContext, tempPath).ConfigureAwait(false);

        await using (BookContext bookContext = await bookStorage.OpenAsync(path, tempPath).ConfigureAwait(false))
        {
            Assert.Equal(BookFileFormat.Vhf2, bookContext.FileFormat);
            Assert.Null(bookContext.VhrCode);
            Assert.Equal(expected, bookContext.Book);
        }

        File.Delete(path);
    }

    private Book GenerateSampleVhf1Book()
    {
        var pratice2 = new[]
        {
            // Vocup v1 file formats do not support seconds or timezones of practices
            new Practice(new DateTime(2020, 8, 19, 16, 17, 0), PracticeResult2.Correct)
        };

        var book = new Book("Deutsch", "Englisch", PracticeMode.AskForForeignLanguage);
        book.Words.Add(new(new[] { new Synonym("Katze", settings) }, new[] { new Synonym("cat", settings) }, book, settings));
        book.Words.Add(new(new[] { new Synonym("Farbe", settings) }, new[]
        {
            new Synonym("colour", Array.Empty<string>(), pratice2, settings),
            new Synonym("color", Array.Empty<string>(), pratice2, settings)
        }, book, settings));
        return book;
    }
    
    private Book GenerateSampleVhf2Book()
    {
        var pratice2 = new[]
        {
            new Practice(new DateTimeOffset(2020, 8, 19, 16, 17, 13, TimeSpan.FromHours(1)), PracticeResult2.Correct)
        };

        var book = new Book("Deutsch", "Englisch", PracticeMode.AskForForeignLanguage);
        book.Words.Add(new(new[] { new Synonym("Katze", settings) }, new[] { new Synonym("cat", settings) }, book, settings));
        book.Words.Add(new(new[] { new Synonym("Farbe", settings) }, new[]
        {
            new Synonym("colour", new[] { "BE" }, pratice2, settings),
            new Synonym("color", new[] { "AE" }, pratice2, settings)
        }, book, settings));
        return book;
    }
}
