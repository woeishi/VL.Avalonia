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

    internal static void SetItems(AvaloniaControls.ItemCollection dst, IReadOnlyCollection<object?> src)
    {
        int i = 0;
        foreach (var item in src)
        {
            if (item != null)
            {
                var idx = dst.IndexOf(item);
                if (idx == -1)
                {
                    dst.Add(item);
                    idx = dst.Count - 1;
                }
                else if (idx != i)
                    dst.Insert(idx, item);
                i++;
            }
        }
        for (var r = dst.Count-1;  r >= i; r--)
        {
            dst.RemoveAt(r);
        }
    }
}
