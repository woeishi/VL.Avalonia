using Avalonia;
using System.Globalization;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Data
{
    public enum SupportedBindingMode
    {
        OneWay,
        TwoWay,
    }

    public enum SupprotedBindingInitialValueHandling
    {
        None,

        /// <summary>
        /// Sets channel value to control before bind
        /// </summary>
        ChannelToControl,

        /// <summary>
        /// Control value to channel before bind
        /// </summary>
        ControlToChannel,
    }

    /// <summary>
    /// Binds an <see cref="IChannel{TValue}"/> to an Avalonia property of type <typeparamref name="TProperty"/>,
    /// optionally converting between the channel value type and the property value type.
    /// </summary>
    [ProcessNode(Name = "Binding (Converters)", FragmentSelection = FragmentSelection.Explicit)]
    public class BindingNode<TControl, TAvaloniaProperty, TValue, TProperty> : IDisposable
        where TControl : AvaloniaObject
        where TAvaloniaProperty : AvaloniaProperty
    {
        protected TControl? _input;
        protected TAvaloniaProperty? _property;
        protected SupprotedBindingInitialValueHandling _initialValueHandling =
            SupprotedBindingInitialValueHandling.ChannelToControl;
        protected IChannel<TValue>? _channel;
        protected IChannel<TValue>? _internalChannel = ChannelHelpers.CreateChannelOfType<TValue>();

        protected SupportedBindingMode _mode = SupportedBindingMode.OneWay;

        protected readonly Func<TValue?, TProperty?>? _valueToProperty;
        protected readonly Func<TProperty?, TValue?>? _propertyToValue;

        private readonly CompositeDisposable _subscriptions = new();

        [Fragment]
        public BindingNode(
            [Pin(Visibility = Model.PinVisibility.Optional)]
                Func<TValue?, TProperty?>? valueToProperty,
            [Pin(Visibility = Model.PinVisibility.Optional)]
                Func<TProperty?, TValue?>? propertyToValue
        )
        {
            _valueToProperty = valueToProperty;
            _propertyToValue = propertyToValue;
        }

        [Fragment(Order = PinOrder.Main)]
        public void SetInput(TControl? input)
        {
            if (ReferenceEquals(_input, input))
                return;

            _input = input;

            OnInputChanged();

            Bind();
        }

        public void SetProperty(TAvaloniaProperty? property)
        {
            if (_property != property)
            {
                _property = property;

                Bind();
            }
        }

        [Fragment(Order = PinOrder.Action)]
        public void SetMode(SupportedBindingMode mode = SupportedBindingMode.OneWay)
        {
            if (_mode != mode)
            {
                _mode = mode;

                Bind();
            }
        }

        [Fragment(Order = PinOrder.Action)]
        public void SetInitialValueHandling(
            [Pin(Visibility = Model.PinVisibility.Optional)]
                SupprotedBindingInitialValueHandling initialValueHandling =
                SupprotedBindingInitialValueHandling.ChannelToControl
        )
        {
            if (_initialValueHandling != initialValueHandling)
            {
                _initialValueHandling = initialValueHandling;
                Bind();
            }
        }

        [Fragment(Order = PinOrder.Action)]
        public void SetChannel(IChannel<TValue>? channel)
        {
            if (_channel != channel)
            {
                _channel = channel;

                Bind();
            }
        }

        /// <summary>
        /// Called after <see cref="_input"/> changed and before the binding is re-established.
        /// </summary>
        protected virtual void OnInputChanged() { }

        /// <summary>
        /// Converts a channel value into a property value, using the optional converter.
        /// </summary>
        protected TProperty? ToProperty(TValue? value)
        {
            if (_valueToProperty is not null)
                return _valueToProperty(value);

            if (typeof(TValue) == typeof(TProperty))
                return Unsafe.As<TValue?, TProperty?>(ref value);

            return Convert<TProperty>(value);
        }

        /// <summary>
        /// Converts a property value into a channel value, using the optional converter.
        /// </summary>
        protected TValue? ToValue(TProperty? value)
        {
            if (_propertyToValue is not null)
                return _propertyToValue(value);

            if (typeof(TValue) == typeof(TProperty))
                return Unsafe.As<TProperty?, TValue?>(ref value);

            return Convert<TValue>(value);
        }

        /// <summary>
        /// Converts a channel value into a boxed property value, boxing at most once.
        /// </summary>
        protected object? ToPropertyObject(TValue? value) => ToProperty(value);

        /// <summary>
        /// Converts an already boxed property value into a channel value, without re-boxing.
        /// </summary>
        protected TValue? FromPropertyObject(object? value) =>
            _propertyToValue is null && typeof(TValue) != typeof(TProperty)
                ? Convert<TValue>(value)
                : ToValue(Convert<TProperty>(value));

        /// <summary>
        /// Converts a value of an unrelated type without throwing. Returns the default value when no
        /// conversion is possible - a converter should be supplied for those cases.
        /// </summary>
        private static TTarget? Convert<TTarget>(object? value)
        {
            if (value is null)
                return default;

            if (value is TTarget target)
                return target;

            try
            {
                var targetType =
                    Nullable.GetUnderlyingType(typeof(TTarget)) ?? typeof(TTarget);

                return (TTarget?)
                    System.Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
            }
            catch (Exception e)
                when (e
                        is InvalidCastException
                            or FormatException
                            or OverflowException
                            or ArgumentException
                )
            {
                return default;
            }
        }

        protected virtual void Bind()
        {
            _subscriptions.Clear();

            if (_input is null || _property is null)
                return;

            var channel = _channel ?? _internalChannel;

            if (channel is null)
                return;

            // Initial value handling

            if (_initialValueHandling is SupprotedBindingInitialValueHandling.ChannelToControl)
            {
                // Set from channel to control
                _input.SetValue(_property, ToPropertyObject(channel.Value));
            }
            if (_initialValueHandling is SupprotedBindingInitialValueHandling.ControlToChannel)
            {
                //  Set from control to channel
                channel.SetValue(FromPropertyObject(_input.GetValue(_property)));
            }

            var channelToBind = channel.Select(ToPropertyObject);

            _subscriptions.Add(_input.Bind(_property, channelToBind));

            switch (_mode)
            {
                case SupportedBindingMode.TwoWay:
                    var observable = _input.GetObservable(_property).Skip(1);

                    _subscriptions.Add(
                        observable.Subscribe(x => channel.OnNext(FromPropertyObject(x)!))
                    );

                    return;
                case SupportedBindingMode.OneWay:
                default:
                    return;
            }
        }

        public void Dispose()
        {
            _subscriptions.Clear();
            _internalChannel?.Dispose();
        }
    }

    /// <summary>
    /// A <see cref="BindingNode{TControl, TAvaloniaProperty, TValue, TProperty}"/> for the common case
    /// where the Avalonia property value type and the channel value type are the same.
    /// </summary>
    [ProcessNode(Name = "Binding", FragmentSelection = FragmentSelection.Explicit)]
    public class BindingNode<TControl, TAvaloniaProperty, TValue>
        : BindingNode<TControl, TAvaloniaProperty, TValue, TValue>
        where TControl : AvaloniaObject
        where TAvaloniaProperty : AvaloniaProperty
    {
        [Fragment]
        public BindingNode()
            : base(null, null) { }
    }
}
