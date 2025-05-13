using Avalonia;
using Avalonia.OpenGL;
using Avalonia.Platform;
using Avalonia.Skia;

namespace VL.Skia.Avalonia
{
    internal class GammaSkiaBackendContext : IPlatformRenderInterfaceContext
    {
        private ISkiaGpu? _gpu;
        public GammaSkiaBackendContext(ISkiaGpu? gpu)
        {
            _gpu = gpu;

            var features = new Dictionary<Type, object>();

            if (gpu != null)
            {
                void TryFeature<T>() where T : class
                {
                    if (gpu!.TryGetFeature<T>() is { } feature)
                        features!.Add(typeof(T), feature);
                }
                // TODO12: extend ISkiaGpu with PublicFeatures instead
                TryFeature<IOpenGlTextureSharingRenderInterfaceContextFeature>();
                TryFeature<IExternalObjectsRenderInterfaceContextFeature>();
            }

            PublicFeatures = features;
        }

        public void Dispose()
        {
            _gpu?.Dispose();
            _gpu = null;
        }

        /// <inheritdoc />
        public IRenderTarget CreateRenderTarget(IEnumerable<object> surfaces)
        {
            //if (surfaces is not IList)
            //    surfaces = surfaces.ToList();

            //if (_gpu?.TryCreateRenderTarget(surfaces) is { } gpuRenderTarget)
            //{
            //    return new SkiaGpuRenderTarget(_gpu, gpuRenderTarget);
            //}

            //foreach (var surface in surfaces)
            //{
            //    if (surface is IFramebufferPlatformSurface framebufferSurface)
            //        return new FramebufferRenderTarget(framebufferSurface);
            //}

            //throw new NotSupportedException(
            //    "Don't know how to create a Skia render target from any of provided surfaces");

            foreach (var s in surfaces)
                if (s is CallerInfo c)
                    return new GammaSkiaRenderTarget(c);

            // Can't figure out
            // return _platformRenderInterface.CreateRenderTarget(surfaces);

            throw new NotSupportedException("Don't know how to create a Skia render target from any of provided surfaces");
        }

        public IDrawingContextLayerImpl CreateOffscreenRenderTarget(PixelSize pixelSize, double scaling)
        {
            //using (var gr = (_gpu as ISkiaGpuWithPlatformGraphicsContext)?.TryGetGrContext())
            //{
            //    var createInfo = new SurfaceRenderTarget.CreateInfo
            //    {
            //        Width = pixelSize.Width,
            //        Height = pixelSize.Height,
            //        Dpi = new Vector(96 * scaling, 96 * scaling),
            //        Format = null,
            //        DisableTextLcdRendering = false,
            //        GrContext = gr?.Value,
            //        Gpu = _gpu,
            //        DisableManualFbo = true,
            //        Session = null
            //    };

            //    return new SurfaceRenderTarget(createInfo);
            //}

            throw new NotImplementedException();
        }

        public bool IsLost => _gpu?.IsLost ?? false;
        public IReadOnlyDictionary<Type, object> PublicFeatures { get; }

        public object? TryGetFeature(Type featureType) => _gpu?.TryGetFeature(featureType);
    }
}
