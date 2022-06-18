using System.Windows.Controls;
using Vocup.ViewModels;

namespace Vocup.Forms;

/// <summary>
/// Interaction logic for PracticePage.xaml
/// </summary>
public partial class PracticeControl : UserControl
{
    public PracticeControl()
    {
        InitializeComponent();
        DataContext = new PracticeViewModel();
    }
}
