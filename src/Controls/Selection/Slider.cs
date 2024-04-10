using AvaloniaControls = Avalonia.Controls;
using AvaloniaLayout = Avalonia.Layout;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class Slider : AvaloniaControls.Slider
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.Slider);
    public Slider Output => this;

    public new double Value => base.Value;

    public new AvaloniaLayout.Orientation Orientation
    {
        get => base.Orientation;
        set => base.Orientation = value;
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

}
