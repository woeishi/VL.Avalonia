using Avalonia;
using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
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

        private Optional<Control> _child;

        /// <summary>Sets the decorated control.</summary>
        [Fragment(Order = PinOrder.Main)]
        public void SetChild([Pin(Visibility = PinVisibility.Visible)] Optional<Control> child)
        {
            if (_child == child)
                return;

            _child = child;

            if (child.HasValue)
            {
                if (child.Value is Control control)
                {
                    ReparentingHelper.DetachFromParent(NodeContext, control);
                }

                _output.SetValue(Decorator.ChildProperty, child.Value);
            }
            else
            {
                _output.ClearValue(Decorator.ChildProperty);
            }
        }

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
