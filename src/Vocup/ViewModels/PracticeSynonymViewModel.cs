using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vocup.ViewModels;

public class PracticeSynonymViewModel : ViewModelBase
{
    private string _input;
    public string Input
    {
        get => _input;
        set => this.RaiseAndSetIfChanged(ref _input, value);
    }
}
