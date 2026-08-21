using System.Runtime.InteropServices;
using Avalonia.Platform;
using WinFormsControl = System.Windows.Forms.Control;
using WinFormsCursor = System.Windows.Forms.Cursor;
using WinFormsCursors = System.Windows.Forms.Cursors;

namespace VL.Avalonia.Skia
{
    /// <summary>
    /// Applies Avalonia cursor changes to the WinForms control hosting a <see cref="GammaTopLevelImpl"/>.
    /// </summary>
    /// <remarks>
    /// The layer has no window of its own, so cursors are applied to the WinForms control that
    /// hosts us (the VL.Skia renderer). We can't take it from the notification sender: upstream
    /// layers such as TransformUpstream replace it with their own space-mapping sender. Instead we
    /// resolve the window under the mouse while handling a mouse notification (see
    /// <see cref="TrackHost"/>), where the pointer is provably over our host. Doing it from
    /// <see cref="SetCursor"/> would be wrong, since that can also be triggered by focus/layout
    /// changes while the mouse sits over a foreign window. Hosts that aren't WinForms based (e.g.
    /// Stride) resolve to null and simply don't get cursor changes.
    /// </remarks>
    internal sealed class WinFormsCursorController
    {
        private WinFormsControl? _hostControl;

        // The last control we saw alive on the WinForms UI thread. Cursor.Hide/Show wraps a
        // per-thread Win32 counter, so Show() must run on the exact thread that called Hide() -
        // we keep this around so we can still balance it even once _hostControl itself is gone
        // (e.g. disposed, or the pointer moved over a window we don't recognize).
        private WinFormsControl? _uiThreadAnchor;
        private IntPtr _hostHandle;
        private GammaSkiaCursorImpl? _cursor;
        private bool _cursorHidden;

        /// <summary>
        /// Resolves the WinForms control currently under the mouse and re-applies the cursor if
        /// it changed. Call this while handling a mouse notification, where the pointer is
        /// provably over our host.
        /// </summary>
        public void TrackHost()
        {
            var handle = WindowFromPoint(GetCursorPosition());
            if (handle == _hostHandle && _hostControl is { IsDisposed: false })
                return;

            _hostHandle = handle;

            var previous = _hostControl;
            var control = handle != IntPtr.Zero ? WinFormsControl.FromChildHandle(handle) : null;
            _hostControl = control is { IsDisposed: false } ? control : null;
            if (_hostControl is not null)
                _uiThreadAnchor = _hostControl;

            // Hand the control we're leaving back its default cursor.
            if (previous is { IsDisposed: false } && !ReferenceEquals(previous, _hostControl))
                RunOnControlThread(previous, () =>
                {
                    if (!previous.IsDisposed)
                        previous.Cursor = WinFormsCursors.Default;
                });

            Apply();
        }

        public void SetCursor(ICursorImpl? cursor)
        {
            _cursor = cursor as GammaSkiaCursorImpl;
            Apply();
        }

        /// <summary>
        /// Restores the current host's default cursor and forgets it. Call this on disposal.
        /// </summary>
        public void Reset()
        {
            var control = _hostControl is { IsDisposed: false } ? _hostControl : null;
            var anchor = control ?? (_uiThreadAnchor is { IsDisposed: false } ? _uiThreadAnchor : null);

            if (anchor is not null)
                RunOnControlThread(anchor, () =>
                {
                    if (control is { IsDisposed: false })
                        control.Cursor = WinFormsCursors.Default;
                    SetCursorHidden(false);
                });
            else
                _cursorHidden = false;

            _cursor = null;
            _hostControl = null;
            _uiThreadAnchor = null;
            _hostHandle = IntPtr.Zero;
        }

        private void Apply()
        {
            if (_hostControl is not { IsDisposed: false } control)
            {
                // Never leave the cursor hidden once we lost track of the host. Marshal to
                // whichever control we last saw alive on the UI thread, since Show() must run on
                // the same thread that called Hide().
                if (_uiThreadAnchor is { IsDisposed: false } anchor)
                    RunOnControlThread(anchor, () => SetCursorHidden(false));
                else
                    _cursorHidden = false;
                return;
            }

            var cursor = _cursor;
            RunOnControlThread(control, () =>
            {
                if (control.IsDisposed)
                    return;

                SetCursorHidden(cursor?.IsHidden ?? false);

                var target = cursor?.Cursor ?? WinFormsCursors.Default;
                if (!ReferenceEquals(control.Cursor, target))
                    control.Cursor = target;
            });
        }

        // Cursor.Hide/Show are refcounted, but the counter is per-thread (it's a thin wrapper
        // around the Win32 ShowCursor API), so this must run on the same UI thread that owns the
        // host control - callers reach this via RunOnControlThread.
        private void SetCursorHidden(bool hidden)
        {
            if (hidden == _cursorHidden)
                return;

            _cursorHidden = hidden;
            if (hidden)
                WinFormsCursor.Hide();
            else
                WinFormsCursor.Show();
        }

        // Notifications arrive on the VL render thread, but WinForms controls (Cursor included)
        // may only be touched from the thread that created their handle.
        private static void RunOnControlThread(WinFormsControl control, Action action)
        {
            if (control.IsDisposed)
                return;

            if (control.InvokeRequired)
            {
                try
                {
                    control.BeginInvoke(action);
                }
                catch (InvalidOperationException)
                {
                    // Handle not created yet, or the control got disposed in the meantime - safe
                    // to drop, cursor changes aren't critical to correctness.
                }
            }
            else
            {
                action();
            }
        }

        private static POINT GetCursorPosition()
        {
            var position = WinFormsCursor.Position;
            return new POINT { X = position.X, Y = position.Y };
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(POINT point);
    }
}
