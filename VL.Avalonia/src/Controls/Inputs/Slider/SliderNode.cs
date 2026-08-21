using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Layout;
using System.Globalization;
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
public abstract partial class SliderNodeBase<TControl, TValue> : RangeBaseNodeBase<TControl, TValue>
    where TControl : Slider, new()
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
        Converter = nameof(ToProperty),
        Order = PinOrder.Style,
        PinVisibility = PinVisibility.Optional
    )]
    private Optional<TValue> _tickFrequency;

    /// <summary>Sets a value that indicates where to draw tick marks in relation to the track.</summary>
    [ImplementProperty(
        typeof(Slider),
        nameof(Slider.TickPlacementProperty),
        Order = PinOrder.Style,
        PinVisibility = PinVisibility.Optional
    )]
    private Optional<TickPlacement> _tickPlacement;

    private Optional<Spread<TValue>> _ticks;

    /// <param name="ticks">
    /// Defines the ticks to be drawn on the tick bar.
    /// </param>
    [Fragment(Order = PinOrder.Style)]
    public void SetTicks([Pin(Visibility = PinVisibility.Optional)] Optional<Spread<TValue>> ticks)
    {
        if (_ticks == ticks)
            return;

        _ticks = ticks;

        if (_ticks.HasValue && _ticks.Value.Count > 0)
        {
            var list = new AvaloniaList<double>(_ticks.Value.Select(ToProperty));

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
public class SliderNode : SliderNodeBase<Slider, float>
{
    [Fragment]
    public SliderNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

    protected override double ToProperty(float value) => value;

    protected override float ToValue(double value) => (float)value;
}

/// <summary>
/// A control that lets the user select from a range of whole numbers by moving a Thumb control along a Track.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/slider">Slider</see>
/// <br/>PseudoClasses: :vertical, :horizontal, :pressed
/// <br/>TemplateParts: PART_DecreaseButton, PART_IncreaseButton, PART_Track
/// </summary>
[ProcessNode(Name = "Slider (Integer)")]
public class SliderInteger32Node : SliderNodeBase<Slider, int>
{
    [Fragment]
    public SliderInteger32Node([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

    protected override double ToProperty(int value) => value;

    protected override int ToValue(double value) => (int)Math.Round(value);
}

/// <summary>
/// Generic wrapper for <see cref="Slider"/>, allows to provide converters for value.
/// </summary>
[ProcessNode(Name = "Slider (Advanced Experimental)")]
public class SliderAdvancedExperimentalNode<TValue> : SliderNodeBase<Slider, TValue>
{
    private static readonly Type ValueType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
    private static readonly bool IsConvertible = typeof(IConvertible).IsAssignableFrom(ValueType);

    /// <summary>
    /// Fallback used until <see cref="SetToValueConverter"/> supplies a converter. Yields
    /// <c>default</c> for types that cannot be converted, so the node never throws while unwired.
    /// </summary>
    protected static Func<double, TValue> DefaultToValueConverter = static (x) =>
    {
        if (!IsConvertible)
            return default!;

        try
        {
            return (TValue)Convert.ChangeType(x, ValueType, CultureInfo.InvariantCulture);
        }
        catch (Exception e) when (e is InvalidCastException or FormatException or OverflowException)
        {
            return default!;
        }
    };

    /// <summary>
    /// Fallback used until <see cref="SetFromValueConverter"/> supplies a converter. Yields
    /// <c>0</c> for types that cannot be converted, so the node never throws while unwired.
    /// </summary>
    protected static Func<TValue, double> DefaultFromValueConverter = static (x) =>
    {
        if (x is null || !IsConvertible)
            return 0d;

        try
        {
            return Convert.ToDouble(x, CultureInfo.InvariantCulture);
        }
        catch (Exception e) when (e is InvalidCastException or FormatException or OverflowException)
        {
            return 0d;
        }
    };

    private Func<double, TValue> _toValueConverter = DefaultToValueConverter;
    private Func<TValue, double> _fromValueConverter = DefaultFromValueConverter;

    [Fragment]
    public SliderAdvancedExperimentalNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

    protected override double ToProperty(TValue value) => _fromValueConverter(value);

    protected override TValue ToValue(double value) => _toValueConverter(value);

    public void SetToValueConverter([Pin(Visibility = PinVisibility.Optional)] Func<double, TValue> toValueConverter)
    {
        _toValueConverter = toValueConverter ?? DefaultToValueConverter;
    }

    public void SetFromValueConverter([Pin(Visibility = PinVisibility.Optional)] Func<TValue, double> fromValueConverter)
    {
        _fromValueConverter = fromValueConverter ?? DefaultFromValueConverter;
    }
}
