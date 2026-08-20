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
    /// ColorView
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/colorpicker/">ColorView</see>
    /// https://github.com/AvaloniaUI/Avalonia/blob/56dd94c0b9dd8aeacb907676764872705b1986e5/src/Avalonia.Controls.ColorPicker/ColorSlider/ColorSlider.Properties.cs
    /// </summary>
    [ProcessNode(Name = "ColorView")]
    public partial class ColorViewWrapper<T> : ControlNodeBase<T>
        where T : ColorView, new()
    {
        protected ChannelTwoWayBinding<HsvColor> _hsvBinding;
        protected ChannelTwoWayBinding<Color> _color;
        protected ChannelTwoWayBinding<int> _selectedIndex;
        protected ChannelTwoWayBinding<ColorModel> _colorModel;

        [Fragment]
        public ColorViewWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext)
            : base(nodeContext)
        {
            _hsvBinding = new ChannelTwoWayBinding<HsvColor>(_output, ColorView.HsvColorProperty);
            _selectedIndex = new ChannelTwoWayBinding<int>(
                _output,
                ColorView.SelectedIndexProperty
            );
            _color = new ChannelTwoWayBinding<Color>(_output, ColorView.ColorProperty);
            _colorModel = new ChannelTwoWayBinding<ColorModel>(
                _output,
                ColorView.ColorModelProperty
            );
        }

        /// <param name="hsvColorChannel">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetHsvColorChannel(IChannel<HsvColor> hsvColorChannel) =>
            _hsvBinding.SetChannel(hsvColorChannel);

        /// <param name="selectedIndex">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetSelectedIndex(IChannel<int> selectedIndex) =>
            _selectedIndex.SetChannel(selectedIndex);

        /// <param name="color">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetColor(IChannel<Color> color) => _color.SetChannel(color);

        /// <param name="colorModel">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetColorModel(IChannel<ColorModel> colorModel) =>
            _colorModel.SetChannel(colorModel);

        protected Optional<ColorSpectrumComponents> _colorSpectrumComponents;

        [Fragment(Order = PinOrder.Main)]
        public void SetColorSpectrumComponent(
            [Pin(Visibility = Model.PinVisibility.Visible)]
                Optional<ColorSpectrumComponents> colorSpectrumComponents
        )
        {
            if (_colorSpectrumComponents != colorSpectrumComponents)
            {
                if (colorSpectrumComponents.HasValue)
                {
                    _output.SetValue(
                        ColorView.ColorSpectrumComponentsProperty,
                        colorSpectrumComponents.Value
                    );
                }
                else
                {
                    _output.ClearValue(ColorView.ColorSpectrumComponentsProperty);
                }

                _colorSpectrumComponents = colorSpectrumComponents;
            }
        }

        protected Optional<ColorSpectrumShape> _alphaComponentPosition;

        [Fragment(Order = PinOrder.Main)]
        public void SetAlphaComponentPosition(
            [Pin(Visibility = Model.PinVisibility.Visible)]
                Optional<ColorSpectrumShape> alphaComponentPosition
        )
        {
            if (_alphaComponentPosition != alphaComponentPosition)
            {
                if (alphaComponentPosition.HasValue)
                {
                    _output.SetValue(
                        ColorView.ColorSpectrumShapeProperty,
                        alphaComponentPosition.Value
                    );
                }
                else
                {
                    _output.ClearValue(ColorView.ColorSpectrumShapeProperty);
                }

                _alphaComponentPosition = alphaComponentPosition;
            }
        }

        protected Optional<AlphaComponentPosition> _hexInputAlphaPosition;

        [Fragment(Order = PinOrder.Main)]
        public void SetHexInputAlphaPosition(
            [Pin(Visibility = Model.PinVisibility.Visible)]
                Optional<AlphaComponentPosition> hexInputAlphaPosition
        )
        {
            if (_hexInputAlphaPosition != hexInputAlphaPosition)
            {
                if (hexInputAlphaPosition.HasValue)
                {
                    _output.SetValue(
                        ColorView.HexInputAlphaPositionProperty,
                        hexInputAlphaPosition.Value
                    );
                }
                else
                {
                    _output.ClearValue(ColorView.HexInputAlphaPositionProperty);
                }

                _hexInputAlphaPosition = hexInputAlphaPosition;
            }
        }

        protected Optional<bool> _isAccentColorsVisible;

        [Fragment(Order = PinOrder.Main)]
        public void SetIsAccentColorsVisible(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isAccentColorVisible
        )
        {
            if (_isAccentColorsVisible != isAccentColorVisible)
            {
                if (isAccentColorVisible.HasValue)
                {
                    _output.SetValue(
                        ColorView.IsAccentColorsVisibleProperty,
                        isAccentColorVisible.Value
                    );
                }
                else
                {
                    _output.ClearValue(ColorView.IsAccentColorsVisibleProperty);
                }

                _isAccentColorsVisible = isAccentColorVisible;
            }
        }

        protected Optional<bool> _isAlphaVisible;

        [Fragment(Order = PinOrder.Main)]
        public void SetIsAlphaVisible(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isAlphaVisible
        )
        {
            if (_isAlphaVisible != isAlphaVisible)
            {
                if (isAlphaVisible.HasValue)
                {
                    _output.SetValue(ColorView.IsAlphaVisibleProperty, isAlphaVisible.Value);
                }
                else
                {
                    _output.ClearValue(ColorView.IsAlphaVisibleProperty);
                }

                _isAlphaVisible = isAlphaVisible;
            }
        }

        protected Optional<bool> _isColorComponentsVisible;

        [Fragment(Order = PinOrder.Main)]
        public void SetIsColorComponentVisible(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isColorComponentVisible
        )
        {
            if (_isColorComponentsVisible != isColorComponentVisible)
            {
                if (isColorComponentVisible.HasValue)
                {
                    _output.SetValue(
                        ColorView.IsColorComponentsVisibleProperty,
                        isColorComponentVisible.Value
                    );
                }
                else
                {
                    _output.ClearValue(ColorView.IsColorComponentsVisibleProperty);
                }

                _isColorComponentsVisible = isColorComponentVisible;
            }
        }

        protected Optional<bool> _isColorModelVisible;

        [Fragment(Order = PinOrder.Main)]
        public void SetIsColorModelVisible(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isColorPreviewVisible
        )
        {
            if (_isColorModelVisible != isColorPreviewVisible)
            {
                if (isColorPreviewVisible.HasValue)
                {
                    _output.SetValue(
                        ColorView.IsColorModelVisibleProperty,
                        isColorPreviewVisible.Value
                    );
                }
                else
                {
                    _output.ClearValue(ColorView.IsColorModelVisibleProperty);
                }

                _isColorModelVisible = isColorPreviewVisible;
            }
        }

        protected Optional<bool> _isColorPreviewVisible;

        [Fragment(Order = PinOrder.Main)]
        public void SetIsColorPreviewVisible(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isColorPreviewVisible
        )
        {
            if (_isColorPreviewVisible != isColorPreviewVisible)
            {
                if (isColorPreviewVisible.HasValue)
                {
                    _output.SetValue(
                        ColorView.IsColorPreviewVisibleProperty,
                        isColorPreviewVisible.Value
                    );
                }
                else
                {
                    _output.ClearValue(ColorView.IsColorPreviewVisibleProperty);
                }

                _isColorPreviewVisible = isColorPreviewVisible;
            }
        }

        protected Optional<bool> _isColorSpectrumVisible;

        [Fragment(Order = PinOrder.Main)]
        public void SetIsColorSpectrumVisible(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isColorSpectrumVisible
        )
        {
            if (_isColorSpectrumVisible != isColorSpectrumVisible)
            {
                if (isColorSpectrumVisible.HasValue)
                {
                    _output.SetValue(
                        ColorView.IsColorSpectrumVisibleProperty,
                        isColorSpectrumVisible.Value
                    );
                }
                else
                {
                    _output.ClearValue(ColorView.IsColorSpectrumVisibleProperty);
                }

                _isColorSpectrumVisible = isColorSpectrumVisible;
            }
        }

        protected Optional<bool> _isColorSpectrumSliderVisible;

        [Fragment(Order = PinOrder.Main)]
        public void SetIsColorSpectrumSliderVisible(
            [Pin(Visibility = Model.PinVisibility.Visible)]
                Optional<bool> isColorSpectrumSliderVisible
        )
        {
            if (_isColorSpectrumSliderVisible != isColorSpectrumSliderVisible)
            {
                if (isColorSpectrumSliderVisible.HasValue)
                    _output.SetValue(
                        ColorView.IsColorSpectrumSliderVisibleProperty,
                        isColorSpectrumSliderVisible.Value
                    );
                else
                    _output.ClearValue(ColorView.IsColorSpectrumSliderVisibleProperty);

                _isColorSpectrumSliderVisible = isColorSpectrumSliderVisible;
            }
        }

        protected Optional<bool> _isComponentSliderVisible;

        [Fragment(Order = PinOrder.Main)]
        public void SetIsComponentSliderVisible(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isComponentSliderVisible
        )
        {
            if (_isComponentSliderVisible != isComponentSliderVisible)
            {
                if (isComponentSliderVisible.HasValue)
                    _output.SetValue(
                        ColorView.IsComponentSliderVisibleProperty,
                        isComponentSliderVisible.Value
                    );
                else
                    _output.ClearValue(ColorView.IsComponentSliderVisibleProperty);

                _isComponentSliderVisible = isComponentSliderVisible;
            }
        }

        protected Optional<bool> _isComponentTextInputVisible;

        [Fragment(Order = PinOrder.Main)]
        public void SetIsComponentTextInputVisible(
            [Pin(Visibility = Model.PinVisibility.Visible)]
                Optional<bool> isComponentTextInputVisible
        )
        {
            if (_isComponentTextInputVisible != isComponentTextInputVisible)
            {
                if (isComponentTextInputVisible.HasValue)
                    _output.SetValue(
                        ColorView.IsComponentTextInputVisibleProperty,
                        isComponentTextInputVisible.Value
                    );
                else
                    _output.ClearValue(ColorView.IsComponentTextInputVisibleProperty);

                _isComponentTextInputVisible = isComponentTextInputVisible;
            }
        }

        protected Optional<bool> _isHexInputVisible;

        [Fragment(Order = PinOrder.Main)]
        public void SetIsHexInputVisible(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isHexInputVisible
        )
        {
            if (_isHexInputVisible != isHexInputVisible)
            {
                if (isHexInputVisible.HasValue)
                    _output.SetValue(ColorView.IsHexInputVisibleProperty, isHexInputVisible.Value);
                else
                    _output.ClearValue(ColorView.IsHexInputVisibleProperty);

                _isHexInputVisible = isHexInputVisible;
            }
        }

        protected Optional<int> _maxHue;

        [Fragment(Order = PinOrder.Main)]
        public void SetMaxHue([Pin(Visibility = Model.PinVisibility.Visible)] Optional<int> maxHue)
        {
            if (_maxHue != maxHue)
            {
                if (maxHue.HasValue)
                    _output.SetValue(ColorView.MaxHueProperty, maxHue.Value);
                else
                    _output.ClearValue(ColorView.MaxHueProperty);

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
                    _output.SetValue(ColorView.MaxSaturationProperty, maxSaturation.Value);
                else
                    _output.ClearValue(ColorView.MaxSaturationProperty);

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
                    _output.SetValue(ColorView.MaxValueProperty, maxValue.Value);
                else
                    _output.ClearValue(ColorView.MaxValueProperty);

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
                    _output.SetValue(ColorView.MinHueProperty, minHue.Value);
                else
                    _output.ClearValue(ColorView.MinHueProperty);

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
                    _output.SetValue(ColorView.MinSaturationProperty, minSaturation.Value);
                else
                    _output.ClearValue(ColorView.MinSaturationProperty);

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
                    _output.SetValue(ColorView.MinValueProperty, minValue.Value);
                else
                    _output.ClearValue(ColorView.MinValueProperty);

                _minValue = minValue;
            }
        }

        protected Optional<IEnumerable<Color>?> _palleteColors;

        [Fragment(Order = PinOrder.Main)]
        public void SetPaletteColors(
            [Pin(Visibility = Model.PinVisibility.Visible)]
                Optional<IEnumerable<Color>?> paletteColors
        )
        {
            if (_palleteColors != paletteColors)
            {
                if (paletteColors.HasValue)
                    _output.SetValue(ColorView.PaletteColorsProperty, paletteColors.Value);
                else
                    _output.ClearValue(ColorView.PaletteColorsProperty);

                _palleteColors = paletteColors;
            }
        }

        protected Optional<int> _paletteColumnCount;

        [Fragment(Order = PinOrder.Main)]
        public void SetPaletteColumnCount(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<int> paletteColumnCount
        )
        {
            if (_paletteColumnCount != paletteColumnCount)
            {
                if (paletteColumnCount.HasValue)
                    _output.SetValue(ColorView.MinValueProperty, paletteColumnCount.Value);
                else
                    _output.ClearValue(ColorView.MinValueProperty);

                _paletteColumnCount = paletteColumnCount;
            }
        }

        protected Optional<IColorPalette> _pallette;

        [Fragment(Order = PinOrder.Main)]
        public void SetPalette(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<IColorPalette> palette
        )
        {
            if (_pallette != palette)
            {
                if (palette.HasValue)
                    _output.SetValue(ColorView.PaletteProperty, palette.Value);
                else
                    _output.ClearValue(ColorView.PaletteProperty);

                _pallette = palette;
            }
        }
    }
}

