using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "GridSplitter")]
public partial class GridSplitterWrapper : ControlNodeBase<GridSplitter>
{
    [Fragment]
    public GridSplitterWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

    [ImplementProperty("GridSplitter.BackgroundProperty")]
    private Optional<IBrush> _background;

    [ImplementProperty("GridSplitter.ResizeDirectionProperty")]
    private Optional<GridResizeDirection> _resizeDirection;
}

