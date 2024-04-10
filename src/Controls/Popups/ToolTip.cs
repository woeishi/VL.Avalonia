using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

public static partial class Control
{
    public static T? ToolTip<T>(T? control, object? tip, AvaloniaControls.PlacementMode placementMode, double horizontalOffset, double verticalOffset) where T : AvaloniaControls.Control
    {
        if (control != null)
        {
            if (AvaloniaControls.ToolTip.GetTip(control) != tip)
                AvaloniaControls.ToolTip.SetTip(control, tip);

            AvaloniaControls.ToolTip.SetPlacement(control, placementMode);

            AvaloniaControls.ToolTip.SetHorizontalOffset(control, horizontalOffset);
            AvaloniaControls.ToolTip.SetVerticalOffset(control, verticalOffset);
        }
        return control;
    }
}
