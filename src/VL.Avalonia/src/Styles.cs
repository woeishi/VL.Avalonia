using Avalonia.Layout;
using Avalonia.Media;
using VL.Lib.Collections;

namespace VL.Avalonia
{
    public partial class Styles
    {
        #region Style Factory Base
        public interface IAvaloniaStyle
        {
            void ApplyStyle(object owner);
        }
        #endregion

        #region Style Implementations
        public record struct StyleOrientation(IAvaloniaStyle? Style, Orientation Orientation) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                var property = owner.GetType().GetProperty("Orientation");

                if (property != null)
                {
                    property.SetValue(owner, Orientation);
                }

                Style?.ApplyStyle(owner);
            }
        }


        public record struct StyleBackground(IAvaloniaStyle? Style, IBrush? Brush) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("Background")?.SetValue(owner, Brush);

                Style?.ApplyStyle(owner);
            }
        }

        public record struct StyleForeground(IAvaloniaStyle? Style, IBrush? Brush) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("Foreground")?.SetValue(owner, Brush);

                Style?.ApplyStyle(owner);
            }
        }
        #region StyleStroke
        public record struct StyleStroke(IAvaloniaStyle? Style, IBrush? Brush) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("Stroke")?.SetValue(owner, Brush);
                Style?.ApplyStyle(owner);
            }
        }

        public record struct StyleStrokeDashArray(IAvaloniaStyle? Style, Spread<float> DashArray) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("StrokeDashArray")?.SetValue(owner, DashArray.Select(x => (double)x).ToArray());
                Style?.ApplyStyle(owner);
            }
        }

        public record struct StyleStrokeDashOffset(IAvaloniaStyle? Style, float DashOffset) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("StrokeDashOffset")?.SetValue(owner, (double)DashOffset);
                Style?.ApplyStyle(owner);
            }
        }

        public record struct StyleStrokeJoint(IAvaloniaStyle? Style, PenLineJoin? Join) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("StrokeLineJoin")?.SetValue(owner, Join);
                Style?.ApplyStyle(owner);
            }
        }

        public record struct StyleStrokeCap(IAvaloniaStyle? Style, PenLineCap? Cap) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("StrokeLineCap")?.SetValue(owner, Cap);
                Style?.ApplyStyle(owner);
            }
        }

        public record struct StyleStrokeThickness(IAvaloniaStyle? Style, float Thickness) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("StrokeThickness")?.SetValue(owner, (double)Thickness);
                Style?.ApplyStyle(owner);
            }
        }

        public record struct StyleSpacing(IAvaloniaStyle? Style, float Spacing) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("Spacing")?.SetValue(owner, (double)Spacing);
                Style?.ApplyStyle(owner);
            }
        }
        #endregion

        public record struct StyleHorizontalAlignment(IAvaloniaStyle? Style, HorizontalAlignment Alignment) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("HorizontalAlignment")?.SetValue(owner, Alignment);
                Style?.ApplyStyle(owner);
            }
        }

        public record struct StyleVerticalAlignment(IAvaloniaStyle? Style, VerticalAlignment Alignment) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("VerticalAlignment")?.SetValue(owner, Alignment);
                Style?.ApplyStyle(owner);
            }
        }

        /// <summary>
        /// Represents a horizontal content alignment style that combines a specific alignment value  with an optional
        /// underlying style.
        /// </summary>
        /// <remarks>This type allows you to specify a horizontal alignment for content while optionally
        /// applying  additional styling through an <see cref="IAvaloniaStyle"/> instance. It is commonly used to 
        /// configure UI elements that support horizontal content alignment.</remarks>
        /// <param name="Style"></param>
        /// <param name="Alignment"></param>
        public record struct StyleHorizontalContentAlignment(IAvaloniaStyle? Style, HorizontalAlignment Alignment) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("HorizontalContentAlignment")?.SetValue(owner, Alignment);
                Style?.ApplyStyle(owner);
            }
        }

        /// <summary>
        /// Represents a vertical content alignment style that combines a specific vertical alignment  with an optional
        /// Avalonia style.
        /// </summary>
        /// <remarks>This type is used to apply a vertical alignment to the
        /// <c>VerticalContentAlignment</c> property  of an object, while optionally applying additional styling through
        /// the provided Avalonia style.</remarks>
        /// <param name="Style">An optional Avalonia style to apply. Can be <see langword="null"/> if no additional styling is required.</param>
        /// <param name="Alignment">The vertical alignment to apply to the object's <c>VerticalContentAlignment</c> property.</param>
        public record struct StyleVerticalContentAlignment(IAvaloniaStyle? Style, VerticalAlignment Alignment) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("VerticalContentAlignment")?.SetValue(owner, Alignment);
                Style?.ApplyStyle(owner);
            }
        }

        public record struct StyleWidth(IAvaloniaStyle? Style, float Width) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("Width")?.SetValue(owner, Width);
                Style?.ApplyStyle(owner);
            }
        }

        public record struct StyleHeight(IAvaloniaStyle? Style, float Height) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("Height")?.SetValue(owner, Height);
                Style?.ApplyStyle(owner);
            }
        }

        #region FontStyles

        /// <summary>
        /// Represents a font family style that can be applied to an object, optionally combining it with another style.
        /// </summary>
        /// <remarks>This type encapsulates a font family name and an optional style, allowing for the
        /// application of font-related styling to an object. If a secondary style is provided, it will be applied after
        /// the font family is set.</remarks>
        /// <param name="Style"></param>
        /// <param name="FontFamily"></param>
        public record struct StyleFontFamily(IAvaloniaStyle? Style, string FontFamily) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("FontFamily")?.SetValue(owner, new FontFamily(FontFamily));
                Style?.ApplyStyle(owner);
            }
        }

        /// <summary>
        /// Represents a font size style that can be applied to an object, optionally combined with another style.
        /// </summary>
        /// <remarks>This type encapsulates a font size value and an optional nested style. When applied,
        /// it sets the  "FontSize" property of the target object to the specified font size and applies the nested
        /// style,  if provided.</remarks>
        /// <param name="Style"></param>
        /// <param name="FontSize"></param>
        public record struct StyleFontSize(IAvaloniaStyle? Style, float FontSize) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("FontSize")?.SetValue(owner, (double)FontSize);
                Style?.ApplyStyle(owner);
            }
        }

        /// <summary>
        /// Represents a combination of a style and a font style that can be applied to an object.
        /// </summary>
        /// <remarks>This type encapsulates a font style and an optional Avalonia style, allowing both to
        /// be applied  to an object. The font style is applied directly to the object's "FontStyle" property, if it
        /// exists,  while the optional Avalonia style is applied using its own logic.</remarks>
        /// <param name="Style"></param>
        /// <param name="FontStyle"></param>
        public record struct StyleFontStyle(IAvaloniaStyle? Style, FontStyle FontStyle) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("FontStyle")?.SetValue(owner, FontStyle);
                Style?.ApplyStyle(owner);
            }
        }

        /// <summary>
        /// Represents a font weight style that can be applied to an object, optionally combining it with another style.
        /// </summary>
        /// <remarks>This type encapsulates a specific <see cref="FontWeight"/> value and an optional
        /// nested style. When applied, it sets the <c>FontWeight</c> property of the target object and applies the
        /// nested style, if provided.</remarks>
        /// <param name="Style"></param>
        /// <param name="FontWeight"></param>
        public record struct StyleFontWeight(IAvaloniaStyle? Style, FontWeight FontWeight) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("FontWeight")?.SetValue(owner, FontWeight);
                Style?.ApplyStyle(owner);
            }
        }
        /// <summary>
        /// Represents a style that applies a specific <see cref="FontStretch"/> value to an object, optionally
        /// combining it with another style.
        /// </summary>
        /// <remarks>This record struct is used to encapsulate a <see cref="FontStretch"/> value and an
        /// optional nested style (<see cref="IAvaloniaStyle"/>). When applied, it sets the <c>FontStretch</c> property
        /// of the target object and applies the nested style, if provided.</remarks>
        /// <param name="Style"></param>
        /// <param name="FontStretch"></param>
        public record struct StyleFontStretch(IAvaloniaStyle? Style, FontStretch FontStretch) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("FontStretch")?.SetValue(owner, FontStretch);
                Style?.ApplyStyle(owner);
            }
        }

        /// <summary>
        /// Represents a style that applies a baseline offset to an object, along with an optional nested style.
        /// </summary>
        /// <remarks>This record struct combines a baseline offset value with an optional style. When
        /// applied, it sets the baseline offset property of the target object and applies the nested style, if
        /// provided.</remarks>
        /// <param name="Style">An optional style to apply after setting the baseline offset. Can be <see langword="null"/>.</param>
        /// <param name="BaselineOffset">The baseline offset value to apply to the target object.</param>
        public record struct StyleBaselineOffset(IAvaloniaStyle? Style, float BaselineOffset) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("BaselineOffset")?.SetValue(owner, (double)BaselineOffset);
                Style?.ApplyStyle(owner);
            }
        }

        /// <summary>
        /// Represents a style that applies a specific line height to an object, optionally combining it with another
        /// style.
        /// </summary>
        /// <remarks>This struct is used to set the line height of an object and optionally apply
        /// additional styling through  a nested <see cref="IAvaloniaStyle"/>. The line height is applied directly to
        /// the object's "LineHeight" property  if it exists.</remarks>
        /// <param name="Style">An optional nested style to apply after setting the line height. Can be <see langword="null"/>.</param>
        /// <param name="LineHeight">The line height value to apply to the object's "LineHeight" property.</param>
        public record struct StyleLineHeight(IAvaloniaStyle? Style, float LineHeight) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("LineHeight")?.SetValue(owner, (double)LineHeight);
                Style?.ApplyStyle(owner);
            }
        }
        /// <summary>
        /// Represents a style that applies a specific line spacing value to an object,  optionally combining it with
        /// another style.
        /// </summary>
        /// <remarks>This struct is used to define and apply a line spacing style to an object.  It can be
        /// combined with an optional base style (<see cref="IAvaloniaStyle"/>)  to apply additional styling. The line
        /// spacing value is applied to the  "LineSpacing" property of the target object, if such a property
        /// exists.</remarks>
        /// <param name="Style"></param>
        /// <param name="LineSpacing"></param>
        public record struct StyleLineSpacing(IAvaloniaStyle? Style, float LineSpacing) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("LineSpacing")?.SetValue(owner, (double)LineSpacing);
                Style?.ApplyStyle(owner);
            }
        }

        /// <summary>
        /// Represents a style that applies letter spacing to an object, with optional cascading of another style.
        /// </summary>
        /// <remarks>This struct encapsulates a letter spacing value and an optional nested style. When
        /// applied, it sets the  "LetterSpacing" property of the target object and optionally applies the nested
        /// style.</remarks>
        /// <param name="Style"></param>
        /// <param name="LetterSpacing"></param>
        public record struct StyleLetterSpacing(IAvaloniaStyle? Style, float LetterSpacing) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("LetterSpacing")?.SetValue(owner, (double)LetterSpacing);
                Style?.ApplyStyle(owner);
            }
        }

        /// <summary>
        /// Represents a style configuration that applies a maximum number of lines to an object,  optionally combining
        /// with another style.
        /// </summary>
        /// <remarks>This record struct is used to enforce a maximum line count on an object while
        /// optionally  applying additional styling through the provided <see cref="IAvaloniaStyle"/>.</remarks>
        /// <param name="Style">An optional style to apply in addition to the maximum line count. Can be <see langword="null"/>.</param>
        /// <param name="MaxLines">The maximum number of lines to enforce on the styled object.</param>
        public record struct StyleMaxLines(IAvaloniaStyle? Style, int MaxLines) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("MaxLines")?.SetValue(owner, MaxLines);
                Style?.ApplyStyle(owner);
            }
        }

        /// <summary>
        /// Represents a combination of a style and text alignment that can be applied to an object.
        /// </summary>
        /// <remarks>This record struct encapsulates a style and a text alignment value, allowing both to
        /// be applied  to an object. The <see cref="Style"/> property represents an optional style to apply, while 
        /// <see cref="TextAlignment"/> specifies the alignment of text.</remarks>
        /// <param name="Style"></param>
        /// <param name="TextAlignment"></param>
        public record struct StyleTextAlignment(IAvaloniaStyle? Style, TextAlignment TextAlignment) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("TextAlignment")?.SetValue(owner, TextAlignment);
                Style?.ApplyStyle(owner);
            }
        }

        /// <summary>
        /// Represents a combination of a style and text wrapping behavior that can be applied to an object.
        /// </summary>
        /// <remarks>This record struct encapsulates a style and a text wrapping configuration, allowing
        /// both to be applied  to an object. The <see cref="Style"/> property specifies the optional style to apply,
        /// while the  <see cref="TextWrapping"/> property defines the text wrapping behavior.</remarks>
        /// <param name="Style"></param>
        /// <param name="TextWrapping"></param>
        public record struct StyleTextWrapping(IAvaloniaStyle? Style, TextWrapping TextWrapping) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("TextWrapping")?.SetValue(owner, TextWrapping);
                Style?.ApplyStyle(owner);
            }
        }

        // TODDO: IMPLEMENT
        //public record struct StyleTextTrimming(IAvaloniaStyle? Style, TextTrimming TextTrimming) : IAvaloniaStyle
        //{
        //    public void ApplyStyle(object owner)
        //    {
        //        owner.GetType().GetProperty("TextTrimming")?.SetValue(owner, TextTrimming);
        //        Style?.ApplyStyle(owner);
        //    }
        //}
        // TODDO: IMPLEMENT
        //public record struct StyleTextDecorations(IAvaloniaStyle? Style, TextDecorationCollection? TextDecorations) : IAvaloniaStyle
        //{
        //    public void ApplyStyle(object owner)
        //    {
        //        owner.GetType().GetProperty("TextDecorations")?.SetValue(owner, TextDecorations);
        //        Style?.ApplyStyle(owner);
        //    }
        //}

        // TODDO: IMPLEMENT
        //public record struct StyleFontFeatures(IAvaloniaStyle? Style, FontFeatureCollection? FontFeatures) : IAvaloniaStyle
        //{
        //    public void ApplyStyle(object owner)
        //    {
        //        owner.GetType().GetProperty("FontFeature")?.SetValue(owner, FontFeatures);
        //        Style?.ApplyStyle(owner);
        //    }
        //}

        #endregion

        #endregion

        // TOODO: Move this to codegen
        #region StyleNodes

        public static StyleOrientation SetOrientation(IAvaloniaStyle? style, Orientation orientation) =>
             new StyleOrientation(style, orientation);

        public static StyleBackground SetBackground(IAvaloniaStyle? style, IBrush? brush) =>
            new StyleBackground(style, brush);

        public static StyleForeground SetForeground(IAvaloniaStyle? style, IBrush? brush) =>
    new StyleForeground(style, brush);

        public static StyleSpacing SetSpacing(IAvaloniaStyle? style, float spacing) =>
             new StyleSpacing(style, spacing);

        public static StyleHorizontalAlignment SetHorizontalAlignment(IAvaloniaStyle? style, HorizontalAlignment alignment) =>
            new StyleHorizontalAlignment(style, alignment);

        public static StyleVerticalAlignment SetVerticalAlignment(IAvaloniaStyle? style, VerticalAlignment alignment) =>
            new StyleVerticalAlignment(style, alignment);

        /// <inheritdoc cref="StyleHorizontalContentAlignment"/>
        public static StyleHorizontalContentAlignment SetHorizontalContentAlignment(IAvaloniaStyle? style, HorizontalAlignment alignment) =>
            new StyleHorizontalContentAlignment(style, alignment);

        /// <inheritdoc cref="StyleVerticalContentAlignment"/>
        public static StyleVerticalContentAlignment SetVerticalContentAlignment(IAvaloniaStyle? style, VerticalAlignment alignment) =>
            new StyleVerticalContentAlignment(style, alignment);


        public static StyleWidth SetWidth(IAvaloniaStyle? style, float width) =>
            new StyleWidth(style, width);

        public static StyleHeight SetHeight(IAvaloniaStyle? style, float height) =>
         new StyleHeight(style, height);
        public static StyleStroke SetStroke(IAvaloniaStyle? style, IBrush? brush) =>
           new StyleStroke(style, brush);

        public static StyleStrokeDashArray SetStrokeDashArray(IAvaloniaStyle? style, Spread<float> dashArray) =>
           new StyleStrokeDashArray(style, dashArray);

        public static StyleStrokeDashOffset SetStrokeDashOffset(IAvaloniaStyle? style, float dashOffset) =>
         new StyleStrokeDashOffset(style, dashOffset);

        public static StyleStrokeJoint SetStrokeJoint(IAvaloniaStyle? style, PenLineJoin? join) =>
            new StyleStrokeJoint(style, join);


        public static StyleStrokeCap SetStrokeCap(IAvaloniaStyle? style, PenLineCap? cap) =>
            new StyleStrokeCap(style, cap);
        public static StyleStrokeThickness SetStrokeThickness(IAvaloniaStyle? style, float thickness) =>
            new StyleStrokeThickness(style, thickness);


        /// <inheritdoc cref="StyleFontFamily"/>
        public static StyleFontFamily SetFontFamily(IAvaloniaStyle? style, FontFamily fontFamily) =>
            new StyleFontFamily(style, fontFamily.Name);

        /// <inheritdoc cref="StyleFontSize"/>
        public static StyleFontSize SetFontSize(IAvaloniaStyle? style, float fontSize) =>
            new StyleFontSize(style, fontSize);

        /// <inheritdoc cref="StyleFontStyle"/>
        public static StyleBaselineOffset SetBaselineOffset(IAvaloniaStyle? style, float baselineOffset) =>
            new StyleBaselineOffset(style, baselineOffset);

        /// <inheritdoc cref="StyleFontWeight"/>
        public static StyleLineHeight SetLineHeight(IAvaloniaStyle? style, float lineHeight) =>
            new StyleLineHeight(style, lineHeight);

        /// <inheritdoc cref="StyleLineSpacing"/>
        public static StyleLineSpacing SetLineSpacing(IAvaloniaStyle? style, float lineSpacing) =>
            new StyleLineSpacing(style, lineSpacing);

        /// <inheritdoc cref="StyleLetterSpacing"/>
        public static StyleLetterSpacing SetLetterSpacing(IAvaloniaStyle? style, float letterSpacing) =>
            new StyleLetterSpacing(style, letterSpacing);

        /// <inheritdoc cref="StyleMaxLines"/>
        public static StyleMaxLines SetMaxLines(IAvaloniaStyle? style, int maxLines) =>
            new StyleMaxLines(style, maxLines);

        /// <inheritdoc cref="StyleTextAlignment"/>
        public static StyleTextAlignment SetTextAlignment(IAvaloniaStyle? style, TextAlignment textAlignment) =>
            new StyleTextAlignment(style, textAlignment);

        /// <inheritdoc cref="StyleTextWrapping"/>
        public static StyleTextWrapping SetTextWrapping(IAvaloniaStyle? style, TextWrapping textWrapping) =>
            new StyleTextWrapping(style, textWrapping);

        /// <inheritdoc cref="StyleFontFamily"/>
        public static StyleFontFamily SetFontFamily(IAvaloniaStyle? style, string fontFamily) =>
            new StyleFontFamily(style, fontFamily);

        /// <inheritdoc cref="StyleFontStyle"/>
        public static StyleFontStyle SetFontStyle(IAvaloniaStyle? style, FontStyle fontStyle) =>
            new StyleFontStyle(style, fontStyle);

        /// <inheritdoc cref="StyleFontWeight"/>
        public static StyleFontWeight SetFontWeight(IAvaloniaStyle? style, FontWeight fontWeight) =>
            new StyleFontWeight(style, fontWeight);

        // TODO: ENUMS bug, when not initially set
        /// <inheritdoc cref="StyleFontStretch"/>
        public static StyleFontStretch SetFontStretch(IAvaloniaStyle? style, FontStretch fontStretch) =>
            new StyleFontStretch(style, fontStretch);



        #endregion
    }
}
