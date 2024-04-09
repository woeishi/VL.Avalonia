using VL.Lib.Collections;
using AvaloniaControls = Avalonia.Controls;
using AvaloniaLayout = Avalonia.Layout;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class StackPanel : AvaloniaControls.StackPanel
{
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.StackPanel);

    public StackPanel Output => this;

    public new Spread<AvaloniaControls.Control?> Children
    {
        set => ControlsExtension.SetControls(base.Children, value);
    }

    public new AvaloniaLayout.Orientation Orientation
    {
        get => base.Orientation;
        set => base.Orientation = value;
    }

    public new double Spacing
    {
        get => base.Spacing;
        set => base.Spacing = value;
    }
}
