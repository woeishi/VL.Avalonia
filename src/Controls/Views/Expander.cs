using AvaloniaControls = Avalonia.Controls;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class Expander : AvaloniaControls.Expander
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.Expander);
    public Expander Output => this;

    public new object? Header
    {
        get => base.Header;
        set { if (base.Header != value) base.Header = value; }
    }

    public new object? Content
    {
        get => base.Content;
        set { if (base.Content != value) base.Content = value; }
    }

    public new AvaloniaControls.ExpandDirection ExpandDirection
    {
        get => base.ExpandDirection;
        set => base.ExpandDirection = value;
    }

    public new bool IsExpanded
    {
        get => base.IsExpanded;
        set => base.IsExpanded = value;
    }
}
