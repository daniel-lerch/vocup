using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using Vocup.Properties;

namespace Vocup.Models;

public class BackupMeta
{
    public List<BookMeta> Books { get; }
    public List<string> Results { get; }
    public List<string> SpecialChars { get; }

    public BackupMeta()
    {
        Books = new List<BookMeta>();
        Results = new List<string>();
        SpecialChars = new List<string>();
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
                    string? line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) break;
                    string[] parts = line.Split('|');
                    if (parts.Length < 3)
                        continue; // Skip invalid item
                    backup.Books.Add(new BookMeta(int.Parse(parts[0]), parts[1], parts[2]));
                }
            }

            var results = archive.GetEntry("vhr.log");
            if (results == null) return false;

            using (StreamReader reader = new StreamReader(results.Open()))
            {
                while (true)
                {
                    string? line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) break;
                    backup.Results.Add(line);
                }
            }

            var chars = archive.GetEntry("chars.log");
            if (chars == null) return false;

            using (StreamReader reader = new StreamReader(chars.Open()))
            {
                while (true)
                {
                    string? line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) break;
                    backup.SpecialChars.Add(line);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(string.Format(Messages.VdpCorruptFile, ex), Messages.VdpCorruptFileT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
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
}
