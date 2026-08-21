using Avalonia.Controls;
using VL.Avalonia.Controls;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Model;

namespace VL.Avalonia.Custom.Controls.Value
{
    /// <summary>
    /// A control that lets the user change value.
    /// <br/>NumberField<br/>
    /// </summary>
    [ProcessNode(Name = "NumberField")]
    public class NumberFieldNode : NumericUpDownNodeBase<NumberField, float?>
    {
        protected override TwoWayBinding<float?, decimal?> ValueBinding { get; }

        public NumberFieldNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            ValueBinding = new TwoWayBinding<float?, decimal?>(
                _output,
                NumericUpDown.ValueProperty,
                (x) => (decimal?)x,
                (x) => (float?)x
            );
        }
    }

    /// <summary>
    /// Wrapper for <see cref="NumberField"/>
    /// </summary>
    [ProcessNode(Name = "NumberField (Float)")]
    public class NumberFieldFloatNode : NumericUpDownNodeBase<NumberField, float>
    {
        protected override TwoWayBinding<float, decimal?> ValueBinding { get; }
        public NumberFieldFloatNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            ValueBinding = new TwoWayBinding<float, decimal?>(
                _output,
                NumericUpDown.ValueProperty,
                (x) => (decimal?)x,
                (x) => x == null ? 0f : (float)x.Value
            );
        }
    }

    /// <summary>
    /// Wrapper for <see cref="NumberField"/>
    /// </summary>
    [ProcessNode(Name = "NumberField (Integer)")]
    public class NumberFieldIntegerNode : NumericUpDownNodeBase<NumberField, int>
    {
        protected override TwoWayBinding<int, decimal?> ValueBinding { get; }
        public NumberFieldIntegerNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            ValueBinding = new TwoWayBinding<int, decimal?>(
                _output,
                NumericUpDown.ValueProperty,
                (x) => (decimal?)x,
                (x) => x == null ? 0 : (int)Math.Round(x.Value)
            );
        }
    }

    /// <summary>
    /// Generic wrapper for <see cref="NumberField"/>, allows to provide converters for value.
    /// </summary>
    // [ProcessNode(Name = "NumberField (Advanced Experimental)")]
    public class NumberFieldAdvancedExperimentalNode<TValue> : NumericUpDownNodeBase<NumberField, TValue>
    {
        protected static Func<decimal?, TValue> DefaultToValueConverter =
            (x) => x is null ? default! : (TValue)Convert.ChangeType(x, typeof(TValue));
        protected static Func<TValue, decimal?> DefaultFromValueConverter =
            (x) => x is null ? null : (decimal?)Convert.ChangeType(x, typeof(decimal));

        private Func<decimal?, TValue> _toValueConverter = DefaultToValueConverter;
        private Func<TValue, decimal?> _fromValueConverter = DefaultFromValueConverter;

        protected override TwoWayBinding<TValue, decimal?> ValueBinding { get; }
        public NumberFieldAdvancedExperimentalNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            ValueBinding = new TwoWayBinding<TValue, decimal?>(
                _output,
                NumericUpDown.ValueProperty,
                (x) => _fromValueConverter(x),
                (x) => _toValueConverter(x)
            );
        }

        public void SetToValueConverter(Func<decimal?, TValue> toValueConverter)
        {
            _toValueConverter = toValueConverter ?? DefaultToValueConverter;
        }

        public void SetFromValueConverter(Func<TValue, decimal?> fromValueConverter)
        {
            _fromValueConverter = fromValueConverter ?? DefaultFromValueConverter;
        }
    }
}
