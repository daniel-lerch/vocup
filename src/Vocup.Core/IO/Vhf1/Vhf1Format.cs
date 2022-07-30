using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO.Vhf1;

internal class Vhf1Format : BookFileFormat
{
    public static Vhf1Format Instance { get; } = new Vhf1Format();

    private Vhf1Format() { }

    internal override async ValueTask<BookContext> ReadBookAsync(FileStream stream, string? vhrPath)
    {
        using var reader = new StringReader(await ReadAndDecryptAsync(stream).ConfigureAwait(false));

        string? version = reader.ReadLine();
        string? vhrCode = reader.ReadLine();
        string? motherTongue = reader.ReadLine();
        string? foreignLanguage = reader.ReadLine();

        if (string.IsNullOrWhiteSpace(version) || version != "1.0")
        {
            throw new VhfFormatException(VhfError.InvalidVersion);
        }

        if (vhrCode == null)
        {
            throw new VhfFormatException(VhfError.InvalidVhrCode);
        }

        if (string.IsNullOrWhiteSpace(motherTongue) ||
            string.IsNullOrWhiteSpace(foreignLanguage) ||
            motherTongue == foreignLanguage)
        {
            throw new VhfFormatException(VhfError.InvalidLanguages);
        }

        var words = new List<Word>();

        while (true)
        {
            string? line = reader.ReadLine();
            if (line == null) break;
            string[] columns = line.Split('#');
            if (columns.Length != 3)
            {
                throw new VhfFormatException(VhfError.InvalidRow);
            }
            words.Add(new Word(
                motherTongue: new[] { new Synonym(value: columns[0]) },
                foreignLanguage: string.IsNullOrWhiteSpace(columns[2])
                    ? new[] { new Synonym(value: columns[1]) }
                    : new[] { new Synonym(value: columns[1]), new Synonym(value: columns[2]) }));
        }

        BookContext bookContext = new(new Book(motherTongue, foreignLanguage, words))
        {
            FileFormat = this,
            FileStream = stream,
            VhrCode = vhrCode,
        };

        // Read results from .vhr file

        if (!string.IsNullOrWhiteSpace(bookContext.VhrCode) && !string.IsNullOrWhiteSpace(vhrPath)
            && !await ReadResults(bookContext, stream.Name, vhrPath).ConfigureAwait(false))
        {
            bookContext.VhrCode = null;
        }

        return bookContext;
    }

    internal override async ValueTask WriteBookAsync(BookContext bookContext, string? vhrPath)
    {
        if (bookContext.FileStream == null)
            throw new ArgumentNullException(nameof(bookContext) + "." + nameof(bookContext.FileStream), "You have to select a file before saving");

        string content;

        using (var writer = new StringWriter())
        {
            writer.WriteLine("1.0");
            writer.WriteLine(bookContext.VhrCode);
            writer.WriteLine(bookContext.Book.MotherTongue);
            writer.WriteLine(bookContext.Book.ForeignLanguage);

            foreach (Word word in bookContext.Book.Words)
            {
                writer.Write(word.MotherTongue[0].Value);
                writer.Write('#');
                writer.Write(word.ForeignLanguage[0].Value);
                writer.Write('#');

                if (word.ForeignLanguage.Count > 1)
                    writer.Write(word.ForeignLanguage[1].Value);

                writer.WriteLine();
            }

            content = writer.ToString();
        }

        await EncryptAndWriteAsync(bookContext.FileStream, content).ConfigureAwait(false);

        // Write results to a .vhr file

        if (!string.IsNullOrWhiteSpace(bookContext.VhrCode) && !string.IsNullOrWhiteSpace(vhrPath))
        {
            await WriteResults(bookContext, bookContext.FileStream.Name, vhrPath).ConfigureAwait(false);
        }
    }

    private static async Task<bool> ReadResults(BookContext bookContext, string fileName, string vhrPath)
    {
        try
        {
            using var file = new FileStream(Path.Combine(vhrPath, bookContext.VhrCode + ".vhr"), FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new StringReader(await ReadAndDecryptAsync(file).ConfigureAwait(false));

            string? path = reader.ReadLine();
            string? mode = reader.ReadLine();

            if (string.IsNullOrWhiteSpace(path) ||
                string.IsNullOrWhiteSpace(mode) || !int.TryParse(mode, out int imode) || imode < 1 || imode > 2)
            {
                return false; // Ignore files with invalid header
            }

            var results = new List<(int stateNumber, DateTime date)>();

            while (true)
            {
                string? line = reader.ReadLine();
                if (line == null) break;
                string[] columns = line.Split('#');
                if (columns.Length != 2 || !int.TryParse(columns[0], out int state) || state < 0)
                {
                    return false;
                }
                DateTime time = default;
                if (!string.IsNullOrWhiteSpace(columns[1])
                    && !DateTime.TryParseExact(columns[1], "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
                {
                    return false;
                }
                results.Add((state, time));
            }

            if (bookContext.Book.Words.Count != results.Count)
            {
                return false;
            }

            FileInfo vhfInfo = new FileInfo(fileName);
            FileInfo pathInfo = new FileInfo(path);

            if (!vhfInfo.FullName.Equals(pathInfo.FullName, StringComparison.OrdinalIgnoreCase) && pathInfo.Exists)
            {
                bookContext.GenerateVhrCode(); // Save new results file if the old one is in use by another file
            }

            bookContext.Book.PracticeMode = imode == 2 ? PracticeMode.AskForMotherTongue : PracticeMode.AskForForeignLanguage;

            for (int i = 0; i < bookContext.Book.Words.Count; i++)
            {
                Word word = bookContext.Book.Words[i];

                IList<Synonym> synonyms = bookContext.Book.PracticeMode == PracticeMode.AskForForeignLanguage ?
                    word.ForeignLanguage :
                    word.MotherTongue;

                foreach (Synonym synonym in synonyms)
                {
                    synonym.GeneratePracticeHistory(results[i].stateNumber, results[1].date);
                }
            }

            return true;
        }
        catch (Exception ex) when (ex is IOException || ex is VhfFormatException)
        {
            return false; // Practice results are useful but not necessary so we ignore file not found and all other exceptions
        }
    }

    private static async ValueTask WriteResults(BookContext bookContext, string fileName, string vhrPath)
    {
        string results;

        using (var writer = new StringWriter())
        {
            writer.WriteLine(fileName);
            writer.Write((int)bookContext.Book.PracticeMode + 1);

            foreach (Word word in bookContext.Book.Words)
            {
                writer.WriteLine();

                IList<Synonym> synonyms = bookContext.Book.PracticeMode == PracticeMode.AskForForeignLanguage ?
                    word.ForeignLanguage :
                    word.MotherTongue;

                int minPracticeStateNumber = synonyms.Min(synonym => synonym.GetPracticeStateNumber());
                DateTimeOffset lastPractice = synonyms.Max(synonym => synonym.GetLastPracticeDate());

                writer.Write(minPracticeStateNumber);
                writer.Write('#');
                if (lastPractice != default)
                    writer.Write(lastPractice.DateTime.ToString("dd.MM.yyyy HH:mm"));
            }

            results = writer.ToString();
        }

        string absoluteFileName = Path.Combine(vhrPath, bookContext.VhrCode + ".vhr");
        using FileStream file = new(absoluteFileName, FileMode.Create, FileAccess.Write, FileShare.None);
        await EncryptAndWriteAsync(file, results).ConfigureAwait(false);
    }

    private static async ValueTask<string> ReadAndDecryptAsync(Stream stream)
    {
        try
        {
            byte[] ciphertext;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                ciphertext = Convert.FromBase64String(await reader.ReadToEndAsync().ConfigureAwait(false));

            using MemoryStream plainstream = new MemoryStream();
            using DES csp = DES.Create();
            using ICryptoTransform transform = csp.CreateDecryptor(
                new byte[] { 1, 8, 3, 4, 3, 2, 7, 1 },
                new byte[] { 3, 1, 3, 4, 6, 3, 4, 8 });
            using CryptoStream cipherstream = new CryptoStream(plainstream, transform, CryptoStreamMode.Write);
            cipherstream.Write(ciphertext, 0, ciphertext.Length);
            cipherstream.FlushFinalBlock();
            return Encoding.UTF8.GetString(plainstream.ToArray());
        }
        catch (Exception ex) when (ex is FormatException || ex is CryptographicException)
        {
            throw new VhfFormatException(VhfError.InvalidCiphertext, ex);
        }
    }

    private static async ValueTask EncryptAndWriteAsync(Stream stream, string content)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(content);
        using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
        using MemoryStream cipherstream = new MemoryStream();
        using DES csp = DES.Create();
        using ICryptoTransform transform = csp.CreateEncryptor(
            new byte[] { 1, 8, 3, 4, 3, 2, 7, 1 },
            new byte[] { 3, 1, 3, 4, 6, 3, 4, 8 });
        using CryptoStream plainstream = new CryptoStream(cipherstream, transform, CryptoStreamMode.Write);
        plainstream.Write(buffer, 0, buffer.Length);
        plainstream.FlushFinalBlock();
        await writer.WriteAsync(Convert.ToBase64String(cipherstream.ToArray())).ConfigureAwait(false);
    }
}
