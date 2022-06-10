using ReactiveUI;
using System.Windows.Input;

namespace Vocup.Maui
{
    public class MainPageViewModel : ReactiveObject
    {
        private ObservableAsPropertyHelper<string> _buttonText;
        private int _counter;

        public MainPageViewModel()
        {
            Battery = new BatteryViewModel();
            ButtonCommand = ReactiveCommand.Create(() => Count++);
            this.WhenAnyValue(x => x.Count, counter => counter switch
            {
                0 => "Click me",
                1 => $"Clicked 1 time",
                _ => $"Clicked {counter} times"
            }).ToProperty(this, x => x.ButtonText, out _buttonText);
        }

        public BatteryViewModel Battery { get; }
        public ICommand ButtonCommand { get; }
        public int Count
        {
            get => _counter;
            set => this.RaiseAndSetIfChanged(ref _counter, value);
        }
        public string ButtonText => _buttonText.Value;
    }
}
