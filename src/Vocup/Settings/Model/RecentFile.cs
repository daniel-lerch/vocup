using System;
using Vocup.Settings.Core;

namespace Vocup.Settings.Model;

public class RecentFile : SettingsBase
{
    public RecentFile(string fileName, DateTime lastAccess, DateTime lastAvailable)
    {
        FileName = fileName;
        LastAccess = lastAccess;
        LastAvalailable = lastAvailable;
    }

    public string FileName { get; }

    private DateTime _lastAccess;
    public DateTime LastAccess
    {
        get => _lastAccess;
        set => RaiseAndSetIfChanged(ref _lastAccess, value);
    }

    private DateTime _lastAvailable;
    /// <summary>
    /// Gets or sets the last time the file was available on the file system.
    /// </summary>
    public DateTime LastAvalailable
    {
        get => _lastAvailable;
        set => RaiseAndSetIfChanged(ref _lastAvailable, value);
    }
}
