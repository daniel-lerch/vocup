using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Vocup.Avalonia.Views
{
    public class ComparisonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool?))
            {
                throw new ArgumentException($"Target type {targetType.FullName} is not supported", nameof(targetType));
            }

            return value?.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!parameter.GetType().IsAssignableFrom(targetType))
            {
                throw new ArgumentException($"Cannot assign type {parameter.GetType().FullName} to {targetType.FullName}");
            }

            return value?.Equals(true) == true ? parameter : BindingOperations.DoNothing;
        }
    }
}
