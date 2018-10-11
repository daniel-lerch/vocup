using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Vocup.Forms;
using Vocup.Models;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup
{
    static class Program
    {
        private static SplashScreen splash;
        private static program_form mainForm;

        /// <summary>
        /// The main entry-point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            // Verhindert eine Fehlerhafte Installation falls das Programm geöffnet ist
            Mutex mutex = new Mutex(false, AppInfo.ProductName, out bool newinstance);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            splash = new SplashScreen();
            splash.Show();
            Application.DoEvents();

            CreateVhfFolder();
            CreateVhrFolder();
            if (Settings.Default.StartupCounter == 0)
                Settings.Default.Upgrade(); // Keep old settings with new version
            Settings.Default.StartupCounter++;
            Settings.Default.Save();
            Application.DoEvents();

            mainForm = new program_form();
            Application.DoEvents();

            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                FileInfo info = new FileInfo(args[0]);
                if (info.Extension == ".vhf")
                {
                    mainForm.readfile(info.FullName);
                }
                else if (info.Extension == ".vdp")
                {
                    // TODO: Rewrite this method and ensure independency of MainForm
                    mainForm.restore_backup(info.FullName);
                }
                else
                {
                    MessageBox.Show(string.Format(Messages.OpenUnknownFile, info.FullName),
                        Messages.OpenUnknownFileT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (Settings.Default.StartScreen == (int)StartScreen.LastFile && File.Exists(Settings.Default.LastFile))
            {
                mainForm.readfile(Settings.Default.LastFile);
            }
            Application.DoEvents();

            splash.Close();
            Application.Run(mainForm);
        }

        /// <summary>
        /// Checks the currently configured folder for .vhf files and creates it if not existing.
        /// </summary>
        private static void CreateVhfFolder()
        {
            string personal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string folder = Path.Combine(personal, Words.VocabularyBooks); // default path

            if (string.IsNullOrWhiteSpace(Settings.Default.VhfPath) || Settings.Default.VhfPath.Equals(folder, StringComparison.OrdinalIgnoreCase))
            {
                Directory.CreateDirectory(folder);
                Settings.Default.VhfPath = folder;
            }
            else
            {
                Directory.CreateDirectory(Settings.Default.VhfPath);
            }
        }

        /// <summary>
        /// Checks the currently configured folder for .vhr files and creates it if not existing.
        /// </summary>
        private static void CreateVhrFolder()
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folder = Path.Combine(appdata, AppInfo.ProductName); // default path

            if (string.IsNullOrWhiteSpace(Settings.Default.VhrPath) || Settings.Default.VhrPath.Equals(folder, StringComparison.OrdinalIgnoreCase))
            {
                Directory.CreateDirectory(folder);
                Settings.Default.VhrPath = folder;
            }
            else
            {
                Directory.CreateDirectory(Settings.Default.VhrPath);
            }
        }
    }
}