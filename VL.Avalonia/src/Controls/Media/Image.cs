using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using Path = VL.Lib.IO.Path;

namespace VL.Avalonia.Controls;

/// <summary>
/// Simple unoptimized image loader for Avalonia.
/// The image can display raster images from a specified image source.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/image">Slider</see>
/// </summary>
[ProcessNode(Name = "Image")]
public partial class ImageWrapper : ControlNodeBase<Image>
{
    [Fragment]
    public ImageWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

    protected Optional<Path> _source;

    /// <param name="source">
    /// Sets image source path, and loads it as Avalonia bitmap
    /// </param>
    [Fragment(Order = PinOrder.Main)]
    public void SetSource(Optional<Path> source)
    {
        if (_source != source)
        {
            if (source.HasValue && source.Value.IsFile)
            {
                var bitmap = new Bitmap(source.Value.ToString());

                _output.SetValue(Image.SourceProperty, bitmap);
            }
            else
            {
                _output.ClearValue(Image.SourceProperty);
            }

            _source = source;
        }
    }

    /* NOT IMPLEMENTED
    [ImplementProperty("Image.BlendModeProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<BitmapBlendingMode> _blendMode;
    */

    /// <param name="stretch">
    /// Sets a value controlling how the image will be stretched.
    /// </param>
    [ImplementProperty("Image.StretchProperty ", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<Stretch> _stretch;

    /// <param name="stretch">
    /// Sets a value controlling in what direction the image will be stretched.
    /// </param>
    [ImplementProperty(
        "Image.StretchDirectionProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<StretchDirection> _stretchDirection;
}

