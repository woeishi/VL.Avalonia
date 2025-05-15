using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Raw;
using Avalonia.Platform;
using Avalonia.Rendering;
using Avalonia.Rendering.Composition;
using Avalonia.Skia;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using VL.Lib.IO.Notifications;
using VL.Skia;

namespace VL.Avalonia.Host
{
    // source
    // https://github.com/vvvv/VL.StandardLibs/blob/dev/azeno/avalonia/VL.AvaloniaUI/src/RootElementImpl.cs
    sealed class RootElementImpl : ITopLevelImpl
    {
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();


        // https://github.com/AvaloniaUI/Avalonia/issues/11381
        public IKeyboardDevice KeyboardDevice { get; }
        public IMouseDevice MouseDevice { get; }
        public IInputRoot InputRoot { get; set; }

        public RootElementImpl()
        {
            MouseDevice = new MouseDevice();
            //https://github.com/AvaloniaUI/Avalonia/blob/master/src/Windows/Avalonia.Win32/Input/WindowsKeyboardDevice.cs#L7
            //KeyboardDevice = WindowsKeyboardDevice.Instance
        }

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

        public IPlatformHandle? Handle => throw new NotImplementedException();


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

        // That's not on Elias file
        public Compositor Compositor { get; set; }

        public Action? Closed { get; set; }
        public Action? LostFocus { get; set; }

        public WindowTransparencyLevel TransparencyLevel { get; set; } = WindowTransparencyLevel.None;
        public AcrylicPlatformCompensationLevels AcrylicCompensationLevels { get; } = new AcrylicPlatformCompensationLevels();

        private ulong Timestamp => (ulong)_stopwatch.ElapsedMilliseconds;

        public IPopupImpl? CreatePopup()
        {
            return null;
        }

        public IRenderer CreateRenderer(IRenderRoot root)
        {
            // TODO
            return new SkiaImmediateRenderer(root);
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

        public void SetCursor(ICursorImpl? cursor)
        {

        }

        public void SetFrameThemeVariant(PlatformThemeVariant themeVariant)
        {
            throw new NotImplementedException();
        }



        public void SetInputRoot(IInputRoot inputRoot)
        {
            InputRoot = inputRoot;
        }

        public void SetTransparencyLevelHint(IReadOnlyList<WindowTransparencyLevel> transparencyLevels)
        {
            throw new NotImplementedException();
        }

        public object? TryGetFeature(Type featureType)
        {
            throw new NotImplementedException();
        }
    }
}
