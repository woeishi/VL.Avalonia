using Avalonia.Controls;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "Panel (Spectral)")]
public partial class PanelSpectralWrapper
{
    [ImplementOutput]
    protected readonly Panel _output = new Panel();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementClasses]
    protected Optional<string> _classes;

    [ImplementChildren]
    protected Spread<Control?> _children;

    [ImplementProperty("Panel.NameProperty", PinVisibility = Model.PinVisibility.Hidden)]
    protected Optional<string> _name;

    [ImplementProperty("Panel.HorizontalAlignmentProperty", PinVisibility = Model.PinVisibility.Hidden)]
    protected Optional<HorizontalAlignment> _horizontalAlignment;

    [ImplementProperty("Panel.VerticalAlignmentProperty", PinVisibility = Model.PinVisibility.Hidden)]
    protected Optional<VerticalAlignment> _verticalAlignment;
}

[ProcessNode(Name = "Panel")]
public partial class PanelWrapper : PanelSpectralWrapper
{
    [ImplementChildren(IsPinGroup = true)]
    protected Spread<Control?> _children;
}
