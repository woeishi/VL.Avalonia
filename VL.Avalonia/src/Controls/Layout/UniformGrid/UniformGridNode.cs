using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="UniformGrid"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class UniformGridNodeBase<T> : PanelNodeBase<T>
        where T : UniformGrid, new()
    {
        [Fragment]
        public UniformGridNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary>Specifies the row count. If set to 0, row count will be calculated automatically.</summary>
        [ImplementProperty(
            typeof(UniformGrid),
            nameof(UniformGrid.RowsProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<int> _rows;

        /// <summary>Specifies the column count. If set to 0, column count will be calculated automatically.</summary>
        [ImplementProperty(
            typeof(UniformGrid),
            nameof(UniformGrid.ColumnsProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<int> _columns;

        /// <summary>Specifies, for the first row, the column where the items should start.</summary>
        [ImplementProperty(
            typeof(UniformGrid),
            nameof(UniformGrid.FirstColumnProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<int> _firstColumn;
    }

    /// <summary>
    /// Wrapper for <see cref="UniformGrid"/>
    /// </summary>
    [ProcessNode(Name = "UniformGrid")]
    public class UniformGridNode : UniformGridNodeBase<UniformGrid>
    {
        [Fragment]
        public UniformGridNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetChildren(
            [Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<Control> children
        )
        {
            base.SetChildren(children);
        }
    }

    /// <inheritdoc cref="UniformGridNode"/>
    [ProcessNode(Name = "UniformGrid (Spectral)")]
    public class UniformGridSpectralNode : UniformGridNodeBase<UniformGrid>
    {
        [Fragment]
        public UniformGridSpectralNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetChildren(IReadOnlyList<Control> children)
        {
            base.SetChildren(children);
        }
    }
}
