using AvaloniaControls = Avalonia.Controls;
using AvaloniaMedia = Avalonia.Media;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class NumericUpDown : AvaloniaControls.NumericUpDown
{
    // needed to inherit xaml definition
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.NumericUpDown);
    public NumericUpDown Output => this;

    public new decimal? Value
    {
        get => base.Value;
        set { if (value != null && value != base.Value) base.Value = value; }
    }

    public new decimal Increment
    {
        get => base.Increment;
        set => base.Increment = value;
    }

    public new decimal Minimum
    {
        get => base.Minimum;
        set => base.Minimum = value;
    }

    public new decimal Maximum
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

    public new string FormatString
    {
        get => base.FormatString;
        set => base.FormatString = value;
    }

    public new AvaloniaControls.Location ButtonSpinnerLocation
    {
        get => base.ButtonSpinnerLocation;
        set => base.ButtonSpinnerLocation = value;
    }

    public new bool AllowSpin
    {
        get => base.AllowSpin;
        set => base.AllowSpin = value;
    }

    public new bool ShowButtonSpinner
    {
        get => base.ShowButtonSpinner;
        set => base.ShowButtonSpinner = value;
    }
}
