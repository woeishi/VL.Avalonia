using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Model;

namespace VL.Avalonia.Controls;

/// <summary>
/// Base wrapper for <see cref="Slider"/>
/// </summary>
[ProcessNode]
public abstract partial class SliderNodeBase<T> : RangeBaseNodeBase<T>
    where T : Slider, new()
{
    [Fragment]
    public SliderNodeBase([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

    /// <summary>Sets the orientation of a Slider.</summary>
    [ImplementProperty(
        typeof(Slider),
        nameof(Slider.OrientationProperty),
        Order = PinOrder.Style,
        PinVisibility = PinVisibility.Optional
    )]
    private Optional<Orientation> _orientation;

    /// <summary>Sets the direction of increasing value. true if the direction of increasing value is to the left for a horizontal slider or down for a vertical slider; otherwise, false. The default is false.</summary>
    [ImplementProperty(
        typeof(Slider),
        nameof(Slider.IsDirectionReversedProperty),
        Order = PinOrder.Style,
        PinVisibility = PinVisibility.Optional
    )]
    private Optional<bool> _isDirectionReversed;

    /// <summary>Sets a value that indicates whether the Slider automatically moves the Thumb to the closest tick mark.</summary>
    [ImplementProperty(
        typeof(Slider),
        nameof(Slider.IsSnapToTickEnabledProperty),
        Order = PinOrder.Style,
        PinVisibility = PinVisibility.Optional
    )]
    private Optional<bool> _isSnapToTickEnabled;

    /// <summary>Sets the interval between tick marks.</summary>
    [ImplementProperty(
        typeof(Slider),
        nameof(Slider.TickFrequencyProperty),
        Order = PinOrder.Style,
        PinVisibility = PinVisibility.Optional
    )]
    private Optional<float> _tickFrequency;

    /// <summary>Sets a value that indicates where to draw tick marks in relation to the track.</summary>
    [ImplementProperty(
        typeof(Slider),
        nameof(Slider.TickPlacementProperty),
        Order = PinOrder.Style,
        PinVisibility = PinVisibility.Optional
    )]
    private Optional<TickPlacement> _tickPlacement;

    private Optional<Spread<float>> _ticks;

    /// <param name="ticks">
    /// Defines the ticks to be drawn on the tick bar.
    /// </param>
    [Fragment(Order = PinOrder.Style)]
    public void SetTicks([Pin(Visibility = PinVisibility.Optional)] Optional<Spread<float>> ticks)
    {
        if (_ticks == ticks)
            return;

        _ticks = ticks;

        if (_ticks.HasValue && _ticks.Value.Count > 0)
        {
            var list = new AvaloniaList<double>(_ticks.Value.Select(x => (double)x));

            _output.SetValue(Slider.TicksProperty, list);
        }
        else
        {
            _output.ClearValue(Slider.TicksProperty);
        }
    }
}

/// <summary>
/// A control that lets the user select from a range of values by moving a Thumb control along a Track.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/slider">Slider</see>
/// <br/>PseudoClasses: :vertical, :horizontal, :pressed
/// <br/>TemplateParts: PART_DecreaseButton, PART_IncreaseButton, PART_Track
/// </summary>
[ProcessNode(Name = "Slider")]
public class SliderNode : SliderNodeBase<Slider>
{
    [Fragment]
    public SliderNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
}
