using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class RepeatButton : AvaloniaControls.RepeatButton
{
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.RepeatButton);
    public RepeatButton Output => this;

    public new object? Content
    {
        get => base.Content;
        set { if (base.Content != value) base.Content = value; }
    }

    public new AvaloniaControls.ClickMode ClickMode
    {
        get => base.ClickMode;
        set => base.ClickMode = value;
    }

    public new bool IsPressed => base.IsPressed;

    public new int Interval
    {
        get => base.Interval;
        set => base.Interval = value;
    }

    public new int Delay
    {
        get => base.Delay;
        set => base.Delay = value;
    }
}
