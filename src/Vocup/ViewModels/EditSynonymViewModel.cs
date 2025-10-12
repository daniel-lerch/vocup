using ReactiveUI;
using Vocup.Models;

namespace Vocup.ViewModels;

public class EditSynonymViewModel : ViewModelBase
{
    private readonly Synonym synonym;
    private readonly ObservableAsPropertyHelper<string> valueHelper;

    public EditSynonymViewModel(Synonym synonym)
    {
        this.synonym = synonym;
        valueHelper = synonym.WhenAnyValue(s => s.Value).ToProperty(this, s => s.Value);
    }

    public string Value
    {
        get => valueHelper.Value;
        set => synonym.Value = value;
    }
}
