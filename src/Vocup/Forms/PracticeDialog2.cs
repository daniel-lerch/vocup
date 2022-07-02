using System.Windows.Forms;

namespace Vocup.Forms;

public partial class PracticeDialog2 : Form
{
    public PracticeDialog2()
    {
        InitializeComponent();
        elementHost.Child = new SettingsControl();
    }
}
