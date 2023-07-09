using Avalonia;
using System;

namespace Sandbox;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        var result = BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);
        System.Diagnostics.Debug.WriteLine("here we are", result);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<MinimalApp>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
