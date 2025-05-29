using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "PathIcon")]
public partial class PathIconWrapper
{
    [ImplementOutput]
    protected PathIcon _output = new PathIcon();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementClasses]
    protected Optional<string> _classes;

    protected Optional<Geometry> _data;
    [Fragment(Order = -5)]
    public void SetData(Optional<Geometry> data)
    {
        if (_data != data)
        {
            _data = data;

            _output.SetValue(PathIcon.DataProperty, _data.Value);
        }
    }
}

