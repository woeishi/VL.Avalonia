using Avalonia.Input;
using VL.Core.Import;

namespace VL.Avalonia.Input
{
    [ProcessNode]
    public abstract class CursorNode
    {
        public Cursor Output { get; protected set; }
    }

    [ProcessNode]
    public abstract class StandardCursorNode : CursorNode
    {

        public StandardCursorNode(StandardCursorType cursorType)
        {
            Output = new Cursor(cursorType);
        }
    }

    [ProcessNode(Name = "ArrowCursor")]
    public class ArrowCursorNode : StandardCursorNode
    {
        public ArrowCursorNode() : base(StandardCursorType.Arrow) { }
    }

    [ProcessNode(Name = "IbeamCursor")]
    public class IbeamCursorNode : StandardCursorNode
    {
        public IbeamCursorNode() : base(StandardCursorType.Ibeam) { }
    }

    [ProcessNode(Name = "WaitCursor")]
    public class WaitCursorNode : StandardCursorNode
    {
        public WaitCursorNode() : base(StandardCursorType.Wait) { }
    }

    [ProcessNode(Name = "CrossCursor")]
    public class CrossCursorNode : StandardCursorNode
    {
        public CrossCursorNode() : base(StandardCursorType.Cross) { }
    }

    [ProcessNode(Name = "UpArrowCursor")]
    public class UpArrowCursorNode : StandardCursorNode
    {
        public UpArrowCursorNode() : base(StandardCursorType.UpArrow) { }
    }

    [ProcessNode(Name = "SizeWestEastCursor")]
    public class SizeWestEastCursorNode : StandardCursorNode
    {
        public SizeWestEastCursorNode() : base(StandardCursorType.SizeWestEast) { }
    }

    [ProcessNode(Name = "SizeNorthSouthCursor")]
    public class SizeNorthSouthCursorNode : StandardCursorNode
    {
        public SizeNorthSouthCursorNode() : base(StandardCursorType.SizeNorthSouth) { }
    }

    [ProcessNode(Name = "SizeAllCursor")]
    public class SizeAllCursorNode : StandardCursorNode
    {
        public SizeAllCursorNode() : base(StandardCursorType.SizeAll) { }
    }

    [ProcessNode(Name = "NoCursor")]
    public class NoCursorNode : StandardCursorNode
    {
        public NoCursorNode() : base(StandardCursorType.No) { }
    }

    [ProcessNode(Name = "HandCursor")]
    public class HandCursorNode : StandardCursorNode
    {
        public HandCursorNode() : base(StandardCursorType.Hand) { }
    }

    [ProcessNode(Name = "AppStartingCursor")]
    public class AppStartingCursorNode : StandardCursorNode
    {
        public AppStartingCursorNode() : base(StandardCursorType.AppStarting) { }
    }

    [ProcessNode(Name = "HelpCursor")]
    public class HelpCursorNode : StandardCursorNode
    {
        public HelpCursorNode() : base(StandardCursorType.Help) { }
    }

    [ProcessNode(Name = "TopSideCursor")]
    public class TopSideCursorNode : StandardCursorNode
    {
        public TopSideCursorNode() : base(StandardCursorType.TopSide) { }
    }

    [ProcessNode(Name = "BottomSideCursor")]
    public class BottomSideCursorNode : StandardCursorNode
    {
        public BottomSideCursorNode() : base(StandardCursorType.BottomSide) { }
    }

    [ProcessNode(Name = "LeftSideCursor")]
    public class LeftSideCursorNode : StandardCursorNode
    {
        public LeftSideCursorNode() : base(StandardCursorType.LeftSide) { }
    }

    [ProcessNode(Name = "RightSideCursor")]
    public class RightSideCursorNode : StandardCursorNode
    {
        public RightSideCursorNode() : base(StandardCursorType.RightSide) { }
    }

    [ProcessNode(Name = "TopLeftCornerCursor")]
    public class TopLeftCornerCursorNode : StandardCursorNode
    {
        public TopLeftCornerCursorNode() : base(StandardCursorType.TopLeftCorner) { }
    }

    [ProcessNode(Name = "TopRightCornerCursor")]
    public class TopRightCornerCursorNode : StandardCursorNode
    {
        public TopRightCornerCursorNode() : base(StandardCursorType.TopRightCorner) { }
    }

    [ProcessNode(Name = "BottomLeftCornerCursor")]
    public class BottomLeftCornerCursorNode : StandardCursorNode
    {
        public BottomLeftCornerCursorNode() : base(StandardCursorType.BottomLeftCorner) { }
    }

    [ProcessNode(Name = "BottomRightCornerCursor")]
    public class BottomRightCornerCursorNode : StandardCursorNode
    {
        public BottomRightCornerCursorNode() : base(StandardCursorType.BottomRightCorner) { }
    }

    [ProcessNode(Name = "DragMoveCursor")]
    public class DragMoveCursorNode : StandardCursorNode
    {
        public DragMoveCursorNode() : base(StandardCursorType.DragMove) { }
    }

    [ProcessNode(Name = "DragCopyCursor")]
    public class DragCopyCursorNode : StandardCursorNode
    {
        public DragCopyCursorNode() : base(StandardCursorType.DragCopy) { }
    }

    [ProcessNode(Name = "DragLinkCursor")]
    public class DragLinkCursorNode : StandardCursorNode
    {
        public DragLinkCursorNode() : base(StandardCursorType.DragLink) { }
    }

    [ProcessNode(Name = "NoneCursor")]
    public class NoneCursorNode : StandardCursorNode
    {
        public NoneCursorNode() : base(StandardCursorType.None) { }
    }

}
