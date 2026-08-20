using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="TemplatedControl"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class TemplatedControlNodeBase<T> : ControlNodeBase<T>
        where T : TemplatedControl, new()
    {
        [Fragment]
        public TemplatedControlNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /*
         * DESIGN NOTE:
         * The following StyledProperties are intentionally omitted from the main node
         * to prevent visual pin clutter. They are available as separate nodes via
         * extension methods instead (see TemplatedControlExtensions.cs):
         *
         * IBrush? Background
         * BackgroundSizing BackgroundSizing
         * IBrush? BorderBrush
         * Thickness BorderThickness
         * CornerRadius CornerRadius
         * FontFamily FontFamily
         * FontFeatureCollection? FontFeatures
         * double FontSize (exposed as float on facade)
         * FontStyle FontStyle
         * FontWeight FontWeight
         * FontStretch FontStretch
         * IBrush? Foreground
         * Thickness Padding
         */

        /*
         * Attached Properties:
         * - bool IsTemplateFocusTarget
         */

        /// <summary>Sets the template that defines the control's appearance.</summary>
        [ImplementProperty(
            typeof(TemplatedControl),
            nameof(TemplatedControl.TemplateProperty),
            Order = PinOrder.DataTemplate,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<IControlTemplate> _controlTemplate;
    }
}
