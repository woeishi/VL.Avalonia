using Avalonia;
using Avalonia.Controls;

namespace VL.Avalonia
{
    public class AppBuilderExtensions
    {
        public static AppBuilder CreateDefault() => AppBuilder.Configure<Application>();
        public static AppBuilder StartClassicDesktopStyleApplication(AppBuilder builder, ShutdownMode shutdownMode = ShutdownMode.OnExplicitShutdown)
        {
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Run(() => { })
                .ContinueWith((_) =>
                    builder.StartWithClassicDesktopLifetime(Array.Empty<string>(), ShutdownMode.OnExplicitShutdown), scheduler);

            return builder;
        }
    }
}
