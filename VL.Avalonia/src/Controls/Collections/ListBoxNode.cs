using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="ListBox"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class ListBoxNodeBase<T> : SelectingItemsControlNodeBase<ListBox, T>
    {
        [Fragment]
        public ListBoxNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary>Sets the selection mode.</summary>
        [ImplementProperty(
            typeof(ListBox),
            nameof(ListBox.SelectionModeProperty),
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<SelectionMode> _selectionMode;
    }

    /// <summary>
    /// Ungeneric wrapper for <see cref="ListBox"/>
    /// </summary>
    [ProcessNode(Name = "ListBox")]
    public class ListBoxNode : ListBoxNodeBase<object>
    {
        [Fragment]
        public ListBoxNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<object?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="ListBoxNode"/>
    [ProcessNode(Name = "ListBox (Spectral)")]
    public class ListBoxSpectralNode : ListBoxNodeBase<object>
    {
        [Fragment]
        public ListBoxSpectralNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<object?> items)
        {
            base.SetItems(items);
        }
    }

    /// <summary>
    /// Generic wrapper for <see cref="ListBox"/>
    /// </summary>
    [ProcessNode(Name = "ListBox (Advanced)")]
    public class ListBoxNode<T> : ListBoxNodeBase<T>
    {
        [Fragment]
        public ListBoxNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<T?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="ListBoxNode{T}"/>
    [ProcessNode(Name = "ListBox (Advanced Spectral)")]
    public class ListBoxSpectralNode<T> : ListBoxNodeBase<T>
    {
        [Fragment]
        public ListBoxSpectralNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T?> items)
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="ListBoxNode{T}"/>
    [ProcessNode(Name = "ListBox (Advanced Reactive)")]
    public class ListBoxReactiveNode<T> : ListBoxNodeBase<T>
    {
        [Fragment]
        public ListBoxReactiveNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }
}
