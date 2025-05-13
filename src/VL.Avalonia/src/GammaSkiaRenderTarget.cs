using Avalonia.Platform;
using Stride.Core.Diagnostics;

namespace VL.Avalonia
{
    sealed class RenderTarget : RenderTarget2
    {
        private readonly CallerInfo callerInfo;

        public RenderTarget(CallerInfo callerInfo)
        {
            this.callerInfo = callerInfo;
        }

        public IDrawingContextImpl CreateDrawingContext(IVisualBrushRenderer visualBrushRenderer)
        {
            return Avalonia.Skia.Helpers.DrawingContextHelper.WrapSkiaCanvas(callerInfo.Canvas, new Vector(96, 96), visualBrushRenderer);
        }

        public void Dispose()
        {
        }
    }
}
