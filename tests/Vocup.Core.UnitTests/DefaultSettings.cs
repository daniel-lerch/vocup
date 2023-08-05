using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Vocup.Settings;

namespace Vocup.Core.UnitTests;

public class DefaultSettings : ReactiveObject, IVocupSettings
{
    public DefaultSettings(string vhrPath)
    {
        VhrPath = vhrPath;
    }

    [Reactive] public string VhrPath { get; set; }
    [Reactive] public int MaxPracticeCount { get; set; } = 2;
}
