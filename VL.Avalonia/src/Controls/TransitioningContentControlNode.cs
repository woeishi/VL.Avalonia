using Avalonia.Animation;
using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="TransitioningContentControl"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class TransitioningContentControlNodeBase<T> : ContentControlNodeBase<T>
        where T : TransitioningContentControl, new()
    {
        [Fragment]
        public TransitioningContentControlNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary>Sets the page transition.</summary>
        [ImplementProperty(
            typeof(TransitioningContentControl),
            nameof(TransitioningContentControl.PageTransitionProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<IPageTransition> _pageTransition;

        /// <summary>Sets a value indicating whether the transition should be reversed.</summary>
        [ImplementProperty(
            typeof(TransitioningContentControl),
            nameof(TransitioningContentControl.IsTransitionReversedProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _isTransitionReversed;
    }

    /// <summary>
    /// Wrapper for <see cref="TransitioningContentControl"/>
    /// </summary>
    [ProcessNode(Name = "TransitioningContentControl")]
    public class TransitioningContentControlNode
        : TransitioningContentControlNodeBase<TransitioningContentControl>
    {
        [Fragment]
        public TransitioningContentControlNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
