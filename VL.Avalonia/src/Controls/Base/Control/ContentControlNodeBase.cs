using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="ContentControl"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class ContentControlNodeBase<TControl>
        : TemplatedControlNodeBase<TControl>
        where TControl : ContentControl, new()
    {
        [Fragment]
        public ContentControlNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary>Sets the content to display.</summary>
        [ImplementProperty(
            typeof(ContentControl),
            nameof(ContentControl.ContentProperty),
            Order = PinOrder.Main
        )]
        private Optional<object> _content;

        /// <summary>Sets the data template used to display the content of the control.</summary>
        [ImplementProperty(
            typeof(ContentControl),
            nameof(ContentControl.ContentTemplateProperty),
            Order = PinOrder.Secondary,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<IDataTemplate> _contentTemplate;

        /// <summary>Sets sets the horizontal alignment of the content within the control.</summary>
        [ImplementProperty(
            typeof(ContentControl),
            nameof(ContentControl.HorizontalContentAlignmentProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<HorizontalAlignment> _horizontalContentAlignment;

        /// <summary>Sets the vertical alignment of the content within the control.</summary>
        [ImplementProperty(
            typeof(ContentControl),
            nameof(ContentControl.VerticalContentAlignmentProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<VerticalAlignment> _verticalContentAlignment;
    }
}
