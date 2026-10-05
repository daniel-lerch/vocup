using System;
using System.IO;
using System.Threading.Tasks;
using Vocup.IO;
using Vocup.Models;
using Xunit;

namespace Vocup.UnitTests;

public class InitializeServicesFixture : IAsyncDisposable
{
    private readonly IAsyncDisposable disposable;
    public InitializeServicesFixture()
    {
        disposable = Program.InitializeServices();
    }
    public ValueTask DisposeAsync()
    {
        return disposable.DisposeAsync();
    }
}

public class BookFileFormatTests : IClassFixture<InitializeServicesFixture>
{
    private readonly InitializeServicesFixture fixture;

    public BookFileFormatTests(InitializeServicesFixture fixture)
    {
        this.fixture = fixture;
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
        string vhrCode = "o5xqm7rdg6y9fecs9ykuuckv";
        VocabularyBook original = GenerateSampleBook();
        original.VhrCode = vhrCode;

        using (FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None))
            BookFileFormat.Vhf1.Write(stream, original, tempPath, includeResults: true);

        Assert.False(string.IsNullOrEmpty(original.FilePath));

        VocabularyBook actual = new();
        Assert.True(BookFileFormat.DetectAndRead(path, actual, tempPath));

        Assert.Equal(original.MotherTongue, actual.MotherTongue);
        Assert.Equal(original.ForeignLang, actual.ForeignLang);
        Assert.Equal(original.Words.Count, actual.Words.Count);
        Assert.Equal(original.Words[1].PracticeStateNumber, actual.Words[1].PracticeStateNumber);
        Assert.Equal(original.Words[1].PracticeDate, actual.Words[1].PracticeDate);
        Assert.Equal(original.FilePath, actual.FilePath);
        Assert.Equal(original.VhrCode, actual.VhrCode);
        Assert.Equal(original.PracticeMode, actual.PracticeMode);

        File.Delete(path);
        File.Delete(Path.Combine(tempPath, vhrCode + ".vhr"));
    }

    [Fact]
    public void TestWriteReadVhf1_PracticeModeMixed()
    {
        string tempPath = Path.GetTempPath();
        string path = Path.Combine(tempPath, $"Vocup_{nameof(TestWriteReadVhf1_PracticeModeMixed)}.vhf");
        string vhrCode = "ina5ucmjup2sbcioxdsrvqsu";
        VocabularyBook original = GenerateSampleBook();
        original.PracticeMode = PracticeMode.AskForBothMixed;
        original.VhrCode = vhrCode;

        using (FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None))
            BookFileFormat.Vhf1.Write(stream, original, tempPath, includeResults: true);

        Assert.False(string.IsNullOrEmpty(original.FilePath));

        VocabularyBook actual = new();
        Assert.True(BookFileFormat.DetectAndRead(path, actual, tempPath));

        Assert.Equal(original.MotherTongue, actual.MotherTongue);
        Assert.Equal(original.ForeignLang, actual.ForeignLang);
        Assert.Equal(original.Words.Count, actual.Words.Count);
        Assert.Equal(original.Words[1].PracticeStateNumber, actual.Words[1].PracticeStateNumber);
        Assert.Equal(original.Words[1].PracticeDate, actual.Words[1].PracticeDate);
        Assert.Equal(original.FilePath, actual.FilePath);
        Assert.Equal(original.VhrCode, actual.VhrCode);
        Assert.Equal(PracticeMode.AskForForeignLang, actual.PracticeMode);

        File.Delete(path);
        File.Delete(Path.Combine(tempPath, vhrCode + ".vhr"));
    }

    [Fact]
    public void TestWriteReadVhf1_WithoutResults()
    {
        string tempPath = Path.GetTempPath();
        string path = Path.Combine(tempPath, $"Vocup_{nameof(TestWriteReadVhf1_WithoutResults)}.vhf");
        string vhrCode = "wetwjlvwhspsre4slcb01mwk";
        VocabularyBook original = GenerateSampleBook();
        original.VhrCode = vhrCode;

        using (FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None))
            BookFileFormat.Vhf1.Write(stream, original, tempPath, includeResults: false);

        Assert.False(string.IsNullOrEmpty(original.FilePath));

        VocabularyBook actual = new();
        Assert.True(BookFileFormat.DetectAndRead(path, actual, tempPath));

        Assert.Equal(original.MotherTongue, actual.MotherTongue);
        Assert.Equal(original.ForeignLang, actual.ForeignLang);
        Assert.Equal(original.Words.Count, actual.Words.Count);
        Assert.Equal(default, actual.Words[1].PracticeStateNumber);
        Assert.Equal(default, actual.Words[1].PracticeDate);
        Assert.Equal(original.FilePath, actual.FilePath);
        Assert.Null(actual.VhrCode);
        Assert.Equal(PracticeMode.AskForForeignLang, actual.PracticeMode);
        Assert.False(File.Exists(Path.Combine(tempPath, vhrCode + ".vhr")));

        File.Delete(path);
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
        VocabularyBook original = GenerateSampleBook();

        using (FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None))
            BookFileFormat.Vhf2.Write(stream, original, vhrPath: null!, includeResults: true);

        Assert.False(string.IsNullOrEmpty(original.FilePath));

        VocabularyBook actual = new();
        Assert.True(BookFileFormat.DetectAndRead(path, actual, tempPath));

        Assert.Equal(original.MotherTongue, actual.MotherTongue);
        Assert.Equal(original.ForeignLang, actual.ForeignLang);
        Assert.Equal(original.Words.Count, actual.Words.Count);
        Assert.Equal(original.Words[1].PracticeStateNumber, actual.Words[1].PracticeStateNumber);
        Assert.Equal(original.Words[1].PracticeDate, actual.Words[1].PracticeDate);
        Assert.Equal(original.Words[1].CreationTime, actual.Words[1].CreationTime);
        Assert.Equal(original.FilePath, actual.FilePath);
        Assert.Null(actual.VhrCode); // VhrCode is not used in vhf2 format
        Assert.Equal(original.PracticeMode, actual.PracticeMode);

        File.Delete(path);
    }

    [Fact]
    public void TestWriteReadVhf2_WithoutResults()
    {
        string tempPath = Path.GetTempPath();
        string path = Path.Combine(tempPath, $"Vocup_{nameof(TestWriteReadVhf2_WithoutResults)}.vhf");
        VocabularyBook original = GenerateSampleBook();

        using (FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None))
            BookFileFormat.Vhf2.Write(stream, original, vhrPath: null!, includeResults: false);

        Assert.False(string.IsNullOrEmpty(original.FilePath));

        VocabularyBook actual = new();
        Assert.True(BookFileFormat.DetectAndRead(path, actual, tempPath));

        Assert.Equal(original.MotherTongue, actual.MotherTongue);
        Assert.Equal(original.ForeignLang, actual.ForeignLang);
        Assert.Equal(original.Words.Count, actual.Words.Count);
        Assert.Equal(default, actual.Words[1].PracticeStateNumber);
        Assert.Equal(default, actual.Words[1].PracticeDate);
        Assert.Equal(original.Words[1].CreationTime, actual.Words[1].CreationTime);
        Assert.Equal(original.FilePath, actual.FilePath);
        Assert.Null(actual.VhrCode); // VhrCode is not used in vhf2 format
        Assert.Equal(original.PracticeMode, actual.PracticeMode);

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
            PracticeDate = new DateTime(2025, 10, 23, 12, 54, 0),
            CreationTime = new DateTime(2020, 8, 19, 16, 17, 5),
        });
        return book;
    }
}
