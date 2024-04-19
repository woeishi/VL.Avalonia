using AvaloniaControls = Avalonia.Controls;
using AvaloniaLayout = Avalonia.Layout;

namespace VL.Avalonia.Controls;

public static class ItemsControl
{
    public static T? Items<T>(T? control, out AvaloniaControls.ItemCollection items) where T : AvaloniaControls.ItemsControl
    {
        items = (control != null) ? control.Items : default;
        return control;
    }

    public static T? ItemCount<T>(T? control, out int itemCount) where T : AvaloniaControls.ItemsControl
    {
        itemCount = (control != null) ? control.ItemCount : default;
        return control;
    }
    public static T? ItemsPanel<T>(T? control, out AvaloniaControls.ITemplate<AvaloniaControls.Panel?>? itemsPanel) where T : AvaloniaControls.ItemsControl
    {
        itemsPanel = (control != null) ? control.ItemsPanel : default;
        return control;
    }

    public static T? SetItemsPanel<T>(T? control, AvaloniaControls.ITemplate<AvaloniaControls.Panel?>? itemsPanel) where T : AvaloniaControls.ItemsControl
    {
        if (control != null) control.ItemsPanel = itemsPanel;
        return control;
    }

    public static T? ItemsSource<T>(T? control, out System.Collections.IEnumerable? itemsSource) where T : AvaloniaControls.ItemsControl
    {
        itemsSource = (control != null) ? control.ItemsSource : default;
        return control;
    }

    public static T? SetItemsSource<T>(T? control, System.Collections.IEnumerable? itemsSource) where T : AvaloniaControls.ItemsControl
    {
        if (control != null) control.ItemsSource = itemsSource;
        return control;
    }
}