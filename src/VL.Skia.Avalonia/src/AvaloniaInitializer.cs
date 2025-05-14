using Avalonia;
using Avalonia.Logging;
using Avalonia.Themes.Fluent;
using VL.Core.CompilerServices;
using Application = Avalonia.Application;

[assembly: AssemblyInitializer(typeof(VL.Skia.Avalonia.AvaloniaInitializer))]

namespace VL.Skia.Avalonia
{
    public sealed class AvaloniaInitializer : AssemblyInitializer<AvaloniaInitializer>
    {
        public static Application Instance;
        public static void Init() => Instance ??=
            AppBuilder.Configure<App>()
            .UseGammaSkia()
            .LogToTrace(LogEventLevel.Verbose)
            .SetupWithLifetime(new GammaSkiaWinFormsLifetime())
            .Instance;

        sealed class App : Application
        {
            public override void Initialize()
            {
                //Styles.Add(new Avalonia.Themes.Default.DefaultTheme());
                Styles.Add(new FluentTheme());
                base.Initialize();
            }

            public override void RegisterServices()
            {
                base.RegisterServices();
            }

            public override void OnFrameworkInitializationCompleted()
            {
                base.OnFrameworkInitializationCompleted();
            }
        }
    }
}