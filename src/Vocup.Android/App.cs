namespace Vocup.Android;

public class App : Vocup.App
{
    private readonly MainActivity? mainActivity;

    public App() { } // Default constructor for Avalonia

    public App(MainActivity mainActivity)
    {
        this.mainActivity = mainActivity;
    }
}
