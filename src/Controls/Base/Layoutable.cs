using AvaloniaBase = Avalonia;
using AvaloniaLayout = Avalonia.Layout;

namespace VL.Avalonia.Controls;

public static class Layoutable
{
    public static T? Width<T>(T? control, out double width) where T : AvaloniaLayout.Layoutable
    {
        width = (control != null) ? control.Width : default;
        return control;
    }

    public static T? SetWidth<T>(T? control, double width) where T : AvaloniaLayout.Layoutable
    {
        if (control != null) control.Width = width;
        return control;
    }
    public static T? Height<T>(T? control, out double height) where T : AvaloniaLayout.Layoutable
    {
        height = (control != null) ? control.Height : default;
        return control;
    }

    public static T? SetHeight<T>(T? control, double height) where T : AvaloniaLayout.Layoutable
    {
        if (control != null) control.Height = height;
        return control;
    }

    public static T? MinWidth<T>(T? control, out double minWidth) where T : AvaloniaLayout.Layoutable
    {
        minWidth = (control != null) ? control.MinWidth : default;
        return control;
    }

    public static T? SetMinWidth<T>(T? control, double minWidth) where T : AvaloniaLayout.Layoutable
    {
        if (control != null) control.MinWidth = minWidth;
        return control;
    }

    public static T? MaxWidth<T>(T? control, out double maxWidth) where T : AvaloniaLayout.Layoutable
    {
        maxWidth = (control != null) ? control.MaxWidth : default;
        return control;
    }

    public static T? SetMaxWidth<T>(T? control, double maxWidth) where T : AvaloniaLayout.Layoutable
    {
        if (control != null) control.MaxWidth = maxWidth;
        return control;
    }

    public static T? MinHeight<T>(T? control, out double minHeight) where T : AvaloniaLayout.Layoutable
    {
        minHeight = (control != null) ? control.MinHeight : default;
        return control;
    }

    public static T? SetMinHeight<T>(T? control, double minHeight) where T : AvaloniaLayout.Layoutable
    {
        if (control != null) control.MinHeight = minHeight;
        return control;
    }

    public static T? MaxHeight<T>(T? control, out double maxHeight) where T : AvaloniaLayout.Layoutable
    {
        maxHeight = (control != null) ? control.MaxHeight : default;
        return control;
    }

    public static T? SetMaxHeight<T>(T? control, double maxHeight) where T : AvaloniaLayout.Layoutable
    {
        if (control != null) control.MaxHeight = maxHeight;
        return control;
    }

    public static T? Margin<T>(T? control, out AvaloniaBase.Thickness margin) where T : AvaloniaLayout.Layoutable
    {
        margin = (control != null) ? control.Margin : default;
        return control;
    }

    public static T? SetMargin<T>(T? control, AvaloniaBase.Thickness margin) where T : AvaloniaLayout.Layoutable
    {
        if (control != null) control.Margin = margin;
        return control;
    }

    public static T? HorizontalAlignment<T>(T? control, out AvaloniaLayout.HorizontalAlignment horizontalAlignment) where T : AvaloniaLayout.Layoutable
    {
        horizontalAlignment = (control != null) ? control.HorizontalAlignment : default;
        return control;
    }

    public static T? SetHorizontalAlignment<T>(T? control, AvaloniaLayout.HorizontalAlignment horizontalAlignment) where T : AvaloniaLayout.Layoutable
    {
        if (control != null) control.HorizontalAlignment = horizontalAlignment;
        return control;
    }

    public static T? VerticalAlignment<T>(T? control, out AvaloniaLayout.VerticalAlignment verticalAlignment) where T : AvaloniaLayout.Layoutable
    {
        verticalAlignment = (control != null) ? control.VerticalAlignment : default;
        return control;
    }

    public static T? SetVerticalAlignment<T>(T? control, AvaloniaLayout.VerticalAlignment verticalAlignment) where T : AvaloniaLayout.Layoutable
    {
        if (control != null) control.VerticalAlignment = verticalAlignment;
        return control;
    }

    public static T? DesiredSize<T>(T? control, out AvaloniaBase.Size desiredSize) where T : AvaloniaLayout.Layoutable
    {
        desiredSize = (control != null) ? control.DesiredSize : default;
        return control;
    }

    public static T? IsMeasureValid<T>(T? control, out bool isMeasureValid) where T : AvaloniaLayout.Layoutable
    {
        isMeasureValid = (control != null) ? control.IsMeasureValid : default;
        return control;
    }

    public static T? IsArrangeValid<T>(T? control, out bool isArrangeValid) where T : AvaloniaLayout.Layoutable
    {
        isArrangeValid = (control != null) ? control.IsArrangeValid : default;
        return control;
    }

    public static T? UseLayoutRounding<T>(T? control, out bool useLayoutRounding) where T : AvaloniaLayout.Layoutable
    {
        useLayoutRounding = (control != null) ? control.UseLayoutRounding : default;
        return control;
    }

    public static T? SetUseLayoutRounding<T>(T? control, bool useLayoutRounding) where T : AvaloniaLayout.Layoutable
    {
        if (control != null) control.UseLayoutRounding = useLayoutRounding;
        return control;
    }
}
