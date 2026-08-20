using System.Diagnostics;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Raw;
using Avalonia.Platform;
using Avalonia.Rendering.Composition;
using Avalonia.Skia;
using VL.Lib.IO.Notifications;
using VL.Skia;
using Platform = Avalonia.Input.Platform;
using Point = Avalonia.Point;
using Size = Avalonia.Size;
using TouchDevice = Avalonia.Input.TouchDevice;
using Vector2 = Stride.Core.Mathematics.Vector2;
using WinFormsControl = System.Windows.Forms.Control;
using WinFormsCursor = System.Windows.Forms.Cursor;
using WinFormsCursors = System.Windows.Forms.Cursors;

namespace VL.Avalonia.Skia
{
    /// <summary>
    /// vvvv gamma ITopLevelImpl implementation for Avalonia.
    /// </summary>
    sealed class GammaTopLevelImpl : ITopLevelImpl
    {
        public IKeyboardDevice KeyboardDevice { get; }
        public IMouseDevice MouseDevice { get; }
        public IPointerDevice TouchDevice { get; }
        public Compositor Compositor { get; }

        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();

        public GammaTopLevelImpl(Compositor compositor)
        {
            KeyboardDevice = GammaDevices.KeyboardDevice;
            MouseDevice = new MouseDevice();
            TouchDevice = new TouchDevice();

            Compositor = compositor;
        }

        private float _scaling = 1.0f;
        public double RenderScaling => _scaling;
        public double DesktopScaling => _scaling;

        public void SetScaling(float scaling)
        {
            if (_scaling != scaling)
            {
                if (scaling == 0.0f)
                    throw new ArgumentOutOfRangeException("Scaling can't be 0!");

                _scaling = scaling;

                ScalingChanged?.Invoke(scaling);

                SetClientSize(_requestedSize, true);
            }
        }

        private Size _clientSize;
        private Size _requestedSize;
        public Size ClientSize => _clientSize;

        public void SetClientSize(Size clientSize, bool force = false)
        {
            if (_requestedSize != clientSize || force)
            {
                _requestedSize = clientSize;
                _clientSize = _requestedSize / _scaling;

                Resized?.Invoke(_clientSize, WindowResizeReason.Unspecified);
            }
        }

        // TODO: Implement
        // NEEDS WINDOW HANDLE HERE
        public IPlatformHandle? Handle => new PlatformHandle(IntPtr.Zero, "STUB");

        public IInputRoot InputRoot { get; set; }

        private RawInputModifiers _inputModifiers;
        public Action<RawInputEventArgs>? Input { get; set; }

        public void SetInputRoot(IInputRoot inputRoot) => InputRoot = inputRoot;

        /// <summary>
        /// When true (default), <see cref="Notify"/> subtracts the on-screen origin captured
        /// during the last <see cref="Render"/> from incoming positions so the layer hit-tests in
        /// its local space — useful when the layer is rendered as a sub-region of a larger
        /// viewport (e.g. embedded in a VL.ImGui SkiaWidget on Stride). The
        /// <see cref="SendNotification"/> path is unaffected; its caller supplies its own
        /// position transform.
        /// </summary>
        public bool CompensateRenderOffset { get; set; } = true;

        private Point _renderOffset;

        internal bool Notify(INotification notification, CallerInfo caller)
        {
            var position = new Point(0, 0);

            if (notification is NotificationWithPosition n)
            {
                position = n.Position.ToPoint();
                if (CompensateRenderOffset)
                    position = new Point(position.X - _renderOffset.X, position.Y - _renderOffset.Y);
            }

            return HandleNotification(notification, position);
        }

        internal bool SendNotification(
            INotification notification,
            Func<NotificationWithPosition, Vector2> getPosition
        )
        {
            var position = new Point(0, 0);

            if (notification is NotificationWithPosition n)
                position = getPosition(n).ToPoint();

            return HandleNotification(notification, position);
        }


        internal bool HandleNotification(INotification notification, Point position)
        {
            if (InputRoot is null || Input is not { } input)
            {
                return false;
            }

            if (notification is MouseNotification mouseNotification)
                return HandleMouseNotification(mouseNotification, input, position);
            if (notification is KeyNotification keyNotification)
                return HandleKeyNotification(keyNotification, input);
            if (notification is TouchNotification touchNotification)
                return HandleTouchNotification(touchNotification, input, position);
            return false;
        }

        private bool HandleMouseNotification(
            MouseNotification notification,
            Action<RawInputEventArgs> input,
            Point position
        )
        {
            // While touches are active, Stride still emits synthetic mouse events that Windows
            // generates at OS level for the touch (via VL.Stride.InputTranslation's separate
            // MouseButtonListener — independent of the touch-notification fallback we suppress
            // with notification.Handled). Avalonia has only one mouse pointer instance, so these
            // synthetic clicks compete with the touch pointers for capture and break multi-touch.
            // Drop them whenever any touch is in flight.
            if (_activeTouchIds.Count > 0)
                return true;

            var e = default(RawInputEventArgs);

            if (notification is MouseButtonNotification m)
            {
                if (m.Kind == MouseNotificationKind.MouseDown)
                    _inputModifiers |= m.Buttons.ToModifier();
                else if (m.Kind == MouseNotificationKind.MouseUp)
                    _inputModifiers ^= m.Buttons.ToModifier();
            }

            if (notification is MouseDownNotification mouseDown)
                input(
                    e = new RawPointerEventArgs(
                        MouseDevice,
                        Timestamp,
                        InputRoot,
                        mouseDown.Buttons.ToEventType(false),
                        position / _scaling,
                        _inputModifiers
                    )
                );
            else if (notification is MouseUpNotification mouseUp)
                input(
                    e = new RawPointerEventArgs(
                        MouseDevice,
                        Timestamp,
                        InputRoot,
                        mouseUp.Buttons.ToEventType(true),
                        position / _scaling,
                        _inputModifiers
                    )
                );
            else if (notification is MouseMoveNotification mouseMove)
                input(
                    e = new RawPointerEventArgs(
                        MouseDevice,
                        Timestamp,
                        InputRoot,
                        RawPointerEventType.Move,
                        position / _scaling,
                        _inputModifiers
                    )
                );
            else if (notification is MouseWheelNotification mouseWheel)
                input(
                    e = new RawMouseWheelEventArgs(
                        MouseDevice,
                        Timestamp,
                        InputRoot,
                        position / _scaling,
                        new Vector(0, mouseWheel.WheelDelta / _scaling * 0.01),
                        _inputModifiers
                    )
                );

            if (e != null)
                return e.Handled;

            return false;
        }

        private bool HandleKeyNotification(
            KeyNotification notification,
            Action<RawInputEventArgs> input
        )
        {
            var e = default(RawInputEventArgs);

            if (notification is KeyCodeNotification keyCode)
            {
                _inputModifiers = keyCode.KeyData.ToModifier();
            }

            if (notification is KeyDownNotification keyDown)
                input(
                    e = new RawKeyEventArgs(
                        KeyboardDevice,
                        Timestamp,
                        InputRoot,
                        RawKeyEventType.KeyDown,
                        keyDown.KeyData.ToKey(),
                        _inputModifiers,
                        keyDown.KeyData.ToPhysicalKey(),
                        keyDown.KeyData.ToKeySymbol()
                    )
                );
            else if (notification is KeyUpNotification keyUp)
                input(
                    e = new RawKeyEventArgs(
                        KeyboardDevice,
                        Timestamp,
                        InputRoot,
                        RawKeyEventType.KeyUp,
                        keyUp.KeyData.ToKey(),
                        _inputModifiers,
                        keyUp.KeyData.ToPhysicalKey(),
                        keyUp.KeyData.ToKeySymbol()
                    )
                );
            else if (
                notification is KeyPressNotification keyPress
                && !char.IsControl(keyPress.KeyChar)
            )
                input(
                    e = new RawTextInputEventArgs(
                        KeyboardDevice,
                        Timestamp,
                        InputRoot,
                        keyPress.KeyChar.ToString()
                    )
                );

            if (e != null)
                return e.Handled;

            return false;
        }

        // Stride recycles touch ids (0, 1, 2 ...) after a finger lifts, which confuses Avalonia's
        // per-pointer capture tracking. Map each incoming touch session to a locally-unique
        // RawPointerId that's never reused, so back-to-back touches look like distinct pointers.
        private long _nextRawPointerId = 1;
        private readonly Dictionary<int, long> _activeTouchIds = new();

        private bool HandleTouchNotification(
            TouchNotification notification,
            Action<RawInputEventArgs> input,
            Point position
        )
        {
            var eventType = notification.Kind.GetTouchPointerEventType();

            long rawPointerId;
            switch (notification.Kind)
            {
                case TouchNotificationKind.TouchDown:
                    rawPointerId = _nextRawPointerId++;
                    _activeTouchIds[notification.Id] = rawPointerId;
                    break;
                case TouchNotificationKind.TouchUp:
                    if (!_activeTouchIds.Remove(notification.Id, out rawPointerId))
                        rawPointerId = _nextRawPointerId++;
                    break;
                default:
                    if (!_activeTouchIds.TryGetValue(notification.Id, out rawPointerId))
                        rawPointerId = _nextRawPointerId++;
                    break;
            }

            var e = new RawPointerEventArgs(
                TouchDevice,
                Timestamp,
                InputRoot,
                eventType,
                position / _scaling,
                _inputModifiers
            )
            {
                RawPointerId = rawPointerId,
            };

            input(e);

            // Mark the notification as handled. Otherwise Stride's InputTranslation emits a
            // synthetic MouseDown fallback for every unhandled touch, which Avalonia then
            // routes alongside the real touch event, breaking multi-touch (a single Mouse
            // pointer can't track multiple fingers). We set the flag on the notification
            // itself rather than only returning true, so it propagates even when the upstream
            // caller (e.g. VL.Stride.SkiaRenderer's input subscription) discards the bool.
            // Avalonia's own e.Handled stays false on capture, so we can't rely on it.
            notification.Handled = true;
            return true;
        }

        public Action<Rect>? Paint { get; set; }

        public Action<Size, WindowResizeReason>? Resized { get; set; }
        public Action<double>? ScalingChanged { get; set; }
        public Action<WindowTransparencyLevel>? TransparencyLevelChanged { get; set; }

        public Action? Closed { get; set; }
        public Action? LostFocus { get; set; }

        public WindowTransparencyLevel TransparencyLevel { get; set; } =
            WindowTransparencyLevel.None;

        public AcrylicPlatformCompensationLevels AcrylicCompensationLevels { get; } =
            new(1.0, 1.0, 1.0);

        private ulong Timestamp => (ulong)_stopwatch.ElapsedMilliseconds;

        public IPopupImpl? CreatePopup() => null;

        private readonly List<object> _surfaces = new List<object>();
        public IEnumerable<object> Surfaces => _surfaces;

        internal void Render(CallerInfo caller)
        {
            var bounds = caller.ViewportBounds.ToAvaloniaRect();
            SetClientSize(bounds.Size);
            _renderOffset = bounds.Position;

            _surfaces.Clear();
            _surfaces.Add(caller);

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
            ResetCursor();

            // TODO
        }

        public Point PointToClient(PixelPoint point) => point.ToPoint(_scaling);

        public PixelPoint PointToScreen(Point point) => PixelPoint.FromPoint(point, _scaling);

        // The layer has no window of its own, so cursors are applied to the WinForms control that
        // hosts us (the VL.Skia renderer). We can't take it from the notification sender: upstream
        // layers such as TransformUpstream replace it with their own space-mapping sender. Instead
        // we look up the window under the mouse - Avalonia only asks for a cursor while the
        // pointer is over us, so that window is our host. Hosts that aren't WinForms based (e.g.
        // Stride) resolve to null and simply don't get cursor changes.
        private WinFormsControl? _hostControl;
        private GammaSkiaCursorImpl? _cursor;
        private bool _cursorHidden;

        public void SetCursor(ICursorImpl? cursor)
        {
            var impl = cursor as GammaSkiaCursorImpl;
            if (ReferenceEquals(_cursor, impl))
                return;

            _cursor = impl;
            ApplyCursor();
        }

        private void ApplyCursor()
        {
            var control = ResolveHostControl();

            if (!ReferenceEquals(control, _hostControl))
            {
                // Hand the previous host back its default cursor before moving on.
                if (_hostControl is { IsDisposed: false } previous)
                    previous.Cursor = WinFormsCursors.Default;

                _hostControl = control;
            }

            if (control is null)
                return;

            var hidden = _cursor?.IsHidden ?? false;
            if (hidden != _cursorHidden)
            {
                _cursorHidden = hidden;
                if (hidden)
                    WinFormsCursor.Hide();
                else
                    WinFormsCursor.Show();
            }

            var target = _cursor?.Cursor ?? WinFormsCursors.Default;
            if (!ReferenceEquals(control.Cursor, target))
                control.Cursor = target;
        }

        private WinFormsControl? ResolveHostControl()
        {
            var handle = WindowFromPoint(GetCursorPosition());
            if (handle == IntPtr.Zero)
                return null;

            var control = WinFormsControl.FromChildHandle(handle);
            return control is { IsDisposed: false } ? control : null;
        }

        private static POINT GetCursorPosition()
        {
            var position = WinFormsCursor.Position;
            return new POINT { X = position.X, Y = position.Y };
        }

        private void ResetCursor()
        {
            if (_cursorHidden)
            {
                _cursorHidden = false;
                WinFormsCursor.Show();
            }

            if (_hostControl is { IsDisposed: false } control)
                control.Cursor = WinFormsCursors.Default;

            _cursor = null;
            _hostControl = null;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(POINT point);

        public void SetFrameThemeVariant(PlatformThemeVariant themeVariant)
        {
            // TODO:
        }

        public void SetTransparencyLevelHint(
            IReadOnlyList<WindowTransparencyLevel> transparencyLevels
        )
        {
            foreach (var transparencyLevel in transparencyLevels)
            {
                if (
                    transparencyLevel == WindowTransparencyLevel.Transparent
                    || transparencyLevel == WindowTransparencyLevel.None
                )
                {
                    TransparencyLevel = transparencyLevel;
                    return;
                }
            }
        }

        public object? TryGetFeature(Type featureType)
        {
            if (featureType == typeof(Platform.IClipboard))
            {
                return AvaloniaLocator.Current.GetService<Platform.IClipboard>();
            }
            return null;
        }

        internal bool SendNotification(INotification notification, bool v1, bool v2)
        {
            throw new NotImplementedException();
        }
    }
}
