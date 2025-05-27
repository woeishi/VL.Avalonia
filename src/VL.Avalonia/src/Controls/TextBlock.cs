using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;


// TODO: 
// Runs https://github.com/AvaloniaUI/Avalonia/blob/ae0573a789f829d1f5d168e313a79fcbcb9ffc83/src/Avalonia.Controls/TextBlock.cs#L166
// Inline's https://github.com/AvaloniaUI/Avalonia/blob/ae0573a789f829d1f5d168e313a79fcbcb9ffc83/src/Avalonia.Controls/TextBlock.cs#L167
// Suspect the best way to do it is to add TextBlock (Advanced) or TextBlock (Runs)


/// <summary>
/// Simple text block control that displays text.
/// More advanced text styling can be performed using Style properties.
/// </summary>
[ProcessNode(Name = "TextBlock")]
public partial class TextBlockWrapper
{
    [ImplementOutput]
    private TextBlock _output = new TextBlock();

    [ImplementStyle]
    private Optional<IAvaloniaStyle> _style;

    private Optional<string> _text;
    public void SetText(Optional<string> text)
    {
        if (_text != text)
        {
            _text = text;
            _output.SetValue(TextBlock.TextProperty, text.Value);
        }
    }

    private Optional<string> _fontFamily;
    public void SetFontFamily(Optional<string> fontFamily)
    {
        if (_fontFamily != fontFamily)
        {
            _fontFamily = fontFamily;

            if (_fontFamily.HasValue)
                _output.SetValue(TextBlock.FontFamilyProperty, new FontFamily(fontFamily.Value));
        }
    }

    private Optional<int> _fontSize;
    public void SetFontSize(Optional<int> fontSize)
    {
        if (_fontSize != fontSize)
        {
            _fontSize = fontSize;
            _output.SetValue(TextBlock.FontSizeProperty, fontSize.Value);
        }
    }

    private Optional<FontStyle> _fontStyle;
    public void SetFontStyle(Optional<FontStyle> fontStyle)
    {
        if (_fontStyle != fontStyle)
        {
            _fontStyle = fontStyle;
            _output.SetValue(TextBlock.FontStyleProperty, fontStyle.Value);
        }
    }

    private Optional<FontWeight> _fontWeight;
    public void SetFontWeight(Optional<FontWeight> fontWeight)
    {
        if (_fontWeight != fontWeight)
        {
            _fontWeight = fontWeight;
            _output.SetValue(TextBlock.FontWeightProperty, fontWeight.Value);
        }
    }

    private Optional<TextAlignment> _textAlignment;
    public void SetTextAlignment(Optional<TextAlignment> textAlignment)
    {
        if (_textAlignment != textAlignment)
        {
            _textAlignment = textAlignment;
            _output.SetValue(TextBlock.TextAlignmentProperty, textAlignment.Value);
        }
    }
}
