using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Layout;
using Avalonia.Media;
using System.Globalization;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="NumericUpDown"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class NumericUpDownNodeBase<TControl, TValue> : ControlNodeBase<TControl>, IDisposable
        where TControl : NumericUpDown, new()
    {
        protected abstract TwoWayBinding<TValue, decimal?> ValueBinding { get; }

        /// <summary>
        /// Converts a value of the node's value type into the <see cref="decimal"/> the control
        /// expects. Returning <c>null</c> resets the target property to its default.
        /// </summary>
        protected abstract decimal? ToProperty(TValue value);

        /// <summary>
        /// Converts the <see cref="decimal"/> reported by the control back into the node's value type.
        /// </summary>
        protected abstract TValue ToValue(decimal? value);

        private TwoWayBinding<string?, string?> _textBinding;

        [Fragment]
        public NumericUpDownNodeBase([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext)
            : base(nodeContext)
        {
            _textBinding = new TwoWayBinding<string?, string?>(_output, NumericUpDown.TextProperty);
        }

        /// <param name="valueChannel">
        /// The current numeric value of the control
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetValueChannel(IChannel<TValue> valueChannel) =>
            ValueBinding.Bind(valueChannel);

        /// <param name="textChannel">
        /// The formatted string representation of the value
        /// </param>
        [Fragment(Order = PinOrder.Action)]
        public void SetTextChannel(
            [Pin(Visibility = PinVisibility.Optional)] IChannel<string?> textChannel
        ) => _textBinding.Bind(textChannel);

        /// <summary>Sets the placeholder text shown when the value is null.</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.WatermarkProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<string> _watermark;

        /// <summary>Sets the minimum allowed value.</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.MinimumProperty),
            Converter = nameof(ToProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<TValue> _minimum;

        /// <summary>Sets the maximum allowed value.</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.MaximumProperty),
            Converter = nameof(ToProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<TValue> _maximum;

        /// <summary>Sets the amount by which to increment or decrement the value.</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.IncrementProperty),
            Converter = nameof(ToProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<TValue> _increment;

        /// <summary>Sets whether the value should be automatically clipped to the min/max range.</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.ClipValueToMinMaxProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _clipValueToMinMax;

        /// <summary>Sets whether increment/decrement operations are allowed via keyboard, buttons, or mouse wheel.</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.AllowSpinProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _allowSpin;

        /// <summary>Sets whether the up/down buttons should be displayed.</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.ShowButtonSpinnerProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _showButtonSpinner;

        /// <summary>Sets the location of the spinner buttons (Left or Right).</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.ButtonSpinnerLocationProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<Location> _buttonSpinnerLocation;

        /// <summary>Sets whether the control is read-only (cannot be edited by user).</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.IsReadOnlyProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _isReadOnly;

        /// <summary>Sets the format string used to display the value (e.g., "F2", "C", "P").</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.FormatStringProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<string> _formatString;

        /// <summary>Sets the NumberFormatInfo used for formatting and parsing.</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.NumberFormatProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<NumberFormatInfo> _numberFormat;

        /// <summary>Sets the NumberStyles used when parsing text input.</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.ParsingNumberStyleProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<NumberStyles> _parsingNumberStyle;

        /// <summary>Sets the custom converter for bidirectional text-value conversion.</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.TextConverterProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<IValueConverter> _textConverter;

        /// <summary>Sets the horizontal alignment of content within the control.</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.HorizontalContentAlignmentProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<HorizontalAlignment> _horizontalContentAlignment;

        /// <summary>Sets the vertical alignment of content within the control.</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.VerticalContentAlignmentProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<VerticalAlignment> _verticalContentAlignment;

        /// <summary>Sets the horizontal alignment of text within the text box.</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.TextAlignmentProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<TextAlignment> _textAlignment;

        /// <summary>Sets the custom content positioned on the left side of the text area.</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.InnerLeftContentProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<object> _innerLeftContent;

        /// <summary>Sets the custom content positioned on the right side of the text area.</summary>
        [ImplementProperty(
            typeof(NumericUpDown),
            nameof(NumericUpDown.InnerRightContentProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<object> _innerRightContent;

        public override void Dispose()
        {
            ValueBinding?.Dispose();
            _textBinding?.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// Wrapper for <see cref="NumericUpDown"/>
    /// </summary>
    [ProcessNode(Name = "NumericUpDown")]
    public class NumericUpDownNode : NumericUpDownNodeBase<NumericUpDown, float?>
    {
        protected override TwoWayBinding<float?, decimal?> ValueBinding { get; }

        public NumericUpDownNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            ValueBinding = new TwoWayBinding<float?, decimal?>(
                _output,
                NumericUpDown.ValueProperty,
                ToProperty,
                ToValue
            );
        }

        protected override decimal? ToProperty(float? value) => (decimal?)value;

        protected override float? ToValue(decimal? value) => (float?)value;
    }

    /// <summary>
    /// Wrapper for <see cref="NumericUpDown"/>
    /// </summary>
    [ProcessNode(Name = "NumericUpDown (Float)")]
    public class NumericUpDownFloatNode : NumericUpDownNodeBase<NumericUpDown, float>
    {
        protected override TwoWayBinding<float, decimal?> ValueBinding { get; }

        public NumericUpDownFloatNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            ValueBinding = new TwoWayBinding<float, decimal?>(
                _output,
                NumericUpDown.ValueProperty,
                ToProperty,
                ToValue
            );
        }

        protected override decimal? ToProperty(float value) => (decimal)value;

        protected override float ToValue(decimal? value) => value is null ? 0f : (float)value.Value;
    }

    /// <summary>
    /// Wrapper for <see cref="NumericUpDown"/>
    /// </summary>
    [ProcessNode(Name = "NumericUpDown (Integer)")]
    public class NumericUpDownInteger32Node : NumericUpDownNodeBase<NumericUpDown, int>
    {
        protected override TwoWayBinding<int, decimal?> ValueBinding { get; }

        public NumericUpDownInteger32Node([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            _output.SetFormatString("0");

            ValueBinding = new TwoWayBinding<int, decimal?>(
                _output,
                NumericUpDown.ValueProperty,
                ToProperty,
                ToValue
            );
        }

        protected override decimal? ToProperty(int value) => value;

        protected override int ToValue(decimal? value) => value is null ? 0 : (int)Math.Round(value.Value);
    }

    /// <summary>
    /// Generic wrapper for <see cref="NumericUpDown"/>, allows to provide converters for value.
    /// </summary>
    // BLCOKED BY: https://forum.vvvv.org/t/bug-crash-with-nullable-decimal/25230
    // [ProcessNode(Name = "NumericUpDown (Advanced Experimental)")]
    public class NumericUpDownAdvancedExperimentalNode<TValue> : NumericUpDownNodeBase<NumericUpDown, TValue>
    {
        protected static Func<decimal?, TValue> DefaultToValueConverter =
            (x) => x is null ? default! : (TValue)Convert.ChangeType(x, typeof(TValue));
        protected static Func<TValue, decimal?> DefaultFromValueConverter =
            (x) => x is null ? null : (decimal?)Convert.ChangeType(x, typeof(decimal));

        protected override decimal? ToProperty(TValue value) => _fromValueConverter(value);

        protected override TValue ToValue(decimal? value) => _toValueConverter(value);

        private Func<decimal?, TValue> _toValueConverter = DefaultToValueConverter;
        private Func<TValue, decimal?> _fromValueConverter = DefaultFromValueConverter;

        protected override TwoWayBinding<TValue, decimal?> ValueBinding { get; }

        public NumericUpDownAdvancedExperimentalNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            ValueBinding = new TwoWayBinding<TValue, decimal?>(
                _output,
                NumericUpDown.ValueProperty,
                ToProperty,
                ToValue
            );
        }

        public void SetToValueConverter([Pin(Visibility = PinVisibility.Optional)] Func<decimal?, TValue> toValueConverter)
        {
            _toValueConverter = toValueConverter ?? DefaultToValueConverter;
        }

        public void SetFromValueConverter([Pin(Visibility = PinVisibility.Optional)] Func<TValue, decimal?> fromValueConverter)
        {
            _fromValueConverter = fromValueConverter ?? DefaultFromValueConverter;
        }
    }
}
