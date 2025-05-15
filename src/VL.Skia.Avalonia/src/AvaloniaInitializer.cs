using Avalonia;
using Avalonia.Logging;
using VL.Core.CompilerServices;
using Application = Avalonia.Application;

[assembly: AssemblyInitializer(typeof(VL.Skia.Avalonia.AvaloniaInitializer))]

namespace VL.Skia.Avalonia
{
    public sealed class AvaloniaInitializer : AssemblyInitializer<AvaloniaInitializer>
    {
        // There is already static Application.Current.Instance
        // in Avalonia, might it's better to use it?
        public static Application Instance;
        public static void Init() => Instance ??=
            AppBuilder.Configure<App>()
            .UseGammaSkia()
            .UseGammaSkiaDefaults()
            .LogToTrace(LogEventLevel.Verbose)
            .SetupWithLifetime(new GammaSkiaWinFormsLifetime())
            .Instance;

        sealed class App : Application
        {
            public override void Initialize()
            {
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