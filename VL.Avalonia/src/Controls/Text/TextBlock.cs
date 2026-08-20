using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

// TODO: RichTextBox

/// <summary>
/// Base class for controls inherited from TextBlock
/// </summary>
/// <typeparam name="T"></typeparam>
[ProcessNode()]
public abstract partial class TextBlockWrapperBase<T> : ControlNodeBase<T>, IDisposable
    where T : TextBlock, new()
{
    private readonly ChannelOptionalBinding<string?> _textBinding;

    [Fragment]
    public TextBlockWrapperBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext)
        : base(nodeContext)
    {
        _textBinding = new ChannelOptionalBinding<string?>(_output, TextBlock.TextProperty);
    }

    /// <param name="text">
    /// The text content displayed by the TextBlock
    /// </param>
    [Fragment(Order = PinOrder.Main)]
    public void SetText(Optional<string> text)
    {
        _textBinding.SetValue(text);
    }

    /// <param name="textChannel">
    /// Binds TextBlock text source to the provided channel
    /// </param>
    [Fragment(Order = PinOrder.Main)]
    public void SetTextChannel(
        [Pin(Visibility = Model.PinVisibility.Optional)] IChannel<string?> textChannel
    )
    {
        _textBinding.SetChannel(textChannel);
    }

    #region Font Properties

    /// <param name="fontFamily">
    /// The font family used to draw the text
    /// </param>
    [ImplementProperty(
        "TextBlock.FontFamilyProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<FontFamily> _fontFamily;

    /// <param name="fontSize">
    /// The size of the text in points
    /// </param>
    [ImplementProperty("TextBlock.FontSizeProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _fontSize;

    /// <param name="fontWeight">
    /// The font weight (normal, bold, etc.)
    /// </param>
    [ImplementProperty(
        "TextBlock.FontWeightProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<FontWeight> _fontWeight;

    /// <param name="fontStyle">
    /// The font style (normal, italic, oblique)
    /// </param>
    [ImplementProperty("TextBlock.FontStyleProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<FontStyle> _fontStyle;

    /// <param name="fontStretch">
    /// The font stretch (normal, condensed, expanded, etc.)
    /// </param>
    [ImplementProperty(
        "TextBlock.FontStretchProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<FontStretch> _fontStretch;

    /* TODO:
    /// <summary>
    /// Text decorations like underline, strikethrough, etc.
    /// </summary>
    [ImplementProperty("TextBlock.TextDecorationsProperty")]
    protected Optional<TextDecorationCollection> _textDecorations;
    */

    #endregion
    #region Text Layout Properties

    /// <param name="textAlignment">
    /// How text is aligned horizontally (Left, Center, Right, Justify, Start, End)
    /// </param>
    [ImplementProperty(
        "TextBlock.TextAlignmentProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<TextAlignment> _textAlignment;

    /// <param name="textWrapping">
    /// How text wraps when it exceeds the available width
    /// </param>
    [ImplementProperty(
        "TextBlock.TextWrappingProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<TextWrapping> _textWrapping;

    /* TODO:
    /// <summary>
    /// How text is trimmed when it exceeds available space
    /// </summary>
    [ImplementProperty("TextBlock.TextTrimmingProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<TextTrimming> _textTrimming;
    */

    /// <param name="maxLines">
    /// Maximum number of text lines to display
    /// </param>
    [ImplementProperty("TextBlock.MaxLinesProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _maxLines;

    #endregion
    #region Spacing and Layout Properties

    /// <param name="lineHeight">
    /// Height of each line of text (NaN for automatic)
    /// </param>
    [ImplementProperty(
        "TextBlock.LineHeightProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<float> _lineHeight;

    /// <param name="lineSpacing">
    /// Extra vertical spacing between lines
    /// </param>
    [ImplementProperty(
        "TextBlock.LineSpacingProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<float> _lineSpacing;

    /// <param name="letterSpacing">
    /// Spacing between individual characters
    /// </param>
    [ImplementProperty(
        "TextBlock.LetterSpacingProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<float> _letterSpacing;

    #endregion
    #region Advanced Typography Properties

    /* TODO
    /// <summary>
    /// OpenType font features for advanced typography
    /// </summary>
    [ImplementProperty("TextBlock.FontFeaturesProperty")]
    protected Optional<FontFeatureCollection> _fontFeatures;
    */

    /// <param name="baselineOffset">
    /// Baseline offset adjustment for vertical text positioning
    /// </param>
    [ImplementProperty(
        "TextBlock.BaselineOffsetProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<float> _baselineOffset;

    #endregion

    [Fragment(IsHidden = true)]
    public void Dispose()
    {
        _textBinding?.Dispose();
    }
}

/// <summary>
/// Simple text block control that displays text.
/// More advanced text styling can be performed using Style properties.
/// </summary>
[ProcessNode(Name = "TextBlock")]
public partial class TextBlockWrapper : TextBlockWrapperBase<TextBlock>
{
    [Fragment]
    public TextBlockWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] VL.Core.NodeContext nodeContext) : base(nodeContext) { }
}

