using AvaloniaInput = Avalonia.Input;

namespace VL.Avalonia.Controls;

public static class InputElement
{
    public static T? Focusable<T>(T? control, out bool focusable) where T : AvaloniaInput.InputElement
    {
        focusable = (control != null) ? control.Focusable : default;
        return control;
    }

    public static T? IsEnabled<T>(T? control, out bool isEnabled) where T : AvaloniaInput.InputElement
    {
        isEnabled = (control != null) ? control.IsEnabled : default;
        return control;
    }

    public static T? Cursor<T>(T? control, out AvaloniaInput.Cursor? cursor) where T : AvaloniaInput.InputElement
    {
        cursor = (control != null) ? control.Cursor : default;
        return control;
    }

    public static T? IsEffectivelyEnabled<T>(T? control, out bool isEffectivelyEnabled) where T : AvaloniaInput.InputElement
    {
        isEffectivelyEnabled = (control != null) ? control.IsEffectivelyEnabled : default;
        return control;
    }

    public static T? IsEffectivelyVisible<T>(T? control, out bool isEffectivelyVisible) where T : AvaloniaInput.InputElement
    {
        isEffectivelyVisible = (control != null) ? control.IsEffectivelyVisible : default;
        return control;
    }

    public static T? IsKeyboardFocusWithin<T>(T? control, out bool isKeyboardFocusWithin) where T : AvaloniaInput.InputElement
    {
        isKeyboardFocusWithin = (control != null) ? control.IsKeyboardFocusWithin : default;
        return control;
    }

    public static T? IsFocused<T>(T? control, out bool isFocused) where T : AvaloniaInput.InputElement
    {
        isFocused = (control != null) ? control.IsFocused : default;
        return control;
    }

    public static T? IsHitTestVisible<T>(T? control, out bool isHitTestVisible) where T : AvaloniaInput.InputElement
    {
        isHitTestVisible = (control != null) ? control.IsHitTestVisible : default;
        return control;
    }

    public static T? IsPointerOver<T>(T? control, out bool isPointerOver) where T : AvaloniaInput.InputElement
    {
        isPointerOver = (control != null) ? control.IsPointerOver : default;
        return control;
    }
}
