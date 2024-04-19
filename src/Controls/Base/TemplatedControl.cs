using AvaloniaBase = Avalonia;
using AvaloniaControls = Avalonia.Controls;
using AvaloniaMedia = Avalonia.Media;

namespace VL.Avalonia.Controls;

public static class TemplatedControl
{
    public static T? Background<T>(T? control, out AvaloniaMedia.IBrush? background) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        background = (control != null) ? control.Background : default;
        return control;
    }

    public static T? SetBackground<T>(T? control, AvaloniaMedia.IBrush? background) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        if (control != null) control.Background = background;
        return control;
    }

    public static T? BorderBrush<T>(T? control, out AvaloniaMedia.IBrush? borderBrush) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        borderBrush = (control != null) ? control.BorderBrush : default;
        return control;
    }

    public static T? SetBorderBrush<T>(T? control, AvaloniaMedia.IBrush? borderBrush) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        if (control != null) control.BorderBrush = borderBrush;
        return control;
    }

    public static T? BorderThickness<T>(T? control, out AvaloniaBase.Thickness borderThickness) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        borderThickness = (control != null) ? control.BorderThickness : default;
        return control;
    }

    public static T? SetBorderThickness<T>(T? control, AvaloniaBase.Thickness borderThickness = default) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        if (control != null) control.BorderThickness = borderThickness;
        return control;
    }

    public static T? CornerRadius<T>(T? control, out AvaloniaBase.CornerRadius cornerRadius) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        cornerRadius = (control != null) ? control.CornerRadius : default;
        return control;
    }

    public static T? SetCornerRadius<T>(T? control, AvaloniaBase.CornerRadius cornerRadius = default) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        if (control != null) control.CornerRadius = cornerRadius;
        return control;
    }

    public static T? FontFamily<T>(T? control, out AvaloniaMedia.FontFamily fontFamily) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        fontFamily = (control != null) ? control.FontFamily : default;
        return control;
    }

    public static T? SetFontFamily<T>(T? control, AvaloniaMedia.FontFamily fontFamily) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        if (control != null) control.FontFamily = fontFamily;
        return control;
    }

    public static T? FontSize<T>(T? control, out double fontSize) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        fontSize = (control != null) ? control.FontSize : default;
        return control;
    }

    public static T? SetFontSize<T>(T? control, double fontSize = default) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        if (control != null) control.FontSize = fontSize;
        return control;
    }

    public static T? FontStyle<T>(T? control, out AvaloniaMedia.FontStyle fontStyle) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        fontStyle = (control != null) ? control.FontStyle : default;
        return control;
    }

    public static T? SetFontStyle<T>(T? control, AvaloniaMedia.FontStyle fontStyle) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        if (control != null) control.FontStyle = fontStyle;
        return control;
    }

    public static T? FontWeight<T>(T? control, out AvaloniaMedia.FontWeight fontWeight) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        fontWeight = (control != null) ? control.FontWeight : default;
        return control;
    }

    public static T? SetFontWeight<T>(T? control, AvaloniaMedia.FontWeight fontWeight) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        if (control != null) control.FontWeight = fontWeight;
        return control;
    }

    public static T? FontStretch<T>(T? control, out AvaloniaMedia.FontStretch fontStretch) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        fontStretch = (control != null) ? control.FontStretch : default;
        return control;
    }

    public static T? SetFontStretch<T>(T? control, AvaloniaMedia.FontStretch fontStretch) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        if (control != null) control.FontStretch = fontStretch;
        return control;
    }

    public static T? Foreground<T>(T? control, out AvaloniaMedia.IBrush? foreground) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        foreground = (control != null) ? control.Foreground : default;
        return control;
    }

    public static T? SetForeground<T>(T? control, AvaloniaMedia.IBrush? foreground) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        if (control != null) control.Foreground = foreground;
        return control;
    }

    public static T? Padding<T>(T? control, out AvaloniaBase.Thickness padding) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        padding = (control != null) ? control.Padding : default;
        return control;
    }

    public static T? SetPadding<T>(T? control, AvaloniaBase.Thickness padding = default) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        if (control != null) control.Padding = padding;
        return control;
    }

    public static T? Template<T>(T? control, out AvaloniaControls.Templates.IControlTemplate? template) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        template = (control != null) ? control.Template : default;
        return control;
    }

    public static T? SetTemplate<T>(T? control, AvaloniaControls.Templates.IControlTemplate? template) where T : AvaloniaControls.Primitives.TemplatedControl
    {
        if (control != null) control.Template = template;
        return control;
    }
}