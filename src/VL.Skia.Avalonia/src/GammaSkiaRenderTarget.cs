﻿using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia.Helpers;
using SkiaSharp;
using System.Runtime.CompilerServices;

namespace VL.Skia.Avalonia
{
    // Will break likely on latest Avalonia 11.3.0 this interface is IRenderTarget2
    // https://github.com/AvaloniaUI/Avalonia/blob/master/src/Skia/Avalonia.Skia/Gpu/SkiaGpuRenderTarget.cs#L8
    sealed class GammaSkiaRenderTarget : IRenderTarget
    {
        private readonly CallerInfo callerInfo;

        public GammaSkiaRenderTarget(CallerInfo callerInfo)
        {
            this.callerInfo = callerInfo;
        }

        public bool IsCorrupted
        {
            get
            {
                return IsDiposed(callerInfo.Canvas);

                [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_IsDisposed")]
                static extern bool IsDiposed(SKNativeObject obj);
            }
        }

        public IDrawingContextImpl CreateDrawingContext(bool useScaledDrawing)
        {
            return DrawingContextHelper.WrapSkiaCanvas(callerInfo.Canvas, new Vector(96, 96));
        }

        public void Dispose()
        {
        }
    }
}
