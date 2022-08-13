using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using Vocup.Util;

namespace Vocup.Controls
{
    /// <summary>
    /// Interaction logic for LicensesControl.xaml
    /// </summary>
    public partial class LicensesControl : UserControl
    {
        public LicensesControl()
        {
            InitializeComponent();
        }

        private async void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ValueTask task = Launcher.LaunchUriAsync(e.Uri.AbsoluteUri);
            e.Handled = true;
            await task;
        }
    }
}
