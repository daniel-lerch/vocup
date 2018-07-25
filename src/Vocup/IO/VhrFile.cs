﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Properties;

namespace Vocup.IO.Internal
{
    internal class VhrFile : VocupFile
    {
        public bool Read(VocabularyBook book) // TODO: Add exception handling
        {
            FileInfo vhrInfo = new FileInfo(Path.Combine(Settings.Default.path_vhr, book.VhrCode + ".vhr"));
            if (!vhrInfo.Exists)
                return false;

            string plaintext = ReadFile(vhrInfo.FullName);

            using (StringReader reader = new StringReader(plaintext))
            {
                string path = reader.ReadLine();
                string mode = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(path) ||
                    string.IsNullOrWhiteSpace(mode) || !int.TryParse(mode, out int imode) || !((PracticeMode)imode).IsValid())
                {
                    DeleteInvalidFile(vhrInfo);
                    return false;
                }

                List<Tuple<int, DateTime>> results = new List<Tuple<int, DateTime>>();

                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null) break;
                    string[] columns = line.Split('#');
                    if (columns.Length != 2 || !int.TryParse(columns[0], out int state) || !PracticeStateHelper.Parse(state).IsValid())
                    {
                        DeleteInvalidFile(vhrInfo);
                        return false;
                    }
                    DateTime time = DateTime.MinValue;
                    // DateTime.Parse() works with the format dd.MM.yyyy HH:mm
                    if (!string.IsNullOrWhiteSpace(columns[1]) && !DateTime.TryParse(columns[1], out time))
                    {
                        DeleteInvalidFile(vhrInfo);
                        return false;
                    }
                    results.Add(new Tuple<int, DateTime>(state, time));
                }

                bool countMatch = book.Words.Count == results.Count;

                FileInfo vhfInfo = new FileInfo(book.FilePath);
                FileInfo pathInfo = new FileInfo(path);

                if (vhfInfo.FullName.Equals(pathInfo.FullName, StringComparison.OrdinalIgnoreCase))
                {
                    if (!countMatch)
                    {
                        MessageBox.Show(Messages.VhrInvalidRowCount, Messages.VhrInvalidFileT, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        try { vhrInfo.Delete(); } catch { }
                        return false;
                    }
                }
                else
                {
                    if (!countMatch)
                    {
                        MessageBox.Show(Messages.VhrInvalidRowCountAndOtherFile, Messages.VhrInvalidFileT, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (pathInfo.Exists)
                        book.GenerateVhrCode(); // Save new results file if old one is in use by another file

                    book.UnsavedChanges = true;
                }

                book.FilePath = path;
                book.PracticeMode = (PracticeMode)imode;

                for (int i = 0; i < book.Words.Count; i++)
                {
                    VocabularyWord word = book.Words[i];
                    Tuple<int, DateTime> result = results[i];
                    word.PracticeStateNumber = result.Item1;
                    word.PracticeDate = result.Item2;
                }
            }

            return false;
        }

        public bool Write(VocabularyBook book) // TODO: Add exception handling
        {
            string raw;

            using (StringWriter writer = new StringWriter())
            {
                writer.WriteLine(book.FilePath);
                writer.Write((int)book.PracticeMode);

                foreach (VocabularyWord word in book.Words)
                {
                    writer.WriteLine();

                    writer.Write(word.PracticeStateNumber);
                    writer.Write('#');
                    if (word.PracticeDate != DateTime.MinValue)
                        writer.Write(word.PracticeDate.ToString("dd.MM.yyyy HH:mm"));
                }

                raw = writer.ToString();
            }

            WriteFile(Path.Combine(Settings.Default.path_vhr, book.VhrCode + ".vhr"), raw);

            return true;
        }

        private void DeleteInvalidFile(FileInfo info)
        {
            MessageBox.Show(Messages.VhrInvalidFile, Messages.VhrInvalidFileT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            try
            {
                info.Delete();
            }
            catch { }
        }
    }
}
