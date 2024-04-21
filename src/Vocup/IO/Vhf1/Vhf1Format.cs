using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO.Vhf1;

public class Vhf1Format : BookFileFormat
{
    public static Vhf1Format Instance { get; } = new();

    private Vhf1Format() { }

    public async ValueTask Read(FileStream stream, VocabularyBook book, string vhrPath)
    {
        string decrypted = await ReadAndDecryptAsync(stream).ConfigureAwait(false);
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
        book.ForeignLang = foreignLanguage;

        while (true)
        {
            string? line = reader.ReadLine();
            if (line == null) break;
            string[] columns = line.Split('#');
            if (columns.Length != 3)
            {
                throw new VhfFormatException(VhfError.InvalidRow);
            }
            book.Words.Add(new(columns[0], columns[1])
            {
                ForeignLangSynonym = columns[2],
                Owner = book
            });
        }

        book.FilePath = stream.Name;

        if (!string.IsNullOrEmpty(vhrCode))
        {
            // Read results from .vhr file
            await ReadResults(book, stream.Name, vhrCode, vhrPath).ConfigureAwait(false);
        }
    }

    public async ValueTask Write(FileStream stream, VocabularyBook book, string vhrPath)
    {
        StringBuilder content = new();
        content.AppendLine("1.0");
        content.AppendLine(book.VhrCode);
        content.AppendLine(book.MotherTongue);
        content.AppendLine(book.ForeignLang);

        foreach (VocabularyWord word in book.Words)
        {
            content.Append(word.MotherTongue);
            content.Append('#');
            content.Append(word.ForeignLang);
            content.Append('#');
            content.AppendLine(word.ForeignLangSynonym);
        }

        await EncryptAndWriteAsync(stream, content.ToString()).ConfigureAwait(false);

        if (!string.IsNullOrEmpty(book.VhrCode))
        {
            // Write results to .vhr file
            await WriteResults(book, stream.Name, book.VhrCode, vhrPath).ConfigureAwait(false);
        }
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

    private async ValueTask ReadResults(VocabularyBook book, string fileName, string vhrCode, string vhrPath)
    {
        try
        {
            using FileStream file = new(Path.Combine(vhrPath, vhrCode + ".vhr"), FileMode.Open, FileAccess.Read, FileShare.Read);
            string decrypted = await ReadAndDecryptAsync(file).ConfigureAwait(false);
            using StringReader reader = new(decrypted);

            string? path = reader.ReadLine();
            string? mode = reader.ReadLine();

            if (string.IsNullOrWhiteSpace(path) ||
                string.IsNullOrWhiteSpace(mode) || !int.TryParse(mode, out int imode) || !((PracticeMode)imode).IsValid())
            {
                return; // Ignore files with invalid header
            }

            var results = new List<(int stateNumber, DateTime date)>();

            while (true)
            {
                string? line = reader.ReadLine();
                if (line == null) break;
                string[] columns = line.Split('#');
                if (columns.Length != 2 || !int.TryParse(columns[0], out int state) || state < 0)
                {
                    return;
                }
                DateTime time = default;
                if (!string.IsNullOrWhiteSpace(columns[1])
                    && !DateTime.TryParseExact(columns[1], "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
                {
                    return;
                }
                results.Add((state, time));
            }

            if (book.Words.Count != results.Count)
            {
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
                VocabularyWord word = book.Words[i];
                (word.PracticeStateNumber, word.PracticeDate) = results[i];
            }

            book.VhrCode = vhrCode;
        }
        catch (Exception ex) when (ex is IOException || ex is VhfFormatException)
        {
            // Practice results are useful but not necessary so we ignore file not found and all other exceptions
        }
    }

    private async ValueTask WriteResults(VocabularyBook book, string fileName, string vhrCode, string vhrPath)
    {
        string results;

        using (StringWriter writer = new())
        {
            writer.WriteLine(fileName);
            writer.Write((int)book.PracticeMode);

            foreach (VocabularyWord word in book.Words)
            {
                writer.WriteLine();

                writer.Write(word.PracticeStateNumber);
                writer.Write('#');
                if (word.PracticeDate != default)
                    writer.Write(word.PracticeDate.ToString("dd.MM.yyyy HH:mm"));
            }

            results = writer.ToString();
        }

        string absoluteFileName = Path.Combine(vhrPath, vhrCode + ".vhr");
        using FileStream file = new(absoluteFileName, FileMode.Create, FileAccess.Write, FileShare.None);
        await EncryptAndWriteAsync(file, results).ConfigureAwait(false);
    }

    private static async ValueTask<string> ReadAndDecryptAsync(Stream stream)
    {
        try
        {
            byte[] ciphertext;
            using (StreamReader reader = new(stream, Encoding.UTF8))
                ciphertext = Convert.FromBase64String(await reader.ReadToEndAsync().ConfigureAwait(false));

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

    private static async ValueTask EncryptAndWriteAsync(Stream stream, string content)
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
        await writer.WriteAsync(Convert.ToBase64String(cipherstream.ToArray())).ConfigureAwait(false);
    }
}
