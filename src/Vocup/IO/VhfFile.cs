using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocup.Models;

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
                    // TODO: mbox invalid file
                    return false;
                }
                else if (versionObj.CompareTo(Util.AppInfo.GetFileVersion()) == 1)
                {
                    // TODO: mbox newer version needed
                    return false;
                }

                if (vhrCode == null)
                {
                    // TODO: mbox invalid file
                    return false;
                }
                else
                {
                    book.VhrCode = vhrCode;
                }

                if (string.IsNullOrWhiteSpace(motherTongue) || 
                    string.IsNullOrWhiteSpace(foreignLang) ||
                    motherTongue == foreignLang)
                {
                    // TODO: mbox invalid file
                    return false;
                }

                book.MotherTongue = motherTongue;
                book.ForeignLang = foreignLang;
            }

            return true;
        }

        public bool Write(string path, VocabularyBook book)
        {

        }
    }
}
