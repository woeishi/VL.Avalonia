using Avalonia.Controls;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Ungeneric wrapper for <see cref="ContextMenu"/>
    /// </summary>
    [ProcessNode(Name = "ContextMenu")]
    public class ContextMenuNode : ContextMenuNodeBase<object>
    {
        [Fragment]
        public ContextMenuNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<object?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="ContextMenuNode"/>
    [ProcessNode(Name = "ContextMenu (Spectral)")]
    public class ContextMenuSpectralNode : ContextMenuNodeBase<object>
    {
        [Fragment]
        public ContextMenuSpectralNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<object?> items)
        {
            base.SetItems(items);
        }
    }

    /// <summary>
    /// Generic wrapper for <see cref="ContextMenu"/>
    /// </summary>
    [ProcessNode(Name = "ContextMenu (Advanced)")]
    public class ContextMenuNode<T> : ContextMenuNodeBase<T>
    {
        [Fragment]
        public ContextMenuNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<T?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="ContextMenuNode{T}"/>
    [ProcessNode(Name = "ContextMenu (Advanced Spectral)")]
    public class ContextMenuSpectralNode<T> : ContextMenuNodeBase<T>
    {
        [Fragment]
        public ContextMenuSpectralNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T?> items)
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="ContextMenuNode{T}"/>
    [ProcessNode(Name = "ContextMenu (Advanced Reactive)")]
    public class ContextMenuReactiveNode<T> : ContextMenuNodeBase<T>
    {
        [Fragment]
        public ContextMenuReactiveNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }
}
