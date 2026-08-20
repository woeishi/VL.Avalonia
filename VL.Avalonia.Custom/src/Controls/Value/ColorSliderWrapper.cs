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
    /// ColorSlider
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/colorpicker/">ColorSlider</see>
    /// </summary>
    [ProcessNode(Name = "ColorSlider")]
    public partial class ColorSliderWrapper : RangeBaseNodeBase<ColorSlider>
    {
        [Fragment]
        public ColorSliderWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            _hsvColorBinding = new ChannelTwoWayBinding<HsvColor>(
                _output,
                ColorSlider.HsvColorProperty
            );
            _colorChannel = new ChannelTwoWayBinding<Color>(_output, ColorSlider.ColorProperty);
        }

        protected ChannelTwoWayBinding<HsvColor> _hsvColorBinding;

        /// <param name="hsvColorChannel">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetHsvColorChannel(IChannel<HsvColor> hsvColorChannel) =>
            _hsvColorBinding.SetChannel(hsvColorChannel);

        protected ChannelTwoWayBinding<Color> _colorChannel;

        /// <param name="colorChannel">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetColorChannel(IChannel<Color> colorChannel) =>
            _colorChannel.SetChannel(colorChannel);

        protected Optional<ColorComponent> _colorComponent;

        [Fragment(Order = PinOrder.Main)]
        public void SetColorComponent(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<ColorComponent> colorComponent
        )
        {
            if (_colorComponent != colorComponent)
            {
                if (colorComponent.HasValue)
                {
                    _output.SetValue(ColorSlider.ColorComponentProperty, colorComponent.Value);
                }
                else
                {
                    _output.ClearValue(ColorSlider.ColorComponentProperty);
                }

                _colorComponent = colorComponent;
            }
        }

        protected Optional<ColorModel> _colorModel;

        [Fragment(Order = PinOrder.Main)]
        public void SetColorModel(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<ColorModel> colorModel
        )
        {
            if (_colorModel != colorModel)
            {
                if (colorModel.HasValue)
                {
                    _output.SetValue(ColorSlider.ColorModelProperty, colorModel.Value);
                }
                else
                {
                    _output.ClearValue(ColorSlider.ColorModelProperty);
                }

                _colorModel = colorModel;
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
                    _output.SetValue(ColorSlider.IsAlphaVisibleProperty, isAlphaVisible.Value);
                }
                else
                {
                    _output.ClearValue(ColorSlider.IsAlphaVisibleProperty);
                }

                _isAlphaVisible = isAlphaVisible;
            }
        }

        protected Optional<bool> _isPerspective;

        [Fragment(Order = PinOrder.Main)]
        public void SetIsPerspective(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isPerspective
        )
        {
            if (_isPerspective != isPerspective)
            {
                if (isPerspective.HasValue)
                {
                    _output.SetValue(ColorSlider.IsPerceptiveProperty, isPerspective.Value);
                }
                else
                {
                    _output.ClearValue(ColorSlider.IsPerceptiveProperty);
                }

                _isPerspective = isPerspective;
            }
        }

        protected Optional<bool> _isRoundingEnable;

        [Fragment(Order = PinOrder.Main)]
        public void SetIsRoundingEnable(
            [Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isRoundingEnable
        )
        {
            if (_isRoundingEnable != isRoundingEnable)
            {
                if (isRoundingEnable.HasValue)
                {
                    _output.SetValue(ColorSlider.IsRoundingEnabledProperty, isRoundingEnable.Value);
                }
                else
                {
                    _output.ClearValue(ColorSlider.IsRoundingEnabledProperty);
                }

                _isRoundingEnable = isRoundingEnable;
            }
        }
    }
}
