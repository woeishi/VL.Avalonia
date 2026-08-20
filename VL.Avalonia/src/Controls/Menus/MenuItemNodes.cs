using Avalonia.Controls;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Ungeneric wrapper for <see cref="MenuItem"/>
    /// </summary>
    [ProcessNode(Name = "MenuItem")]
    public class MenuItemNode : MenuItemNodeBase<object>
    {
        [Fragment]
        public MenuItemNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<object?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="MenuItemNode"/>
    [ProcessNode(Name = "MenuItem (Spectral)")]
    public class MenuItemSpectralNode : MenuItemNodeBase<object>
    {
        [Fragment]
        public MenuItemSpectralNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<object?> items)
        {
            base.SetItems(items);
        }
    }

    /// <summary>
    /// Generic wrapper for <see cref="MenuItem"/>
    /// </summary>
    [ProcessNode(Name = "MenuItem (Advanced)")]
    public class MenuItemNode<T> : MenuItemNodeBase<T>
    {
        [Fragment]
        public MenuItemNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<T?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="MenuItemNode{T}"/>
    [ProcessNode(Name = "MenuItem (Advanced Spectral)")]
    public class MenuItemSpectralNode<T> : MenuItemNodeBase<T>
    {
        [Fragment]
        public MenuItemSpectralNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T?> items)
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="MenuItemNode{T}"/>
    [ProcessNode(Name = "MenuItem (Advanced Reactive)")]
    public class MenuItemReactiveNode<T> : MenuItemNodeBase<T>
    {
        [Fragment]
        public MenuItemReactiveNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }
}
