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

    [ImplementChildren]
    protected Spread<Control>? _children;

    [ImplementOptional<StackPanel>(nameof(StackPanel.OrientationProperty))]
    protected Optional<Orientation> _orientation;

    [ImplementOptional<StackPanel>(nameof(StackPanel.SpacingProperty))]
    protected Optional<int> _spacing;
}

[ProcessNode(Name = "StackPanel")]
public partial class StackPanelWrapper : StackPanelSpectralWrapper
{
    public StackPanelWrapper() : base() { }

    [ImplementChildren(IsPinGroup = true)]
    protected Spread<Control>? _children;
}
