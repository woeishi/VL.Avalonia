using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using static VL.Avalonia.Styles;
using AvaloniaThickness = Avalonia.Thickness;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "Border")]
public partial class BorderWrapper
{
    [ImplementOutput]
    protected Border _output = new Border();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    protected Optional<Control> _child;
    [Fragment(Order = -10)]
    public void SetChild(Optional<Control> child)
    {
        if (_child != child)
        {
            _child = child;

            _output.SetValue(Border.ChildProperty, child.HasValue ? child.Value : null);
        }
    }

    protected Optional<IBrush> _background;
    public void SetBackground([Pin(Visibility = Model.PinVisibility.Optional)] Optional<IBrush> background)
    {
        if (_background != background)
        {
            _background = background;
            if (background.HasValue)
            {
                _output.SetValue(Border.BackgroundProperty, background.Value);
            }
            else
            {
                _output.ClearValue(Border.BackgroundProperty);
            }
        }
    }

    protected Optional<IBrush> _borderBrush;
    public void SetBorderBrush(Optional<IBrush> borderBrush)
    {
        if (_borderBrush != borderBrush)
        {
            _borderBrush = borderBrush;

            if (borderBrush.HasValue)
            {
                _output.SetValue(Border.BorderBrushProperty, borderBrush.Value);
            }
            else
            {
                _output.ClearValue(Border.BorderBrushProperty);
            }
        }
    }

    protected Optional<AvaloniaThickness> _borderThickness;
    public void SetBorderThickness(Optional<AvaloniaThickness> borderThickness)
    {
        if (_borderThickness != borderThickness)
        {
            _borderThickness = borderThickness;

            if (borderThickness.HasValue)
            {
                _output.SetValue(Border.BorderThicknessProperty, borderThickness.Value);
            }
            else
            {
                _output.ClearValue(Border.BorderThicknessProperty);
            }
        }
    }

    protected Optional<CornerRadius> _cornerRadius;
    public void SetCornerRadius(Optional<CornerRadius> cornerRadius)
    {
        if (_cornerRadius != cornerRadius)
        {
            _cornerRadius = cornerRadius;
            if (cornerRadius.HasValue)
            {
                _output.SetValue(Border.CornerRadiusProperty, cornerRadius.Value);
            }
            else
            {
                _output.ClearValue(Border.CornerRadiusProperty);
            }
        }
    }

    protected Optional<BoxShadows> _boxShadow;
    public void SetBoxShadow([Pin(Visibility = Model.PinVisibility.Optional)] Optional<BoxShadows> boxShadow)
    {
        if (_boxShadow != boxShadow)
        {
            _boxShadow = boxShadow;
            if (boxShadow.HasValue)
            {
                _output.SetValue(Border.BoxShadowProperty, boxShadow.Value);
            }
            else
            {
                _output.ClearValue(Border.BoxShadowProperty);
            }
        }
    }

    protected Optional<AvaloniaThickness> _padding;
    public void SetPadding([Pin(Visibility = Model.PinVisibility.Optional)] Optional<AvaloniaThickness> padding)
    {
        if (_padding != padding)
        {
            _padding = padding;
            if (padding.HasValue)
            {
                _output.SetValue(Border.PaddingProperty, padding.Value);
            }
            else
            {
                _output.ClearValue(Border.PaddingProperty);
            }
        }
    }

    protected Optional<AvaloniaThickness> _margin;
    public void SetMargin([Pin(Visibility = Model.PinVisibility.Optional)] Optional<AvaloniaThickness> margin)
    {
        if (_margin != margin)
        {
            _margin = margin;
            if (margin.HasValue)
            {
                _output.SetValue(Border.MarginProperty, margin.Value);
            }
            else
            {
                _output.ClearValue(Border.MarginProperty);
            }
        }
    }
}
