using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using static VL.Avalonia.Styles;
using AvaloniaThickness = Avalonia.Thickness;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "Border")]
public partial class BorderWrapper
{
    [ImplementOutput]
    protected Border _output = new Border();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementClasses]
    protected Optional<string> _classes;

    [ImplementProperty("Border.ChildProperty", Order = -10)]
    protected Optional<Control> _child;

    [ImplementProperty("Border.NameProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<string> _name;

    [ImplementProperty("Border.BackgroundProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IBrush> _background;

    [ImplementProperty("Border.BorderBrushProperty")]
    protected Optional<IBrush> _borderBrush;

    [ImplementProperty("Border.BorderThicknessProperty")]
    protected Optional<AvaloniaThickness> _borderThickness;

    [ImplementProperty("Border.CornerRadiusProperty")]
    protected Optional<CornerRadius> _cornerRadius;

    [ImplementProperty("Border.BoxShadowProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<BoxShadows> _boxShadow;

    [ImplementProperty("Border.PaddingProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<AvaloniaThickness> _padding;

    [ImplementProperty("Border.MarginProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<AvaloniaThickness> _margin;
}
