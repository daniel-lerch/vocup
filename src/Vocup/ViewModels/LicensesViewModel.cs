using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace Vocup.ViewModels;

public class LicensesViewModel
{
    public LicensesViewModel()
    {
        NavigateToUri = ReactiveCommand.Create<string>(async uri =>
        {
            await LaunchUri.Handle(new Uri(uri));
        });
    }

    public ReactiveCommand<string, Unit> NavigateToUri { get; }

    public Interaction<Uri, bool> LaunchUri { get; } = new();

    public List<Component> Components { get; } = [
        new()
        {
            Name = "Avalonia",
            License = "",
            Url = "https://avaloniaui.net"
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
    ];

    public class Component
    {
        public required string Name { get; init; }
        public required string License { get; init; }
        public required string Url { get; init; }
    }
}
