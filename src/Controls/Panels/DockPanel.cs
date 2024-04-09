using VL.Lib.Collections;
using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class DockPanel : AvaloniaControls.DockPanel
{
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.DockPanel);

    public DockPanel Output => this;

    public new Spread<AvaloniaControls.Control?> Children
    {
        set => ControlsExtension.SetControls(base.Children, value);
    }

    public new bool LastChildFill
    {
        get => base.LastChildFill;
        set => base.LastChildFill = value;
    }
}
