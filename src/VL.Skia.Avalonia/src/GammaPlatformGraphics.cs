using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;

namespace VL.Skia.Avalonia
{
    sealed class GammaPlatformGraphics : IPlatformGraphics
    {
        bool IPlatformGraphics.UsesSharedContext => true;

        IPlatformGraphicsContext IPlatformGraphics.CreateContext()
        {
            throw new NotImplementedException();
        }

        IPlatformGraphicsContext IPlatformGraphics.GetSharedContext()
        {
            return new PlatformGraphicsContext();
        }

        sealed class PlatformGraphicsContext : ISkiaGpu
        {
            public bool IsLost => false;

            public void Dispose()
            {
            }

            public IDisposable EnsureCurrent()
            {
                return Disposable.Empty;
            }

            public ISkiaGpuRenderTarget? TryCreateRenderTarget(IEnumerable<object> surfaces)
            {
                // Shouldn't get called
                throw new NotImplementedException();
            }

            public ISkiaSurface? TryCreateSurface(PixelSize size, ISkiaGpuRenderSession? session)
            {
                // Shouldn't get called
                throw new NotImplementedException();
            }

            public object? TryGetFeature(Type featureType)
            {
                return null;
            }
        }
    }
}
