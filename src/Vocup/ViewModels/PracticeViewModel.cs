using ReactiveUI;
using System.Collections.ObjectModel;

namespace Vocup.ViewModels;

public class PracticeViewModel : ReactiveObject
{
    public PracticeViewModel()
    {
        MotherTongue = new() { new(string.Empty) };
        ForeignLanguage = new() { new(string.Empty), new(string.Empty) };
    }

    public ObservableCollection<Synonym> MotherTongue { get; }
    public ObservableCollection<Synonym> ForeignLanguage { get; }

    public class Synonym : ReactiveObject
    {
        private string _value;

        public Synonym(string value)
        {
            _value = value;
        }

        public string Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }
    }
}
