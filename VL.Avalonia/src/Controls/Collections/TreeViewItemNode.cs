using Avalonia.Controls;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="TreeViewItem"/>
    /// </summary>
    [ProcessNode]
    public abstract class TreeViewItemNodeBase<T>
        : HeaderedItemsControlNodeBase<TreeViewItem, T>,
            IDisposable
    {
        private TwoWayBinding<bool> _isExpandedBinding;
        private TwoWayBinding<bool> _isSelectedBinding;

        [Fragment]
        public TreeViewItemNodeBase([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            _isExpandedBinding = new(_output, TreeViewItem.IsExpandedProperty);
            _isSelectedBinding = new(_output, TreeViewItem.IsSelectedProperty);
        }

        /// <inheritdoc cref="TreeViewItem.IsExpandedProperty"/>
        [Fragment(Order = PinOrder.Action)]
        public void SetIsExpandedChannel(
            [Pin(Visibility = PinVisibility.Optional)] IChannel<bool> isExpandedChannel
        )
        {
            _isExpandedBinding.Bind(isExpandedChannel);
        }

        /// <inheritdoc cref="TreeViewItem.IsSelectedProperty"/>
        [Fragment(Order = PinOrder.Action)]
        public void SetIsSelectedChannel(
            [Pin(Visibility = PinVisibility.Optional)] IChannel<bool> isSelectedChannel
        )
        {
            _isSelectedBinding.Bind(isSelectedChannel);
        }

        public override void Dispose()
        {
            _isExpandedBinding?.Dispose();
            _isSelectedBinding?.Dispose();

            base.Dispose();
        }
    }

    /// <summary>
    /// Ungeneric wrapper for <see cref="TreeViewItem"/>
    /// </summary>
    [ProcessNode(Name = "TreeViewItem")]
    public class TreeViewItemNode : TreeViewItemNodeBase<object>
    {
        [Fragment]
        public TreeViewItemNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<object?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="TreeViewItemNode"/>
    [ProcessNode(Name = "TreeViewItem (Spectral)")]
    public class TreeViewItemSpectralNode : TreeViewItemNodeBase<object>
    {
        [Fragment]
        public TreeViewItemSpectralNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<object?> items)
        {
            base.SetItems(items);
        }
    }

    /// <summary>
    /// Generic wrapper for <see cref="TreeViewItem"/>
    /// </summary>
    [ProcessNode(Name = "TreeViewItem (Advanced)")]
    public class TreeViewItemNode<T> : TreeViewItemNodeBase<T>
    {
        [Fragment]
        public TreeViewItemNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<T?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="TreeViewItemNode{T}"/>
    [ProcessNode(Name = "TreeViewItem (Advanced Spectral)")]
    public class TreeViewItemSpectralNode<T> : TreeViewItemNodeBase<T>
    {
        [Fragment]
        public TreeViewItemSpectralNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T?> items)
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="TreeViewItemNode{T}"/>
    [ProcessNode(Name = "TreeViewItem (Advanced Reactive)")]
    public class TreeViewItemReactiveNode<T> : TreeViewItemNodeBase<T>
    {
        [Fragment]
        public TreeViewItemReactiveNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }
}
