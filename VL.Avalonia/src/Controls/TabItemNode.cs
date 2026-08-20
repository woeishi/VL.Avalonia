using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="TabItem"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class TabItemNodeBase<T> : HeaderedControlNodeBase<T>, IDisposable
        where T : HeaderedContentControl, new()
    {
        private TwoWayBinding<bool> _isSelectedBinding;

        [Fragment]
        public TabItemNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            _isSelectedBinding = new TwoWayBinding<bool>(_output, TabItem.IsSelectedProperty);
        }

        /// <param name="isSelectedChannel">Binds whether this list item is currently selected.</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetIsSelectedChannel(
            [Pin(Visibility = Model.PinVisibility.Optional)] IChannel<bool> isSelectedChannel
        ) => _isSelectedBinding.Bind(isSelectedChannel);

        /// <summary>Sets placement of this tab relative to the outer <see cref="TabControl"/>, if there is one.</summary>
        [ImplementProperty(
            typeof(TabItem),
            nameof(TabItem.TabStripPlacementProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<Dock> _tabStripPlacement;

        public override void Dispose()
        {
            _isSelectedBinding?.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// Wrapper for <see cref="TabItem"/>
    /// </summary>
    [ProcessNode(Name = "TabItem")]
    public class TabItemNode : TabItemNodeBase<TabItem>
    {
        [Fragment]
        public TabItemNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
