using Avalonia.Platform;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vocup.Settings.Core;
using Vocup.Util;
using OldSettings = Vocup.Properties.Settings;

namespace Vocup.Settings;

public class WindowsSettingsLoader : SettingsLoaderBase<VocupSettings>
{
    public WindowsSettingsLoader()
        : base(
            new DirectoryInfo(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Vocup")),
            "settings.2.json")
    { }

    protected override async ValueTask OnSettingsCreated(SettingsContext<VocupSettings> settings)
    {
        try
        {
            if (!await MigrateJsonSettings1(settings).ConfigureAwait(false))
            {
                MigrateLegacySettings(settings);
            }
        }
        catch (Exception ex)
        {
            // Loosing settings of Vocup is not a serious porblem but having Vocup repeatedly crashing at startup is a serious problem xD
            Debug.WriteLine(ex);
        }
    }

    private async ValueTask<bool> MigrateJsonSettings1(SettingsContext<VocupSettings> settings)
    {
        string path = Path.Combine(directory.FullName, "settings.1.json");
        if (!File.Exists(path)) return false;

        using FileStream stream = new(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        JsonSettings1? oldSettings = await JsonSerializer.DeserializeAsync<JsonSettings1>(stream).ConfigureAwait(false);
        if (oldSettings == null) return false;

        if (!string.IsNullOrEmpty(oldSettings.LastFile))
            settings.Value.RecentFiles.Add(new(oldSettings.LastFile, DateTime.MinValue, DateTime.MinValue));
        settings.Value.StartScreen = oldSettings.StartScreen;
        settings.Value.AutoSave = oldSettings.AutoSave;
        settings.Value.DisableInternetServices = oldSettings.DisableInternetServices;
        settings.Value.LastInternetConnection = oldSettings.LastInternetConnection;
        settings.Value.VhfPath = oldSettings.VhfPath;
        settings.Value.VhrPath = oldSettings.VhrPath;
        settings.Value.StartupCounter = oldSettings.StartupCounter;
        settings.Value.ColumnResize = oldSettings.ColumnResize;
#pragma warning disable WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        settings.Value.ThemeVariant = oldSettings.ColorMode switch
        {
            SystemColorMode.Classic => PlatformThemeVariant.Light,
            SystemColorMode.Dark => PlatformThemeVariant.Dark,
            _ => null,
        };
#pragma warning restore WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        settings.Value.OverrideCulture = oldSettings.OverrideCulture;
        settings.Value.PracticePercentageUnpracticed = oldSettings.PracticePercentageUnpracticed;
        settings.Value.PracticePercentageCorrect = oldSettings.PracticePercentageCorrect;
        settings.Value.PracticePercentageWrong = oldSettings.PracticePercentageWrong;
        settings.Value.MaxPracticeCount = oldSettings.MaxPracticeCount;
        settings.Value.UserEvaluates = oldSettings.UserEvaluates;
        settings.Value.PracticeFastContinue = oldSettings.PracticeFastContinue;
        settings.Value.PracticeSoundFeedback = oldSettings.PracticeSoundFeedback;
        settings.Value.PracticeShowResultList = oldSettings.PracticeShowResultList;
        settings.Value.EvaluateOptionalExpressions = oldSettings.EvaluateOptionalExpressions;
        settings.Value.EvaluateTolerateNoSynonym = oldSettings.EvaluateTolerateNoSynonym;
        settings.Value.EvaluateTolerateWhiteSpace = oldSettings.EvaluateTolerateWhiteSpace;
        settings.Value.EvaluateToleratePunctuationMark = oldSettings.EvaluateToleratePunctuationMark;
        settings.Value.EvaluateTolerateSpecialChar = oldSettings.EvaluateTolerateSpecialChar;
        settings.Value.EvaluateTolerateArticle = oldSettings.EvaluateTolerateArticle;
        settings.Value.WindowWidth = oldSettings.MainFormBounds.Width;
        settings.Value.WindowHeight = oldSettings.MainFormBounds.Height;
        settings.Value.WindowPosition = oldSettings.MainFormBounds.Location.ToAvaloniaPixelPoint();
        settings.Value.WindowState = oldSettings.MainFormWindowState.ToAvaloniaWindowState();
        settings.Value.MainFormSplitterDistance = oldSettings.MainFormSplitterDistance;
        settings.Value.SpecialCharTab = oldSettings.SpecialCharTab;
        settings.Value.PracticeDialogSize = oldSettings.PracticeDialogSize.ToAvaloniaSize();
        settings.Value.Version = oldSettings.Version;
        return true;
    }

    private static void MigrateLegacySettings(SettingsContext<VocupSettings> settings)
    {
        // The legacy settings are stored in a path like this:
        // %LocalAppData%\VectorData\Vocup_Path_d34vxcazn0c55ls04bd5mjhnte2ypcjl\1.8.6.0\user.config

        string? thisVersionDirectory = GetAppConfigDirectory();
        string? thisUrlDirectory = Path.GetDirectoryName(thisVersionDirectory);
        string? rootSettingsDirectory = Path.GetDirectoryName(thisUrlDirectory);

        if (thisUrlDirectory == null)
        {
            Debug.WriteLine("Could not determine current user settings hash directory");
        }
        else if (Directory.Exists(rootSettingsDirectory))
        {
            if (!Directory.Exists(thisUrlDirectory))
                MigrateOtherAppConfigVersions(thisUrlDirectory, rootSettingsDirectory);

            MigrateFromAppConfig(settings);
        }

        // If the root settings directory does not exist there are no settings to restore
    }

    private static void MigrateFromAppConfig(SettingsContext<VocupSettings> settings)
    {
        OldSettings.Default.Upgrade();

#pragma warning disable CS0618 // Type or member is obsolete

        if (!string.IsNullOrEmpty(OldSettings.Default.LastFile))
            settings.Value.RecentFiles.Add(new(OldSettings.Default.LastFile, DateTime.MinValue, DateTime.MinValue));
        settings.Value.StartScreen = OldSettings.Default.StartScreen;
        settings.Value.AutoSave = OldSettings.Default.AutoSave;
        settings.Value.DisableInternetServices = OldSettings.Default.DisableInternetServices;
        settings.Value.LastInternetConnection = OldSettings.Default.LastInternetConnection;
        settings.Value.VhfPath = OldSettings.Default.VhfPath;
        settings.Value.VhrPath = OldSettings.Default.VhrPath;
        settings.Value.StartupCounter = OldSettings.Default.StartupCounter;
        settings.Value.ColumnResize = OldSettings.Default.ColumnResize;
        settings.Value.OverrideCulture = OldSettings.Default.OverrideCulture;
        settings.Value.PracticePercentageUnpracticed = OldSettings.Default.PracticePercentageUnpracticed;
        settings.Value.PracticePercentageCorrect = OldSettings.Default.PracticePercentageCorrect;
        settings.Value.PracticePercentageWrong = OldSettings.Default.PracticePercentageWrong;
        settings.Value.MaxPracticeCount = OldSettings.Default.MaxPracticeCount;
        settings.Value.UserEvaluates = OldSettings.Default.UserEvaluates;
        settings.Value.PracticeFastContinue = OldSettings.Default.PracticeFastContinue;
        settings.Value.PracticeSoundFeedback = OldSettings.Default.PracticeSoundFeedback;
        settings.Value.PracticeShowResultList = OldSettings.Default.PracticeShowResultList;
        settings.Value.EvaluateOptionalExpressions = OldSettings.Default.EvaluateOptionalExpressions;
        settings.Value.EvaluateTolerateNoSynonym = OldSettings.Default.EvaluateTolerateNoSynonym;
        settings.Value.EvaluateTolerateWhiteSpace = OldSettings.Default.EvaluateTolerateWhiteSpace;
        settings.Value.EvaluateToleratePunctuationMark = OldSettings.Default.EvaluateToleratePunctuationMark;
        settings.Value.EvaluateTolerateSpecialChar = OldSettings.Default.EvaluateTolerateSpecialChar;
        settings.Value.EvaluateTolerateArticle = OldSettings.Default.EvaluateTolerateArticle;
        settings.Value.WindowWidth = OldSettings.Default.MainFormBounds.Width;
        settings.Value.WindowHeight = OldSettings.Default.MainFormBounds.Height;
        settings.Value.WindowPosition = OldSettings.Default.MainFormBounds.Location.ToAvaloniaPixelPoint();
        settings.Value.WindowState = OldSettings.Default.MainFormWindowState.ToAvaloniaWindowState();
        settings.Value.MainFormSplitterDistance = OldSettings.Default.MainFormSplitterDistance;
        settings.Value.SpecialCharTab = OldSettings.Default.SpecialCharTab;
        if (Version.TryParse(OldSettings.Default.Version, out Version? version)) settings.Value.Version = version;

#pragma warning restore CS0618 // Type or member is obsolete
    }

    private static void MigrateOtherAppConfigVersions(string urlDirectory, string rootDirectory)
    {
        if (!Directory.Exists(urlDirectory) && Directory.Exists(rootDirectory))
        {
            Version? version = null;
            FileInfo? settingsFile = null;

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

            if (settingsFile is not null)
            {
                string restoreDirectory = Path.Combine(urlDirectory, version!.ToString());
                Directory.CreateDirectory(restoreDirectory);
                settingsFile.CopyTo(Path.Combine(restoreDirectory, "user.config"));
            }
        }
    }

    private static string? GetAppConfigDirectory()
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
