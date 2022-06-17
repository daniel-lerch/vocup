using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Vocup.Settings.Core;

public abstract class SettingsBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected T RaiseAndSetIfChanged<T>(
        ref T backingField,
        T newValue,
        [CallerMemberName] string? propertyName = null)
    {
        if (propertyName is null)
        {
            throw new ArgumentNullException(nameof(propertyName));
        }

        if (EqualityComparer<T>.Default.Equals(backingField, newValue))
        {
            return newValue;
        }

        backingField = newValue;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return newValue;
    }
}
