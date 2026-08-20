using Avalonia.Controls;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="Canvas"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class CanvasNodeBase<T> : PanelNodeBase<T>
        where T : Canvas, new()
    {
        [Fragment]
        public CanvasNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }

    /// <summary>
    /// Wrapper for <see cref="Canvas"/>
    /// </summary>
    [ProcessNode(Name = "Canvas")]
    public class CanvasNode : CanvasNodeBase<Canvas>
    {
        [Fragment]
        public CanvasNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetChildren(
            [Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<Control> children
        )
        {
            base.SetChildren(children);
        }
    }

    /// <inheritdoc cref="CanvasNode"/>
    [ProcessNode(Name = "Canvas (Spectral)")]
    public class CanvasNodeSpectral : CanvasNodeBase<Canvas>
    {
        [Fragment]
        public CanvasNodeSpectral([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetChildren(IReadOnlyList<Control> children)
        {
            base.SetChildren(children);
        }
    }
}
