using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Raw;
using Avalonia.Platform;
using Avalonia.Rendering.Composition;
using Avalonia.Skia;
using System.Diagnostics;
using VL.Lib.IO.Notifications;
using Point = Avalonia.Point;
using Size = Avalonia.Size;

namespace VL.Skia.Avalonia
{
    // source
    // https://github.com/vvvv/VL.StandardLibs/blob/dev/azeno/avalonia/VL.AvaloniaUI/src/RootElementImpl.cs
    sealed class AvaloniaRootImpl : ITopLevelImpl
    {
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();


        // https://github.com/AvaloniaUI/Avalonia/issues/11381
        public IKeyboardDevice KeyboardDevice { get; }
        public IMouseDevice MouseDevice { get; }
        public IInputRoot InputRoot { get; set; }

        public AvaloniaRootImpl(Compositor compositor)
        {
            MouseDevice = new MouseDevice();
            //https://github.com/AvaloniaUI/Avalonia/blob/master/src/Windows/Avalonia.Win32/Input/WindowsKeyboardDevice.cs#L7
            //KeyboardDevice = WindowsKeyboardDevice.Instance

            Compositor = compositor;
        }


        // That's something important throws without it
        // https://github.com/MrJul/Estragonia/blob/0aa807421c9e52bc56128c69798ffc11093f0a61/src/JLeb.Estragonia/AvaloniaControl.cs#L130
        // https://github.com/MrJul/Estragonia/blob/0aa807421c9e52bc56128c69798ffc11093f0a61/src/JLeb.Estragonia/GodotPlatform.cs#L48
        public Compositor Compositor { get; set; }

        // TODO: DPI
        public double DesktopScaling => 1.0;

        private double _renderScaling = 1;
        /// <summary>
        /// Render Scaling
        /// </summary>
        public double RenderScaling
        {
            get => _renderScaling;
            set
            {
                if (_renderScaling != value)
                {
                    _renderScaling = value;
                    ScalingChanged?.Invoke(value);
                }
            }
        }

        private Size _clientSize;
        /// <summary>
        /// Client Size
        /// </summary>
        public Size ClientSize
        {
            get => _clientSize;
            set
            {
                if (_clientSize != value)
                {
                    _clientSize = value;
                    Resized?.Invoke(value, WindowResizeReason.Unspecified);
                }
            }
        }


        private readonly List<CallerInfo> _callerInfos = new List<CallerInfo>();
        /// <summary>
        /// Caller Info's
        /// </summary>
        public IEnumerable<object> Surfaces => _callerInfos;

        // TODO: Implement
        // NEEDS WINDO HANDLE HERE
        public IPlatformHandle? Handle => new PlatformHandle(IntPtr.Zero, "STUB");



        public Action<RawInputEventArgs>? Input { get; set; }

        private RawInputModifiers modifiers;
        internal bool Notify(INotification notification, CallerInfo caller)
        {
            var e = default(RawInputEventArgs);

            if (notification is KeyCodeNotification keyCode)
            {
                modifiers = keyCode.KeyData.ToModifier();
            }
            if (notification is MouseButtonNotification m)
            {
                if (m.Kind == MouseNotificationKind.MouseDown)
                    modifiers |= m.Buttons.ToModifier();
                else if (m.Kind == MouseNotificationKind.MouseUp)
                    modifiers ^= m.Buttons.ToModifier();
            }

            if (notification is KeyDownNotification keyDown)
                Input?.Invoke(e = new RawKeyEventArgs(KeyboardDevice, Timestamp, InputRoot, RawKeyEventType.KeyDown, keyDown.KeyData.ToKey(), modifiers));
            else if (notification is KeyUpNotification keyUp)
                Input?.Invoke(e = new RawKeyEventArgs(KeyboardDevice, Timestamp, InputRoot, RawKeyEventType.KeyUp, keyUp.KeyData.ToKey(), modifiers));
            else if (notification is KeyPressNotification keyPress && !char.IsControl(keyPress.KeyChar))
                Input?.Invoke(e = new RawTextInputEventArgs(KeyboardDevice, Timestamp, InputRoot, keyPress.KeyChar.ToString()));
            else if (notification is MouseDownNotification mouseDown)
                Input?.Invoke(e = new RawPointerEventArgs(MouseDevice, Timestamp, InputRoot, mouseDown.Buttons.ToEventType(false), mouseDown.Position.ToPoint(), modifiers));
            else if (notification is MouseUpNotification mouseUp)
                Input?.Invoke(e = new RawPointerEventArgs(MouseDevice, Timestamp, InputRoot, mouseUp.Buttons.ToEventType(true), mouseUp.Position.ToPoint(), modifiers));
            else if (notification is MouseMoveNotification mouseMove)
                Input?.Invoke(e = new RawPointerEventArgs(MouseDevice, Timestamp, InputRoot, RawPointerEventType.Move, mouseMove.Position.ToPoint(), modifiers));
            else if (notification is MouseWheelNotification mouseWheel)
                Input?.Invoke(e = new RawMouseWheelEventArgs(MouseDevice, Timestamp, InputRoot, mouseWheel.Position.ToPoint(), new Vector(mouseWheel.WheelDelta, 0), modifiers));

            if (e != null)
                return e.Handled;

            return false;
        }


        public Action<Rect>? Paint { get; set; }

        public Action<Size, WindowResizeReason>? Resized { get; set; }

        // Wonder is it DesktopScaling or RenderScaling
        public Action<double>? ScalingChanged { get; set; }


        public Action<WindowTransparencyLevel>? TransparencyLevelChanged { get; set; }


        public Action? Closed { get; set; }
        public Action? LostFocus { get; set; }

        public WindowTransparencyLevel TransparencyLevel { get; set; } = WindowTransparencyLevel.None;

        // https://github.com/MrJul/Estragonia/blob/0aa807421c9e52bc56128c69798ffc11093f0a61/src/JLeb.Estragonia/GodotTopLevelImpl.cs#L76
        public AcrylicPlatformCompensationLevels AcrylicCompensationLevels { get; } = new(1.0, 1.0, 1.0);

        private ulong Timestamp => (ulong)_stopwatch.ElapsedMilliseconds;

        public IPopupImpl? CreatePopup()
        {
            return null;
        }

        internal void Render(CallerInfo caller)
        {
            var bounds = caller.ViewportBounds.ToAvaloniaRect();
            ClientSize = bounds.Size;

            _callerInfos.Clear();
            _callerInfos.Add(caller);

            caller.Canvas.Save();
            try
            {
                Paint?.Invoke(bounds);
            }
            finally
            {
                caller.Canvas.Restore();
            }
        }

        public void Dispose()
        {
            // TODO
        }

        public Point PointToClient(PixelPoint point) => point.ToPoint(RenderScaling);
        public PixelPoint PointToScreen(Point point) => PixelPoint.FromPoint(point, RenderScaling);

        // example from Godot
        // https://github.com/MrJul/Estragonia/blob/0aa807421c9e52bc56128c69798ffc11093f0a61/src/JLeb.Estragonia/GodotTopLevelImpl.cs#L364
        public void SetCursor(ICursorImpl? cursor)
        {
            //
        }

        public void SetFrameThemeVariant(PlatformThemeVariant themeVariant)
        {
            // throw new NotImplementedException();
        }



        public void SetInputRoot(IInputRoot inputRoot)
        {
            InputRoot = inputRoot;
        }

        // copy paste from here
        // https://github.com/MrJul/Estragonia/blob/0aa807421c9e52bc56128c69798ffc11093f0a61/src/JLeb.Estragonia/GodotTopLevelImpl.cs#L376
        public void SetTransparencyLevelHint(IReadOnlyList<WindowTransparencyLevel> transparencyLevels)
        {
            foreach (var transparencyLevel in transparencyLevels)
            {
                if (transparencyLevel == WindowTransparencyLevel.Transparent || transparencyLevel == WindowTransparencyLevel.None)
                {
                    TransparencyLevel = transparencyLevel;
                    return;
                }
            }
        }

        // example from Godot
        // https://github.com/MrJul/Estragonia/blob/0aa807421c9e52bc56128c69798ffc11093f0a61/src/JLeb.Estragonia/GodotTopLevelImpl.cs#L388
        public object? TryGetFeature(Type featureType)
        {
            // throws 
            return null;
        }
    }
}
