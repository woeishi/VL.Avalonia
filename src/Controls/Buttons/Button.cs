using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class Button : AvaloniaControls.Button
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.Button);
    public Button Output => this;

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
}
