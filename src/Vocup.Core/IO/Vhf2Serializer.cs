using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO
{
    internal class Vhf2Serializer : BookSerializer
    {
        private const string versionQuote = "VOCUP VOCABULARY BOOK ";
        private readonly JsonSerializerOptions options;
        private readonly Version version;

        public Vhf2Serializer()
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

        public override BookFileFormat FileFormat => BookFileFormat.Vhf_2_0;

        internal override async Task<Book> ReadBookAsync(FileStream stream, string? vhrPath)
        {
            try
            {
                using var archive = new ZipArchive(stream, ZipArchiveMode.Read);

                ZipArchiveEntry versionFile = archive.GetEntry("VERSION");
                if (versionFile == null)
                    throw new VhfFormatException(VhfError.InvalidVersion);

                Version version;

                using (var reader = new StreamReader(versionFile.Open(), false))
                {
                    string line = await reader.ReadLineAsync().ConfigureAwait(false);
                    if (!line.StartsWith(versionQuote, StringComparison.Ordinal)
                        || !Version.TryParse(line.Substring(versionQuote.Length), out version))
                        throw new VhfFormatException(VhfError.InvalidVersion);
                }

                if (version > this.version) throw new VhfFormatException(VhfError.UpdateRequired);

                ZipArchiveEntry bookFile = archive.GetEntry("book.json");
                if (bookFile == null)
                    throw new VhfFormatException(VhfError.EmptyArchive);

                using (Stream bookStream = bookFile.Open())
                {
                    Book? book = await JsonSerializer.DeserializeAsync<Book>(bookStream, options).ConfigureAwait(false);
                    if (book is null) throw new VhfFormatException(VhfError.InvalidJsonBook);
                    book.Serializer = this;
                    return book;
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

        internal override async Task WriteBookAsync(FileStream stream, Book book, string? vhrPath)
        {
            using var archive = new ZipArchive(stream, ZipArchiveMode.Create);

            ZipArchiveEntry versionFile = archive.CreateEntry("VERSION");
            using (var writer = new StreamWriter(versionFile.Open()))
            {
                await writer.WriteLineAsync(versionQuote + version.ToString()).ConfigureAwait(false);
                await writer.FlushAsync().ConfigureAwait(false);
            }

            ZipArchiveEntry bookFile = archive.CreateEntry("book.json");
            using (Stream bookStream = bookFile.Open())
            {
                await JsonSerializer.SerializeAsync(bookStream, book, options).ConfigureAwait(false);
            }
        }
    }
}
