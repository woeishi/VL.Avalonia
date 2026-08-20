using Avalonia.Animation;
using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="Expander"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class ExpanderNodeBase<T> : HeaderedControlNodeBase<T>, IDisposable
        where T : Expander, new()
    {
        private TwoWayBinding<bool> _isExpandedBinding;

        [Fragment]
        public ExpanderNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            _isExpandedBinding = new TwoWayBinding<bool>(_output, Expander.IsExpandedProperty);
        }

        /// <param name="isExpandedChannel">Binds IsExpanded property.</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetIsExpandedChannel(IChannel<bool> isExpandedChannel) =>
            _isExpandedBinding.Bind(isExpandedChannel);

        /// <summary>Sets the direction in which the <see cref="Expander"/> opens.</summary>
        [ImplementProperty(
            typeof(Expander),
            nameof(Expander.ExpandDirectionProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<ExpandDirection> _expandDirection;

        /// <summary>Sets the transition used when expanding or collapsing the content.</summary>
        [ImplementProperty(
            typeof(Expander),
            nameof(Expander.ContentTransitionProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<IPageTransition> _contentTransition;

        public override void Dispose()
        {
            _isExpandedBinding?.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// Wrapper for <see cref="Expander"/>
    /// </summary>
    [ProcessNode(Name = "Expander")]
    public class ExpanderNode : ExpanderNodeBase<Expander>
    {
        [Fragment]
        public ExpanderNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
