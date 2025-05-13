using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;

using Helpers = Avalonia.Skia.Helpers;

namespace VL.Skia.Avalonia
{
    // Will break likely on latest Avalonia 11.3.0 this interface is IRenderTarget2
    // https://github.com/AvaloniaUI/Avalonia/blob/master/src/Skia/Avalonia.Skia/Gpu/SkiaGpuRenderTarget.cs#L8
    sealed class GammaSkiaRenderTarget : IRenderTarget
    {
        private readonly CallerInfo _callerInfo;

        public GammaSkiaRenderTarget(CallerInfo callerInfo)
        {
            _callerInfo = callerInfo;
        }

        public bool IsCorrupted => false;

        public IDrawingContextImpl CreateDrawingContext(PixelSize expectedPixelSize,
        out RenderTargetDrawingContextProperties properties) =>
        CreateDrawingContextCore(expectedPixelSize, false, out properties);

        public IDrawingContextImpl CreateDrawingContext(bool useScaledDrawing)
            => CreateDrawingContextCore(null, useScaledDrawing, out _);

        IDrawingContextImpl CreateDrawingContextCore(PixelSize? expectedPixelSize,
            bool useScaledDrawing,
            out RenderTargetDrawingContextProperties properties)
        {
            properties = default;

            return Helpers.DrawingContextHelper.WrapSkiaCanvas(_callerInfo.Canvas, SkiaPlatform.DefaultDpi);
        }

        public void Dispose()
        {

        }
    }
}
