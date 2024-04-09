using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class RadioButton : AvaloniaControls.RadioButton
{
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.RadioButton);
    public RadioButton Output => this;

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

    public new bool? IsChecked
    {
        get => base.IsChecked;
        set => base.IsChecked = value;
    }

    public new bool IsThreeState
    {
        get => base.IsThreeState;
        set => base.IsThreeState = value;
    }

    public new string? GroupName
    {
        get => base.GroupName;
        set => base.GroupName = value;
    }
}
