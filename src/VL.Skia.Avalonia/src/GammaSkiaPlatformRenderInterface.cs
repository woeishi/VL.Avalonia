using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Media.TextFormatting;
using Avalonia.Platform;
using Point = Avalonia.Point;

namespace VL.Skia.Avalonia
{
    public class GammaSkiaPlatformRenderInterface : IPlatformRenderInterface
    {
        private readonly IPlatformRenderInterface _platformRenderInterface;

        public GammaSkiaPlatformRenderInterface()
        {
            _platformRenderInterface = AvaloniaLocator.Current.GetService<IPlatformRenderInterface>();
        }

        public bool SupportsIndividualRoundRects => _platformRenderInterface.SupportsIndividualRoundRects;

        public AlphaFormat DefaultAlphaFormat => _platformRenderInterface.DefaultAlphaFormat;

        public PixelFormat DefaultPixelFormat => _platformRenderInterface.DefaultPixelFormat;

        public bool SupportsRegions => _platformRenderInterface.SupportsRegions;

        // https://github.com/AvaloniaUI/Avalonia/blob/aaf6fe9cb64316ce03f6134a166f918b60e8a399/src/Skia/Avalonia.Skia/PlatformRenderInterface.cs#L31
        public IPlatformRenderInterfaceContext CreateBackendContext(IPlatformGraphicsContext? graphicsContext)
        {
            return new GammaSkiaBackendContext(null);

            //if (graphicsContext == null)
            //    return new GammaSkiaBackendContext(null);
            //if (graphicsContext is ISkiaGpu skiaGpu)
            //    return new GammaSkiaBackendContext(skiaGpu);
            //// if (graphicsContext is IGlContext gl)
            ////    return new GammaSkiaBackendContext(new GlSkiaGpu(gl, _maxResourceBytes));
            ////if (graphicsContext is IMetalDevice metal)
            ////    return new GammaSkiaBackendContext(new SkiaMetalGpu(metal, _maxResourceBytes));
            ////if (graphicsContext is IVulkanPlatformGraphicsContext vulkanContext)
            ////    return new GammaSkiaBackendContext(new VulkanSkiaGpu(vulkanContext, _maxResourceBytes));
            //throw new ArgumentException("Graphics context of type is not supported");
        }

        public IGeometryImpl CreateCombinedGeometry(GeometryCombineMode combineMode, IGeometryImpl g1, IGeometryImpl g2) =>
            _platformRenderInterface.CreateCombinedGeometry(combineMode, g1, g2);

        public IGeometryImpl CreateEllipseGeometry(Rect rect) =>
            _platformRenderInterface.CreateEllipseGeometry(rect);

        public IGeometryImpl CreateGeometryGroup(FillRule fillRule, IReadOnlyList<IGeometryImpl> children) =>
            _platformRenderInterface.CreateGeometryGroup(fillRule, children);

        public IGeometryImpl BuildGlyphRunGeometry(GlyphRun glyphRun) =>
            _platformRenderInterface.BuildGlyphRunGeometry(glyphRun);

        public IGlyphRunImpl CreateGlyphRun(IGlyphTypeface glyphTypeface, double fontRenderingEmSize, IReadOnlyList<GlyphInfo> glyphInfos, Point baselineOrigin) =>
            _platformRenderInterface.CreateGlyphRun(glyphTypeface, fontRenderingEmSize, glyphInfos, baselineOrigin);

        public IGeometryImpl CreateLineGeometry(Point p1, Point p2) =>
            _platformRenderInterface.CreateLineGeometry(p1, p2);

        public IGeometryImpl CreateRectangleGeometry(Rect rect) =>
            _platformRenderInterface.CreateRectangleGeometry(rect);

        public IPlatformRenderInterfaceRegion CreateRegion() =>
            _platformRenderInterface.CreateRegion();


        public IRenderTargetBitmapImpl CreateRenderTargetBitmap(PixelSize size, Vector dpi) =>
            _platformRenderInterface.CreateRenderTargetBitmap(size, dpi);

        public IStreamGeometryImpl CreateStreamGeometry() =>
            _platformRenderInterface.CreateStreamGeometry();

        public IWriteableBitmapImpl CreateWriteableBitmap(PixelSize size, Vector dpi, PixelFormat format, AlphaFormat alphaFormat) =>
            _platformRenderInterface.CreateWriteableBitmap(size, dpi, format, alphaFormat);

        public bool IsSupportedBitmapPixelFormat(PixelFormat format) =>
            _platformRenderInterface.IsSupportedBitmapPixelFormat(format);

        public IBitmapImpl LoadBitmap(string fileName) =>
            _platformRenderInterface.LoadBitmap(fileName);

        public IBitmapImpl LoadBitmap(Stream stream) =>
            _platformRenderInterface.LoadBitmap(stream);

        public IBitmapImpl LoadBitmap(PixelFormat format, AlphaFormat alphaFormat, nint data, PixelSize size, Vector dpi, int stride) =>
            _platformRenderInterface.LoadBitmap(format, alphaFormat, data, size, dpi, stride);

        public IBitmapImpl LoadBitmapToHeight(Stream stream, int height, BitmapInterpolationMode interpolationMode = BitmapInterpolationMode.HighQuality) =>
            _platformRenderInterface.LoadWriteableBitmapToHeight(stream, height, interpolationMode);

        public IBitmapImpl LoadBitmapToWidth(Stream stream, int width, BitmapInterpolationMode interpolationMode = BitmapInterpolationMode.HighQuality) =>
            _platformRenderInterface.LoadBitmapToWidth(stream, width, interpolationMode);

        public IWriteableBitmapImpl LoadWriteableBitmap(string fileName) =>
            _platformRenderInterface.LoadWriteableBitmap(fileName);

        public IWriteableBitmapImpl LoadWriteableBitmap(Stream stream) =>
            _platformRenderInterface.LoadWriteableBitmap(stream);

        public IWriteableBitmapImpl LoadWriteableBitmapToHeight(Stream stream, int height, BitmapInterpolationMode interpolationMode = BitmapInterpolationMode.HighQuality) =>
            _platformRenderInterface.LoadWriteableBitmapToHeight(stream, height, interpolationMode);

        public IWriteableBitmapImpl LoadWriteableBitmapToWidth(Stream stream, int width, BitmapInterpolationMode interpolationMode = BitmapInterpolationMode.HighQuality) =>
            _platformRenderInterface.LoadWriteableBitmapToWidth(stream, width, interpolationMode);

        public IBitmapImpl ResizeBitmap(IBitmapImpl bitmapImpl, PixelSize destinationSize, BitmapInterpolationMode interpolationMode = BitmapInterpolationMode.HighQuality) =>
            _platformRenderInterface.ResizeBitmap(bitmapImpl, destinationSize, interpolationMode);
    }
}
