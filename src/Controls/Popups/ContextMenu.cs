using VL.Lib.Collections;
using System.Collections;
using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

public static partial class Control
{
    public static T? AttachContextMenu<T>(T? control, AvaloniaControls.ContextMenu? contextMenu) where T : AvaloniaControls.Control
    {
        if (control != null && control.ContextMenu != contextMenu)
        {
            control.ContextMenu = contextMenu;
        }
        return control;
    }
}


[ProcessNode]
public class ContextMenu : AvaloniaControls.ContextMenu
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.ContextMenu);
    public ContextMenu Output => this;

    public new bool IsOpen => base.IsOpen;
   
    public new Spread<AvaloniaControls.MenuItem?> Items
    {
        set => ControlsExtension.SetItems(base.Items, value);
    }
    public new AvaloniaControls.SelectionMode SelectionMode
    {
        get => base.SelectionMode;
        set => base.SelectionMode = value;
    }

    public new int SelectedIndex => base.SelectedIndex;

    public new object? SelectedItem => base.SelectedItem;

    public new IList SelectedItems => base.SelectedItems;

    public new AvaloniaControls.Selection.ISelectionModel Selection => base.Selection;

}
