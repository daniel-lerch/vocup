using ReactiveUI;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Vocup.Models
{
    public class Synonym : ReactiveObject
    {
        private string value;

        public Synonym(string value)
        {
            this.value = value;
            Flags = new ObservableCollection<string>();
            Practices = new ObservableCollection<Practice>();
        }

        [JsonConstructor]
        public Synonym(string value, ObservableCollection<string> flags, ObservableCollection<Practice> practices)
        {
            this.value = value;
            Flags = flags;
            Practices = practices;
        }

        public string Value
        {
            get => value;
            set => this.RaiseAndSetIfChanged(ref this.value, value);
        }
        public ObservableCollection<string> Flags { get; }
        public ObservableCollection<Practice> Practices { get; }
    }
}
