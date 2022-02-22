using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Vocup.Util
{
    public static class SettingsImporter
    {
        public static void Run()
        {
            string currentDirectory = GetSettingsDirectory();
            string baseDirectory = Path.GetDirectoryName(currentDirectory);
            //if (!Directory.Exists(baseDirectory))
            {
                string rootDirectory = Path.GetDirectoryName(baseDirectory);
                //MessageBox.Show(string.Join(", ", Directory.GetDirectories(rootDirectory)));
                //Directory.CreateDirectory(baseDirectory);
            }
        }

        private static string GetSettingsDirectory()
        {
            Assembly assembly = Assembly.Load("System.Configuration.ConfigurationManager");
            Type type = assembly.GetType("System.Configuration.ClientConfigPaths");
            object instance = type.InvokeMember("Current", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetProperty, null, null, null);
            return type.InvokeMember("LocalConfigDirectory", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty, null, instance, null) as string;
        }
    }
}
