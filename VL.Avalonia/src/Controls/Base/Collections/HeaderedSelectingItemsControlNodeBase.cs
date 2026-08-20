using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="HeaderedSelectingItemsControl"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class HeaderedSelectingItemsControlNodeBase<TControl, TValue>
        : SelectingItemsControlNodeBase<TControl, TValue>
        where TControl : HeaderedSelectingItemsControl, new()
    {
        [Fragment]
        public HeaderedSelectingItemsControlNodeBase([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary>Sets the content of the control's header.</summary>
        [ImplementProperty(
            typeof(HeaderedItemsControl),
            nameof(HeaderedItemsControl.HeaderProperty),
            Order = PinOrder.Secondary
        )]
        private Optional<object> _header;

        /// <summary>Sets the data template used to display the header content of the control.</summary>
        [ImplementProperty(
            typeof(HeaderedItemsControl),
            nameof(HeaderedItemsControl.HeaderTemplateProperty),
            Order = PinOrder.Secondary,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<IDataTemplate> _headerTemplate;
    }
}
