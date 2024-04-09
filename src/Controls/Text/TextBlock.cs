using AvaloniaControls = Avalonia.Controls;
using AvaloniaMedia = Avalonia.Media;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class TextBlock : AvaloniaControls.TextBlock
{
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.TextBlock);
    public TextBlock Output => this;

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

    public new AvaloniaMedia.TextWrapping TextWrapping
    {
        get => base.TextWrapping;
        set => base.TextWrapping = value;
    }
}
