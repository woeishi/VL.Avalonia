using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="LayoutTransformControl"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class LayoutTransformControlNodeBase<T> : DecoratorNodeBase<T>
        where T : LayoutTransformControl, new()
    {
        [Fragment]
        public LayoutTransformControlNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary>Sets a graphics transformation that should apply to this element when layout is performed.</summary>
        [ImplementProperty(
            typeof(LayoutTransformControl),
            nameof(LayoutTransformControl.LayoutTransformProperty),
            Order = PinOrder.Action
        )]
        private Optional<ITransform> _layoutTransform;

        /// <summary>Sets <see cref="Visual.RenderTransformProperty"/> for layout transforms.</summary>
        [ImplementProperty(
            typeof(LayoutTransformControl),
            nameof(LayoutTransformControl.UseRenderTransformProperty),
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _useRenderTransform;
    }

    /// <summary>
    /// Wrapper for <see cref="LayoutTransformControl"/>
    /// </summary>
    [ProcessNode(Name = "LayoutTransformControl")]
    public class LayoutTransformControlNode
        : LayoutTransformControlNodeBase<LayoutTransformControl>
    {
        [Fragment]
        public LayoutTransformControlNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
