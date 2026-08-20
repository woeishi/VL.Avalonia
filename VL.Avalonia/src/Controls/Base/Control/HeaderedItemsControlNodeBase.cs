using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <inheritdoc cref="HeaderedItemsControl"/>
    [ProcessNode]
    public abstract partial class HeaderedItemsControlNodeBase<TControl, TValue>
        : ItemsControlNodeBase<TControl, TValue>
        where TControl : HeaderedItemsControl, new()
    {
        [Fragment]
        public HeaderedItemsControlNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

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
