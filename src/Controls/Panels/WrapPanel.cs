using VL.Lib.Collections;
using AvaloniaControls = Avalonia.Controls;
using AvaloniaLayout = Avalonia.Layout;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class WrapPanel : AvaloniaControls.WrapPanel
{
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.WrapPanel);

    public WrapPanel Output => this;

    public new Spread<AvaloniaControls.Control?> Children
    {
        set => ControlsExtension.SetControls(base.Children, value);
    }

    public new AvaloniaLayout.Orientation Orientation
    {
        get => base.Orientation;
        set => base.Orientation = value;
    }

    public new double ItemWidth
    {
        get => base.ItemWidth;
        set => base.ItemWidth = value;
    }

    public new double ItemHeight
    {
        get => base.ItemHeight;
        set => base.ItemHeight = value;
    }
}
