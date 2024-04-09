using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

internal static class ControlsExtension
{
    internal static void SetControls(AvaloniaControls.Controls dst, IReadOnlyCollection<AvaloniaControls.Control?> src)
    {
        int i = 0;
        foreach (var control in src)
        {
            if (control !=null)
            {
                var idx = dst.IndexOf(control);
                if (idx == -1)
                {
                    dst.Add(control);
                    idx = dst.Count - 1;
                }
                dst.Move(idx, i);
                i++;
            }
        }
        if (dst.Count > i)
        {
            dst.RemoveRange(i - 1, dst.Count - i);
        }
    }
}
