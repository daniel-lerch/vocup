using LostTech.App;
using LostTech.App.DataBinding;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using LSettings = LostTech.App.Settings;

namespace Vocup.Settings.Core;

public class VersionedSettingsLoader<T> where T : class, ICopyable<T>, INotifyPropertyChanged, new()
{
    protected DirectoryInfo directory;
    protected string basename;

    public VersionedSettingsLoader(DirectoryInfo directory, string basename)
    {
        this.directory = directory;
        this.basename = basename;
    }

    public virtual async Task<VersionedSettings<T>> LoadAsync()
    {
        LSettings settings = new(directory, ClonableFreezerFactory.Instance, JsonSerializerFactory.Instance, JsonSerializerFactory.Instance);
        SettingsSet<T, T> settingsSet = await settings.LoadOrCreate<T>(basename + ".1.json").ConfigureAwait(false);
        settingsSet.Autosave = true;
        return new VersionedSettings<T>(settings, settingsSet);
    }
}
