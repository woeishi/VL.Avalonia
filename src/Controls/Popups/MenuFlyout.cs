using VL.Lib.Collections;
using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;


[ProcessNode]
public class MenuFlyout : AvaloniaControls.MenuFlyout
{
    public MenuFlyout Output => this;

    public new bool IsOpen => base.IsOpen;

    public new Spread<AvaloniaControls.MenuItem?> Items
    {
        set => ControlsExtension.SetItems(base.Items, value);
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
