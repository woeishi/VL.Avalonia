using Avalonia.Controls;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="StackPanel"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class StackPanelNodeBase<T> : PanelNodeBase<T>
        where T : StackPanel, new()
    {
        [Fragment]
        public StackPanelNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary>Sets the size of the spacing to place between child controls.</summary>
        [ImplementProperty(
            typeof(StackPanel),
            nameof(StackPanel.SpacingProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<float> _spacing;

        /// <summary>Sets the orientation in which child controls will be laid out.</summary>
        [ImplementProperty(
            typeof(StackPanel),
            nameof(StackPanel.OrientationProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<Orientation> _orientation;

        /// <summary>Sets whether the horizontal snap points for the <see cref="StackPanel"/> are equidistant from each other.</summary>
        [ImplementProperty(
            typeof(StackPanel),
            nameof(StackPanel.AreHorizontalSnapPointsRegularProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _areHorizontalSnapPointsRegular;

        /// <summary>Sets whether the vertical snap points for the <see cref="StackPanel"/> are equidistant from each other.</summary>
        [ImplementProperty(
            typeof(StackPanel),
            nameof(StackPanel.AreVerticalSnapPointsRegularProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _areVerticalSnapPointsRegular;
    }
}
