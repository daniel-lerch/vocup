using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Vocup.Settings;
using Vocup.Settings.Model;

namespace Vocup.Util;

public class RecentFilesService
{
    private readonly VocupSettings settings;

    public RecentFilesService(VocupSettings settings)
    {
        this.settings = settings;
    }

    public void InteractedWith(string fileName)
    {
        var recentFile = settings.RecentFiles.FirstOrDefault(x => x.FileName == fileName);
        if (recentFile == null)
        {
            settings.RecentFiles.Add(new(fileName, DateTime.Now, DateTime.Now));
        }
        else
        {
            recentFile.LastAccess = DateTime.Now;
            recentFile.LastAvalailable = DateTime.Now;
        }
    }

    public bool TryGetMostRecent([NotNullWhen(true)] out RecentFile? recentFile)
    {
        recentFile = settings.RecentFiles.OrderByDescending(x => x.LastAccess).Where(Exists).FirstOrDefault();
        return recentFile != null;
    }

    private static bool Exists(RecentFile recentFile)
    {
        bool exists = File.Exists(recentFile.FileName);
        if (exists)
        {
            recentFile.LastAvalailable = DateTime.Now;
        }
        return exists;
    }
}
