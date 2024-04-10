using AvaloniaControls = Avalonia.Controls;
using AvaloniaMedia = Avalonia.Media;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class ProgressBar : AvaloniaControls.ProgressBar
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.ProgressBar);
    public ProgressBar Output => this;

    public new double Value
    {
        get => base.Value;
        set { if (value != base.Value) base.Value = value; }
    }

    public new double Minimum
    {
        get => base.Minimum;
        set => base.Minimum = value;
    }

    public new double Maximum
    {
        get => base.Maximum;
        set => base.Maximum = value;
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

    public new AvaloniaMedia.IBrush? Foreground
    {
        get => base.Foreground;
        set => base.Foreground = value;
    }

    public new bool ShowProgressText
    {
        get => base.ShowProgressText;
        set => base.ShowProgressText = value;
    }

    public new string ProgressTextFormat
    {
        get => base.ProgressTextFormat;
        set => base.ProgressTextFormat = value;
    }

}
