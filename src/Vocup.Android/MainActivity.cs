using Android.App;
using Android.Content;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using ReactiveUI.Avalonia;
using System.Linq;

namespace Vocup.Android;

[Activity(
    Label = "Vocup",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    // Avalonia only supports single instance https://github.com/AvaloniaUI/Avalonia/issues/17943
    LaunchMode = LaunchMode.SingleInstance,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
[IntentFilter(["android.intent.action.VIEW"],
    Categories = [Intent.CategoryDefault, Intent.CategoryBrowsable],
    DataSchemes = ["file", "content"],
    DataMimeType = "application/octet-stream",
    DataPathPattern = ".*\\.vhf")]
public class MainActivity : AvaloniaMainActivity<App>
{
    public MainActivity()
    {
        ((IAvaloniaActivity)this).Activated += HandleIntent;
    }

    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .WithInterFont()
            .UseReactiveUI(rxuiBuilder => { });
    }

    private static void HandleIntent(object? sender, ActivatedEventArgs e)
    {
        if (e is FileActivatedEventArgs fileActivated && Avalonia.Application.Current is App app)
        {
            if (fileActivated.Files.FirstOrDefault() is IStorageFile file)
            {
                app.OpenFile(file);
            }
        }
    }
}
