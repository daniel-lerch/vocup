using LostTech.App;
using LostTech.App.DataBinding;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Vocup.Util;
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
        await default(HopToThreadPoolAwaitable); // Force blocking IO to run on a background thread
        directory.Create();

        LSettings settings = new(directory, ClonableFreezerFactory.Instance, JsonSerializerFactory.Instance, JsonSerializerFactory.Instance);
        SettingsSet<T, T>? settingsSet = null;
        bool created = false;

        try
        {
            settingsSet = await settings.Load<T>(filename).ConfigureAwait(false);
        }
        catch (Exception)
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
