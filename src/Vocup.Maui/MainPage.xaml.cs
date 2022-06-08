namespace Vocup.Maui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        BatteryViewModel Battery { get; }

        public MainPage()
        {
            InitializeComponent();
            Battery = new BatteryViewModel();
            BindingContext = this;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}