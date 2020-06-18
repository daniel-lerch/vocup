using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO
{
    internal class Vhf1Reader : BookReader
    {
        public override async Task<Book> ReadBookAsync(Stream stream)
        {
            using (StringReader reader = new StringReader(await ReadAndDecryptAsync(stream).ConfigureAwait(false)))
            {
                string version = reader.ReadLine();
                string vhrCode = reader.ReadLine();
                string motherTongue = reader.ReadLine();
                string foreignLanguage = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(version) || version != "1.0")
                {
                    throw new Vhf1FormatException(Vhf1Error.InvalidVersion);
                }

                if (vhrCode == null)
                {
                    throw new Vhf1FormatException(Vhf1Error.InvalidVhrCode);
                }

                if (string.IsNullOrWhiteSpace(motherTongue) ||
                    string.IsNullOrWhiteSpace(foreignLanguage) ||
                    motherTongue == foreignLanguage)
                {
                    throw new Vhf1FormatException(Vhf1Error.InvalidLanguages);
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
                        throw new Vhf1FormatException(Vhf1Error.InvalidRow);
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

        private static async Task<string> ReadAndDecryptAsync(Stream stream)
        {
            byte[] ciphertext;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                ciphertext = Convert.FromBase64String(await reader.ReadToEndAsync().ConfigureAwait(false));

            using (MemoryStream plainstream = new MemoryStream())
            using (DES csp = DES.Create())
            using (ICryptoTransform transform = csp.CreateDecryptor(
                new byte[] { 1, 8, 3, 4, 3, 2, 7, 1 },
                new byte[8] { 3, 1, 3, 4, 6, 3, 4, 8 }))
            using (CryptoStream cipherstream = new CryptoStream(plainstream, transform, CryptoStreamMode.Write))
            {
                cipherstream.Write(ciphertext, 0, ciphertext.Length);
                cipherstream.FlushFinalBlock();
                return Encoding.UTF8.GetString(plainstream.ToArray());
            }
        }
    }
}
