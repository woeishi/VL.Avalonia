using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using VL.Avalonia.Controls;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Custom.Controls.Value
{
    /// <summary>
    /// ColorSpectrum
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/colorpicker/">ColorSpectrum</see>
    /// </summary>
    [ProcessNode(Name = "ColorSpectrum")]
    public partial class ColorSpectrumWrapper : ControlNodeBase<ColorSpectrum>
    {
        protected ChannelTwoWayBinding<Color> _colorBinding;
        protected ChannelTwoWayBinding<HsvColor> _hsvColorBinding;

        [Fragment]
        public ColorSpectrumWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext)
            : base(nodeContext)
        {
            _colorBinding = new ChannelTwoWayBinding<Color>(_output, ColorSpectrum.ColorProperty);
            _hsvColorBinding = new ChannelTwoWayBinding<HsvColor>(
                _output,
                ColorSpectrum.HsvColorProperty
            );
        }

        /// <param name="colorChannel">
        /// Gets or sets the colorChannel
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetColorChannel(IChannel<Color>? colorChannel) =>
            _colorBinding.SetChannel(colorChannel);

        /// <param name="hsvColorChannel">
        /// Gets or sets the hsvColorChannel
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetHsvColorChannel(IChannel<HsvColor> hsvColorChannel) =>
            _hsvColorBinding.SetChannel(hsvColorChannel);

        protected Optional<ColorSpectrumComponents> _components;

        [Fragment(Order = PinOrder.Main)]
        public void SetComponents(
            [Pin(Visibility = Model.PinVisibility.Visible)]
                Optional<ColorSpectrumComponents> components
        )
        {
            if (_components != components)
            {
                if (components.HasValue)
                {
                    _output.SetValue(ColorSpectrum.ComponentsProperty, components.Value);
                }
                else
                {
                    _output.ClearValue(ColorSpectrum.ComponentsProperty);
                }

                _components = components;
            }
        }

        protected Optional<int> _maxHue;

        [Fragment(Order = PinOrder.Main)]
        public void SetMaxHue([Pin(Visibility = Model.PinVisibility.Visible)] Optional<int> maxHue)
        {
            if (_maxHue != maxHue)
            {
                if (maxHue.HasValue)
                    _output.SetValue(ColorSpectrum.MaxHueProperty, maxHue.Value);
                else
                    _output.ClearValue(ColorSpectrum.MaxHueProperty);

                _maxHue = maxHue;
            }
        }

        protected Optional<int> _maxSaturation;

        [Fragment(Order = PinOrder.Main)]
        public void SetMaxSaturation(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<int> maxSaturation
        )
        {
            if (_maxSaturation != maxSaturation)
            {
                if (maxSaturation.HasValue)
                    _output.SetValue(ColorSpectrum.MaxSaturationProperty, maxSaturation.Value);
                else
                    _output.ClearValue(ColorSpectrum.MaxSaturationProperty);

                _maxSaturation = maxSaturation;
            }
        }

        protected Optional<int> _maxValue;

        [Fragment(Order = PinOrder.Main)]
        public void SetMaxValue(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<int> maxValue
        )
        {
            if (_maxValue != maxValue)
            {
                if (maxValue.HasValue)
                    _output.SetValue(ColorSpectrum.MaxValueProperty, maxValue.Value);
                else
                    _output.ClearValue(ColorSpectrum.MaxValueProperty);

                _maxValue = maxValue;
            }
        }

        protected Optional<int> _minHue;

        [Fragment(Order = PinOrder.Main)]
        public void SetMinHue([Pin(Visibility = Model.PinVisibility.Visible)] Optional<int> minHue)
        {
            if (_minHue != minHue)
            {
                if (minHue.HasValue)
                    _output.SetValue(ColorSpectrum.MinHueProperty, minHue.Value);
                else
                    _output.ClearValue(ColorSpectrum.MinHueProperty);

                _minHue = minHue;
            }
        }

        protected Optional<int> _minSaturation;

        [Fragment(Order = PinOrder.Main)]
        public void SetMinSaturation(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<int> minSaturation
        )
        {
            if (_minSaturation != minSaturation)
            {
                if (minSaturation.HasValue)
                    _output.SetValue(ColorSpectrum.MinSaturationProperty, minSaturation.Value);
                else
                    _output.ClearValue(ColorSpectrum.MinSaturationProperty);

                _minSaturation = minSaturation;
            }
        }

        protected Optional<int> _minValue;

        [Fragment(Order = PinOrder.Main)]
        public void SetMinValue(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<int> minValue
        )
        {
            if (_minValue != minValue)
            {
                if (minValue.HasValue)
                    _output.SetValue(ColorSpectrum.MinValueProperty, minValue.Value);
                else
                    _output.ClearValue(ColorSpectrum.MinValueProperty);

                _minValue = minValue;
            }
        }

        protected Optional<ColorSpectrumShape> _shape;

        [Fragment(Order = PinOrder.Main)]
        public void SetColorSpectrumShape(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<ColorSpectrumShape> shape
        )
        {
            if (_shape != shape)
            {
                if (shape.HasValue)
                {
                    _output.SetValue(ColorSpectrum.ShapeProperty, shape.Value);
                }
                else
                {
                    _output.ClearValue(ColorSpectrum.ShapeProperty);
                }

                _shape = shape;
            }
        }

        protected Optional<ColorComponent> _thirdComponent;

        [Fragment(Order = PinOrder.Main)]
        public void SetThirdComponent(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<ColorComponent> thirdComponent
        )
        {
            if (_thirdComponent != thirdComponent)
            {
                if (thirdComponent.HasValue)
                {
                    _output.SetValue(ColorSpectrum.ThirdComponentProperty, thirdComponent.Value);
                }
                else
                {
                    _output.ClearValue(ColorSpectrum.ThirdComponentProperty);
                }

                _thirdComponent = thirdComponent;
            }
        }
    }
}

