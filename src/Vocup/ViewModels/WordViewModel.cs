using DynamicData;
using DynamicData.Binding;
using System;
using System.Collections.ObjectModel;
using Vocup.Models;

namespace Vocup.ViewModels;

public class WordViewModel : ViewModelBase, IDisposable
{
    private readonly IDisposable motherTongueOperation;
    private readonly IDisposable foreignLanguageOperation;

    public WordViewModel(ObservableCollection<Synonym> motherTongue, ObservableCollection<Synonym> foreignLanguage)
    {
        motherTongueOperation = motherTongue.ToObservableChangeSet()
            .Transform(s => new SynonymViewModel(s))
            .Bind(out _motherTongue)
            .DisposeMany()
            .Subscribe();

        foreignLanguageOperation = foreignLanguage.ToObservableChangeSet()
            .Transform(s => new SynonymViewModel(s))
            .Bind(out _foreignLanguage)
            .DisposeMany()
            .Subscribe();
    }

    private ReadOnlyObservableCollection<SynonymViewModel> _motherTongue;
    public ReadOnlyObservableCollection<SynonymViewModel> MotherTongue => _motherTongue;

    private ReadOnlyObservableCollection<SynonymViewModel> _foreignLanguage;
    public ReadOnlyObservableCollection<SynonymViewModel> ForeignLanguage => _foreignLanguage;

    public void Dispose()
    {
        motherTongueOperation.Dispose();
        foreignLanguageOperation.Dispose();
    }
}
