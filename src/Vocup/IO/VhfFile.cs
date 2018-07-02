using System;
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
    internal class VhfFile : VocupFile
    {
        public bool Read(string path, VocabularyBook book)
        {
            string plaintext = ReadFile(path);

            using (StringReader reader = new StringReader(plaintext))
            {
                string version = reader.ReadLine();
                string vhrCode = reader.ReadLine();
                string motherTongue = reader.ReadLine();
                string foreignLang = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(version) || !Version.TryParse(version, out Version versionObj))
                {
                    MessageBox.Show(Messages.VhfInvalidVersion, Messages.VhfInvalidFileT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (versionObj.CompareTo(Util.AppInfo.FileVersion) == 1)
                {
                    // TODO: mbox newer version needed
                    return false;
                }

                if (vhrCode == null)
                {
                    MessageBox.Show(Messages.VhfInvalidVhrCode, Messages.VhfInvalidFileT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                book.VhrCode = vhrCode;

                if (string.IsNullOrWhiteSpace(motherTongue) || 
                    string.IsNullOrWhiteSpace(foreignLang) ||
                    motherTongue == foreignLang)
                {
                    MessageBox.Show(Messages.VhfInvalidLanguages, Messages.VhfInvalidFileT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                book.MotherTongue = motherTongue;
                book.ForeignLang = foreignLang;

                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null) break;
                    string[] columns = line.Split('#');
                    if (columns.Length < 3)
                    {
                        // TODO: mbox invalid file
                        return false;
                    }
                    VocabularyWord word = new VocabularyWord()
                    {
                        Owner = book,
                        MotherTongue = columns[0],
                        ForeignLang = columns[1],
                        ForeignLangSynonym = columns[2]
                    };
                    book.Words.Add(word);
                }
            }

            return true;
        }

        public bool Write(string path, VocabularyBook book)
        {
            return false;
        }
    }
}
