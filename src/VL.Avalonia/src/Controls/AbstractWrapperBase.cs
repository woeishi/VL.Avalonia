using VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;


/// <summary>
/// hmm might wrap style setter in abstract class?
/// </summary>
/// <typeparam name="T"></typeparam>
public class AbstractWrapperBase<T>
{
    private T _output;
    public T Output { get => _output; }

    private IAvaloniaStyle? _style;
    public IAvaloniaStyle? Style
    {
        set
        {
            if (!_style?.Equals(value) ?? _style != value)
            {
                _style = value;
                _style?.ApplyStyle(_output);
            }
        }
    }
}
