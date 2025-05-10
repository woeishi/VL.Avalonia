using Avalonia.Data;
using Avalonia.Media;
using Avalonia.Styling;

namespace VL.Avalonia.Helpers;

// To study
// https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Base/Styling/Selector.cs

public interface AvaloniaStyle<T>
{
    public Action<T> ApplyStyle { get; }

    public Func<Setter> BuildStyle();

}

public class StyleBackgorund<T>
{
    public Func<Setter> BuildStyle(Optional<IBrush> brush) => () => new Setter(T.BackgroundProperty, brush);
}
