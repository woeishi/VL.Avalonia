using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Controls;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Custom.Controls.Value
{
    /// <summary>
    /// ColorHexTextBox
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/colorpicker/">Custom Color Picker</see>
    /// </summary>
    [ProcessNode(Name = "ColorHexTextBox")]
    public partial class ColorHexTextBoxWrapper : ControlNodeBase<ColorHexTextBox>
    {
        protected ChannelTwoWayBinding<HsvColor> _hsvBinding;
        protected ChannelTwoWayBinding<Color> _color;

        [Fragment]
        public ColorHexTextBoxWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext)
            : base(nodeContext)
        {
            _hsvBinding = new ChannelTwoWayBinding<HsvColor>(_output, ColorView.HsvColorProperty);
            _color = new ChannelTwoWayBinding<Color>(_output, ColorView.ColorProperty);
        }

        /// <param name="hsvColorChannel">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetHsvColorChannel(IChannel<HsvColor> hsvColorChannel) =>
            _hsvBinding.SetChannel(hsvColorChannel);

        /// <param name="color">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetColor(IChannel<Color> color) => _color.SetChannel(color);
    }
}

