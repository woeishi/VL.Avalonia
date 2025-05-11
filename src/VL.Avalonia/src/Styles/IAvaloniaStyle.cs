namespace VL.Avalonia.Styles;


/// <summary>
/// Test helper interface to apply styles to controls.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAvaloniaStyle
{
    void ApplyStyle(object owner);
}