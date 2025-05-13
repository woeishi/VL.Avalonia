using Avalonia;
using VL.Core.CompilerServices;
using Application = Avalonia.Application;

[assembly: AssemblyInitializer(typeof(VL.Skia.Avalonia.AvaloniaInitializer))]

namespace VL.Skia.Avalonia
{
    public sealed class AvaloniaInitializer : AssemblyInitializer<AvaloniaInitializer>
    {
        public static Application Instance;
        public static void Init() => Instance ??=
            AppBuilderExtensions
            .CreateDefault()
            .UseWin32()
            .UseGammaSkia()
            .LogToTrace()
            .SetupWithLifetime(new GammaSkiaWinFormsLifetime())
            .Instance;
    }
}