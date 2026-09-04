using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Data;
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

        /// <summary>Sets channel value to control before bind</summary>
        ChannelToControl,

        /// <summary>Control value to channel before bind</summary>
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

            // Rebind unconditionally so a null input detaches the previous control.
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

        protected virtual void Bind()
        {
            _subscriptions.Clear();

            _subscriptions.Add(
                BindingHelpers.Bind(
                    _input,
                    _property,
                    _channel ?? _internalChannel,
                    _mode is SupportedBindingMode.TwoWay
                        ? BindingMode.TwoWay
                        : BindingMode.OneWay,
                    _valueToProperty,
                    _propertyToValue,
                    _initialValueHandling
                )
            );
        }

        public void Dispose()
        {
            _subscriptions.Dispose();
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
