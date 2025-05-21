﻿using Avalonia.Controls.Shapes;
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
    private Rectangle _output = new Rectangle();

    [ImplementStyle]
    private Optional<IAvaloniaStyle> _style;

    private Optional<IBrush> _fill;
    public void SetFill(Optional<IBrush> fill)
    {
        if (_fill != fill)
        {
            _fill = fill;
            _output.Fill = fill.Value;
        }
    }
}
