using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="HeaderedContentControl"/>.
    /// </summary>
    [ProcessNode]
    public abstract partial class HeaderedControlNodeBase<T> : ContentControlNodeBase<T>
        where T : HeaderedContentControl, new()
    {
        [Fragment]
        public HeaderedControlNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary>Sets the header content (can be a string, UI element, or any object).</summary>
        [ImplementProperty(
            typeof(HeaderedContentControl),
            nameof(HeaderedContentControl.HeaderProperty),
            Order = PinOrder.Secondary,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<object> _header;

        /// <summary>Sets the data template used to display the header content.</summary>
        [ImplementProperty(
            typeof(HeaderedContentControl),
            nameof(HeaderedContentControl.HeaderTemplateProperty),
            Order = PinOrder.DataTemplate,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<IDataTemplate> _headerTemplate;
    }
}
