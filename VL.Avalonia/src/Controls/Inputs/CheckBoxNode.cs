using Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="CheckBox"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class CheckBoxNodeBase<T> : ToggleButtonNodeBase<T>
        where T : CheckBox, new()
    {
        [Fragment]
        public CheckBoxNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }

    /// <summary>
    /// The <c>CheckBox</c> is a control that allows users to select or deselect an option. It displays a check mark when selected and can also support a third indeterminate state. The CheckBox is commonly used in forms and settings to represent boolean choices or to indicate partial selection in hierarchical data.
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/checkbox">CheckBox</see>
    /// </summary>
    [ProcessNode(Name = "CheckBox")]
    public class CheckBoxNode : CheckBoxNodeBase<CheckBox>
    {
        [Fragment]
        public CheckBoxNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
