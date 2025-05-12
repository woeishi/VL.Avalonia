using Avalonia.Controls;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

/// <summary>
/// Abstract Control wrapper to process node<br/>
/// </summary>
/// <typeparam name="T">Control</typeparam>
public abstract class AbstractWrapperBase<T> where T : Control
{
    public abstract T Output { get; }
    public abstract IAvaloniaStyle? Style { set; }
}
