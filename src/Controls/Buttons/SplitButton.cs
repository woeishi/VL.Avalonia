using Avalonia.Controls.Primitives;
using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class SplitButton : AvaloniaControls.SplitButton
{
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.SplitButton);
    public SplitButton Output => this;

    public new object? Content
    {
        get => base.Content;
        set { if (base.Content != value) base.Content = value; }
    }

    public new FlyoutBase? Flyout
    {
        get => base.Flyout;
        set { if (base.Flyout != value) base.Flyout = value; }
    }

}
