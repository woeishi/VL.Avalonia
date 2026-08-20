using Avalonia.Controls;
using Avalonia.Layout;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="WrapPanel"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class WrapPanelNodeBase<T> : PanelNodeBase<T>
        where T : WrapPanel, new()
    {
        [Fragment]
        public WrapPanelNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary>Sets the orientation in which child controls will be arranged.</summary>
        [ImplementProperty(
            typeof(WrapPanel),
            nameof(WrapPanel.OrientationProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<Orientation> _orientation;

        /// <summary>Sets the width of all items in the WrapPanel.</summary>
        [ImplementProperty(
            typeof(WrapPanel),
            nameof(WrapPanel.ItemWidthProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<float> _itemWidth;

        /// <summary>Sets the height of all items in the WrapPanel.</summary>
        [ImplementProperty(
            typeof(WrapPanel),
            nameof(WrapPanel.ItemHeightProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<float> _itemHeight;
    }

    /// <summary>
    /// Wrapper for <see cref="WrapPanel"/>
    /// </summary>
    [ProcessNode(Name = "WrapPanel")]
    public class WrapPanelNode : WrapPanelNodeBase<WrapPanel>
    {
        [Fragment]
        public WrapPanelNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetChildren(
            [Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<Control> children
        )
        {
            base.SetChildren(children);
        }
    }

    /// <inheritdoc cref="WrapPanelNode"/>
    [ProcessNode(Name = "WrapPanel (Spectral)")]
    public class WrapPanelSpectralNode : WrapPanelNodeBase<WrapPanel>
    {
        [Fragment]
        public WrapPanelSpectralNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetChildren(IReadOnlyList<Control> children)
        {
            base.SetChildren(children);
        }
    }
}
