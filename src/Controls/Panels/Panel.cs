using VL.Lib.Collections;
using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class Panel : AvaloniaControls.Panel
{
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.Panel);

    public Panel Output => this;

    public new Spread<AvaloniaControls.Control?> Children
    {
        set => ControlsExtension.SetControls(base.Children, value);
    }
}
