using Avalonia.Controls.Embedding;
using Avalonia.Skia;
using VL.Core.Import;
using VL.Lib.IO.Notifications;
using RectangleF = Stride.Core.Mathematics.RectangleF;

namespace VL.Skia.Avalonia
{
    [ProcessNode(HasStateOutput = true, Name = "AvaloniaLayer")]
    public sealed class AvaloniaLayer : ILayer, IDisposable
    {
        private readonly AvaloniaRootImpl rootImpl;
        private readonly EmbeddableControlRoot controlRoot;
        public AvaloniaLayer()
        {
            AvaloniaInitializer.Init();
            rootImpl = new AvaloniaRootImpl();
            controlRoot = new EmbeddableControlRoot(rootImpl);
        }
        public object Content
        {
            get => controlRoot.Content;
            set => controlRoot.Content = value;
        }
        public void Dispose()
        {
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
            }
            rootImpl.Render(caller);
        }
    }

}
