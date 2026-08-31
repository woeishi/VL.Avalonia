using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="Viewbox"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class ViewboxNodeBase<T> : ControlNodeBase<T>
        where T : Viewbox, new()
    {
        [Fragment]
        public ViewboxNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        private Optional<Control> _child;

        /// <summary>Sets the child control hosted by the Viewbox.</summary>
        [Fragment(Order = PinOrder.Main)]
        public void SetChild([Pin(Visibility = VL.Model.PinVisibility.Visible)] Optional<Control> child)
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

                _output.SetValue(Viewbox.ChildProperty, child.Value);
            }
            else
            {
                _output.ClearValue(Viewbox.ChildProperty);
            }
        }

        [ImplementProperty(
            typeof(Viewbox),
            nameof(Viewbox.StretchProperty),
            Order = PinOrder.Style
        )]
        private Optional<Stretch> _stretch;

        [ImplementProperty(
            typeof(Viewbox),
            nameof(Viewbox.StretchDirectionProperty),
            Order = PinOrder.Style
        )]
        private Optional<StretchDirection> _stretchDirection;
    }

    /// <summary>
    /// Wrapper for <see cref="Viewbox"/>
    /// </summary>
    [ProcessNode(Name = "Viewbox")]
    public class ViewboxNode : ViewboxNodeBase<Viewbox>
    {
        [Fragment]
        public ViewboxNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
