using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vocup.IO;
using Vocup.Models;
using Xunit;

namespace Vocup.Core.UnitTests.IO;

public class BookStorageTests
{
    private readonly BookStorage bookStorage = new BookStorage();
    private readonly string tempPath = Path.GetTempPath();

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
        Assert.Equal(PracticeMode2.AskForForeignLanguage, book.PracticeMode);
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
        Assert.Equal(PracticeMode2.AskForForeignLanguage, book.PracticeMode);
        Assert.Single(book.Words);
        Assert.Equal(2, book.Words[0].MotherTongue.Count);
        Assert.Equal(2, book.Words[0].ForeignLanguage.Count);
    }

    [Fact]
    public async Task TestWriteReadVhf1()
    {
        Book expected = GenerateSampleVhf1Book();
        string path = Path.Combine(Path.GetTempPath(), "Vocup_751e0198-5ed8-439d-9041-efb3d594c400.vhf");

        await using (BookContext sampleContext = new(expected)
        {
            VhrCode = "o5xqm7rdg6y9fecs9ykuuckv",
            FileFormat = BookFileFormat.Vhf1,
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

        await using (BookContext sampleContext = new(expected)
        {
            FileFormat = BookFileFormat.Vhf2,
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
            new Practice
            {
                // Vocup v1 file formats do not support seconds or timezones of practices
                Date = new DateTime(2020, 8, 19, 16, 17, 0),
                Result = PracticeResult2.Correct
            }
        };

        return new Book("Deutsch", "Englisch", PracticeMode2.AskForForeignLanguage, new[]
        {
            new Word(new[] { new Synonym("Katze") }, new[] { new Synonym("cat") }),
            new Word(new[] { new Synonym("Farbe") }, new[]
            {
                new Synonym("colour", Array.Empty<string>(), pratice2),
                new Synonym("color", Array.Empty<string>(), pratice2)
            })
        });
    }

    private Book GenerateSampleVhf2Book()
    {
        var pratice2 = new[]
        {
            new Practice
            {
                Date = new DateTimeOffset(2020, 8, 19, 16, 17, 13, TimeSpan.FromHours(1)),
                Result = PracticeResult2.Correct
            }
        };

        return new Book("Deutsch", "Englisch", PracticeMode2.AskForForeignLanguage, new[]
        {
            new Word(new[] { new Synonym("Katze") }, new[] { new Synonym("cat") }),
            new Word(new[] { new Synonym("Farbe") }, new[]
            {
                new Synonym("colour", new[] { "BE" }, pratice2),
                new Synonym("color", new[] { "AE" }, pratice2)
            })
        });
    }
}
