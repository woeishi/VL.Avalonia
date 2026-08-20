using Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Wrapper for <see cref="SplitView"/>
    /// </summary>
    [ProcessNode(Name = "SplitView")]
    public class SplitViewNode : SplitViewNodeBase<SplitView>
    {
        [Fragment]
        public SplitViewNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
