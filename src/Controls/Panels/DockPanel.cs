using VL.Lib.Collections;
using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

public static partial class Control
{
    public static T? Dock<T>(T? control, AvaloniaControls.Dock dock) where T : AvaloniaControls.Control
    {
        if (control != null && AvaloniaControls.DockPanel.GetDock(control) != dock)
        {
            AvaloniaControls.DockPanel.SetDock(control, dock);
        }
        return control;
    }
}

[ProcessNode]
public class DockPanel : AvaloniaControls.DockPanel
{
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.DockPanel);

    public DockPanel Output => this;

    public new Spread<AvaloniaControls.Control?> Children
    {
        set => ControlsExtension.SetControls(base.Children, value);
    }

    public new bool LastChildFill
    {
        get => base.LastChildFill;
        set => base.LastChildFill = value;
    }
}
