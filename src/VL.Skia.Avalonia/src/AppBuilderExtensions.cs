using Avalonia;
using Avalonia.Platform;
using Avalonia.Rendering;
using Avalonia.Rendering.Composition;
using Avalonia.Themes.Fluent;
using Avalonia.Threading;

namespace VL.Skia.Avalonia;

public static class AppBuilderExtensions
{
    /// <summary>
    /// Initializes app with VL.Skia platform render interface 
    /// </summary>
    /// <param name="appBuilder">AppBuilder</param>
    /// <returns>AppBuilder</returns>
    public static AppBuilder UseGammaSkia(this AppBuilder appBuilder) =>
        appBuilder
        .UseStandardRuntimePlatformSubsystem()
        .UseSkia()
        .UseWindowingSubsystem(() =>
        {
            AvaloniaSynchronizationContext.AutoInstall = false;

            var platformGraphics = new GammaPlatformGraphics();
            AvaloniaLocator.CurrentMutable
                .Bind<IDispatcherImpl>().ToConstant(new GammaDispatcherImpl(Thread.CurrentThread))
                .Bind<IPlatformGraphics>().ToConstant(platformGraphics)
                .Bind<IRenderTimer>().ToConstant(GammaRenderTimer.Instance)
                .Bind<Compositor>().ToConstant(new Compositor(platformGraphics, useUiThreadForSynchronousCommits: true));
        })
        .AfterPlatformServicesSetup(_ =>
        {
            var renderInterface = new GammaSkiaPlatformRenderInterface();
            AvaloniaLocator.CurrentMutable.Bind<IPlatformRenderInterface>()
                .ToConstant(renderInterface);
        });

    public static AppBuilder UseGammaSkiaDefaults(this AppBuilder appBuilder) =>
        appBuilder
        .WithInterFont()
        .AfterSetup((_) => appBuilder?.Instance?.Styles.Add(new FluentTheme()));
}