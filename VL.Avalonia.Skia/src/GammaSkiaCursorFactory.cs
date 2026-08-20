using System.Collections.Concurrent;
using Avalonia;
using Avalonia.Input;
using Avalonia.Platform;
using WinFormsCursor = System.Windows.Forms.Cursor;
using WinFormsCursors = System.Windows.Forms.Cursors;

namespace VL.Avalonia.Skia
{
    internal sealed class GammaSkiaCursorFactory : ICursorFactory
    {
        private static readonly ConcurrentDictionary<
            StandardCursorType,
            GammaSkiaCursorImpl
        > _cursors = new();

        /// <summary>
        /// Custom cursors are not supported: the layer has no window of its own, so all we can do
        /// is assign one of the host control's cursors. Returning null makes Avalonia fall back to
        /// the default cursor.
        /// </summary>
        public ICursorImpl? CreateCursor(IBitmapImpl cursor, PixelPoint hotSpot) => null;

        public ICursorImpl GetCursor(StandardCursorType cursorType) =>
            _cursors.GetOrAdd(
                cursorType,
                static type => new GammaSkiaCursorImpl(type, ToWinFormsCursor(type))
            );

        private static WinFormsCursor? ToWinFormsCursor(StandardCursorType cursorType) =>
            cursorType switch
            {
                StandardCursorType.Arrow => WinFormsCursors.Arrow,
                StandardCursorType.Ibeam => WinFormsCursors.IBeam,
                StandardCursorType.Wait => WinFormsCursors.WaitCursor,
                StandardCursorType.Cross => WinFormsCursors.Cross,
                StandardCursorType.UpArrow => WinFormsCursors.UpArrow,
                StandardCursorType.SizeWestEast => WinFormsCursors.SizeWE,
                StandardCursorType.SizeNorthSouth => WinFormsCursors.SizeNS,
                StandardCursorType.SizeAll => WinFormsCursors.SizeAll,
                StandardCursorType.No => WinFormsCursors.No,
                StandardCursorType.Hand => WinFormsCursors.Hand,
                StandardCursorType.AppStarting => WinFormsCursors.AppStarting,
                StandardCursorType.Help => WinFormsCursors.Help,
                StandardCursorType.TopSide => WinFormsCursors.SizeNS,
                StandardCursorType.BottomSide => WinFormsCursors.SizeNS,
                StandardCursorType.LeftSide => WinFormsCursors.SizeWE,
                StandardCursorType.RightSide => WinFormsCursors.SizeWE,
                StandardCursorType.TopLeftCorner => WinFormsCursors.SizeNWSE,
                StandardCursorType.BottomRightCorner => WinFormsCursors.SizeNWSE,
                StandardCursorType.TopRightCorner => WinFormsCursors.SizeNESW,
                StandardCursorType.BottomLeftCorner => WinFormsCursors.SizeNESW,
                // WinForms has no dedicated drag cursors.
                StandardCursorType.DragMove => WinFormsCursors.SizeAll,
                StandardCursorType.DragCopy => WinFormsCursors.Arrow,
                StandardCursorType.DragLink => WinFormsCursors.Arrow,
                // Handled by hiding the cursor, see GammaSkiaCursorImpl.IsHidden.
                StandardCursorType.None => null,
                _ => WinFormsCursors.Default,
            };
    }
}
