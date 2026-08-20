using VL.Avalonia.Attributes;
using VL.Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Custom.Controls.Value
{
    /// <summary>
    /// A slider with editable value with middle click to edit value
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/slider">Editable Slider</see>
    /// </summary>
    [ProcessNode(Name = "EditableSlider")]
    public partial class EditableSliderWrapper : RangeBaseNodeBase<EditableSlider>
    {
        [Fragment]
        public EditableSliderWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
