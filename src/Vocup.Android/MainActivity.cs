using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Avalonia;
using Avalonia.Android;
using Avalonia.ReactiveUI;

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
    private const string Tag = "Vocup.Android.MainActivity";

    protected override AppBuilder CreateAppBuilder()
    {
        return AppBuilder.Configure(() => new App(this)).UseAndroid();
    }

    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .WithInterFont()
            .UseReactiveUI();
    }

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        HandleIntent(Intent);
    }

    protected override void OnNewIntent(Intent? intent)
    {
        base.OnNewIntent(intent);
        HandleIntent(intent);
    }

    private void HandleIntent(Intent? intent)
    {
        if (intent?.Action == Intent.ActionView && intent.Data != null)
        {
            using System.IO.Stream? stream = ContentResolver?.OpenInputStream(intent.Data);
            
            if (stream == null)
            {
                Log.Error(Tag, "Stream is null in HandleIntent");
                return;
            }

            if (Avalonia.Application.Current is not App app)
            {
                Log.Error(Tag, "App is null in HandleIntent");
                return;
            }

            Log.Debug(Tag, $"File size is {stream.Length} bytes");

            app.OpenFile(stream);
        }
    }
}
