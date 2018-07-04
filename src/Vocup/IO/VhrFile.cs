using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO.Internal
{
    internal class VhrFile : VocupFile
    {
        public bool Read(VocabularyBook book)
        {
            string plaintext = ReadFile(Path.Combine(Properties.Settings.Default.path_vhr, book.VhrCode + ".vhr"));

            using (StringReader reader = new StringReader(plaintext))
            {
                string path = reader.ReadLine();
                string mode = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(path))
                {
                    // TODO: mbox
                    return false;
                }

                if (string.IsNullOrWhiteSpace(mode) || !int.TryParse(mode, out int imode) || !((PracticeMode)imode).IsValid())
                {
                    // TODO: mbox
                    return false;
                }

                List<Tuple<PracticeState, DateTime>> results = new List<Tuple<PracticeState, DateTime>>();

                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null) break;
                    string[] columns = line.Split('#');
                    if (columns.Length != 2 || !int.TryParse(columns[0], out int state) || !((PracticeState)state).IsValid())
                    {
                        // TODO: mbox
                        return false;
                    }
                    DateTime time = DateTime.MinValue;
                    // DateTime.Parse() does work with the format dd.MM.yyyy HH:mm
                    if (!string.IsNullOrWhiteSpace(columns[1]) && !DateTime.TryParse(columns[1], out time))
                    {
                        // TODO: mbox
                        return false;
                    }
                    results.Add(new Tuple<PracticeState, DateTime>((PracticeState)state, time));
                }

                // TODO: Check property count and apply properties
                book.FilePath = path;
                book.PracticeMode = (PracticeMode)imode;
            }

            return false;
        }

        public bool Write(VocabularyBook book)
        {
            return false;
        }
    }
}
