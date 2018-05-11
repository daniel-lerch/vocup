using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vocup.Util
{
    /// <summary>
    /// Provides util methods for application metadata.
    /// </summary>
    public static class AppInfo
    {
        public static string SpecialCharDirectory { get; } = Path.Combine(Properties.Settings.Default.path_vhr, "specialchar");

        /// <summary>
        /// Returns the product version of the currently running instance.
        /// </summary>
        /// <param name="length">Count of version numbers. Range from 1 to 4.</param>
        /// <returns></returns>
        public static string GetVersion(int length)
        {
            if (length < 1 || length > 4)
                throw new ArgumentOutOfRangeException(nameof(length));
            string version = Application.ProductVersion;
            for (int i = 0; i < 4 - length; i++)
                version = version.Remove(version.LastIndexOf('.'));
            return version;
        }
    }
}
