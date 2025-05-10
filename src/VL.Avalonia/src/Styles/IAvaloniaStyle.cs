namespace VL.Avalonia.Styles;


/// <summary>
/// Test helper interface to apply styles to controls.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAvaloniaStyle<T> where T : class
{
    void ApplyStyle(T owner);
}