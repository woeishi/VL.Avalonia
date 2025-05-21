using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// The Separator control is used to provide visual separators within a Menu control.
/// </summary>
[ProcessNode(Name = "Separator")]
public partial class SeparatorWrapper
{
    [ImplementOutput]
    private Separator _output = new Separator();
}
