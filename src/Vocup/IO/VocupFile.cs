using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Vocup.IO.Internal;

/// <summary>
/// Represents an encrypted, Vocup specific file.
/// </summary>
internal abstract class VocupFile
{
    private static readonly DES csp;

    static VocupFile()
    {
        csp = DES.Create();
        csp.Key = new byte[8] { 1, 8, 3, 4, 3, 2, 7, 1 };
        csp.IV = new byte[8] { 3, 1, 3, 4, 6, 3, 4, 8 };
    }

    /// <summary>
    /// Decrypts Vocup specific file using DES and a hard-coded key.
    /// </summary>
    /// <param name="path"></param>
    /// <returns>The UTF8 encoded plaintext.</returns>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="FileNotFoundException"/>
    /// <exception cref="DirectoryNotFoundException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="FormatException"/>
    /// <exception cref="CryptographicException"/>
    protected string ReadFile(string path)
    {
        byte[] ciphertext;
        using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            if (file.ReadByte() == 0x50 && file.ReadByte() == 0x4B && file.ReadByte() == 0x03 && file.ReadByte() == 0x04)
                throw new NotSupportedException($"The file {path} cannot be opened by Vocup. A later version might be required.");

            file.Position = 0;

            using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
                ciphertext = Convert.FromBase64String(reader.ReadToEnd());
        }

        using MemoryStream plainstream = new MemoryStream();
        using ICryptoTransform transform = csp.CreateDecryptor();
        using CryptoStream cipherstream = new CryptoStream(plainstream, transform, CryptoStreamMode.Write);
        
        cipherstream.Write(ciphertext, 0, ciphertext.Length);
        cipherstream.FlushFinalBlock();
        return Encoding.UTF8.GetString(plainstream.ToArray());
    }

    /// <summary>
    /// Encrypts a Vocup specific file using UTF-8 and DES with a hard-coded key.
    /// </summary>
    /// <param name="path">The path where to save the encrypted file.</param>
    /// <param name="content">The context text that will be encoded with UTF-8 before encryption.</param>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DirectoryNotFoundException"/>
    /// <exception cref="IOException"/>
    protected void WriteFile(string path, string content)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(content);

        using StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);
        using MemoryStream cipherstream = new MemoryStream();
        using ICryptoTransform transform = csp.CreateEncryptor();
        using CryptoStream plainstream = new CryptoStream(cipherstream, transform, CryptoStreamMode.Write);
        
        plainstream.Write(buffer, 0, buffer.Length);
        plainstream.FlushFinalBlock();
        writer.Write(Convert.ToBase64String(cipherstream.ToArray()));
    }
}
