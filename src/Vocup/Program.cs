using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Vocup.Forms;
using Vocup.Util;

namespace Vocup
{
    static class Program
    {
        /// <summary>
        /// The main entry-point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Verhindert eine Fehlerhafte Installation falls das Programm geöffnet ist
            Mutex mutex = new Mutex(false, AppInfo.ProductName, out bool newinstance);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Start SplashScreen first to do other things while loading
            SplashScreen splash = new SplashScreen();
            splash.Show();
            Application.DoEvents();

            Properties.Settings settings = Properties.Settings.Default;

            CreateVhfFolder(settings);
            Application.DoEvents();

            CreateVhrFolder(settings);
            Application.DoEvents();

            settings.Save();
            program_form form = new program_form(args);
            Application.DoEvents();
            Thread.Sleep(1250);

            splash.Close();
            Application.Run(form);
        }

        /// <summary>
        /// Checks the currently configured folder for .vhf files and creates it if not existing.
        /// </summary>
        static void CreateVhfFolder(Properties.Settings settings)
        {
            string personal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string folder = Path.Combine(personal, Properties.Words.VocabularyBooks); // default path

            if (string.IsNullOrWhiteSpace(settings.path_vhf) || settings.path_vhf.Equals(folder, StringComparison.OrdinalIgnoreCase))
            {
                Directory.CreateDirectory(folder);
                settings.path_vhf = folder;
            }
            else
            {
                Directory.CreateDirectory(Properties.Settings.Default.path_vhf);
            }
        }

        /// <summary>
        /// Checks the currently configured folder for .vhr files and creates it if not existing.
        /// </summary>
        static void CreateVhrFolder(Properties.Settings settings)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folder = Path.Combine(appdata, AppInfo.ProductName); // default path

            if (string.IsNullOrWhiteSpace(settings.path_vhr) || settings.path_vhr.Equals(folder, StringComparison.OrdinalIgnoreCase))
            {
                Directory.CreateDirectory(folder);
                Properties.Settings.Default.path_vhr = folder;
            }
            else
            {
                Directory.CreateDirectory(Properties.Settings.Default.path_vhr);
            }
        }
    }
}