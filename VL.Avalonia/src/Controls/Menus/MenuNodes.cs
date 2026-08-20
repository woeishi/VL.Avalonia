using Avalonia.Controls;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="Menu"/>
    /// </summary>
    [ProcessNode]
    public abstract class MenuNodeBase<T> : MenuBaseNode<Menu, T>
    {
        [Fragment]
        public MenuNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }

    /// <summary>
    /// Ungeneric wrapper for <see cref="Menu"/>
    /// </summary>
    [ProcessNode(Name = "Menu")]
    public class MenuNode : MenuNodeBase<object>
    {
        [Fragment]
        public MenuNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<object?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="MenuNode"/>
    [ProcessNode(Name = "Menu (Spectral)")]
    public class MenuSpectralNode : MenuNodeBase<object>
    {
        [Fragment]
        public MenuSpectralNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<object?> items)
        {
            base.SetItems(items);
        }
    }

    /// <summary>
    /// Generic wrapper for <see cref="Menu"/>
    /// </summary>
    [ProcessNode(Name = "Menu (Advanced)")]
    public class MenuNode<T> : MenuNodeBase<T>
    {
        [Fragment]
        public MenuNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<T?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="MenuNode{T}"/>
    [ProcessNode(Name = "Menu (Advanced Spectral)")]
    public class MenuSpectralNode<T> : MenuNodeBase<T>
    {
        [Fragment]
        public MenuSpectralNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T?> items)
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="MenuNode{T}"/>
    [ProcessNode(Name = "Menu (Advanced Reactive)")]
    public class MenuReactiveNode<T> : MenuNodeBase<T>
    {
        [Fragment]
        public MenuReactiveNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }
}
