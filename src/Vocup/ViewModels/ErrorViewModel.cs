namespace Vocup.ViewModels;

public class ErrorViewModel : ViewModelBase
{
    private readonly string _message;

    public ErrorViewModel(string message)
    {
        _message = message;
    }

    public string Message => _message;
}

public class ErrorDesignViewModel : ErrorViewModel
{
    public ErrorDesignViewModel() : base("This is a design-time error message for testing purposes.") { }
}
