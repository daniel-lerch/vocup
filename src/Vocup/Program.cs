using System;
using System.Globalization;
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
        /// <summary>
        /// The main entry-point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            // Prevents the installer from executing while the program is running
            new Mutex(false, AppInfo.ProductName, out _);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SplashScreen splash = new SplashScreen();
            splash.Show();
            Application.DoEvents();

            if (Settings.Default.StartupCounter == 0)
                Settings.Default.Upgrade(); // Keep old settings with new version
            // Warning: Unsaved changes are overridden

            SetCulture();
            CreateVhfFolder();
            CreateVhrFolder();

            Settings.Default.StartupCounter++;
            Settings.Default.Save();
            Application.DoEvents();

            Form form;

            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                FileInfo info = new FileInfo(args[0]);
                if (info.Extension == ".vhf")
                {
                    var mainForm = new MainForm();
                    mainForm.ReadFile(info.FullName);
                    form = mainForm;
                }
                else if (info.Extension == ".vdp")
                {
                    form = new RestoreBackup(info.FullName);
                }
                else
                {
                    form = new MainForm();
                    MessageBox.Show(string.Format(Messages.OpenUnknownFile, info.FullName),
                        Messages.OpenUnknownFileT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (Settings.Default.StartScreen == (int)StartScreen.LastFile && File.Exists(Settings.Default.LastFile))
            {
                var mainForm = new MainForm();
                mainForm.ReadFile(Settings.Default.LastFile);
                form = mainForm;
            }
            else
            {
                form = new MainForm();
            }

            Application.DoEvents();

            splash.Close();
            Application.Run(form);
        }

        /// <summary>
        /// Checks the currently configured folder for .vhf files and creates it if not existing.
        /// </summary>
        internal static void CreateVhfFolder()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            if (string.IsNullOrWhiteSpace(Settings.Default.VhfPath) || Settings.Default.VhfPath.Equals(folder, StringComparison.OrdinalIgnoreCase))
            {
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
        internal static void CreateVhrFolder()
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

        internal static void SetCulture()
        {
            if (!string.IsNullOrWhiteSpace(Settings.Default.OverrideCulture))
            {
                try
                {
                    CultureInfo culture = new CultureInfo(Settings.Default.OverrideCulture);
                    CultureInfo.DefaultThreadCurrentCulture = culture;
                    CultureInfo.DefaultThreadCurrentUICulture = culture;
                }
                catch (CultureNotFoundException)
                {
                    Settings.Default.OverrideCulture = "";
                }
            }
        }
    }
}