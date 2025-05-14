using Avalonia;
using Avalonia.Controls.Embedding;
using Avalonia.Rendering.Composition;
using Avalonia.Skia;
using VL.Core.Import;
using VL.Lib.IO.Notifications;
using RectangleF = Stride.Core.Mathematics.RectangleF;

namespace VL.Skia.Avalonia
{
    [ProcessNode(HasStateOutput = true, Name = "AvaloniaLayer", FragmentSelection = FragmentSelection.Explicit)]
    public sealed class AvaloniaLayer : ILayer, IDisposable
    {
        private readonly AvaloniaRootImpl rootImpl;
        private readonly EmbeddableControlRoot controlRoot;

        [Fragment]
        public AvaloniaLayer()
        {
            AvaloniaInitializer.Init();

            var locator = AvaloniaLocator.Current;
            rootImpl = new AvaloniaRootImpl(locator.GetRequiredService<Compositor>());
            controlRoot = new EmbeddableControlRoot(rootImpl);
        }

        [Fragment]
        public object Content
        {
            set => controlRoot.Content = value;
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
