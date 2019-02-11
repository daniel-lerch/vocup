using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocup.Properties;

namespace Vocup.Models
{
    public class BackupMeta
    {
        public List<BookMeta> Books { get; }
        public List<string> Results { get; }
        public List<string> SpecialChars { get; }

        public class BookMeta
        {
            public BookMeta(int fileId, string vhfPath, string vhrCode)
            {
                FileId = fileId;
                VhfPath = vhfPath;
                VhrCode = vhrCode;
            }

            public int FileId { get; }
            public string VhfPath { get; }
            public string VhrCode { get; }
        }

        public void Write(ZipArchive archive)
        {
            var books = archive.CreateEntry("vhf_vhr.log");
            using (StreamWriter writer = new StreamWriter(books.Open()))
            {
                foreach (BookMeta book in Books)
                {
                    writer.Write(book.FileId);
                    writer.Write('|');
                    writer.Write(book.VhfPath);
                    writer.Write('|');
                    writer.Write(book.VhrCode);
                    writer.WriteLine();
                }
            }

            var results = archive.CreateEntry("vhr.log");
            using (StreamWriter writer = new StreamWriter(results.Open()))
            {
                foreach (string result in Results)
                {
                    writer.WriteLine(result);
                }
            }

            var chars = archive.CreateEntry("chars.log");
            using (StreamWriter writer = new StreamWriter(chars.Open()))
            {
                foreach (string @char in SpecialChars)
                {
                    writer.WriteLine(@char);
                }
            }
        }

        public static bool TryRead(ZipArchive archive, out BackupMeta backup)
        {
            backup = new BackupMeta();
            try
            {
                var books = archive.GetEntry("vhf_vhr.log");
                if (books == null) return false;

                using (StreamReader reader = new StreamReader(books.Open()))
                {
                    while (true)
                    {
                        string line = reader.ReadLine();
                        if (string.IsNullOrEmpty(line)) break;
                        string[] parts = line.Split('|');
                        if (parts.Length < 3)
                            continue; // Skip invalid item
                        // TODO: Read list
                    }
                }

                // TODO: Read result and chars lists as well

                return true;
            }
            catch (Exception ex)
            {
                // TODO: Show exception message
                return false;
            }
        }

        public static string ShrinkPath(string path)
        {
            return path
                .Replace(Settings.Default.VhfPath, "%vhf%")
                .Replace(Settings.Default.VhrPath, "%vhr%")
                .Replace(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "%personal%")
                .Replace(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "%desktop%")
                .Replace(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "%program%")
                .Replace(Environment.GetFolderPath(Environment.SpecialFolder.System), "%system%");
        }

        public static string ExpandPath(string path)
        {
            return path
                .Replace("%vhf%", Settings.Default.VhfPath)
                .Replace("%vhr%", Settings.Default.VhrPath)
                .Replace("%personal%", Environment.GetFolderPath(Environment.SpecialFolder.Personal))
                .Replace("%desktop%", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory))
                .Replace("%program%", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles))
                .Replace("%system%", Environment.GetFolderPath(Environment.SpecialFolder.System));
        }
    }
}
