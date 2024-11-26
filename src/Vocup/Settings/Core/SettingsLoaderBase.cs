using LostTech.App;
using LostTech.App.DataBinding;
using System;
using System.ComponentModel;
#if DEBUG
using System.Diagnostics;
#endif
using System.IO;
using System.Threading.Tasks;
using LSettings = LostTech.App.Settings;

namespace Vocup.Settings.Core;

public class SettingsLoaderBase<T> where T : class, ICopyable<T>, INotifyPropertyChanged, new()
{
    protected DirectoryInfo directory;
    protected string filename;

    public SettingsLoaderBase(DirectoryInfo directory, string filename)
    {
        this.directory = directory;
        this.filename = filename;
    }

    public async ValueTask<SettingsContext<T>> LoadAsync()
    {
        directory.Create();

        LSettings settings = new(directory, ClonableFreezerFactory.Instance, JsonSerializerFactory.Instance, JsonSerializerFactory.Instance);
        SettingsSet<T, T>? settingsSet = null;
        bool created = false;

        try
        {
            settingsSet = await settings.Load<T>(filename).ConfigureAwait(false);
        }
#if DEBUG
        catch (Exception) when (!Debugger.IsAttached)
#else
        catch (Exception)
#endif
        {
            try
            {
                // Delete corrupted settings file if loading failed
                File.Delete(Path.Combine(directory.FullName, filename));
            }
            catch { }
        }
        if (settingsSet is null)
        {
            settingsSet = await settings.LoadOrCreate<T>(filename).ConfigureAwait(false);
            created = true;
        }
        settingsSet.Autosave = true;
        SettingsContext<T> result = new(settings, settingsSet);
        if (created)
            await OnSettingsCreated(result).ConfigureAwait(false);
        return result;
    }

    protected virtual ValueTask OnSettingsCreated(SettingsContext<T> settings) => ValueTask.CompletedTask;
}
