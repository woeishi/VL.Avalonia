using Avalonia.Controls.Shapes;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "Rectangle")]
public partial class RectangleWrapper : ControlNodeBase<Rectangle>
{
    [Fragment]
    public RectangleWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

    [ImplementProperty("Rectangle.FillProperty")]
    protected Optional<IBrush> _fill;

    [ImplementProperty("Rectangle.WidthProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _width;

    [ImplementProperty("Rectangle.HeightProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _height;

    [ImplementProperty("Rectangle.OpacityProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _opacity;
}

