using Avalonia.Controls;
using VL.Avalonia.Helpers;
using VL.Avalonia.Styles;
using VL.Core;
using VL.Model;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

// TODO: REFACTOR

/// <summary>
/// Implements styling, classes, name
/// PseudoClasses :open
/// </summary>
[ProcessNode()]
public abstract class ToolTipWrapperBase<T>
    where T : Control
{
    [Fragment]
    public NodeContext NodeContext { get; }

    [Fragment]
    public ToolTipWrapperBase([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext)
    {
        NodeContext = nodeContext;
    }

    protected readonly ToolTip _output = new ToolTip();
    protected T _input;

    [Fragment(Order = -10)]
    public void SetInput(T? input)
    {
        if (_input != input)
        {
            _input?.ClearValue(ToolTip.TipProperty);

            _input = input;

            if (_input != null)
            {
                ToolTip.SetTip(_input, _output);

                UpdateSetters();
            }
        }
    }

    public T? Output => _input;
    protected abstract void UpdateSetters();

    protected Optional<IAvaloniaStyle> _style;

    [Fragment(Order = -3)]
    public void SetStyle(Optional<IAvaloniaStyle> style)
    {
        if (_style != style)
        {
            _style = style;
            _output.TryUpdateStyles(style.Value);
        }
    }

    protected Optional<string> _classes;

    [Fragment(Order = -2)]
    public void SetClasses(
        [Pin(Visibility = Model.PinVisibility.Optional)] Optional<string> classes
    )
    {
        if (_classes != classes)
        {
            _classes = classes;
            _output.TryUpdateClasses(classes.Value);
        }
    }

    protected Optional<string> _name;

    [Fragment(Order = -1)]
    public void SetName([Pin(Visibility = Model.PinVisibility.Optional)] Optional<string> name)
    {
        if (_name != name)
        {
            _name = name;

            if (name.HasValue)
            {
                _output.SetValue(Control.NameProperty, name.Value);
            }
            else
            {
                _output.ClearValue(Control.NameProperty);
            }
        }
    }

    protected Optional<bool> _isEnabled;

    [Fragment(Order = 9999)]
    public void SetEnabled(
        [Pin(Visibility = Model.PinVisibility.Optional)] Optional<bool> isEnabled
    )
    {
        if (_isEnabled != isEnabled)
        {
            _isEnabled = isEnabled;
            if (isEnabled.HasValue)
            {
                _output.SetValue(Control.IsEnabledProperty, isEnabled.Value);
            }
            else
            {
                _output.ClearValue(Control.IsEnabledProperty);
            }
        }
    }
}

[ProcessNode(Name = "ToolTip")]
public partial class ToolTipWrapper<T> : ToolTipWrapperBase<T>
    where T : Control
{
    [Fragment]
    public ToolTipWrapper([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

    protected Optional<object> _content;

    [Fragment(Order = -5)]
    public void SetContent(Optional<object> content)
    {
        if (_content != content)
        {
            _content = content;

            if (_content.HasValue)
            {
                if (_content.Value is Control control)
                {
                    ReparentingHelper.DetachFromParent(NodeContext, control);
                }

                _output.SetValue(ToolTip.ContentProperty, content.Value);
            }
            else
            {
                _output.ClearValue(ToolTip.ContentProperty);
            }
        }
    }

    protected Optional<PlacementMode> _placement;

    public void SetPlacement(Optional<PlacementMode> placement)
    {
        if (_placement != placement)
        {
            _placement = placement;

            UpdatePlacement();
        }
    }

    protected Optional<float> _horizontalOffset;

    public void SetHorizontalOffset(
        [Pin(Visibility = Model.PinVisibility.Optional)] Optional<float> horizontalOffset
    )
    {
        if (_horizontalOffset != horizontalOffset)
        {
            _horizontalOffset = horizontalOffset;

            UpdateHorizontalOffset();
        }
    }

    protected Optional<float> _verticalOffset;

    public void SetVerticalOffset(
        [Pin(Visibility = Model.PinVisibility.Optional)] Optional<float> verticalOffset
    )
    {
        if (_verticalOffset != verticalOffset)
        {
            _verticalOffset = verticalOffset;

            UpdateVerticalOffset();
        }
    }

    protected Optional<int> _showDelay;

    public void SetShowDelay(
        [Pin(Visibility = Model.PinVisibility.Optional)] Optional<int> showDelay
    )
    {
        if (_showDelay != showDelay)
        {
            _showDelay = showDelay;

            UpdateShowDelay();
        }
    }

    protected Optional<int> _betweenShowDelay;

    public void SetBetweenShowDelay(
        [Pin(Visibility = Model.PinVisibility.Optional)] Optional<int> betweenShowDelay
    )
    {
        if (_betweenShowDelay != betweenShowDelay)
        {
            _betweenShowDelay = betweenShowDelay;

            UpdateBetweenShowDelay();
        }
    }

    protected Optional<bool> _showOnDisabled;

    public void SetShowDisabled(
        [Pin(Visibility = Model.PinVisibility.Optional)] Optional<bool> showOnDisabled
    )
    {
        if (_showOnDisabled != showOnDisabled)
        {
            _showOnDisabled = showOnDisabled;

            UpdateSetters();
        }
    }

    internal void UpdatePlacement()
    {
        if (_input != null)
        {
            if (_placement.HasValue)
            {
                ToolTip.SetPlacement(_input, _placement.Value);
            }
            else
            {
                _input.ClearValue(ToolTip.PlacementProperty);
            }
        }
    }

    internal void UpdateHorizontalOffset()
    {
        if (_input != null)
        {
            if (_horizontalOffset.HasValue)
            {
                ToolTip.SetHorizontalOffset(_input, (float)_horizontalOffset.Value);
            }
            else
            {
                _input.ClearValue(ToolTip.HorizontalOffsetProperty);
            }
        }
    }

    internal void UpdateVerticalOffset()
    {
        if (_input != null)
        {
            if (_verticalOffset.HasValue)
            {
                ToolTip.SetVerticalOffset(_input, (float)_verticalOffset.Value);
            }
            else
            {
                _input.ClearValue(ToolTip.VerticalOffsetProperty);
            }
        }
    }

    internal void UpdateShowDelay()
    {
        if (_input != null)
        {
            if (_showDelay.HasValue)
            {
                ToolTip.SetShowDelay(_input, _showDelay.Value);
            }
            else
            {
                _input.ClearValue(ToolTip.ShowDelayProperty);
            }
        }
    }

    internal void UpdateBetweenShowDelay()
    {
        if (_input != null)
        {
            if (_betweenShowDelay.HasValue)
            {
                ToolTip.SetBetweenShowDelay(_input, _betweenShowDelay.Value);
            }
            else
            {
                _input.ClearValue(ToolTip.BetweenShowDelayProperty);
            }
        }
    }

    internal void UpdateShowOnDisabledProperty()
    {
        if (_input != null)
        {
            if (_showOnDisabled.HasValue)
            {
                ToolTip.SetShowOnDisabled(_input, _showOnDisabled.Value);
            }
            else
            {
                _input.ClearValue(ToolTip.ShowOnDisabledProperty);
            }
        }
    }

    protected override void UpdateSetters()
    {
        if (_input == null)
        {
            return;
        }

        UpdatePlacement();
        UpdateHorizontalOffset();
        UpdateVerticalOffset();
        UpdateShowDelay();
        UpdateBetweenShowDelay();
        UpdateShowOnDisabledProperty();
    }
}
