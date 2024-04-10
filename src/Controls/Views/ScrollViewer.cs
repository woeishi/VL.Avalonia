using AvaloniaBase = Avalonia;
using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class ScrollViewer : AvaloniaControls.ScrollViewer
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.ScrollViewer);
    public ScrollViewer Output => this;

    public new object? Content
    {
        get => base.Content;
        set { if (base.Content != value) base.Content = value; }
    }

    public new bool AllowAutoHide
    {
        get => base.AllowAutoHide;
        set => base.AllowAutoHide = value;
    }

    public new AvaloniaBase.Vector Offset
    {
        get => base.Offset;
        set => base.Offset = value;
    }

    public new AvaloniaControls.Primitives.ScrollBarVisibility HorizontalScrollBarVisibility
    {
        get => base.HorizontalScrollBarVisibility;
        set => base.HorizontalScrollBarVisibility = value;
    }

    public new AvaloniaControls.Primitives.ScrollBarVisibility VerticalScrollBarVisibility
    {
        get => base.VerticalScrollBarVisibility;
        set => base.VerticalScrollBarVisibility = value;
    }

    public new bool CanHorizontallyScroll => base.CanHorizontallyScroll;

    public new bool CanVerticallyScroll => base.CanVerticallyScroll;


}
