using AvaloniaBase = Avalonia;
using AvaloniaControls = Avalonia.Controls;
using AvaloniaMedia = Avalonia.Media;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class Border : AvaloniaControls.Border
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.Border);
    public Border Output => this;

    public new AvaloniaControls.Control? Child
    {
        get => base.Child;
        set { if (base.Child != value) base.Child = value; }
    }

    public new AvaloniaMedia.IBrush? BorderBrush
    {
        get => base.BorderBrush;
        set => base.BorderBrush = value;
    }

    public new AvaloniaBase.Thickness BorderThickness
    {
        get => base.BorderThickness;
        set => base.BorderThickness = value;
    }

    public new AvaloniaBase.CornerRadius CornerRadius
    {
        get => base.CornerRadius;
        set => base.CornerRadius = value;
    }

    public new AvaloniaMedia.BoxShadows BoxShadow
    {
        get => base.BoxShadow;
        set => base.BoxShadow = value;
    }
}
