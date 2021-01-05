using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using System;
using Vocup.Avalonia.Controls;

namespace Vocup.Avalonia.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Style style = new Style(x => x.OfType<Button>());
            style.Setters.Add(new Setter(TransitionsProperty, new Transitions()
            {
                new SolidColorBrushTransition
                {
                    Duration = TimeSpan.FromMilliseconds(250),
                    Property = BackgroundProperty,
                }
            }));

            Styles.Add(style);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
