using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Styling;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="ItemsControl">ItemsControl</see>
    /// </summary>
    [ProcessNode(FragmentSelection = FragmentSelection.Explicit)]
    public abstract partial class ItemsControlNodeBase<TControl, TValue>
        : TemplatedControlNodeBase<TControl>,
            IDisposable
        where TControl : ItemsControl, new()
    {
        private IChannel<IReadOnlyList<TValue>>? _itemsSource;
        private IDisposable? _itemSourceBinding;
        private ISpread? _items;

        [Fragment]
        public ItemsControlNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <param name="itemsSource">Binds observable items source.</param>
        public virtual void SetItemsSource(IChannel<IReadOnlyList<TValue>> itemsSource)
        {
            if (ReferenceEquals(_itemsSource, itemsSource))
                return;

            _itemSourceBinding?.Dispose();
            _itemsSource = itemsSource;
            _items = null;

            if (_itemsSource != null)
            {
                _itemSourceBinding = _output.Bind(
                    ItemsControl.ItemsSourceProperty,
                    itemsSource.StartWith(itemsSource.Value)
                );
            }
            else
            {
                _output.ClearValue(ItemsControl.ItemsSourceProperty);
            }
        }

        /// <param name="items">Sets items on source.</param>
        public virtual void SetItems(Spread<TValue?> items)
        {
            if (ReferenceEquals(_items, items))
                return;

            _itemSourceBinding?.Dispose();
            _items = items;
            _itemsSource = null;

            if (_items != null)
            {
                _output.SetValue(ItemsControl.ItemsSourceProperty, items);
            }
            else
            {
                _output.ClearValue(ItemsControl.ItemsSourceProperty);
            }
        }

        /// <summary>Sets the data template used to display the items in the control.</summary>
        [ImplementProperty(
            typeof(ItemsControl),
            nameof(ItemsControl.ItemTemplateProperty),
            Order = PinOrder.DataTemplate,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<IDataTemplate> _itemTemplate;

        /// <summary>Sets the panel used to display the items.</summary>
        [ImplementProperty(
            typeof(ItemsControl),
            nameof(ItemsControl.ItemsPanelProperty),
            Order = PinOrder.DataTemplate,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<ITemplate<Panel>> _itemsPanel;

        /// <summary>Sets the <see cref="ControlTheme"/> that is applied to the container element generated for each item.</summary>
        [ImplementProperty(
            typeof(ItemsControl),
            nameof(ItemsControl.ItemContainerThemeProperty),
            Order = PinOrder.DataTemplate,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<ControlTheme> _itemContainerTheme;

        public override void Dispose()
        {
            _itemsSource = null;
            _itemSourceBinding?.Dispose();
            _items = null;

            base.Dispose();
        }
    }

    /// <summary>
    /// Ungeneric wrapper for <see cref="ItemsControl"/>
    /// </summary>
    [ProcessNode(Name = "ItemsControl")]
    public partial class ItemsControlNode : ItemsControlNodeBase<ItemsControl, object>
    {
        [Fragment]
        public ItemsControlNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<object?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="ItemsControlNode"/>
    [ProcessNode(Name = "ItemsControl (Spectral)")]
    public partial class ItemsControlSpectralNode : ItemsControlNodeBase<ItemsControl, object>
    {
        [Fragment]
        public ItemsControlSpectralNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<object?> items)
        {
            base.SetItems(items);
        }
    }

    /// <summary>
    /// Generic wrapper for <see cref="ItemsControl"/>
    /// </summary>
    [ProcessNode(Name = "ItemsControl (Advanced)")]
    public partial class ItemsControlNode<T> : ItemsControlNodeBase<ItemsControl, T>
    {
        [Fragment]
        public ItemsControlNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<T?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="ItemsControlNode{T}"/>
    [ProcessNode(Name = "ItemsControl (Advanced Spectral)")]
    public partial class ItemsControlSpectralNode<T> : ItemsControlNodeBase<ItemsControl, T>
    {
        [Fragment]
        public ItemsControlSpectralNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T?> items)
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="ItemsControlNode{T}"/>
    [ProcessNode(Name = "ItemsControl (Advanced Reactive)")]
    public partial class ItemsControlNodeReactive<T> : ItemsControlNodeBase<ItemsControl, T>
    {
        [Fragment]
        public ItemsControlNodeReactive([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }
}
