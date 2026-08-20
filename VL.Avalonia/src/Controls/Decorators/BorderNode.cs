using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Model;
using AvaThickness = Avalonia.Thickness;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="Border"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class BorderNodeBase<T> : DecoratorNodeBase<T>
        where T : Border, new()
    {
        [Fragment]
        public BorderNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary>Sets a brush with which to paint the background.</summary>
        [ImplementProperty(
            typeof(Border),
            nameof(Border.BackgroundProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<IBrush> _background;

        /// <summary>Sets how the background is drawn relative to the border.</summary>
        [ImplementProperty(
            typeof(Border),
            nameof(Border.BackgroundSizingProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<BackgroundSizing> _backgroundSizing;

        /// <summary>Sets a brush with which to paint the border.</summary>
        [ImplementProperty(
            typeof(Border),
            nameof(Border.BorderBrushProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<IBrush> _borderBrush;

        /// <summary>Sets the thickness of the border.</summary>
        [ImplementProperty(
            typeof(Border),
            nameof(Border.BorderThicknessProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<AvaThickness> _borderThickness;

        /// <summary>Sets the radius of the border rounded corners.</summary>
        [ImplementProperty(
            typeof(Border),
            nameof(Border.CornerRadiusProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<CornerRadius> _cornerRadius;

        /// <summary>Sets the box shadow effect parameters</summary>
        [ImplementProperty(
            typeof(Border),
            nameof(Border.BoxShadowProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<BoxShadows> _boxShadow;
    }

    /// <summary>
    /// Wrapper for <see cref="Border"/>
    /// </summary>
    [ProcessNode(Name = "Border")]
    public class BorderNode : BorderNodeBase<Border>
    {
        [Fragment]
        public BorderNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
