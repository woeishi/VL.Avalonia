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
    private readonly StackPanel _output = new StackPanel();

    [ImplementStyle]
    private Optional<IAvaloniaStyle> _style;

    [ImplementChildren]
    private Spread<Control> _children;

    [ImplementOptional<StackPanel>(nameof(StackPanel.OrientationProperty))]
    private Optional<Orientation> _orientation;

    [ImplementOptional<StackPanel>(nameof(StackPanel.SpacingProperty))]
    private Optional<int> _spacing;
}

[ProcessNode(Name = "StackPanel")]
public partial class StackPanelWrapper
{
    [ImplementOutput]
    private readonly StackPanel _output = new StackPanel();

    [ImplementStyle]
    private Optional<IAvaloniaStyle> _style;

    [ImplementChildren(true)]
    private Spread<Control> _children;

    [ImplementOptional<StackPanel>(nameof(StackPanel.SpacingProperty))]
    private Optional<int> _spacing;
}
