using Avalonia;
using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="Decorator"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class DecoratorNodeBase<T> : ControlNodeBase<T>
        where T : Decorator, new()
    {
        [Fragment]
        public DecoratorNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary>Sets the decorated control.</summary>
        [ImplementProperty(
            typeof(Decorator),
            nameof(Decorator.ChildProperty),
            Order = PinOrder.Main,
            PinVisibility = PinVisibility.Visible
        )]
        private Optional<Control> _child;

        /// <summary>Sets the padding to place around the child control.</summary>
        [ImplementProperty(
            typeof(Decorator),
            nameof(Decorator.PaddingProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<Thickness> _padding;
    }
}
