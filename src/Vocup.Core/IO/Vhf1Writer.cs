using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO
{
    public class Vhf1Writer : BookWriter
    {
        public override Task WriteBookAsync(Stream stream, Book book)
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

        private static Task EncryptAndWriteAsync(Stream stream, string content)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            using (MemoryStream cipherstream = new MemoryStream())
            using (DES csp = DES.Create())
            using (ICryptoTransform transform = csp.CreateEncryptor(
                new byte[] { 1, 8, 3, 4, 3, 2, 7, 1 },
                new byte[8] { 3, 1, 3, 4, 6, 3, 4, 8 }))
            using (CryptoStream plainstream = new CryptoStream(cipherstream, transform, CryptoStreamMode.Write))
            {
                plainstream.Write(buffer, 0, buffer.Length);
                plainstream.FlushFinalBlock();
                return writer.WriteAsync(Convert.ToBase64String(cipherstream.ToArray()));
            }
        }
    }
}
