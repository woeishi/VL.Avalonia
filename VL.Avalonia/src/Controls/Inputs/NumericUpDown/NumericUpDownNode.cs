using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Layout;
using Avalonia.Media;
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
    public abstract partial class NumericUpDownNodeBase<T> : ControlNodeBase<T>, IDisposable
        where T : NumericUpDown, new()
    {
        private TwoWayBinding<float?, decimal?> _valueBinding;
        private TwoWayBinding<string?, string?> _textBinding;

        [Fragment]
        public NumericUpDownNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext)
            : base(nodeContext)
        {
            _valueBinding = new TwoWayBinding<float?, decimal?>(
                _output,
                NumericUpDown.ValueProperty,
                (x) => (decimal?)x,
                (x) => (float?)x
            );
            _textBinding = new TwoWayBinding<string?, string?>(_output, NumericUpDown.TextProperty);
        }

        /// <param name="valueChannel">
        /// The current numeric value of the control
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetValueChannel(IChannel<float?> valueChannel) =>
            _valueBinding.Bind(valueChannel);

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
        [ImplementProperty<float, decimal>(
            typeof(NumericUpDown),
            nameof(NumericUpDown.MinimumProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<float> _minimum;

        /// <summary>Sets the maximum allowed value.</summary>
        [ImplementProperty<float, decimal>(
            typeof(NumericUpDown),
            nameof(NumericUpDown.MaximumProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<float> _maximum;

        /// <summary>Sets the amount by which to increment or decrement the value.</summary>
        [ImplementProperty<float, decimal>(
            typeof(NumericUpDown),
            nameof(NumericUpDown.IncrementProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<float> _increment;

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
            _valueBinding?.Dispose();
            _textBinding?.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// Wrapper for <see cref="NumericUpDown"/>
    /// </summary>
    [ProcessNode(Name = "NumericUpDown")]
    public class NumericUpDownNode : NumericUpDownNodeBase<NumericUpDown>
    {
        [Fragment]
        public NumericUpDownNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
