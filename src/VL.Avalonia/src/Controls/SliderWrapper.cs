using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "SliderPrototype")]
public partial class SliderWrapper : AbstractWrapperBase<Slider>
{
    private readonly Slider _output = new Slider();
    public override Slider Output => _output;

    private IChannel<float>? _valueChannel;
    public void SetValueChannel(IChannel<float>? valueChannel)
    {
        if (_valueChannel != valueChannel)
        {
            _valueChannel = valueChannel;
            _valueChannel?.Subscribe((x) => _output.Value = x);

            _output.SetValue(RangeBase.ValueProperty, _valueChannel?.Value ?? 0);
        }
    }
    private IChannel<float>? ValueChannel => _valueChannel;

    private Optional<float> _minimum;
    public void SetMinimum(Optional<float> minimum)
    {
        if (minimum != _minimum)
        {
            _minimum = minimum;

            if (_minimum.HasValue)
            {
                _output.SetValue(RangeBase.MinimumProperty, (double)_minimum.Value);
            }
        }
    }

    private Optional<float> _maximum;
    public void SetMaximum(Optional<float> maximum)
    {
        if (maximum != _maximum)
        {
            _maximum = maximum;

            if (_maximum.HasValue)
            {
                _output.SetValue(RangeBase.MaximumProperty, (double)_maximum.Value);
            }
        }
    }


    [Fragment(IsHidden = true)]
    public void SetupVLDefaults()
    {
        _output.SetValue(RangeBase.MinimumProperty, 0.0);
        _output.SetValue(RangeBase.MaximumProperty, 1.0);
    }

    public SliderWrapper()
    {
        SetupVLDefaults();

        _output.ValueChanged += (s, a) =>
            ValueChannel?.OnNext((float)a.NewValue);
    }

    private IAvaloniaStyle? _style;
    public override IAvaloniaStyle? Style
    {
        set
        {
            if (!_style?.Equals(value) ?? _style != value)
            {
                // Need to test ResetStyle
                //_output.ApplyStyling();

                _style = value;
                _style?.ApplyStyle(Output);
            }
        }
    }
}
