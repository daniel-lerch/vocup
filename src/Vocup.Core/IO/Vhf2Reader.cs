using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO
{
    public class Vhf2Reader : BookReader
    {
        private const string versionQuote = "VOCUP VOCABULARY BOOK ";
        private readonly JsonSerializerOptions options;

        public Vhf2Reader()
        {
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            options.Converters.Add(new PracticeModeConverter());
            options.Converters.Add(new PracticeResultConverter());
        }

        public override async Task<Book> ReadBookAsync(Stream stream)
        {
            using (var archive = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                ZipArchiveEntry versionFile = archive.GetEntry("VERSION");
                if (versionFile == null)
                    throw new VhfFormatException(VhfError.InvalidVersion);

                using (var reader = new StreamReader(versionFile.Open(), false))
                {
                    string line = await reader.ReadLineAsync().ConfigureAwait(false);
                    if (!line.StartsWith(versionQuote, StringComparison.Ordinal)
                        || !Version.TryParse(line.Substring(versionQuote.Length), out Version version))
                        throw new VhfFormatException(VhfError.InvalidVersion);
                }

                // TODO: Handle version

                ZipArchiveEntry bookFile = archive.GetEntry("book.json");
                if (bookFile == null)
                    throw new VhfFormatException(VhfError.BookNotFound);

                using (Stream bookStream = bookFile.Open())
                {
                    return await JsonSerializer.DeserializeAsync<Book>(bookStream, options).ConfigureAwait(false);
                }
            }
        }
    }
}
