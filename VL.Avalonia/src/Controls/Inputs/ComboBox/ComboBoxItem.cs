using Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="ComboBoxItem"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class ComboBoxItemNodeBase<T> : ListBoxItemNodeBase<T>
        where T : ComboBoxItem, new()
    {
        [Fragment]
        public ComboBoxItemNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }

    /// <summary>
    /// Wrapper for <see cref="ComboBoxItem"/>
    /// </summary>
    [ProcessNode(Name = "ComboBoxItem")]
    public class ComboBoxItemNode : ComboBoxItemNodeBase<ComboBoxItem>
    {
        [Fragment]
        public ComboBoxItemNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
