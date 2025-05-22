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
    private PathIcon _output = new PathIcon();

    [ImplementStyle]
    private Optional<IAvaloniaStyle> _style;

    private Optional<Geometry> _data;
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

