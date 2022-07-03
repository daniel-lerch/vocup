using System.Windows;
using System.Windows.Controls;

namespace Vocup.Controls;

/// <summary>
/// Interaction logic for SettingsToggleSwitch.xaml
/// </summary>
public partial class SettingsToggleSwitch : UserControl
{
    public SettingsToggleSwitch()
    {
        InitializeComponent();
        layoutRoot.DataContext = this;
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

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(
            name: nameof(Description),
            propertyType: typeof(string),
            ownerType: typeof(SettingsToggleSwitch),
            new PropertyMetadata(defaultValue: string.Empty));

    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    public static readonly DependencyProperty IsCheckedProperty =
        DependencyProperty.Register(
            name: nameof(IsChecked),
            propertyType: typeof(bool),
            ownerType: typeof(SettingsToggleSwitch),
            new PropertyMetadata(defaultValue: false));
}
