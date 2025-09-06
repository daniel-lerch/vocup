using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace Vocup.Views;

public class PracticeStateToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double doubleValue)
        {
            if (doubleValue == -1.0) // No practices
                return Brushes.LightGray;
            byte red = (byte)(255 * (1.0 - doubleValue));
            byte green = (byte)(255 * doubleValue);
            return new SolidColorBrush(Color.FromRgb(red, green, 0));
        }
        else
        {
            throw new ArgumentException("Unsupported type", nameof(value));
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
