using Avalonia.Controls;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

// https://docs.avaloniaui.net/docs/reference/controls/stackpanel
// http://reference.avaloniaui.net/api/Avalonia.Controls/StackPanel/

[ProcessNode(Name = "StackPanel (Spectral)")]
public partial class StackPanelSpectralWrapper
{
    [ImplementOutput]
    protected readonly StackPanel _output = new StackPanel();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementClasses]
    protected Optional<string> _classes;

    [ImplementChildren]
    protected Spread<Control?> _children;

    [ImplementProperty("StackPanel.NameProperty", PinVisibility = Model.PinVisibility.Hidden)]
    protected Optional<string> _name;

    [ImplementProperty("StackPanel.OrientationProperty")]
    protected Optional<Orientation> _orientation;

    [ImplementProperty("StackPanel.SpacingProperty")]
    protected Optional<int> _spacing;

    [ImplementProperty("StackPanel.HorizontalAlignmentProperty", PinVisibility = Model.PinVisibility.Hidden)]
    protected Optional<HorizontalAlignment> _horizontalAlignment;

    [ImplementProperty("StackPanel.VerticalAlignmentProperty", PinVisibility = Model.PinVisibility.Hidden)]
    protected Optional<VerticalAlignment> _verticalAlignment;
}

[ProcessNode(Name = "StackPanel")]
public partial class StackPanelWrapper : StackPanelSpectralWrapper
{
    [ImplementChildren(IsPinGroup = true)]
    protected Spread<Control?> _children;
}
