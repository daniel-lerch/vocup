using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Vocup.Forms;

namespace Vocup
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //----------------------
            //Überprüfen, ob der Ordner Vokabelhefte im Ordner "Eigene Dateien", oder im selbst bestimmten Ordner vorhanden ist

            string personal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            if (Properties.Settings.Default.path_vhf == "" || Properties.Settings.Default.path_vhf == personal + "\\" + Properties.language.personal_directory)
            {

                DirectoryInfo check_personal = new DirectoryInfo(personal + "\\" + Properties.language.personal_directory);
                if (!check_personal.Exists)
                {
                    Directory.CreateDirectory(personal + "\\" + Properties.language.personal_directory);
                }

                if (Properties.Settings.Default.path_vhf == "")
                {
                    Properties.Settings.Default.path_vhf = personal + "\\" + Properties.language.personal_directory;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                DirectoryInfo check_path = new DirectoryInfo(Properties.Settings.Default.path_vhf);
                if (!check_path.Exists)
                {
                    Directory.CreateDirectory(Properties.Settings.Default.path_vhf);
                }
            }


            //----------------------
            //----------------------
            //Überprüfen, ob der Ordner Vocup im Ordner "Andwendungsdaten" vorhanden ist

            string app_data = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (Properties.Settings.Default.path_vhr == "" || Properties.Settings.Default.path_vhr == app_data + "\\" + Properties.language.name)
            {
                DirectoryInfo check_appdata = new DirectoryInfo(app_data + "\\" + Properties.language.name);
                if (!check_appdata.Exists)
                {
                    Directory.CreateDirectory(app_data + "\\" + Properties.language.name);
                }

                if (Properties.Settings.Default.path_vhr == "")
                {
                    Properties.Settings.Default.path_vhr = app_data + "\\" + Properties.language.name;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                DirectoryInfo check_path = new DirectoryInfo(Properties.Settings.Default.path_vhr);
                if (!check_path.Exists)
                {
                    Directory.CreateDirectory(Properties.Settings.Default.path_vhr);
                }
            }

            //----------------------

            //Verhindert eine Fehlerhafte Installation falls das Programm geöffnet ist

            Mutex mutex = new Mutex(false, Properties.language.name, out bool newinstance);

            //----------------------

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Splash-Screen starten

            SplashScreen splash = new SplashScreen();

            splash.Show();

            Application.DoEvents();


            Thread.Sleep(1250);

            splash.Close();

            Application.Run(new program_form(args));
        }
    }
}