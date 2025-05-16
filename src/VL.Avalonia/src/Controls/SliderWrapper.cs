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
    [ImplementOutput]
    private readonly Slider _output = new Slider();

    [ImplementStyle]
    private Optional<IAvaloniaStyle> _style;

    // Wonder how to Observable to channel
    //private IChannel<float>? _valueChannel;
    //public IChannel<float>? ValueChannel
    //{
    //    private get => _valueChannel;
    //    set
    //    {
    //        if (_valueChannel != value)
    //        {
    //            value = _valueChannel;

    //            if (value != null)
    //            {
    //                _output.Bind(Slider.ValueProperty, );
    //            }

    //        }
    //    }
    //}

    // First approach
    //private IChannel<float>? _valueChannel;
    //public void SetValueChannel(IChannel<float>? valueChannel)
    //{
    //    if (_valueChannel != valueChannel)
    //    {
    //        _valueChannel = valueChannel;
    //        _valueChannel?.Subscribe((x) =>
    //        {
    //            _output.SetValue(Slider.ValueProperty, _valueChannel?.Value ?? 0);
    //        });

    //        _output.SetValue(Slider.ValueProperty, _valueChannel?.Value ?? 0);
    //    }
    //}
    //private IChannel<float>? ValueChannel => _valueChannel;


    public IChannel<float>? ValueChannel { private get; set; }
    ChannelFlange<float> _valueFlange = new ChannelFlange<float>(0.0f);


    [ImplementOptional<Slider>(nameof(Slider.MinimumProperty))]
    private Optional<float> _minimum;

    [ImplementOptional<Slider>(nameof(Slider.MaximumProperty))]
    private Optional<float> _maximum;


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
        {
            _valueFlange.Value = (float)a.NewValue;
            _valueFlange.Update(ValueChannel);

            _output.UpdateLayout();
            // ValueChannel?.OnNext((float)a.NewValue);
        };
    }
}
