using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="RadioButton"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class RadioButtonNodeBase<T> : ToggleButtonNodeBase<T>
        where T : RadioButton, new()
    {
        [Fragment]
        public RadioButtonNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary> sets the name that specifies which RadioButton controls are mutually exclusive.</summary>
        [ImplementProperty(
            typeof(RadioButton),
            nameof(RadioButton.GroupNameProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<string> _groupName;
    }

    /// <summary>
    /// Wrapper for <see cref="RadioButton"/>
    /// </summary>
    [ProcessNode(Name = "RadioButton")]
    public class RadioButtonNode : RadioButtonNodeBase<RadioButton>
    {
        [Fragment]
        public RadioButtonNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
