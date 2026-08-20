using SkiaSharp;
using Stride.Core.Mathematics;
using VL.Avalonia.Skia;
using VL.Core;
using VL.Core.Import;
using VL.Lib.IO.Notifications;
using VL.Lib.Mathematics;
using VL.Skia;

namespace VL.Avalonia.Controls
{
    [ProcessNode]
    public abstract class SkiaMediaControlWrapperBase<T> : ControlNodeBase<T>
        where T : SkiaMediaControlBase, new()
    {
        [Fragment]
        public SkiaMediaControlWrapperBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        protected Optional<SizeMode> _mode;

        public void SetSizeMode(Optional<SizeMode> mode)
        {
            if (_mode != mode)
            {
                if (mode.HasValue)
                {
                    _output.Mode = mode.Value;
                }
                else
                {
                    _output.Mode = null;
                }

                _mode = mode;
            }
        }

        protected Optional<RectangleAnchor> _anchor;

        public void SetAnchor(
            [Pin(Visibility = Model.PinVisibility.Optional)] Optional<RectangleAnchor> anchor
        )
        {
            if (_anchor != anchor)
            {
                if (anchor.HasValue)
                {
                    _output.Anchor = anchor.Value;
                }
                else
                {
                    _output.Anchor = null;
                }

                _anchor = anchor;
            }
        }
    }

    [ProcessNode(Name = "SkiaImageControl")]
    public partial class SkiaImageControlWrapper : SkiaMediaControlWrapperBase<SkiaImageControl>
    {
        [Fragment]
        public SkiaImageControlWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        protected Optional<SKImage> _image;

        [Fragment(Order = PinOrder.Main)]
        public void SetImage(Optional<SKImage> image)
        {
            if (_image != image)
            {
                if (image.HasValue)
                {
                    _output.Image = image.Value;
                }
                else
                {
                    _output.Image = null;
                }

                _image = image;
            }
        }
    }

    [ProcessNode(Name = "SkiaPictureControl")]
    public partial class SkiaPictureControlWrapper : SkiaMediaControlWrapperBase<SkiaPictureControl>
    {
        [Fragment]
        public SkiaPictureControlWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        protected Optional<SKPicture> _picture;

        [Fragment(Order = PinOrder.Main)]
        public void SetPicture(Optional<SKPicture> picture)
        {
            if (_picture != picture)
            {
                if (picture.HasValue)
                {
                    _output.Picture = picture.Value;
                }
                else
                {
                    _output.Picture = null;
                }

                _picture = picture;
            }
        }
    }

    [ProcessNode(Name = "SkiaLayerControl")]
    public partial class SkiaLayerControlWrapper
        : SkiaMediaControlWrapperBase<SkiaLayerControl>,
            IDisposable
    {
        [Fragment]
        public SkiaLayerControlWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        private IDisposable? _notificationsSubscriptions;
        private Optional<IObservable<INotification>> _notificationsSource;

        [Fragment(Order = PinOrder.Main)]
        public void SetNotificationsSource(
            [Pin(Visibility = Model.PinVisibility.Optional)]
                Optional<IObservable<INotification>> notificationsSource
        )
        {
            if (notificationsSource.HasValue)
            {
                if (notificationsSource.Value.Equals(_notificationsSource.Value))
                    return;

                _notificationsSubscriptions?.Dispose();

                _notificationsSource = notificationsSource;

                _notificationsSubscriptions = _notificationsSource.Value.Subscribe(notification =>
                    _layer.Value?.Notify(notification, _callerInfo.Value ?? CallerInfo.Default)
                );
            }
            else
            {
                _notificationsSubscriptions?.Dispose();

                _notificationsSource = default;
            }
        }

        private Optional<CallerInfo> _callerInfo;

        [Fragment(Order = PinOrder.Main)]
        public void SetCallerInfo(
            [Pin(Visibility = Model.PinVisibility.Optional)] Optional<CallerInfo> callerInfo
        )
        {
            if (callerInfo != _callerInfo)
            {
                if (callerInfo.HasValue)
                {
                    _callerInfo = callerInfo.Value;
                }
                else
                {
                    _callerInfo = default;
                }
                _callerInfo = callerInfo;
            }
        }

        protected Optional<ILayer> _layer;

        [Fragment(Order = PinOrder.Main)]
        public void SetLayer(Optional<ILayer> layer)
        {
            if (_layer != layer)
            {
                if (layer.HasValue)
                {
                    _output.Layer = layer.Value;
                }
                else
                {
                    _output.Layer = null;
                }

                _layer = layer;
            }
        }

        protected Optional<RectangleF> _layerBounds;

        [Fragment(Order = PinOrder.Action)]
        public void SetLayerBounds(
            [Pin(Visibility = Model.PinVisibility.Optional)] Optional<RectangleF> layerBounds
        )
        {
            if (_layerBounds != layerBounds)
            {
                if (layerBounds.HasValue)
                {
                    _output.LayerBounds = layerBounds.Value;
                }
                else
                {
                    _output.LayerBounds = null;
                }

                _layerBounds = layerBounds;
            }
        }

        public void Dispose()
        {
            _notificationsSubscriptions?.Dispose();
        }
    }
}

