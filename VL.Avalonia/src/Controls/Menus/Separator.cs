using Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// The Separator control is used to provide visual separators within a Menu control.
/// </summary>
[ProcessNode(Name = "Separator")]
public partial class SeparatorWrapper : ControlNodeBase<Separator>
{
    [Fragment]
    public SeparatorWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
}

