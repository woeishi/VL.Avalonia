using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="TreeView"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class TreeViewNodeBase<TControl, TValue>
        : ItemsControlNodeBase<TControl, TValue>,
            IDisposable
        where TControl : TreeView, new()
    {
        private readonly TwoWayBinding<TValue?, object?> _selectedItemBinding;

        [Fragment]
        public TreeViewNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            _selectedItemBinding = new TwoWayBinding<TValue?, object?>(
                _output,
                TreeView.SelectedItemProperty,
                x => (object?)x,
                y => (TValue?)y
            );
        }

        /// <param name="selectedItemChannel">Binds <see cref="TreeView.SelectedItem"/> property.</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetSelectedItemChannel(
            [Pin(Visibility = PinVisibility.Optional)] IChannel<TValue?> selectedItemChannel
        ) => _selectedItemBinding.Bind(selectedItemChannel);

        /// <summary>Sets a value indicating whether to automatically scroll to newly selected items.</summary>
        [ImplementProperty(
            typeof(TreeView),
            nameof(TreeView.AutoScrollToSelectedItemProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _autoScrollToSelectedItem;

        /// <summary>Sets the selection mode.</summary>
        [ImplementProperty(
            typeof(TreeView),
            nameof(TreeView.SelectionModeProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<SelectionMode> _selectionMode;

        public override void Dispose()
        {
            _selectedItemBinding.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// Ungeneric wrapper for <see cref="TreeView"/>
    /// </summary>
    [ProcessNode(Name = "TreeView")]
    public class TreeViewNode : TreeViewNodeBase<TreeView, object>
    {
        [Fragment]
        public TreeViewNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<object?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="TreeViewNode"/>
    [ProcessNode(Name = "TreeView (Spectral)")]
    public class TreeViewSpectralNode : TreeViewNodeBase<TreeView, object>
    {
        [Fragment]
        public TreeViewSpectralNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<object?> items)
        {
            base.SetItems(items);
        }
    }

    /// <summary>
    /// Generic wrapper for <see cref="TreeView"/>
    /// </summary>
    [ProcessNode(Name = "TreeView (Advanced)")]
    public class TreeViewNode<T> : TreeViewNodeBase<TreeView, T>
    {
        [Fragment]
        public TreeViewNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<T?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="TreeViewNode{T}"/>
    [ProcessNode(Name = "TreeView (Advanced Spectral)")]
    public class TreeViewSpectralNode<T> : TreeViewNodeBase<TreeView, T>
    {
        [Fragment]
        public TreeViewSpectralNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T?> items)
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="TreeViewNode{T}"/>
    [ProcessNode(Name = "TreeView (Advanced Reactive)")]
    public class TreeViewNodeReactive<T> : TreeViewNodeBase<TreeView, T>
    {
        [Fragment]
        public TreeViewNodeReactive([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }
}
