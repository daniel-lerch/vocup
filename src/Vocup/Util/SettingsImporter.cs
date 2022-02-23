using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

#nullable enable

namespace Vocup.Util;

public static class SettingsImporter
{
    public static void Run()
    {
        string? currentDirectory = GetSettingsDirectory();
        string? baseDirectory = Path.GetDirectoryName(currentDirectory);
        if (baseDirectory is null)
        {
            Debug.WriteLine("Could not determine current user settings hash directory");
            return;
        }
        
        if (!Directory.Exists(baseDirectory))
        {
            Version? version = null;
            FileInfo? settingsFile = null;

            string rootDirectory = Path.GetDirectoryName(baseDirectory)!;
            foreach (string hashDirectory in Directory.EnumerateDirectories(rootDirectory))
            {
                foreach (string versionDirectory in Directory.EnumerateDirectories(hashDirectory))
                {
                    if (Version.TryParse(Path.GetFileName(versionDirectory.AsSpan()), out Version? newVersion))
                    {
                        FileInfo fileInfo = new(Path.Combine(versionDirectory, "user.config"));
                        if (fileInfo.Exists)
                        {
                            int cmp = newVersion.CompareTo(version);
                            if (cmp == 1 || (cmp == 0 && fileInfo.LastWriteTime > settingsFile!.LastWriteTime))
                            {
                                version = newVersion;
                                settingsFile = fileInfo;
                            }
                        }
                    }
                }
            }

            if (settingsFile != null)
            {
                string restoreDirectory = Path.Combine(baseDirectory, version!.ToString());
                Directory.CreateDirectory(restoreDirectory);
                settingsFile.CopyTo(Path.Combine(restoreDirectory, "user.config"));
            }
        }
    }

    private static string? GetSettingsDirectory()
    {
        try
        {
            Assembly assembly = Assembly.Load("System.Configuration.ConfigurationManager");
            Type? type = assembly.GetType("System.Configuration.ClientConfigPaths");
            if (type == null) return null;

            object? instance = type.InvokeMember("Current", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetProperty, null, null, null);
            if (instance == null) return null;

            return type.InvokeMember("LocalConfigDirectory", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty, null, instance, null) as string;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return null;
        }
    }
}
