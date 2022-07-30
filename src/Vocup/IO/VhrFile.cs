using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Models.Legacy;
using Vocup.Properties;

namespace Vocup.IO.Internal;

internal class VhrFile : VocupFile
{
    public bool Read(VocabularyBook book)
    {
        if (string.IsNullOrWhiteSpace(book.VhrCode))
            return false;
        FileInfo vhrInfo = new FileInfo(Path.Combine(Program.Settings.VhrPath, book.VhrCode + ".vhr"));
        if (!vhrInfo.Exists)
            return false;

        string plaintext;
        try
        {
            plaintext = ReadFile(vhrInfo.FullName);
        }
        catch (FormatException)
        {
            DeleteInvalidFile(vhrInfo);
            return false;
        }
        catch (System.Security.Cryptography.CryptographicException)
        {
            DeleteInvalidFile(vhrInfo);
            return false;
        }

        using (StringReader reader = new StringReader(plaintext))
        {
            string? path = reader.ReadLine();
            string? mode = reader.ReadLine();

            if (string.IsNullOrWhiteSpace(path) ||
                string.IsNullOrWhiteSpace(mode) || !int.TryParse(mode, out int imode) || !((PracticeMode)imode).IsValid())
            {
                DeleteInvalidFile(vhrInfo);
                return false;
            }

            List<(int stateNumber, DateTime date)> results = new List<(int stateNumber, DateTime date)>();

            while (true)
            {
                string? line = reader.ReadLine();
                if (line == null) break;
                string[] columns = line.Split('#');
                if (columns.Length != 2 || !int.TryParse(columns[0], out int state) || !PracticeStateHelper.Parse(state, Program.Settings.MaxPracticeCount).IsValid())
                {
                    DeleteInvalidFile(vhrInfo);
                    return false;
                }
                DateTime time = DateTime.MinValue;
                if (!string.IsNullOrWhiteSpace(columns[1]) && !DateTime.TryParseExact(columns[1], "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
                {
                    DeleteInvalidFile(vhrInfo);
                    return false;
                }
                results.Add((state, time));
            }

            bool countMatch = book.Words.Count == results.Count;

            FileInfo vhfInfo = new FileInfo(book.FilePath!);
            FileInfo pathInfo = new FileInfo(path);

            if (vhfInfo.FullName.Equals(pathInfo.FullName, StringComparison.OrdinalIgnoreCase))
            {
                if (!countMatch)
                {
                    MessageBox.Show(Messages.VhrInvalidRowCount, Messages.VhrCorruptFileT, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    try { vhrInfo.Delete(); } catch { }
                    return false;
                }
            }
            else
            {
                if (!countMatch)
                {
                    MessageBox.Show(Messages.VhrInvalidRowCountAndOtherFile, Messages.VhrCorruptFileT, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (pathInfo.Exists)
                    book.GenerateVhrCode(); // Save new results file if the old one is in use by another file

                book.UnsavedChanges = true;
            }

            book.PracticeMode = (PracticeMode)imode;

            for (int i = 0; i < book.Words.Count; i++)
            {
                IVocabularyWord word = book.Words[i];
                (word.PracticeStateNumber, word.PracticeDate) = results[i];
            }
        }

        return true;
    }

    public bool Write(VocabularyBook book)
    {
        string raw;

        using (StringWriter writer = new StringWriter())
        {
            writer.WriteLine(book.FilePath);
            writer.Write((int)book.PracticeMode);

            foreach (IVocabularyWord word in book.Words)
            {
                writer.WriteLine();

                writer.Write(word.PracticeStateNumber);
                writer.Write('#');
                if (word.PracticeDate != DateTime.MinValue)
                    writer.Write(word.PracticeDate.ToString("dd.MM.yyyy HH:mm"));
            }

            raw = writer.ToString();
        }

        try
        {
            WriteFile(Path.Combine(Program.Settings.VhrPath, book.VhrCode + ".vhr"), raw);
        }
        catch (Exception ex)
        {
            MessageBox.Show(string.Format(Messages.VocupFileWriteErrorEx, ex), Messages.VocupFileWriteErrorT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        return true;
    }

    /// <summary>
    /// Shows a message box and deletes an invalid result file.
    /// </summary>
    /// <param name="info"></param>
    private void DeleteInvalidFile(FileInfo info)
    {
        MessageBox.Show(Messages.VhrCorruptFile, Messages.VhrCorruptFileT, MessageBoxButtons.OK, MessageBoxIcon.Error);
        try
        {
            info.Delete();
        }
        catch { }
    }
}
