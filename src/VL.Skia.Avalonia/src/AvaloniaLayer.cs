using Avalonia;
using Avalonia.Controls.Embedding;
using Avalonia.Rendering.Composition;
using Avalonia.Skia;
using Avalonia.Styling;
using VL.Core.Import;
using VL.Lib.IO.Notifications;
using VL.Model;
using Application = Avalonia.Application;
using RectangleF = Stride.Core.Mathematics.RectangleF;

namespace VL.Skia.Avalonia
{

    [ProcessNode(HasStateOutput = true, Name = "AvaloniaLayer", FragmentSelection = FragmentSelection.Explicit)]
    public sealed class AvaloniaLayer : ILayer, IDisposable
    {
        private readonly AvaloniaRootImpl rootImpl;
        private readonly EmbeddableControlRoot controlRoot;

        [Fragment]
        public AvaloniaLayer(
            [Pin(Visibility = PinVisibility.Optional)]
            Action<Application>? onSetupApplication = null
            )
        {
            AvaloniaInitializer.Init();

            var locator = AvaloniaLocator.Current;
            rootImpl = new AvaloniaRootImpl(locator.GetRequiredService<Compositor>());
            controlRoot = new EmbeddableControlRoot(rootImpl);

            onSetupApplication?.Invoke(AvaloniaInitializer.Instance);
        }

        [Fragment]
        public object Content
        {
            set => controlRoot.Content = value;
        }

        private ThemeVariant _requestedThemeVariant = ThemeVariant.Default;
        /// <summary>
        /// Requested Theme Variant
        /// </summary>
        [Fragment]
        public ThemeVariant RequestedThemeVariant
        {
            private get => _requestedThemeVariant;
            set
            {
                if (_requestedThemeVariant != value)
                {
                    _requestedThemeVariant = value;
                    controlRoot.RequestedThemeVariant = value;
                }
            }
        }

        public void Dispose()
        {
            controlRoot.StopRendering();
            controlRoot.Dispose();
        }

        public RectangleF? Bounds => default;
        public bool Notify(INotification notification, CallerInfo caller)
        {
            return rootImpl.Notify(notification, caller);
        }
        public void Render(CallerInfo caller)
        {
            if (!controlRoot.IsInitialized)
            {
                rootImpl.ClientSize = caller.ViewportBounds.ToAvaloniaRect().Size;
                controlRoot.Prepare();
                controlRoot.StartRendering();
            }
            rootImpl.Render(caller);

            // TODO: this is a hack to trigger the render loop
            GammaRenderTimer.Instance.TriggerTick(TimeSpan.FromMilliseconds(16));
        }
    }
}
