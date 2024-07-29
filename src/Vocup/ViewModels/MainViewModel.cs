namespace Vocup.ViewModels;

public class MainViewModel : ViewModelBase
{
    public LicensesViewModel Licenses { get; } = new();
}
