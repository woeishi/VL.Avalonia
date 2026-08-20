using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// The PathIcon control can draw an icon graphic from a stream geometry. For example, you can use the icon geometries from the Avalonia UI Fluent icons resource.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/path-icon">Slider</see>
/// </summary>
[ProcessNode(Name = "PathIcon")]
public partial class PathIconWrapper : ControlNodeBase<PathIcon>
{
    [Fragment]
    public PathIconWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

    /// <param name="data">
    /// Sets geometry for the icon.
    /// </param>
    [ImplementProperty("PathIcon.DataProperty", Order = PinOrder.Main)]
    protected Optional<Geometry> _data;
}

