using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Platform.Storage;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Vocup.IO;
using Vocup.Models;
using Xunit;

namespace Vocup.UnitTests;

public class BookFileFormat2Tests
{
    private readonly Window topLevel;

    public BookFileFormat2Tests()
    {
        topLevel = new Window();
    }

    private async ValueTask<IStorageFile> GetFileFromPathAsync(string path)
    {
        IStorageFile? file = await topLevel.StorageProvider.TryGetFileFromPathAsync(path)
            ?? throw new FileNotFoundException("Test file not found.", path);
        return file;
    }

    [AvaloniaFact]
    public async Task TestRead_Empty()
    {
        IStorageFile file = await GetFileFromPathAsync(Path.Join("Resources", "Vhf_Empty.vhf"));
        Assert.NotNull(file);
        Book book = new();
        var ex = await Assert.ThrowsAsync<VhfFormatException>(async () =>
            await BookFileFormat2.DetectAndRead(file, book, "Resources"));
        Assert.Equal(VhfError.InvalidVersion, ex.ErrorCode);
    }

    [AvaloniaFact]
    public async Task TestReadVhf1()
    {
        string vhrPath = "Resources";
        string path = Path.Join("Resources", "Year 11 (vhf1).vhf");
        IStorageFile file = await GetFileFromPathAsync(path);
        Book book = new();
        await BookFileFormat2.DetectAndRead(file, book, vhrPath);

        Assert.Equal("Deutsch", book.MotherTongue);
        Assert.Equal("Englisch", book.ForeignLanguage);
        Assert.Equal(113, book.Words.Count);
        Assert.NotNull(book.File);
        Assert.Equal(file.Path, book.File.Path);
        Assert.Equal("2jgh9u3tuPCfYLxhhCJXGyPN", book.VhrCode);
        Assert.Equal(PracticeMode.AskForForeignLang, book.PracticeMode);
    }

    [AvaloniaFact]
    public async Task TestReadVhf1_InvalidBase64()
    {
        string path = Path.Join("Resources", "Vhf1_InvalidBase64.vhf");
        IStorageFile file = await GetFileFromPathAsync(path);
        Book book = new();
        var ex = await Assert.ThrowsAsync<VhfFormatException>(async () =>
            await BookFileFormat2.DetectAndRead(file, book, "Resources"));
        Assert.Equal(VhfError.InvalidCiphertext, ex.ErrorCode);
    }

    [AvaloniaFact]
    public async Task TestReadVhf1_RandomBase64()
    {
        string path = Path.Join("Resources", "Vhf1_RandomBase64.vhf");
        IStorageFile file = await GetFileFromPathAsync(path);
        Book book = new();
        var ex = await Assert.ThrowsAsync<VhfFormatException>(async () =>
            await BookFileFormat2.DetectAndRead(file, book, "Resources"));
        Assert.Equal(VhfError.InvalidCiphertext, ex.ErrorCode);
    }

    [AvaloniaFact]
    public async Task TestWriteReadVhf1()
    {
        string tempPath = Path.GetTempPath();
        string path = GetTempFilePath();
        string vhrCode = "o5xqm7rdg6y9fecs9ykuuckv";
        Book original = GenerateSampleBook();
        original.VhrCode = vhrCode;

        IStorageFile file;

        using (FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            file = await GetFileFromPathAsync(path); // Wait until the file is created
            await BookFileFormat2.Vhf1.Write(file, stream, original, tempPath, includeResults: true);
        }

        Assert.NotNull(original.File);

        Book actual = new();
        await BookFileFormat2.DetectAndRead(file, actual, tempPath);

        Assert.Equal(original.MotherTongue, actual.MotherTongue);
        Assert.Equal(original.ForeignLanguage, actual.ForeignLanguage);
        Assert.Equal(original.Words.Count, actual.Words.Count);
        Assert.Equal(original.Words[1].ForeignLanguage[0].Practices[0].Result, actual.Words[1].ForeignLanguage[0].Practices[0].Result);
        Assert.Equal(original.Words[1].ForeignLanguage[0].Practices[0].Date, actual.Words[1].ForeignLanguage[0].Practices[0].Date);
        Assert.Equal(original.Words[1].ForeignLanguage[1].Practices[0].Result, actual.Words[1].ForeignLanguage[1].Practices[0].Result);
        Assert.Equal(original.Words[1].ForeignLanguage[1].Practices[0].Date, actual.Words[1].ForeignLanguage[1].Practices[0].Date);
        Assert.NotNull(actual.File);
        Assert.Equal(original.File.Path, actual.File.Path);
        Assert.Equal(original.VhrCode, actual.VhrCode);
        Assert.Equal(original.PracticeMode, actual.PracticeMode);

        File.Delete(path);
        File.Delete(Path.Combine(tempPath, vhrCode + ".vhr"));
    }

    [AvaloniaFact]
    public async Task TestWriteReadVhf1_PracticeModeMixed()
    {
        string tempPath = Path.GetTempPath();
        string path = GetTempFilePath();
        string vhrCode = "ina5ucmjup2sbcioxdsrvqsu";
        Book original = GenerateSampleBook();
        original.PracticeMode = PracticeMode.AskForBothMixed;
        original.VhrCode = vhrCode;

        IStorageFile file;

        using (FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            file = await GetFileFromPathAsync(path); // Wait until the file is created
            await BookFileFormat2.Vhf1.Write(file, stream, original, tempPath, includeResults: true);
        }

        Assert.NotNull(original.File);

        Book actual = new();
        await BookFileFormat2.DetectAndRead(file, actual, tempPath);

        Assert.Equal(original.MotherTongue, actual.MotherTongue);
        Assert.Equal(original.ForeignLanguage, actual.ForeignLanguage);
        Assert.Equal(original.Words.Count, actual.Words.Count);
        Assert.Equal(original.Words[1].ForeignLanguage[0].Practices[0].Result, actual.Words[1].ForeignLanguage[0].Practices[0].Result);
        Assert.Equal(original.Words[1].ForeignLanguage[0].Practices[0].Date, actual.Words[1].ForeignLanguage[0].Practices[0].Date);
        Assert.Equal(original.Words[1].ForeignLanguage[1].Practices[0].Result, actual.Words[1].ForeignLanguage[1].Practices[0].Result);
        Assert.Equal(original.Words[1].ForeignLanguage[1].Practices[0].Date, actual.Words[1].ForeignLanguage[1].Practices[0].Date);
        Assert.NotNull(actual.File);
        Assert.Equal(original.File.Path, actual.File.Path);
        Assert.Equal(original.VhrCode, actual.VhrCode);
        Assert.Equal(PracticeMode.AskForForeignLang, actual.PracticeMode);

        File.Delete(path);
        File.Delete(Path.Combine(tempPath, vhrCode + ".vhr"));
    }

    [AvaloniaFact]
    public async Task TestWriteReadVhf1_WithoutResults()
    {
        string tempPath = Path.GetTempPath();
        string path = GetTempFilePath();
        string vhrCode = "wetwjlvwhspsre4slcb01mwk";
        Book original = GenerateSampleBook();
        original.VhrCode = vhrCode;

        IStorageFile file;

        using (FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            file = await GetFileFromPathAsync(path); // Wait until the file is created
            await BookFileFormat2.Vhf1.Write(file, stream, original, tempPath, includeResults: false);
        }

        Assert.NotNull(original.File);

        Book actual = new();
        await BookFileFormat2.DetectAndRead(file, actual, tempPath);

        Assert.Equal(original.MotherTongue, actual.MotherTongue);
        Assert.Equal(original.ForeignLanguage, actual.ForeignLanguage);
        Assert.Equal(original.Words.Count, actual.Words.Count);
        Assert.Empty(actual.Words[1].ForeignLanguage[0].Practices);
        Assert.Empty(actual.Words[1].ForeignLanguage[1].Practices);
        Assert.NotNull(actual.File);
        Assert.Equal(original.File.Path, actual.File.Path);
        Assert.Null(actual.VhrCode);
        Assert.Equal(PracticeMode.AskForForeignLang, actual.PracticeMode);
        Assert.False(File.Exists(Path.Combine(tempPath, vhrCode + ".vhr")));

        File.Delete(path);
    }

    [AvaloniaFact]
    public async Task TestReadVhf2()
    {
        string path = Path.Join("Resources", "Year 11 (vhf2).vhf");
        IStorageFile file = await GetFileFromPathAsync(path);
        Book book = new();
        await BookFileFormat2.DetectAndRead(file, book, "Resources");

        Assert.Equal("Deutsch", book.MotherTongue);
        Assert.Equal("Englisch", book.ForeignLanguage);
        Assert.Equal(113, book.Words.Count);
        Assert.NotNull(book.File);
        Assert.Equal(file.Path, book.File.Path);
        Assert.Null(book.VhrCode); // VhrCode is not used in vhf2 format
        Assert.Equal(PracticeMode.AskForForeignLang, book.PracticeMode);
    }

    [AvaloniaFact]
    public async Task TestReadVhf2_CompatMode()
    {
        string path = Path.Join("Resources", "Vhf2_CompatMode.vhf");
        IStorageFile file = await GetFileFromPathAsync(path);
        Book book = new();
        await BookFileFormat2.DetectAndRead(file, book, "Resources");

        Assert.Equal("Deutsch", book.MotherTongue);
        Assert.Equal("Englisch", book.ForeignLanguage);
        Assert.Empty(book.Words);
        Assert.NotNull(book.File);
        Assert.Equal(file.Path, book.File.Path);
        Assert.Null(book.VhrCode); // VhrCode is not used in vhf2 format
        Assert.Equal(PracticeMode.AskForForeignLang, book.PracticeMode);
    }

    [AvaloniaFact]
    public async Task TestReadVhf2_UpdateRequired()
    {
        string path = Path.Join("Resources", "Vhf2_UpdateRequired.vhf");
        IStorageFile file = await GetFileFromPathAsync(path);
        Book book = new();
        var ex = await Assert.ThrowsAsync<VhfFormatException>(async () =>
            await BookFileFormat2.DetectAndRead(file, book, "Resources"));
        Assert.Equal(VhfError.UpdateRequired, ex.ErrorCode);
    }

    [AvaloniaFact]
    public async Task TestWriteReadVhf2()
    {
        string tempPath = Path.GetTempPath();
        string path = GetTempFilePath();
        Book original = GenerateSampleBook();

        IStorageFile file;

        using (FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            file = await GetFileFromPathAsync(path); // Wait until the file is created
            await BookFileFormat2.Vhf2.Write(file, stream, original, vhrPath: null!, includeResults: true);
        }

        Assert.NotNull(original.File);

        Book actual = new();
        await BookFileFormat2.DetectAndRead(file, actual, tempPath);

        Assert.Equal(original.MotherTongue, actual.MotherTongue);
        Assert.Equal(original.ForeignLanguage, actual.ForeignLanguage);
        Assert.Equal(original.Words.Count, actual.Words.Count);
        Assert.Equal(original.Words[1].ForeignLanguage[0].Practices[0].Result, actual.Words[1].ForeignLanguage[0].Practices[0].Result);
        Assert.Equal(original.Words[1].ForeignLanguage[0].Practices[0].Date, actual.Words[1].ForeignLanguage[0].Practices[0].Date);
        Assert.Equal(original.Words[1].ForeignLanguage[1].Practices[0].Result, actual.Words[1].ForeignLanguage[1].Practices[0].Result);
        Assert.Equal(original.Words[1].ForeignLanguage[1].Practices[0].Date, actual.Words[1].ForeignLanguage[1].Practices[0].Date);
        Assert.NotNull(actual.File);
        Assert.Equal(original.File.Path, actual.File.Path);
        Assert.Null(actual.VhrCode); // VhrCode is not used in vhf2 format
        Assert.Equal(original.PracticeMode, actual.PracticeMode);

        File.Delete(path);
    }

    [AvaloniaFact]
    public async Task TestWriteReadVhf2_WithoutResults()
    {
        string tempPath = Path.GetTempPath();
        string path = GetTempFilePath();
        Book original = GenerateSampleBook();

        IStorageFile file;

        using (FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            file = await GetFileFromPathAsync(path); // Wait until the file is created
            await BookFileFormat2.Vhf2.Write(file, stream, original, vhrPath: null!, includeResults: false);
        }

        Assert.NotNull(original.File);

        Book actual = new();
        await BookFileFormat2.DetectAndRead(file, actual, tempPath);

        Assert.Equal(original.MotherTongue, actual.MotherTongue);
        Assert.Equal(original.ForeignLanguage, actual.ForeignLanguage);
        Assert.Equal(original.Words.Count, actual.Words.Count);
        Assert.Empty(actual.Words[1].ForeignLanguage[0].Practices);
        Assert.Empty(actual.Words[1].ForeignLanguage[1].Practices);
        Assert.NotNull(actual.File);
        Assert.Equal(original.File.Path, actual.File.Path);
        Assert.Null(actual.VhrCode); // VhrCode is not used in vhf2 format
        Assert.Equal(original.PracticeMode, actual.PracticeMode);

        File.Delete(path);
    }

    private static Book GenerateSampleBook()
    {
        Book book = new()
        {
            MotherTongue = "Deutsch",
            ForeignLanguage = "Englisch",
            PracticeMode = PracticeMode.AskForForeignLang
        };
        book.Words.Add(new(["Katze"], ["cat"]));
        book.Words.Add(new(["Farbe"], ["color", "colour"]));

        // Vocup v1 file formats do not support seconds or timezones of practices
        DateTime practiceDate = new(2025, 9, 2, 16, 52, 0);

        foreach (Synonym synonym in book.Words[1].ForeignLanguage)
        {
            synonym.Practices.Add(new(practiceDate, PracticeResult2.Correct));
        }
        return book;
    }

    private static string GetTempFilePath([CallerMemberName] string? testName = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(testName);
        string tempPath = Path.GetTempPath();
        return Path.Combine(tempPath, $"Vocup_{nameof(BookFileFormat2Tests)}_{testName}.vhf");
    }
}
