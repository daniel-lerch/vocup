using System.Collections.Generic;

namespace Vocup.ViewModels;

public class LicensesViewModel
{
    public List<Component> Components { get; } = [
        new()
        {
            Name = ".NET",
            License = "MIT",
            Url = "https://dotnet.microsoft.com",
        },
        new()
        {
            Name = "Avalonia",
            License = "MIT",
            Url = "https://avaloniaui.net",
        },
        new()
        {
            Name = "CsvHelper",
            License = "MS-PL; Apache-2.0",
            Url = "https://joshclose.github.io/CsvHelper",
        },
        new()
        {
            Name = "CsWin32",
            License = "MIT",
            Url = "https://github.com/microsoft/CsWin32",
        },
        new()
        {
            Name = "Losttech Settings",
            License = "Apache-2.0",
            Url = "https://github.com/losttech/Settings",
        },
        new()
        {
            Name = "Office Icons",
            License = "CC BY-ND 3.0",
            Url = "https://icons8.com/icons/office",
        },
        new()
        {
            Name = "ReactiveUI",
            License = "MIT",
            Url = "https://reactiveui.net",
        },
    ];

    public class Component
    {
        public required string Name { get; init; }
        public required string License { get; init; }
        public required string Url { get; init; }
    }
}
