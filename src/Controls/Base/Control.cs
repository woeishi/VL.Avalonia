using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

public static partial class Control
{
    public static T? FocusAdorner<T>(T? control, out AvaloniaControls.ITemplate<AvaloniaControls.Control> focusAdorner) where T : AvaloniaControls.Control
    {
        focusAdorner = (control != null) ? control.FocusAdorner : default;
        return control;
    }

    public static T? SetFocusAdorner<T>(T? control, AvaloniaControls.ITemplate<AvaloniaControls.Control> focusAdorner) where T : AvaloniaControls.Control
    {
        if (control != null) control.FocusAdorner = focusAdorner;
        return control;
    }

    public static T? ContextMenu<T>(T? control, out AvaloniaControls.ContextMenu? contextMenu) where T : AvaloniaControls.Control
    {
        contextMenu = (control != null) ? control.ContextMenu : default;
        return control;
    }

    public static T? SetContextMenu<T>(T? control, AvaloniaControls.ContextMenu? contextMenu) where T : AvaloniaControls.Control
    {
        if (control != null) control.ContextMenu = contextMenu;
        return control;
    }

    public static T? ContextFlyout<T>(T? control, out AvaloniaControls.Primitives.FlyoutBase? contextFlyout) where T : AvaloniaControls.Control
    {
        contextFlyout = (control != null) ? control.ContextFlyout : default;
        return control;
    }

    public static T? SetContextFlyout<T>(T? control, AvaloniaControls.Primitives.FlyoutBase? contextFlyout) where T : AvaloniaControls.Control
    {
        if (control != null) control.ContextFlyout = contextFlyout;
        return control;
    }

    public static T? IsLoaded<T>(T? control, out bool isLoaded) where T : AvaloniaControls.Control
    {
        isLoaded = (control != null) ? control.IsLoaded : default;
        return control;
    }

    public static T? Tag<T>(T? control, out object? tag) where T : AvaloniaControls.Control
    {
        tag = (control != null) ? control.Tag : default;
        return control;
    }

    public static T? SetTag<T>(T? control, object? tag) where T : AvaloniaControls.Control
    {
        if (control != null) control.Tag = tag;
        return control;
    }
}
