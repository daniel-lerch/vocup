using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO;

public class Vhf2Format2 : BookFileFormat2
{
    private const string headerFileName = "VOCUP VOCABULARY BOOK";
    private const string bookFileName = "book.2.json";
    private readonly JsonSerializerOptions options;
    private readonly Version maxSupportedFileVersion;

    public static Vhf2Format2 Instance { get; } = new();

    private Vhf2Format2()
    {
        options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        options.Converters.Add(new PracticeModeConverter());
        //options.Converters.Add(new PracticeResultConverter());

        maxSupportedFileVersion = new Version(2, 0);
    }

    public ValueTask Read(Stream stream, Book book)
    {
        try
        {
            using ZipArchive archive = new(stream, ZipArchiveMode.Read, leaveOpen: true);

            ZipArchiveEntry headerFile = archive.GetEntry(headerFileName)
                ?? throw new VhfFormatException(VhfError.InvalidHeader);

            Metadata metadata;

            using (Stream headerStream = headerFile.Open())
            {
                metadata = JsonSerializer.Deserialize<Metadata>(headerStream, options)
                    ?? throw new VhfFormatException(VhfError.InvalidHeader);
            }

            ZipArchiveEntry? bookFile = archive.GetEntry(bookFileName)
                ?? throw new VhfFormatException(VhfError.UpdateRequired);

            JsonBook jsonBook;

            using (Stream bookStream = bookFile.Open())
            {
                jsonBook = JsonSerializer.Deserialize<JsonBook>(bookStream, options)
                    ?? throw new VhfFormatException(VhfError.InvalidJsonBook);
            }

            book.MotherTongue = jsonBook.MotherTongue;
            book.ForeignLanguage = jsonBook.ForeignLanguage;
            book.PracticeMode = jsonBook.PracticeMode;
            //book.FilePath = stream.Name;

            foreach (JsonWord jsonWord in jsonBook.Words)
            {
                Word word;
                if (string.IsNullOrWhiteSpace(jsonWord.ForeignLangSynonym))
                    word = new([jsonWord.MotherTongue], [jsonWord.ForeignLang]);
                else
                    word = new([jsonWord.MotherTongue], [jsonWord.ForeignLang, jsonWord.ForeignLangSynonym]);

                book.Words.Add(word);
            }

            //return metadata.FileVersion <= maxSupportedFileVersion;
            return ValueTask.CompletedTask;
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

    protected override ValueTask Write(Stream stream, Book book, string vhrPath, bool includeResults)
    {
        using (ZipArchive archive = new(stream, ZipArchiveMode.Create, leaveOpen: true))
        {
            ZipArchiveEntry headerFile = archive.CreateEntry(headerFileName);
            using (Stream headerStream = headerFile.Open())
            {
                JsonSerializer.Serialize(headerStream, new Metadata(maxSupportedFileVersion), options);
            }

            JsonBook jsonBook = new(book.MotherTongue, book.ForeignLanguage, book.PracticeMode, []);

            foreach (Word word in book.Words)
            {
                int practiceStateNumber = 0; //= includeResults ? word.PracticeStateNumber : 0;
                DateTime practiceDate = default; //= includeResults ? word.PracticeDate : DateTime.MinValue;

                JsonWord jsonWord = new(
                    word.MotherTongue[0].Value,
                    word.ForeignLanguage[0].Value,
                    word.ForeignLanguage.Count > 1 ? word.ForeignLanguage[1].Value : null,
                    practiceStateNumber,
                    practiceDate);

                jsonBook.Words.Add(jsonWord);
            }

            ZipArchiveEntry bookFile = archive.CreateEntry(bookFileName);
            using Stream bookStream = bookFile.Open();
            JsonSerializer.Serialize(bookStream, jsonBook, options);
        }

        return ValueTask.CompletedTask;

        // Delete vhr file when migrating from vhf1 to vhf2
        //TryDeleteVhrFile(book.VhrCode, vhrPath);

        //book.FilePath = stream.Name;
    }

    private record Metadata(Version FileVersion);

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

    /*
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
    */
}
