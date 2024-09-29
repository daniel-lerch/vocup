using System.Drawing;
using System.Windows.Forms;
using Vocup.Properties;
using Vocup.Util;
using Vocup.ViewModels;
using Vocup.Views;

namespace Vocup.Forms;

public partial class AboutBox : Form
{
    public AboutBox()
    {
        InitializeComponent();
        Icon = Icon.FromHandle(Icons.Info.GetHicon());
        AvaloniaControlHost.Content = new AboutView
        {
            DataContext = new AboutViewModel(AppInfo.Version, AppInfo.GetDeployment(), AppInfo.CopyrightInfo)
        };
    }
}
