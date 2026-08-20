using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
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

        [ImplementProperty(typeof(Viewbox), nameof(Viewbox.ChildProperty), Order = PinOrder.Main)]
        private Optional<Control> _child;

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
