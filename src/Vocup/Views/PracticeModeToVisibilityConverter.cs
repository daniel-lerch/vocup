using Avalonia.Data.Converters;
using System;
using System.Globalization;
using Vocup.Models;

namespace Vocup.Views;

public class PracticeModeToVisibilityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not PracticeMode practiceMode)
            throw new ArgumentException("Value must be of type PracticeMode", nameof(value));

        if (!Enum.TryParse(parameter?.ToString(), out PracticeMode parameterMode))
            throw new ArgumentException("Parameter must be a valid PracticeMode value", nameof(parameter));

        return (practiceMode, parameterMode) switch
        {
            (_, PracticeMode.AskForBothMixed) => true, // I cannot think of a use case to pass AsForBothMixed as parameter
            (PracticeMode.AskForBothMixed, _) => true,
            (PracticeMode.AskForForeignLang, PracticeMode.AskForForeignLang) => true,
            (PracticeMode.AskForMotherTongue, PracticeMode.AskForMotherTongue) => true,
            _ => false
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
