using Avalonia.Controls.Primitives;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Avalonia.Controls;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Custom.Controls.Value
{
    /// <summary>
    /// Custom Colorpicker
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/colorpicker/">Custom Color Picker</see>
    /// </summary>
    [ProcessNode(Name = "ColorPreviewer")]
    public partial class ColorPreviewerWrapper : ControlNodeBase<ColorPreviewer>
    {
        protected ChannelTwoWayBinding<HsvColor, HsvColor> _hsvBinding;

        [Fragment]
        public ColorPreviewerWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext)
            : base(nodeContext)
        {
            _hsvBinding = new ChannelTwoWayBinding<HsvColor, HsvColor>(
                _output,
                ColorPreviewer.HsvColorProperty,
                (x) => x,
                (x) => x
            );
        }

        /// <param name="hsvColorChannel">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetHsvColorChannel(IChannel<HsvColor> hsvColorChannel) =>
            _hsvBinding.SetChannel(hsvColorChannel);

        [ImplementProperty("ColorPreviewer.IsAccentColorsVisibleProperty", Order = PinOrder.Main)]
        protected Optional<bool> _isAccentColorsVisible;
    }
}

