using Avalonia.Controls.Shapes;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "Rectangle")]
public partial class RectangleWrapper
{
    [ImplementOutput]
    protected Rectangle _output = new Rectangle();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementClasses]
    protected Optional<string> _classes;

    protected Optional<IBrush> _fill;
    public void SetFill(Optional<IBrush> fill)
    {
        if (_fill != fill)
        {
            _fill = fill;
            _output.SetValue(Rectangle.FillProperty, fill.Value);
        }
    }
}
