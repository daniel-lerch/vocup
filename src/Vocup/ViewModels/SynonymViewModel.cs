using ReactiveUI;
using System;
using Vocup.Models;

namespace Vocup.ViewModels;

public class SynonymViewModel : ViewModelBase, IDisposable
{
    private readonly ObservableAsPropertyHelper<string> valueHelper;
    private readonly ObservableAsPropertyHelper<int> practicesCountHelper;

    public SynonymViewModel(Synonym parent)
    {
        valueHelper = parent.WhenAnyValue(s => s.Value).ToProperty(this, s => s.Value);
        practicesCountHelper = parent.WhenAnyValue(s => s.Practices.Count).ToProperty(this, s => s.PracticesCount);
    }

    public string Value => valueHelper.Value;
    public int PracticesCount => practicesCountHelper.Value;

    public void Dispose()
    {
        valueHelper.Dispose();
    }
}
