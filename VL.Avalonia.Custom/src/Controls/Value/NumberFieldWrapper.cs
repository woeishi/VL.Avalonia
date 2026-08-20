using VL.Avalonia.Attributes;
using VL.Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Custom.Controls.Value
{
    /// <summary>
    /// A control that lets the user change value.
    /// <br/>NumberField<br/>
    /// </summary>
    [ProcessNode(Name = "NumberField")]
    public partial class NumberFieldWrapper : NumericUpDownNodeBase<NumberField>
    {
        [Fragment]
        public NumberFieldWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
