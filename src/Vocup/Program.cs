using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Vocup.Forms;
using Vocup.Models;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup
{
    public static class Program
    {
        private static Mutex mutex;

        /// <summary>
        /// The main entry-point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            // Prevents the installer from executing while the program is running
            mutex = new Mutex(initiallyOwned: true, AppInfo.ProductName, out bool createdNew);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!createdNew)
            {
                // Another instance of Vocup is already running so we change the focus
                SwitchFocus();
                return;
            }

            SplashScreen splash = new SplashScreen();
            splash.Show();
            Application.DoEvents();

            if (Settings.Default.StartupCounter == 0)
            {
                Settings.Default.Upgrade(); // Keep old settings with new version
                                            // Warning: Unsaved changes are overridden
                
                // Reset DisableInternetSettings on update to 1.8.4
                if (AppInfo.IsUwp && (!Version.TryParse(Settings.Default.Version, out Version version) || version < new Version(1, 8, 4)))
                {
                    Settings.Default.DisableInternetServices = false;
                }
            }

            SetCulture();
            if (!CreateVhfFolder() || !CreateVhrFolder())
            {
                Application.Exit();
                return;
            }

            Settings.Default.StartupCounter++;
            Settings.Default.Version = AppInfo.GetVersion(3);
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

        public static void ReleaseMutex()
        {
            mutex.ReleaseMutex();
        }

        private static void SwitchFocus()
        {
            // Take the Vocup process which was started first because there might be multiple newer processes racing for bringing one to front
            Process process = Process.GetProcessesByName(AppInfo.ProductName).OrderBy(x => x.StartTime).FirstOrDefault();
            if (process != null && process.MainWindowHandle != IntPtr.Zero)
            {
                PInvoke.User32.SetForegroundWindow(process.MainWindowHandle);
            }
            else
            {
                MessageBox.Show(Messages.MutexLockedButNoOtherProcess, Messages.MutexLockedButNoOtherProcessT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Checks the currently configured folder for .vhf files and creates it if not existing.
        /// </summary>
        public static bool CreateVhfFolder()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            if (string.IsNullOrWhiteSpace(Settings.Default.VhfPath))
            {
                Settings.Default.VhfPath = folder;
            }
            else if (!Directory.Exists(Settings.Default.VhfPath))
            {
                if (MessageBox.Show(string.Format(Messages.VhfPathNotFound, Settings.Default.VhfPath), Messages.VhfPathNotFoundT, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                    == DialogResult.OK)
                {
                    Settings.Default.VhfPath = folder;
                }
                else return false;
            }

            return true;
        }

        /// <summary>
        /// Checks the currently configured folder for .vhr files and creates it if not existing.
        /// </summary>
        public static bool CreateVhrFolder()
        {
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                AppInfo.ProductName);

            if (string.IsNullOrWhiteSpace(Settings.Default.VhrPath))
            {
                Directory.CreateDirectory(folder);
                Settings.Default.VhrPath = folder;
            }
            else if (!Directory.Exists(Settings.Default.VhrPath))
            {
                if (MessageBox.Show(string.Format(Messages.VhrPathNotFound, Settings.Default.VhrPath), Messages.VhrPathNotFoundT, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                    == DialogResult.OK)
                {
                    Directory.CreateDirectory(folder);
                    Settings.Default.VhrPath = folder;
                }
                else return false;
            }

            return true;
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
