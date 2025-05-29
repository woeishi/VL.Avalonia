using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

/// <summary>
/// The Separator control is used to provide visual separators within a Menu control.
/// </summary>
[ProcessNode(Name = "Separator")]
public partial class SeparatorWrapper
{
    [ImplementOutput]
    protected Separator _output = new Separator();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementClasses]
    protected Optional<string> _classes;
}
