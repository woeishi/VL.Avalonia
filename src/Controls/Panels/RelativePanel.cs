using VL.Lib.Collections;
using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class RelativePanel : AvaloniaControls.RelativePanel
{
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.RelativePanel);

    public RelativePanel Output => this;

    public new Spread<AvaloniaControls.Control?> Children
    {
        set => ControlsExtension.SetControls(base.Children, value);
    }
}
