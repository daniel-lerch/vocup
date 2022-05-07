using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO
{
    internal class Vhf1Serializer : BookSerializer
    {
        public override BookFileFormat FileFormat => BookFileFormat.Vhf_1_0;

        internal override async Task<Book> ReadBookAsync(FileStream stream, string? vhrPath)
        {
            using var reader = new StringReader(await ReadAndDecryptAsync(stream).ConfigureAwait(false));

            string version = reader.ReadLine();
            string vhrCode = reader.ReadLine();
            string motherTongue = reader.ReadLine();
            string foreignLanguage = reader.ReadLine();

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

            var words = new ObservableCollection<Word>();

            while (true)
            {
                string line = reader.ReadLine();
                if (line == null) break;
                string[] columns = line.Split('#');
                if (columns.Length != 3)
                {
                    throw new VhfFormatException(VhfError.InvalidRow);
                }
                words.Add(new Word(
                    motherTongue: new ObservableCollection<Synonym> { new Synonym(value: columns[0]) },
                    foreignLanguage: string.IsNullOrWhiteSpace(columns[2])
                        ? new ObservableCollection<Synonym> { new Synonym(value: columns[1]) }
                        : new ObservableCollection<Synonym> { new Synonym(value: columns[1]), new Synonym(value: columns[2]) }));
            }

            var book = new Book(motherTongue, foreignLanguage, words)
            {
                Serializer = this,
                VhrCode = vhrCode
            };

            // Read results from .vhr file

            if (!string.IsNullOrWhiteSpace(book.VhrCode) && !string.IsNullOrWhiteSpace(vhrPath)
                && !await ReadResults(book, stream.Name, vhrPath).ConfigureAwait(false))
            {
                book.VhrCode = null;
            }

            return book;
        }

        internal override async Task WriteBookAsync(FileStream stream, Book book, string? vhrPath)
        {
            string content;

            using (var writer = new StringWriter())
            {
                writer.WriteLine("1.0");
                writer.WriteLine(book.VhrCode);
                writer.WriteLine(book.MotherTongue);
                writer.WriteLine(book.ForeignLanguage);

                foreach (Word word in book.Words)
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

            await EncryptAndWriteAsync(stream, content).ConfigureAwait(false);

            // Write results to a .vhr file

            if (!string.IsNullOrWhiteSpace(book.VhrCode) && !string.IsNullOrWhiteSpace(vhrPath))
            {
                await WriteResults(book, stream.Name, vhrPath).ConfigureAwait(false);
            }
        }

        private async Task<bool> ReadResults(Book book, string fileName, string vhrPath)
        {
            try
            {
                using var file = new FileStream(Path.Combine(vhrPath, book.VhrCode + ".vhr"), FileMode.Open, FileAccess.Read, FileShare.Read);
                using var reader = new StringReader(await ReadAndDecryptAsync(file).ConfigureAwait(false));

                string path = reader.ReadLine();
                string mode = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(path) ||
                    string.IsNullOrWhiteSpace(mode) || !int.TryParse(mode, out int imode) || !((PracticeMode)(imode - 1)).IsValid())
                {
                    return false; // Ignore files with invalid header
                }

                var results = new List<(int stateNumber, DateTime date)>();

                while (true)
                {
                    string line = reader.ReadLine();
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

                if (book.Words.Count != results.Count)
                {
                    return false;
                }

                FileInfo vhfInfo = new FileInfo(fileName);
                FileInfo pathInfo = new FileInfo(path);

                if (!vhfInfo.FullName.Equals(pathInfo.FullName, StringComparison.OrdinalIgnoreCase) && pathInfo.Exists)
                {
                    book.GenerateVhrCode(); // Save new results file if the old one is in use by another file
                }

                book.PracticeMode = (PracticeMode)(imode - 1);

                for (int i = 0; i < book.Words.Count; i++)
                {
                    Word word = book.Words[i];

                    IList<Synonym> synonyms = book.PracticeMode == PracticeMode.AskForForeignLanguage ?
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

        private async Task WriteResults(Book book, string fileName, string vhrPath)
        {
            string results;

            using (var writer = new StringWriter())
            {
                writer.WriteLine(fileName);
                writer.Write((int)book.PracticeMode + 1);

                foreach (Word word in book.Words)
                {
                    writer.WriteLine();

                    IList<Synonym> synonyms = book.PracticeMode == PracticeMode.AskForForeignLanguage ?
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

            string absoluteFileName = Path.Combine(vhrPath, book.VhrCode + ".vhr");
            using (var file = new FileStream(absoluteFileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await EncryptAndWriteAsync(file, results).ConfigureAwait(false);
            }
        }

        private static async Task<string> ReadAndDecryptAsync(Stream stream)
        {
            try
            {
                byte[] ciphertext;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    ciphertext = Convert.FromBase64String(await reader.ReadToEndAsync().ConfigureAwait(false));

                using (MemoryStream plainstream = new MemoryStream())
                using (DES csp = DES.Create())
                using (ICryptoTransform transform = csp.CreateDecryptor(
                    new byte[] { 1, 8, 3, 4, 3, 2, 7, 1 },
                    new byte[] { 3, 1, 3, 4, 6, 3, 4, 8 }))
                using (CryptoStream cipherstream = new CryptoStream(plainstream, transform, CryptoStreamMode.Write))
                {
                    cipherstream.Write(ciphertext, 0, ciphertext.Length);
                    cipherstream.FlushFinalBlock();
                    return Encoding.UTF8.GetString(plainstream.ToArray());
                }
            }
            catch (Exception ex) when (ex is FormatException || ex is CryptographicException)
            {
                throw new VhfFormatException(VhfError.InvalidCiphertext, ex);
            }
        }

        private static Task EncryptAndWriteAsync(Stream stream, string content)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            using (MemoryStream cipherstream = new MemoryStream())
            using (DES csp = DES.Create())
            using (ICryptoTransform transform = csp.CreateEncryptor(
                new byte[] { 1, 8, 3, 4, 3, 2, 7, 1 },
                new byte[] { 3, 1, 3, 4, 6, 3, 4, 8 }))
            using (CryptoStream plainstream = new CryptoStream(cipherstream, transform, CryptoStreamMode.Write))
            {
                plainstream.Write(buffer, 0, buffer.Length);
                plainstream.FlushFinalBlock();
                return writer.WriteAsync(Convert.ToBase64String(cipherstream.ToArray()));
            }
        }
    }
}
