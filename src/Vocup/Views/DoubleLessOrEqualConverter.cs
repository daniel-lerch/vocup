using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Vocup.Views;

public class DoubleLessOrEqualConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not double doubleValue)
            throw new ArgumentException("Value must be of type double", nameof(value));

        if (parameter is not double doubleParameter && !double.TryParse(parameter?.ToString(), out doubleParameter))
            throw new ArgumentException("Parameter must be of type double", nameof(parameter));

        return doubleValue <= doubleParameter;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
