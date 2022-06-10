namespace Vocup.Maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}
    }
}