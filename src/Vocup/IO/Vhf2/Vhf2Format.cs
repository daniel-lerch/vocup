using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO.Vhf2;

public class Vhf2Format : BookFileFormat
{
    private const string headerFileName = "VOCUP VOCABULARY BOOK";
    private const string bookFileName = "book.2.json";
    private readonly JsonSerializerOptions options;
    private readonly Version fileVersion;

    public static Vhf2Format Instance { get; } = new();

    private Vhf2Format()
    {
        options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        options.Converters.Add(new PracticeModeConverter());
        options.Converters.Add(new PracticeResultConverter());

        fileVersion = new Version(2, 0);
    }

    public async ValueTask Read(Stream stream, VocabularyBook book)
    {
        try
        {
            using ZipArchive archive = new(stream, ZipArchiveMode.Read, leaveOpen: true);

            ZipArchiveEntry versionFile = archive.GetEntry(headerFileName)
                ?? throw new VhfFormatException(VhfError.InvalidVersion);

            Version? version;

            using (StreamReader reader = new(versionFile.Open(), Encoding.UTF8))
            {
                string? line = await reader.ReadLineAsync().ConfigureAwait(false);
                if (line == null || !Version.TryParse(line, out version))
                    throw new VhfFormatException(VhfError.InvalidVersion);
            }

            if (version.Major > fileVersion.Major) throw new VhfFormatException(VhfError.UpdateRequired);

            ZipArchiveEntry? bookFile = archive.GetEntry(bookFileName)
                ?? throw new VhfFormatException(VhfError.EmptyArchive);

            JsonBook jsonBook;

            using (Stream bookStream = bookFile.Open())
            {
                jsonBook = await JsonSerializer.DeserializeAsync<JsonBook>(bookStream, options).ConfigureAwait(false)
                    ?? throw new VhfFormatException(VhfError.InvalidJsonBook);
            }

            book.MotherTongue = jsonBook.MotherTongue;
            book.ForeignLang = jsonBook.ForeignLanguage;
            book.PracticeMode = jsonBook.PracticeMode;
            foreach (JsonWord jsonWord in jsonBook.Words)
            {
                VocabularyWord word = new(jsonWord.MotherTongue, jsonWord.ForeignLang)
                {
                    ForeignLangSynonym = jsonWord.ForeignLangSynonym,
                    PracticeStateNumber = jsonWord.PracticeStateNumber,
                    PracticeDate = jsonWord.PracticeDate
                };
                book.Words.Add(word);
            }
        }
        catch (InvalidDataException ex)
        {
            throw new VhfFormatException(VhfError.CorruptedArchive, ex);
        }
        catch (JsonException ex)
        {
            throw new VhfFormatException(VhfError.InvalidJsonBook, ex);
        }
    }

    public async ValueTask Write(Stream stream, VocabularyBook book)
    {
        using ZipArchive archive = new(stream, ZipArchiveMode.Create, leaveOpen: true);

        ZipArchiveEntry versionFile = archive.CreateEntry(headerFileName);

        using (StreamWriter writer = new(versionFile.Open(), Encoding.UTF8))
        {
            await writer.WriteLineAsync(fileVersion.ToString()).ConfigureAwait(false);
            await writer.FlushAsync().ConfigureAwait(false);
        }

        JsonBook jsonBook = new(book.MotherTongue, book.ForeignLang, book.PracticeMode, []);

        foreach (VocabularyWord word in book.Words)
        {
            JsonWord jsonWord = new(word.MotherTongue, word.ForeignLang, word.ForeignLangSynonym, word.PracticeStateNumber, word.PracticeDate);
            jsonBook.Words.Add(jsonWord);
        }

        ZipArchiveEntry bookFile = archive.CreateEntry(bookFileName);
        using Stream bookStream = bookFile.Open();
        await JsonSerializer.SerializeAsync(bookStream, jsonBook, options).ConfigureAwait(false);
    }

    private record JsonBook(string MotherTongue, string ForeignLanguage, PracticeMode PracticeMode, List<JsonWord> Words);

    private record JsonWord(string MotherTongue, string ForeignLang, string? ForeignLangSynonym, int PracticeStateNumber, DateTime PracticeDate);

    private class PracticeModeConverter : JsonConverter<PracticeMode>
    {
        public override PracticeMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString() ?? throw new InvalidDataException("A PracticeMode cannot be null");
            return (PracticeMode)Enum.Parse(typeToConvert, value);
        }

        public override void Write(Utf8JsonWriter writer, PracticeMode value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    private class PracticeResultConverter : JsonConverter<PracticeResult>
    {
        public override PracticeResult Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString() ?? throw new InvalidDataException("A PracticeResult cannot be null");
            return (PracticeResult)Enum.Parse(typeToConvert, value);
        }

        public override void Write(Utf8JsonWriter writer, PracticeResult value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
