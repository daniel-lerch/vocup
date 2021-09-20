using ReactiveUI;
using System.Collections.Generic;
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
        public Synonym(string value, List<string> flags, List<Practice> practices)
        {
            this.value = value;
            Flags = new ObservableCollection<string>(flags);
            Practices = new ObservableCollection<Practice>(practices);
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
