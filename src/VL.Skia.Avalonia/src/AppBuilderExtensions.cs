using Avalonia;
using Avalonia.Platform;
using Application = Avalonia.Application;

namespace VL.Skia.Avalonia;

public static class AppBuilderExtensions
{
    /// <summary>
    /// Initializes default Avalonia AppBuilder
    /// </summary>
    /// <returns></returns>
    public static AppBuilder CreateDefault() => AppBuilder.Configure<Application>();

    /// <summary>
    /// Initializes app with VL.Skia platform render interface 
    /// </summary>
    /// <param name="appBuilder">AppBuilder</param>
    /// <returns>AppBuilder</returns>
    public static AppBuilder UseGammaSkia(this AppBuilder appBuilder) =>
        appBuilder
        .UseSkia()
        .AfterPlatformServicesSetup(_ =>
        {
            var renderInterface = new GammaSkiaPlatformRenderInterface();
            AvaloniaLocator.CurrentMutable.Bind<IPlatformRenderInterface>()
                .ToConstant(renderInterface);
        });
}