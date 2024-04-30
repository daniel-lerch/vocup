using System;
using System.IO;
using System.Threading.Tasks;
using Vocup.IO;
using Vocup.Models;
using Xunit;

namespace Vocup.UnitTests;

public class BookFileFormatTests : IAsyncDisposable
{
    private readonly IAsyncDisposable disposable;

    public BookFileFormatTests()
    {
        disposable = Program.InitializeServices();
    }

    public ValueTask DisposeAsync()
    {
        return disposable.DisposeAsync();
    }

    [Fact]
    public void TestRead_Empty()
    {
        string path = Path.Join("Resources", "Vhf_Empty.vhf");
        VocabularyBook book = new();
        var ex = Assert.Throws<VhfFormatException>(() => BookFileFormat.DetectAndRead(path, book, "Resources"));
        Assert.Equal(VhfError.InvalidVersion, ex.ErrorCode);
    }

    [Fact]
    public void TestReadVhf1()
    {
        string vhrPath = "Resources";
        string path = Path.Join("Resources", "Year 11 (vhf1).vhf");
        VocabularyBook book = new();
        Assert.True(BookFileFormat.DetectAndRead(path, book, vhrPath));

        Assert.Equal("Deutsch", book.MotherTongue);
        Assert.Equal("Englisch", book.ForeignLang);
        Assert.Equal(113, book.Words.Count);
        Assert.EndsWith(path, book.FilePath);
        Assert.Equal("2jgh9u3tuPCfYLxhhCJXGyPN", book.VhrCode);
        Assert.Equal(PracticeMode.AskForForeignLang, book.PracticeMode);
    }

    [Fact]
    public void TestReadVhf1_InvalidBase64()
    {
        string path = Path.Join("Resources", "Vhf1_InvalidBase64.vhf");
        VocabularyBook book = new();
        var ex = Assert.Throws<VhfFormatException>(() => BookFileFormat.DetectAndRead(path, book, "Resources"));
        Assert.Equal(VhfError.InvalidCiphertext, ex.ErrorCode);
    }

    [Fact]
    public void TestReadVhf1_RandomBase64()
    {
        string path = Path.Join("Resources", "Vhf1_RandomBase64.vhf");
        VocabularyBook book = new();
        var ex = Assert.Throws<VhfFormatException>(() => BookFileFormat.DetectAndRead(path, book, "Resources"));
        Assert.Equal(VhfError.InvalidCiphertext, ex.ErrorCode);
    }

    [Fact]
    public void TestWriteReadVhf1()
    {
        string tempPath = Path.GetTempPath();
        string path = Path.Combine(tempPath, $"Vocup_{nameof(TestWriteReadVhf1)}.vhf");
        VocabularyBook expected = GenerateSampleBook();
        expected.VhrCode = "o5xqm7rdg6y9fecs9ykuuckv";

        using (FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None))
            BookFileFormat.Vhf1.Write(stream, expected, tempPath);

        Assert.False(string.IsNullOrEmpty(expected.FilePath));

        VocabularyBook actual = new();
        Assert.True(BookFileFormat.DetectAndRead(path, actual, tempPath));

        Assert.Equal(expected.MotherTongue, actual.MotherTongue);
        Assert.Equal(expected.ForeignLang, actual.ForeignLang);
        Assert.Equal(expected.Words.Count, actual.Words.Count);
        Assert.Equal(expected.FilePath, actual.FilePath);
        Assert.Equal(expected.VhrCode, actual.VhrCode);
        Assert.Equal(expected.PracticeMode, actual.PracticeMode);

        File.Delete(path);
        File.Delete(Path.Combine(tempPath, "o5xqm7rdg6y9fecs9ykuuckv.vhr"));
    }

    [Fact]
    public void TestWriteReadVhf1_PracticeModeMixed()
    {
        string tempPath = Path.GetTempPath();
        string path = Path.Combine(tempPath, $"Vocup_{nameof(TestWriteReadVhf1)}.vhf");
        VocabularyBook original = GenerateSampleBook();
        original.PracticeMode = PracticeMode.AskForBothMixed;
        original.VhrCode = "ina5ucmjup2sbcioxdsrvqsu";

        using (FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None))
            BookFileFormat.Vhf1.Write(stream, original, tempPath);

        Assert.False(string.IsNullOrEmpty(original.FilePath));

        VocabularyBook actual = new();
        Assert.True(BookFileFormat.DetectAndRead(path, actual, tempPath));

        Assert.Equal(original.MotherTongue, actual.MotherTongue);
        Assert.Equal(original.ForeignLang, actual.ForeignLang);
        Assert.Equal(original.Words.Count, actual.Words.Count);
        Assert.Equal(original.FilePath, actual.FilePath);
        Assert.Equal(original.VhrCode, actual.VhrCode);
        Assert.Equal(PracticeMode.AskForForeignLang, actual.PracticeMode);

        File.Delete(path);
        File.Delete(Path.Combine(tempPath, "o5xqm7rdg6y9fecs9ykuuckv.vhr"));
    }

    [Fact]
    public void TestReadVhf2()
    {
        string path = Path.Join("Resources", "Year 11 (vhf2).vhf");
        VocabularyBook book = new();
        Assert.True(BookFileFormat.DetectAndRead(path, book, "Resources"));

        Assert.Equal("Deutsch", book.MotherTongue);
        Assert.Equal("Englisch", book.ForeignLang);
        Assert.Equal(113, book.Words.Count);
        Assert.EndsWith(path, book.FilePath);
        Assert.Null(book.VhrCode); // VhrCode is not used in vhf2 format
        Assert.Equal(PracticeMode.AskForForeignLang, book.PracticeMode);
    }

    [Fact]
    public void TestReadVhf2_CompatMode()
    {
        string path = Path.Join("Resources", "Vhf2_CompatMode.vhf");
        VocabularyBook book = new();
        Assert.False(BookFileFormat.DetectAndRead(path, book, "Resources"));

        Assert.Equal("Deutsch", book.MotherTongue);
        Assert.Equal("Englisch", book.ForeignLang);
        Assert.Empty(book.Words);
        Assert.EndsWith(path, book.FilePath);
        Assert.Null(book.VhrCode); // VhrCode is not used in vhf2 format
        Assert.Equal(PracticeMode.AskForForeignLang, book.PracticeMode);
    }

    [Fact]
    public void TestReadVhf2_UpdateRequired()
    {
        string path = Path.Join("Resources", "Vhf2_UpdateRequired.vhf");
        VocabularyBook book = new();
        var ex = Assert.Throws<VhfFormatException>(() => BookFileFormat.DetectAndRead(path, book, "Resources"));
        Assert.Equal(VhfError.UpdateRequired, ex.ErrorCode);
    }

    [Fact]
    public void TestWriteReadVhf2()
    {
        string tempPath = Path.GetTempPath();
        string path = Path.Combine(tempPath, $"Vocup_{nameof(TestWriteReadVhf2)}.vhf");
        VocabularyBook expected = GenerateSampleBook();

        using (FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None))
            BookFileFormat.Vhf2.Write(stream, expected, null!);

        Assert.False(string.IsNullOrEmpty(expected.FilePath));

        VocabularyBook actual = new();
        Assert.True(BookFileFormat.DetectAndRead(path, actual, tempPath));

        Assert.Equal(expected.MotherTongue, actual.MotherTongue);
        Assert.Equal(expected.ForeignLang, actual.ForeignLang);
        Assert.Equal(expected.Words.Count, actual.Words.Count);
        Assert.Equal(expected.FilePath, actual.FilePath);
        Assert.Null(actual.VhrCode); // VhrCode is not used in vhf2 format
        Assert.Equal(expected.PracticeMode, actual.PracticeMode);

        File.Delete(path);
    }

    private static VocabularyBook GenerateSampleBook()
    {
        VocabularyBook book = new()
        {
            MotherTongue = "Deutsch",
            ForeignLang = "Englisch",
            PracticeMode = PracticeMode.AskForForeignLang
        };
        book.Words.Add(new("Katze", "cat"));
        book.Words.Add(new("Farbe", "colour")
        {
            ForeignLangSynonym = "color",
            PracticeStateNumber = 2,
            // Vocup v1 file formats do not support seconds or timezones of practices
            PracticeDate = new DateTime(2020, 8, 19, 16, 17, 0)
        });
        return book;
    }
}
