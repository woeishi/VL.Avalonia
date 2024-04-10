using VL.Lib.Collections;
using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class ComboBox : AvaloniaControls.ComboBox
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.ComboBox);
    public ComboBox Output => this;

    public new Spread<AvaloniaControls.ComboBoxItem?> Items
    {
        set => ControlsExtension.SetItems(base.Items, value);
    }
    
    public new int SelectedIndex => base.SelectedIndex;

    public new object? SelectedItem => base.SelectedItem;

    public new bool IsDropDownOpen => base.IsDropDownOpen;
}

[ProcessNode]
public class ComboBoxItem : AvaloniaControls.ComboBoxItem
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.ComboBoxItem);
    public ComboBoxItem Output => this;

    public new object? Content
    {
        get => base.Content;
        set { if (base.Content != value) base.Content = value; }
    }
}
