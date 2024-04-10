using VL.Lib.Collections;
using System.Collections;
using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class TabStrip : AvaloniaControls.Primitives.TabStrip
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.Primitives.TabStrip);
    public TabStrip Output => this;

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
public class TabStripItem : AvaloniaControls.Primitives.TabStripItem
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.Primitives.TabStripItem);
    public TabStripItem Output => this;

    public new object? Content
    {
        get => base.Content;
        set { if (base.Content != value) base.Content = value; }
    }

    public new bool IsSelected => base.IsSelected;
}
