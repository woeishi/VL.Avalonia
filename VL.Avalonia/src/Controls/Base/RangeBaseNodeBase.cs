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
public abstract partial class RangeBaseNodeBase<T> : ControlNodeBase<T>, IDisposable
    where T : RangeBase, new()
{
    private TwoWayBinding<float, double> _valueBinding;

    [Fragment]
    protected RangeBaseNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext)
        : base(nodeContext)
    {
        _valueBinding = new TwoWayBinding<float, double>(
            _output,
            RangeBase.ValueProperty,
            (x) => (double)x,
            (x) => (float)x
        );
    }

    /// <param name="valueChannel">
    /// Gets or sets the current value
    /// </param>
    [Fragment(Order = PinOrder.Main)]
    public void SetValueChannel(IChannel<float> valueChannel) => _valueBinding.Bind(valueChannel);

    /// <summary>Sets the minimum possible value.</summary>
    [ImplementProperty(
        typeof(RangeBase),
        nameof(RangeBase.MinimumProperty),
        Order = PinOrder.Style
    )]
    private Optional<float> _minimum;

    /// <summary>Sets the maximum possible value.</summary>
    [ImplementProperty(
        typeof(RangeBase),
        nameof(RangeBase.MaximumProperty),
        Order = PinOrder.Style
    )]
    private Optional<float> _maximum;

    /// <summary>Sets the small increment value added or subtracted from.</summary>
    [ImplementProperty(
        typeof(RangeBase),
        nameof(RangeBase.SmallChangeProperty),
        Order = PinOrder.Style,
        PinVisibility = PinVisibility.Optional
    )]
    private Optional<float> _smallChange;

    /// <summary>Sets the large increment value added or subtracted from.</summary>
    [ImplementProperty(
        typeof(RangeBase),
        nameof(RangeBase.LargeChangeProperty),
        Order = PinOrder.Style,
        PinVisibility = PinVisibility.Optional
    )]
    private Optional<float> _largeChange;

    public override void Dispose()
    {
        _valueBinding?.Dispose();
        base.Dispose();
    }
}
