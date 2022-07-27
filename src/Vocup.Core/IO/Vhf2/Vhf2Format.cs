﻿using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Threading.Tasks;

namespace Vocup.IO.Vhf2;

internal partial class Vhf2Format : BookFileFormat
{
    private const string versionQuote = "VOCUP VOCABULARY BOOK ";
    private readonly JsonSerializerOptions options;
    private readonly Version version;

    public static Vhf2Format Instance { get; } = new Vhf2Format();

    private Vhf2Format()
    {
        options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        options.Converters.Add(new PracticeModeConverter());
        options.Converters.Add(new PracticeResultConverter());
        version = new Version(2, 0);
    }

    internal override async ValueTask<BookContext> ReadBookAsync(FileStream stream, string? vhrPath)
    {
        try
        {
            using var archive = new ZipArchive(stream, ZipArchiveMode.Read, leaveOpen: true);

            ZipArchiveEntry? versionFile = archive.GetEntry("VERSION");
            if (versionFile == null)
                throw new VhfFormatException(VhfError.InvalidVersion);

            Version? version;

            using (var reader = new StreamReader(versionFile.Open(), false))
            {
                string? line = await reader.ReadLineAsync().ConfigureAwait(false);
                if (line == null
                    || !line.StartsWith(versionQuote, StringComparison.Ordinal)
                    || !Version.TryParse(line.AsSpan(versionQuote.Length), out version))
                    throw new VhfFormatException(VhfError.InvalidVersion);
            }

            if (version > this.version) throw new VhfFormatException(VhfError.UpdateRequired);

            ZipArchiveEntry? bookFile = archive.GetEntry("book.json");
            if (bookFile == null)
                throw new VhfFormatException(VhfError.EmptyArchive);

            JsonBook? jsonBook;
            using (Stream bookStream = bookFile.Open())
            {
                jsonBook = await JsonSerializer.DeserializeAsync<JsonBook>(bookStream, options).ConfigureAwait(false);
            }
            if (jsonBook == null) throw new VhfFormatException(VhfError.InvalidJsonBook);

            return new BookContext(jsonBook.ToBook())
            {
                FileFormat = this,
                FileStream = stream
            };
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

    internal override async ValueTask WriteBookAsync(BookContext bookContext, string? vhrPath)
    {
        if (bookContext.FileStream == null)
            throw new ArgumentNullException(nameof(bookContext) + "." + nameof(bookContext.FileStream), "You have to select a file before saving");

        using var archive = new ZipArchive(bookContext.FileStream, ZipArchiveMode.Create);

        ZipArchiveEntry versionFile = archive.CreateEntry("VERSION");
        using (var writer = new StreamWriter(versionFile.Open()))
        {
            await writer.WriteLineAsync(versionQuote + version.ToString()).ConfigureAwait(false);
            await writer.FlushAsync().ConfigureAwait(false);
        }

        ZipArchiveEntry bookFile = archive.CreateEntry("book.json");
        using Stream bookStream = bookFile.Open();
        await JsonSerializer.SerializeAsync(bookStream, bookContext.Book, options).ConfigureAwait(false);
    }
}
