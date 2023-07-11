using Avalonia;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        BuildAvaloniaApp()
        .AfterSetup(builder => RunAfterSetup(builder))
        .StartWithClassicDesktopLifetime(args);
    }

    static void RunAfterSetup(AppBuilder builder)
    {
        // at this point the application is instatiated, right before OnFrameworkInitializationCompleted callback
        var instance = builder.Instance;
        var lifetime = instance?.ApplicationLifetime;
        
        // cannot schedule earlier, no sta message pump yet for sync context
        RunDelayed(WindowUtil.SpawnWindow);
        RunDelayed(WindowUtil.SpawnWindow, 7500);
    }

    static void RunDelayed(Action action, int ms = 5000)
    {
        var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        Task.Delay(ms)
        .ContinueWith(_ => action(), scheduler);
    }
    

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<Application>() //not building any custom app, pure avalonia application
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
