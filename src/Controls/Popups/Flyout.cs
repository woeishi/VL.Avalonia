using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

public static partial class Control
{
    public static T? AttachFlyout<T>(T? control, AvaloniaControls.Primitives.FlyoutBase? flyout) where T : AvaloniaControls.Control
    {
        if (control != null && control.ContextFlyout != flyout)
        {
            control.ContextFlyout =  flyout;
        }
        return control;
    }
}


[ProcessNode]
public class Flyout : AvaloniaControls.Flyout
{
    public Flyout Output => this;

    public new bool IsOpen => base.IsOpen;

    public new object? Content
    {
        get => base.Content;
        set { if (base.Content != value) base.Content = value; }
    }

    public new AvaloniaControls.PlacementMode Placement
    {
        get => base.Placement;
        set => base.Placement = value;
    }
    public new AvaloniaControls.FlyoutShowMode ShowMode
    {
        get => base.ShowMode;
        set => base.ShowMode = value;
    }
}
