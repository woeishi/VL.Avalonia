using AvaloniaControls = Avalonia.Controls;
using AvaloniaMedia = Avalonia.Media;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class SplitView : AvaloniaControls.SplitView
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.SplitView);
    public SplitView Output => this;

    public new object? Content
    {
        get => base.Content;
        set { if (base.Content != value) base.Content = value; }
    }

    public new AvaloniaControls.SplitViewDisplayMode DisplayMode
    {
        get => base.DisplayMode;
        set => base.DisplayMode = value;
    }

    public new object? Pane
    {
        get => base.Pane;
        set { if (base.Pane != value) base.Pane = value; }
    }

    public new AvaloniaControls.SplitViewPanePlacement PanePlacement
    {
        get => base.PanePlacement;
        set => base.PanePlacement = value;
    }

    public new bool IsPaneOpen
    {
        get => base.IsPaneOpen;
        set => base.IsPaneOpen = value;
    }

    public new double OpenPaneLength
    {
        get => base.OpenPaneLength;
        set => base.OpenPaneLength = value;
    }

    public new double CompactPaneLength
    {
        get => base.CompactPaneLength;
        set => base.CompactPaneLength = value;
    }

    public new AvaloniaMedia.IBrush PaneBackground
    {
        get => base.PaneBackground;
        set => base.PaneBackground = value;
    }
}
