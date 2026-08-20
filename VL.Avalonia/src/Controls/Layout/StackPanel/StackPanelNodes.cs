using Avalonia.Controls;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Wrapper for <see cref="StackPanel"/>
    /// </summary>
    [ProcessNode(Name = "StackPanel")]
    public class StackPanelNode : StackPanelNodeBase<StackPanel>
    {
        [Fragment]
        public StackPanelNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetChildren(
            [Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<Control> children
        )
        {
            base.SetChildren(children);
        }
    }

    /// <inheritdoc cref="StackPanelNode"/>
    [ProcessNode(Name = "StackPanel (Spectral)")]
    public class StackPanelSpectralNode : StackPanelNodeBase<StackPanel>
    {
        [Fragment]
        public StackPanelSpectralNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetChildren(IReadOnlyList<Control> children)
        {
            base.SetChildren(children);
        }
    }
}
