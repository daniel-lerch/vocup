using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vocup.Controls
{
    /// <summary>
    /// Interaction logic for SettingsToggleSwitch.xaml
    /// </summary>
    public partial class SettingsToggleSwitch : UserControl
    {
        public SettingsToggleSwitch()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                name: nameof(Title),
                propertyType: typeof(string),
                ownerType: typeof(SettingsToggleSwitch),
                new PropertyMetadata(defaultValue: string.Empty));

        public string Description { get; set; }
        public bool IsChecked { get; set; }
    }
}
