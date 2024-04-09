using AvaloniaControls = Avalonia.Controls;
using AvaloniaMedia = Avalonia.Media;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class SelectableTextBlock : AvaloniaControls.SelectableTextBlock
{
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.SelectableTextBlock);
    public SelectableTextBlock Output => this;

    public new string? Text
    {
        get => base.Text;
        set { if (value != null && base.Text != value) base.Text = value; }
    }

    public new AvaloniaMedia.FontFamily FontFamily
    {
        get => base.FontFamily;
        set => base.FontFamily = value;
    }

    public new double FontSize
    {
        get => base.FontSize;
        set => base.FontSize = value;
    }

    public new AvaloniaMedia.FontWeight FontWeight
    {
        get => base.FontWeight;
        set => base.FontWeight = value;
    }

    public new AvaloniaMedia.FontStyle FontStyle
    {
        get => base.FontStyle;
        set => base.FontStyle = value;
    }

    public new AvaloniaMedia.TextAlignment TextAlignment
    {
        get => base.TextAlignment;
        set => base.TextAlignment = value;
    }

    public new string SelectedText => base.SelectedText;

    public new AvaloniaMedia.IBrush SelectionBrush
    {
        get => base.SelectionBrush;
        set => base.SelectionBrush = value;
    }
}
