using Avalonia.Controls.Primitives;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;
using VL.Model;

namespace VL.Avalonia.Controls;

/// <summary>
/// Base wrapper for <see cref="RangeBase"/>
/// </summary>
[ProcessNode]
public abstract partial class RangeBaseNodeBase<TControl, TValue> : ControlNodeBase<TControl>, IDisposable
    where TControl : RangeBase, new()
{
    private TwoWayBinding<TValue, double> _valueBinding;

    /// <summary>
    /// Converts a value of the node's value type into the <see cref="double"/> the control expects.
    /// </summary>
    protected abstract double ToProperty(TValue value);

    /// <summary>
    /// Converts the <see cref="double"/> reported by the control back into the node's value type.
    /// </summary>
    protected abstract TValue ToValue(double value);

    [Fragment]
    protected RangeBaseNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext)
        : base(nodeContext)
    {
        _valueBinding = new TwoWayBinding<TValue, double>(
            _output,
            RangeBase.ValueProperty,
            ToProperty,
            ToValue
        );
    }

    /// <param name="valueChannel">
    /// Gets or sets the current value
    /// </param>
    [Fragment(Order = PinOrder.Main)]
    public void SetValueChannel(IChannel<TValue> valueChannel) => _valueBinding.Bind(valueChannel);

    /// <summary>Sets the minimum possible value.</summary>
    [ImplementProperty(
        typeof(RangeBase),
        nameof(RangeBase.MinimumProperty),
        Converter = nameof(ToProperty),
        Order = PinOrder.Style
    )]
    private Optional<TValue> _minimum;

    /// <summary>Sets the maximum possible value.</summary>
    [ImplementProperty(
        typeof(RangeBase),
        nameof(RangeBase.MaximumProperty),
        Converter = nameof(ToProperty),
        Order = PinOrder.Style
    )]
    private Optional<TValue> _maximum;

    /// <summary>Sets the small increment value added or subtracted from.</summary>
    [ImplementProperty(
        typeof(RangeBase),
        nameof(RangeBase.SmallChangeProperty),
        Converter = nameof(ToProperty),
        Order = PinOrder.Style,
        PinVisibility = PinVisibility.Optional
    )]
    private Optional<TValue> _smallChange;

    /// <summary>Sets the large increment value added or subtracted from.</summary>
    [ImplementProperty(
        typeof(RangeBase),
        nameof(RangeBase.LargeChangeProperty),
        Converter = nameof(ToProperty),
        Order = PinOrder.Style,
        PinVisibility = PinVisibility.Optional
    )]
    private Optional<TValue> _largeChange;

    public override void Dispose()
    {
        _valueBinding?.Dispose();
        base.Dispose();
    }
}

/// <summary>
/// Base wrapper for <see cref="RangeBase"/> working on <see cref="float"/> values.
/// </summary>
[ProcessNode]
public abstract class RangeBaseNodeBase<TControl> : RangeBaseNodeBase<TControl, float>
    where TControl : RangeBase, new()
{
    [Fragment]
    protected RangeBaseNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext)
        : base(nodeContext) { }

    protected override double ToProperty(float value) => value;

    protected override float ToValue(double value) => (float)value;
}
