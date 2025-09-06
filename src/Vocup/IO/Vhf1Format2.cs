using Avalonia.Platform.Storage;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO;

public class Vhf1Format2 : BookFileFormat2
{
    public static Vhf1Format2 Instance { get; } = new();

    private Vhf1Format2() { }

    public async ValueTask Read(IStorageFile file, Stream stream, Book book, string? vhrPath)
    {
        string decrypted = await ReadAndDecrypt(stream).ConfigureAwait(false);
        using StringReader reader = new(decrypted);

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

        book.MotherTongue = motherTongue;
        book.ForeignLanguage = foreignLanguage;

        while (true)
        {
            string? line = reader.ReadLine();
            if (line == null) break;
            string[] columns = line.Split('#');
            if (columns.Length != 3)
            {
                throw new VhfFormatException(VhfError.InvalidRow);
            }
            if (string.IsNullOrWhiteSpace(columns[2]))
                book.Words.Add(new([columns[0]], [columns[1]]));
            else
                book.Words.Add(new([columns[0]], [columns[1], columns[2]]));
        }

        book.PracticeMode = PracticeMode.AskForForeignLang; // Default practice mode, may be overwritten when reading results
        book.File = file;

        if (!string.IsNullOrEmpty(vhrPath) && !string.IsNullOrEmpty(vhrCode) && stream is FileStream fileStream)
        {
            // Read results from .vhr file
            await ReadResults(book, fileStream.Name, vhrCode, vhrPath).ConfigureAwait(false);
        }
    }

    public override async ValueTask Write(IStorageFile file, Stream stream, Book book, string vhrPath, bool includeResults)
    {
        StringBuilder content = new();
        content.AppendLine("1.0");
        content.AppendLine(book.VhrCode);
        content.AppendLine(book.MotherTongue);
        content.AppendLine(book.ForeignLanguage);

        foreach (Word word in book.Words)
        {
            content.Append(word.MotherTongue[0].Value);
            content.Append('#');
            content.Append(word.ForeignLanguage[0].Value);
            content.Append('#');
            if (word.ForeignLanguage.Count > 1)
                content.Append(word.ForeignLanguage[1].Value);
            content.AppendLine();
        }

        await EncryptAndWrite(stream, content.ToString());

        if (includeResults && !string.IsNullOrEmpty(book.VhrCode))
        {
            // Write results to .vhr file
            string path = file.TryGetLocalPath() ?? throw new NotSupportedException("Unable to get the path of 'file'");
            await WriteResults(book, path, book.VhrCode, vhrPath);
        }

        book.File = file;
    }

    public string GenerateVhrCode()
    {
        const string chars = "0123456789abcdefghijklmnopqrstuvwxyz";

        Span<char> code = stackalloc char[24];
        Random random = Random.Shared;

        for (int i = 0; i < code.Length; i++)
        {
            int charIdx = random.Next(chars.Length);
            {
                code[i] = chars[charIdx];
            }
        }

        return new string(code);
    }

    private async ValueTask ReadResults(Book book, string fileName, string vhrCode, string vhrPath)
    {
        try
        {
            using FileStream file = new(Path.Combine(vhrPath, vhrCode + ".vhr"), FileMode.Open, FileAccess.Read, FileShare.Read);
            string decrypted = await ReadAndDecrypt(file).ConfigureAwait(false);
            using StringReader reader = new(decrypted);

            string? path = reader.ReadLine();
            string? mode = reader.ReadLine();

            if (string.IsNullOrWhiteSpace(path) ||
                string.IsNullOrWhiteSpace(mode) || !int.TryParse(mode, out int imode) || !((PracticeMode)imode).IsValid())
            {
                // Delete .vhr files with corrupt header
                TryDeleteVhrFile(vhrCode, vhrPath);
                return;
            }

            var results = new List<(int stateNumber, DateTime date)>();

            while (true)
            {
                string? line = reader.ReadLine();
                if (line == null) break;
                string[] columns = line.Split('#');
                if (columns.Length != 2 || !int.TryParse(columns[0], out int state) || state < 0)
                {
                    // Delete .vhr files with corrupt entries
                    TryDeleteVhrFile(vhrCode, vhrPath);
                    return;
                }
                DateTime time = default;
                if (!string.IsNullOrWhiteSpace(columns[1])
                    && !DateTime.TryParseExact(columns[1], "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
                {
                    // Delete .vhr files with corrupt entries
                    TryDeleteVhrFile(vhrCode, vhrPath);
                    return;
                }
                results.Add((state, time));
            }

            if (book.Words.Count != results.Count)
            {
                // Delete .vhr files that are not in sync anymore.
                // This can only happen when using a cloud storage for .vhf files but not .vhr files.
                // When using save as or by copying a file manually, a new .vhr file will be created.
                TryDeleteVhrFile(vhrCode, vhrPath);
                return;
            }

            FileInfo vhfInfo = new(fileName);
            FileInfo pathInfo = new(path);

            if (!vhfInfo.FullName.Equals(pathInfo.FullName, StringComparison.OrdinalIgnoreCase) && pathInfo.Exists)
            {
                vhrCode = GenerateVhrCode(); // Save new results file if the old one is in use by another file
            }

            book.PracticeMode = (PracticeMode)imode;

            for (int i = 0; i < book.Words.Count; i++)
            {
                Word word = book.Words[i];

                // Same practice history for all synonyms. Using the same references is fine as they are immutable.
                var motherTonguePracticeHistory = GeneratePracticeHistory(results[i].stateNumber, results[i].date, book.PracticeMode != PracticeMode.AskForForeignLang);
                var foreignLanguagePracticeHistory = GeneratePracticeHistory(results[i].stateNumber, results[i].date, book.PracticeMode != PracticeMode.AskForMotherTongue);
                
                foreach (Synonym motherTongue in word.MotherTongue)
                {
                    motherTongue.Practices.AddRange(motherTonguePracticeHistory);
                }
                foreach (Synonym foreignLanguage in word.ForeignLanguage)
                {
                    foreignLanguage.Practices.AddRange(foreignLanguagePracticeHistory);
                }
            }

            book.VhrCode = vhrCode;
        }
        catch (Exception ex) when (ex is IOException || ex is VhfFormatException)
        {
            // Delete corrupt .vhr files
            TryDeleteVhrFile(vhrCode, vhrPath);
        }
    }

    private async ValueTask WriteResults(Book book, string fileName, string vhrCode, string vhrPath)
    {
        string results;

        using (StringWriter writer = new())
        {
            PracticeMode mode = book.PracticeMode switch
            {
                PracticeMode.AskForBothMixed => PracticeMode.AskForForeignLang,
                _ => book.PracticeMode
            };

            writer.WriteLine(fileName);
            writer.Write((int)mode);

            foreach (Word word in book.Words)
            {
                writer.WriteLine();

                writer.Write(word.GetPracticeStateNumber(mode));
                writer.Write('#');
                DateTimeOffset lastPracticeDate = word.GetLastPracticeDate(mode);
                if (lastPracticeDate != default)
                    writer.Write(lastPracticeDate.ToString("dd.MM.yyyy HH:mm"));
            }

            results = writer.ToString();
        }

        string absoluteFileName = Path.Combine(vhrPath, vhrCode + ".vhr");
        using FileStream file = new(absoluteFileName, FileMode.Create, FileAccess.Write, FileShare.None);
        await EncryptAndWrite(file, results);
    }

    private static async ValueTask<string> ReadAndDecrypt(Stream stream)
    {
        try
        {
            byte[] ciphertext;
            using (StreamReader reader = new(stream, Encoding.UTF8))
                ciphertext = Convert.FromBase64String(await reader.ReadToEndAsync());

            using MemoryStream plainstream = new();
            using DES csp = DES.Create();
            using ICryptoTransform transform = csp.CreateDecryptor(
                [1, 8, 3, 4, 3, 2, 7, 1],
                [3, 1, 3, 4, 6, 3, 4, 8]);
            using CryptoStream cipherstream = new(plainstream, transform, CryptoStreamMode.Write);
            cipherstream.Write(ciphertext, 0, ciphertext.Length);
            cipherstream.FlushFinalBlock();
            return Encoding.UTF8.GetString(plainstream.ToArray());
        }
        catch (Exception ex) when (ex is FormatException || ex is CryptographicException)
        {
            throw new VhfFormatException(VhfError.InvalidCiphertext, ex);
        }
    }

    private static async ValueTask EncryptAndWrite(Stream stream, string content)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(content);
        using StreamWriter writer = new(stream, Encoding.UTF8);
        using MemoryStream cipherstream = new();
        using DES csp = DES.Create();
        using ICryptoTransform transform = csp.CreateEncryptor(
            [1, 8, 3, 4, 3, 2, 7, 1],
            [3, 1, 3, 4, 6, 3, 4, 8]);
        using CryptoStream plainstream = new(cipherstream, transform, CryptoStreamMode.Write);
        plainstream.Write(buffer, 0, buffer.Length);
        plainstream.FlushFinalBlock();
        await writer.WriteAsync(Convert.ToBase64String(cipherstream.ToArray()));
    }
}
