using VL.Lib.Collections;
using System.Collections;
using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class Menu : AvaloniaControls.Menu
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.Menu);
    public Menu Output => this;

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

[ProcessNode]
public class MenuItem : AvaloniaControls.MenuItem
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.MenuItem);
    public MenuItem Output => this;

    public new object? Header
    {
        get => base.Header;
        set { if (base.Header != value) base.Header = value; }
    }
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

    public new bool IsSelected => base.IsSelected;

    public new bool IsFocused => base.IsFocused;
}
