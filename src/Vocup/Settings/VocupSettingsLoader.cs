using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Vocup.Settings.Core;
using OldSettings = Vocup.Properties.Settings;

namespace Vocup.Settings;

public class VocupSettingsLoader : VersionedSettingsLoader<VocupSettings>
{
    public VocupSettingsLoader()
        : base(
            new DirectoryInfo(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Vocup")),
            "settings")
    { }

    protected override ValueTask OnSettingsCreated(VersionedSettings<VocupSettings> settings)
    {
        MigrateOtherAppConfigVersions();

        settings.Value.GridLines = OldSettings.Default.GridLines;
        settings.Value.LastFile = OldSettings.Default.LastFile;
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
        settings.Value.PracticeHighlightInput = OldSettings.Default.PracticeInputBackColor != System.Drawing.SystemColors.Control;
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
        settings.Value.MainFormBounds = OldSettings.Default.MainFormBounds;
        settings.Value.MainFormWindowState = OldSettings.Default.MainFormWindowState;
        settings.Value.MainFormSplitterDistance = OldSettings.Default.MainFormSplitterDistance;
        settings.Value.SpecialCharTab = OldSettings.Default.SpecialCharTab;
        if (Version.TryParse(OldSettings.Default.Version, out Version? version)) settings.Value.Version = version;

        return ValueTask.CompletedTask;
    }

    private static void MigrateOtherAppConfigVersions()
    {
        string? currentDirectory = GetAppConfigDirectory();
        string? baseDirectory = Path.GetDirectoryName(currentDirectory);
        string? rootDirectory = Path.GetDirectoryName(baseDirectory);

        if (baseDirectory is null)
        {
            Debug.WriteLine("Could not determine current user settings hash directory");
            return;
        }

        try
        {
            if (!Directory.Exists(baseDirectory) && Directory.Exists(rootDirectory))
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
                    string restoreDirectory = Path.Combine(baseDirectory, version!.ToString());
                    Directory.CreateDirectory(restoreDirectory);
                    settingsFile.CopyTo(Path.Combine(restoreDirectory, "user.config"));
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
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
