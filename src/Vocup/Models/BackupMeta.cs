using System;
using System.Collections.Generic;
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
