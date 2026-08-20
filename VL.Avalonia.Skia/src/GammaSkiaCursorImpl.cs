using Avalonia.Input;
using Avalonia.Platform;
using WinFormsCursor = System.Windows.Forms.Cursor;

namespace VL.Avalonia.Skia;

/// <summary>
/// Wraps a WinForms cursor so <see cref="GammaTopLevelImpl.SetCursor"/> can hand it to the control
/// hosting the layer. Only the standard cursor set is supported.
/// </summary>
internal sealed class GammaSkiaCursorImpl : ICursorImpl
{
    public GammaSkiaCursorImpl(StandardCursorType cursorType, WinFormsCursor? cursor)
    {
        CursorType = cursorType;
        Cursor = cursor;
    }

    public StandardCursorType CursorType { get; }

    /// <summary>
    /// The cursor to display, or null for <see cref="StandardCursorType.None"/>, where the cursor
    /// gets hidden instead.
    /// </summary>
    public WinFormsCursor? Cursor { get; }

    public bool IsHidden => CursorType == StandardCursorType.None;

    // We only ever wrap the shared, process-wide System.Windows.Forms.Cursors instances, so there
    // is no handle of our own to release.
    public void Dispose() { }
}
