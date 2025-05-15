using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "SliderPrototype")]
public partial class SliderWrapper
{
    [ImplementOutput<Slider>]
    private readonly Slider _output = new Slider();

    [ImplementChannel<RangeBase>("ValueProperty")]
    private IChannel<double>? _valueChannel;
    //public void SetValueChannel(IChannel<double>? valueChannel)
    //{
    //    if (_valueChannel != valueChannel)
    //    {
    //        _valueChannel = valueChannel;
    //        _valueChannel?.Subscribe((x) =>
    //        {
    //            _output.SetValue(RangeBase.ValueProperty, _valueChannel?.Value ?? 0);
    //        });

    //        _output.SetValue(RangeBase.ValueProperty, _valueChannel?.Value ?? 0);
    //    }
    //}
    //[Fragment(IsHidden = true)]
    //public IChannel<double>? ValueChannel => _valueChannel;

    [ImplementOptional<RangeBase>("MinimumProperty")]
    private Optional<double> _minimum;

    [ImplementOptional<RangeBase>("MaximumProperty")]
    private Optional<double> _maximum;



    [Fragment(IsHidden = true)]
    public void SetupVLDefaults()
    {
        _output.SetValue(RangeBase.MinimumProperty, 0.0);
        _output.SetValue(RangeBase.MaximumProperty, 1.0);
    }

    public SliderWrapper()
    {
        SetupVLDefaults();

        //_output.ValueChanged += (s, a) =>
        //    ValueChannel?.OnNext((float)a.NewValue);
    }

    private IAvaloniaStyle? _style;
    public IAvaloniaStyle? Style
    {
        set
        {
            if (!_style?.Equals(value) ?? _style != value)
            {
                // Need to test ResetStyle
                //_output.ApplyStyling();

                _style = value;
                _style?.ApplyStyle(_output);
            }
        }
    }
}
