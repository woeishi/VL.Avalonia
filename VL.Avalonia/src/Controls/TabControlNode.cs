using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="TabControl"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class TabControlNodeBase<T>
        : SelectingItemsControlNodeBase<TabControl, T>
    {
        [Fragment]
        public TabControlNodeBase([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary>Sets the position of the tab strip.</summary>
        [ImplementProperty(typeof(TabControl), nameof(TabControl.TabStripPlacementProperty))]
        private Optional<Dock> _tabStripPlacement;

        /// <summary>Sets the horizontal alignment of the content.</summary>
        [ImplementProperty(
            typeof(TabControl),
            nameof(TabControl.HorizontalContentAlignmentProperty),
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<HorizontalAlignment> _horizontalContentAlignment;

        /// <summary>Sets the vertical alignment of the content.</summary>
        [ImplementProperty(
            typeof(TabControl),
            nameof(TabControl.VerticalContentAlignmentProperty),
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<VerticalAlignment> _verticalContentAlignment;

        /// <summary>Sets the default data template used to display the content.</summary>
        [ImplementProperty(
            typeof(TabControl),
            nameof(TabControl.ContentTemplateProperty),
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<IDataTemplate> _contentTemplate;
    }

    /// <summary>
    /// Wrapper for <see cref="TabControl"/>
    /// </summary>
    [ProcessNode(Name = "TabControl")]
    public partial class TabControlNode : TabControlNodeBase<object>
    {
        [Fragment]
        public TabControlNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<object?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <summary>
    /// Spectral wrapper for <see cref="TabControl"/>
    /// </summary>
    [ProcessNode(Name = "TabControl (Spectral)")]
    public partial class TabControlSpectralNode : TabControlNodeBase<object>
    {
        [Fragment]
        public TabControlSpectralNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<object?> items)
        {
            base.SetItems(items);
        }
    }

    /// <summary>
    /// Advanced wrapper for <see cref="TabControl"/>
    /// </summary>
    [ProcessNode(Name = "TabControl (Advanced)")]
    public partial class TabControlAdvancedNode<T> : TabControlNodeBase<T>
    {
        [Fragment]
        public TabControlAdvancedNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<T?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <summary>
    /// Advanced spectral wrapper for <see cref="TabControl"/>
    /// </summary>
    [ProcessNode(Name = "TabControl (Advanced Spectral)")]
    public partial class TabControlAdvancedSpectralNode<T> : TabControlNodeBase<T>
    {
        [Fragment]
        public TabControlAdvancedSpectralNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T?> items)
        {
            base.SetItems(items);
        }
    }

    /// <summary>
    /// Reactive wrapper for <see cref="TabControl"/>
    /// </summary>
    [ProcessNode(Name = "TabControl (Advanced Reactive)")]
    public partial class TabControlReactiveNode<T> : TabControlNodeBase<T>
    {
        [Fragment]
        public TabControlReactiveNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }
}
