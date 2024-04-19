using AvaloniaControls = Avalonia.Controls;
using AvaloniaLayout = Avalonia.Layout;

namespace VL.Avalonia.Controls;

public static class ContentControl
{
    public static T? Content<T>(T? control, out object? content) where T : AvaloniaControls.ContentControl
    {
        content = (control != null) ? control.Content : default;
        return control;
    }

    public static T? SetContent<T>(T? control, object? content) where T : AvaloniaControls.ContentControl
    {
        if (control != null) control.Content = content;
        return control;
    }

    public static T? HorizontalContentAlignment<T>(T? control, out AvaloniaLayout.HorizontalAlignment horizontalContentAlignment) where T :AvaloniaControls.ContentControl
    {
        horizontalContentAlignment = (control != null) ? control.HorizontalContentAlignment : default;
        return control;
    }

    public static T? SetHorizontalContentAlignment<T>(T? control, AvaloniaLayout.HorizontalAlignment horizontalContentAlignment) where T : AvaloniaControls.ContentControl
    {
        if (control != null) control.HorizontalContentAlignment = horizontalContentAlignment;
        return control;
    }

    public static T? VerticalContentAlignment<T>(T? control, out AvaloniaLayout.VerticalAlignment verticalContentAlignment) where T : AvaloniaControls.ContentControl
    {
        verticalContentAlignment = (control != null) ? control.VerticalContentAlignment : default;
        return control;
    }

    public static T? SetVerticalContentAlignment<T>(T? control, AvaloniaLayout.VerticalAlignment verticalContentAlignment) where T : AvaloniaControls.ContentControl
    {
        if (control != null) control.VerticalContentAlignment = verticalContentAlignment;
        return control;
    }
}
