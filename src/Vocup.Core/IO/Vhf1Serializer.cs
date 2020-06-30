using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO
{
    internal class Vhf1Serializer
    {
        public async Task<Book> ReadBookAsync(Stream stream)
        {
            using (StringReader reader = new StringReader(await ReadAndDecryptAsync(stream).ConfigureAwait(false)))
            {
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

                Book book = new Book
                {
                    FileVersion = new Version(1, 0),
                    VhrCode = vhrCode,
                    MotherTongue = motherTongue,
                    ForeignLanguage = foreignLanguage
                };

                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null) break;
                    string[] columns = line.Split('#');
                    if (columns.Length != 3)
                    {
                        throw new VhfFormatException(VhfError.InvalidRow);
                    }
                    Word word = new Word();
                    word.MotherTongue.Add(new Synonym { Value = columns[0] });
                    word.ForeignLanguage.Add(new Synonym { Value = columns[1] });
                    if (!string.IsNullOrWhiteSpace(columns[2]))
                        word.ForeignLanguage.Add(new Synonym { Value = columns[2] });

                    book.Words.Add(word);
                }

                // TODO: Read .vhr file

                return book;
            }
        }

        public Task WriteBookAsync(Stream stream, Book book)
        {
            string content;

            using (StringWriter writer = new StringWriter())
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
                        writer.WriteLine(word.ForeignLanguage[1].Value);
                }

                content = writer.ToString();
            }

            return EncryptAndWriteAsync(stream, content);

            // TODO: Write .vhr file
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
            catch (FormatException ex)
            {
                throw new VhfFormatException(VhfError.InvalidCiphertext, ex);
            }
            catch (CryptographicException ex)
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
