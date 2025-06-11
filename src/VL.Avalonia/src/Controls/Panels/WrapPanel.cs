using Avalonia.Controls;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "WrapPanel (Spectral)")]
public partial class WrapPanelSpectralWrapper
{
    [ImplementOutput]
    protected readonly Panel _output = new Panel();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementClasses]
    protected Optional<string> _classes;

    [ImplementChildren]
    protected Spread<Control?> _children;

    [ImplementProperty("WrapPanel.NameProperty", PinVisibility = Model.PinVisibility.Hidden)]
    protected Optional<string> _name;

    [ImplementProperty("WrapPanel.OrientationProperty")]
    protected Optional<Orientation> _orientation;

    [ImplementProperty("WrapPanel.HorizontalAlignmentProperty", PinVisibility = Model.PinVisibility.Hidden)]
    protected Optional<HorizontalAlignment> _horizontalAlignment;

    [ImplementProperty("WrapPanel.VerticalAlignmentProperty", PinVisibility = Model.PinVisibility.Hidden)]
    protected Optional<VerticalAlignment> _verticalAlignment;
}

[ProcessNode(Name = "WrapPanel")]
public partial class WrapPanelWrapper : WrapPanelSpectralWrapper
{
    [ImplementChildren(IsPinGroup = true)]
    protected Spread<Control?> _children;
}


