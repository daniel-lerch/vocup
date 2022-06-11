using LostTech.App;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using LSettings = LostTech.App.Settings;

namespace Vocup.Settings.Core;

public sealed class VersionedSettings<T> : IAsyncDisposable
    where T : class, INotifyPropertyChanged
{
    private readonly LSettings settings;
    private readonly SettingsSet<T, T> settingsSet;

    public VersionedSettings(LSettings settings, SettingsSet<T, T> settingsSet)
    {
        this.settings = settings;
        this.settingsSet = settingsSet;
    }

    public T Value => settingsSet.Value;

    public async ValueTask DisposeAsync()
    {
        await settingsSet.DisposeAsync();
        await settings.DisposeAsync();
    }
}
