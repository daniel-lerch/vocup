using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Vocup.Util
{
    public static class Crypto
    {
        private static readonly DES csp = new DESCryptoServiceProvider
        {
            Key = new byte[8] { 1, 8, 3, 4, 3, 2, 7, 1 },
            IV = new byte[8] { 3, 1, 3, 4, 6, 3, 4, 8 }
        };

        public static string Decrypt(string base64)
        {
            byte[] ciphertext = Convert.FromBase64String(base64);

            using (MemoryStream plainstream = new MemoryStream())
            using (ICryptoTransform transform = csp.CreateDecryptor())
            using (CryptoStream cipherstream = new CryptoStream(plainstream, transform, CryptoStreamMode.Write))
            {
                cipherstream.Write(ciphertext, 0, ciphertext.Length);
                cipherstream.FlushFinalBlock();
                return Encoding.UTF8.GetString(plainstream.ToArray());
            }
        }

        public static string Encrypt(string plaintext)
        {
            byte[] plainbuffer = Encoding.UTF8.GetBytes(plaintext);

            using (MemoryStream cipherstream = new MemoryStream())
            using (ICryptoTransform transform = csp.CreateEncryptor())
            using (CryptoStream plainstream = new CryptoStream(cipherstream, transform, CryptoStreamMode.Write))
            {
                plainstream.Write(plainbuffer, 0, plainbuffer.Length);
                plainstream.FlushFinalBlock();
                return Convert.ToBase64String(cipherstream.ToArray());
            }
        }
    }
}
