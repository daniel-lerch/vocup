using LostTech.App;
using LostTech.App.DataBinding;
using System.ComponentModel;
using System.Threading.Tasks;
using LSettings = LostTech.App.Settings;

namespace Vocup.Settings.Core
{
    public class VersionedSettingsLoader<T> where T : class, ICopyable<T>, INotifyPropertyChanged, new()
    {
        public async Task<VersionedSettings<T>> LoadAsync()
        {
            LSettings settings = new(null, ClonableFreezerFactory.Instance, JsonSerializerFactory.Instance, JsonSerializerFactory.Instance);
            SettingsSet<T, T> settingsSet = await settings.LoadOrCreate<T>(null).ConfigureAwait(false);
            return new VersionedSettings<T>(settings, settingsSet);
        }
    }
}
