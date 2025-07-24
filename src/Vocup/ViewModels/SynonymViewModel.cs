using ReactiveUI;
using System;
using Vocup.Models;

namespace Vocup.ViewModels;

public class SynonymViewModel : ViewModelBase, IDisposable
{
    private readonly ObservableAsPropertyHelper<string> valueHelper;

    public SynonymViewModel(Synonym parent)
    {
        valueHelper = parent.WhenAnyValue(s => s.Value).ToProperty(parent, s => s.Value);
    }

    public string Value => valueHelper.Value;

    public void Dispose()
    {
        valueHelper.Dispose();
    }
}
