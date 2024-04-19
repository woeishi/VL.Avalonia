using AvaloniaBase = Avalonia;
using AvaloniaControls = Avalonia.Controls;
using AvaloniaStyling = Avalonia.Styling;

namespace VL.Avalonia.Controls;

public static class StyledElement
{
    public static T? Name<T>(T? control, out string? name) where T : AvaloniaBase.StyledElement
    {
        name = (control != null) ? control.Name : default;
        return control;
    }

    public static T? SetName<T>(T? control, string? name) where T : AvaloniaBase.StyledElement
    {

        if (control != null) control.Name = name;
        return control;
    }
    public static T? Classes<T>(T? control, out AvaloniaControls.Classes classes) where T : AvaloniaBase.StyledElement
    {
        classes = (control != null) ? control.Classes : default;
        return control;
    }

    public static T? IsInitialized<T>(T? control, out bool isInitialized) where T : AvaloniaBase.StyledElement
    {
        isInitialized = (control != null) ? control.IsInitialized : default;
        return control;
    }

    public static T? Styles<T>(T? control, out AvaloniaStyling.Styles styles) where T : AvaloniaBase.StyledElement
    {
        styles = (control != null) ? control.Styles : new();
        return control;
    }

    public static T? Resources<T>(T? control, out AvaloniaControls.IResourceDictionary resources) where T : AvaloniaBase.StyledElement
    {
        resources = (control != null) ? control.Resources : default;
        return control;
    }

    public static T? SetResources<T>(T? control, AvaloniaControls.IResourceDictionary resources) where T : AvaloniaBase.StyledElement
    {

        if (control != null) control.Resources = resources ?? new AvaloniaControls.ResourceDictionary();
        return control;
    }

    public static T? Parent<T>(T? control, out AvaloniaBase.StyledElement? parent) where T : AvaloniaBase.StyledElement
    {
        parent = (control != null) ? control.Parent : default;
        return control;
    }
}
