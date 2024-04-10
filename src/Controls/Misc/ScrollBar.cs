using AvaloniaControls = Avalonia.Controls;
using AvaloniaLayout = Avalonia.Layout;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class ScrollBar : AvaloniaControls.Primitives.ScrollBar
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.Primitives.ScrollBar);
    public ScrollBar Output => this;

    public new double Value
    {
        get => base.Value;
        set { if (value != base.Value) base.Value = value; }
    }

    public new double Minimum
    {
        get => base.Minimum;
        set => base.Minimum = value;
    }

    public new double Maximum
    {
        get => base.Maximum;
        set => base.Maximum = value;
    }

    public new AvaloniaLayout.Orientation Orientation
    {
        get => base.Orientation;
        set => base.Orientation = value;
    }

    public new AvaloniaLayout.VerticalAlignment VerticalAlignment
    {
        get => base.VerticalAlignment;
        set => base.VerticalAlignment = value;
    }

    public new AvaloniaLayout.HorizontalAlignment HorizontalAlignment
    {
        get => base.HorizontalAlignment;
        set => base.HorizontalAlignment = value;
    }

}
