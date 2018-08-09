using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vocup.Forms;
using Vocup.Util;

namespace Vocup
{
    static class Program
    {
        private static Properties.Settings settings;
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
            mainForm = new program_form(args);
            ThreadPool.QueueUserWorkItem(Initialize, args);
            Application.Run(splash);
            Application.Run(mainForm);
        }

        private static void Initialize(object state)
        {
            string[] args = (string[])state;
            settings = Properties.Settings.Default;
            CreateVhfFolder();
            CreateVhrFolder();
            settings.Save();
            splash.Invoke((MethodInvoker)delegate { splash.Close(); });
        }

        /// <summary>
        /// Checks the currently configured folder for .vhf files and creates it if not existing.
        /// </summary>
        private static void CreateVhfFolder()
        {
            string personal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string folder = Path.Combine(personal, Properties.Words.VocabularyBooks); // default path

            if (string.IsNullOrWhiteSpace(settings.VhfPath) || settings.VhfPath.Equals(folder, StringComparison.OrdinalIgnoreCase))
            {
                Directory.CreateDirectory(folder);
                settings.VhfPath = folder;
            }
            else
            {
                Directory.CreateDirectory(Properties.Settings.Default.VhfPath);
            }
        }

        /// <summary>
        /// Checks the currently configured folder for .vhr files and creates it if not existing.
        /// </summary>
        private static void CreateVhrFolder()
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folder = Path.Combine(appdata, AppInfo.ProductName); // default path

            if (string.IsNullOrWhiteSpace(settings.VhrPath) || settings.VhrPath.Equals(folder, StringComparison.OrdinalIgnoreCase))
            {
                Directory.CreateDirectory(folder);
                settings.VhrPath = folder;
            }
            else
            {
                Directory.CreateDirectory(settings.VhrPath);
            }
        }
    }
}