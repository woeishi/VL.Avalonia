using System.Collections;
using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class ListBox : AvaloniaControls.ListBox
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.ListBox);
    public ListBox Output => this;

    public new System.Collections.IEnumerable? ItemsSource
    {
        get => base.ItemsSource;
        set => base.ItemsSource = value;
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
