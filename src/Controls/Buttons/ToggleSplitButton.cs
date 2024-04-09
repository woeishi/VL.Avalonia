using Avalonia.Controls.Primitives;
using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class ToggleSplitButton : AvaloniaControls.ToggleSplitButton
{
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.ToggleSplitButton);
    public ToggleSplitButton Output => this;

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

    public new bool IsChecked
    {
        get => base.IsChecked;
        set => base.IsChecked = value;
    }
}
