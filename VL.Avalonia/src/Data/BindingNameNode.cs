using Avalonia;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Data
{
    /// <summary>
    /// Binds an <see cref="IChannel{TValue}"/> to an Avalonia property looked up by name on the
    /// input control, optionally converting between the channel value type and the property value type.
    /// </summary>
    [ProcessNode(Name = "Binding (Name Converters)")]
    public class BindingNameAdvancedNode<TControl, TValue, TProperty>
        : BindingNode<TControl, AvaloniaProperty, TValue, TProperty>
        where TControl : AvaloniaObject
    {
        private string? _propertyName;

        [Fragment]
        public BindingNameAdvancedNode(
            [Pin(Visibility = Model.PinVisibility.Optional)]
                Func<TValue?, TProperty?>? valueToProperty,
            [Pin(Visibility = Model.PinVisibility.Optional)]
                Func<TProperty?, TValue?>? propertyToValue
        )
            : base(valueToProperty, propertyToValue) { }

        [Fragment(Order = PinOrder.Main)]
        public void SetProperty(string? propertyName)
        {
            if (_propertyName != propertyName)
            {
                _propertyName = propertyName;

                if (ResolveProperty())
                    Bind();
            }
        }

        protected override void OnInputChanged()
        {
            ResolveProperty();
        }

        /// <summary>
        /// Resolves the current property name against the current input's type.
        /// </summary>
        /// <returns>True if the resolved property changed.</returns>
        private bool ResolveProperty()
        {
            var inputType = _input?.GetType();

            if (inputType is null || string.IsNullOrEmpty(_propertyName))
                return false;

            var prop = AvaloniaPropertyRegistry.Instance.FindRegistered(inputType, _propertyName);

            if (prop is null || prop == _property)
                return false;

            _property = prop;

            return true;
        }
    }

    /// <summary>
    /// A <see cref="BindingNameAdvancedNode{TControl, TValue, TProperty}"/> for the common case where the
    /// Avalonia property value type and the channel value type are the same.
    /// </summary>
    [ProcessNode(Name = "Binding (Name)")]
    public class BindingNameNode<TControl, TValue> : BindingNameAdvancedNode<TControl, TValue, TValue>
        where TControl : AvaloniaObject
    {
        [Fragment]
        public BindingNameNode()
            : base(null, null) { }
    }
}
