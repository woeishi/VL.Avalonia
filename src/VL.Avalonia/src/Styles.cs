using Avalonia;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;
using Stride.Core.Mathematics;
using VL.Avalonia.Helpers;
using VL.Lib.Collections;

using Color = Avalonia.Media.Color;

namespace VL.Avalonia
{
    public partial class Styles
    {
        #region Style Factory Base
        public interface IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style);
        }

        #endregion

        #region Style Implementations


        public record struct StyleSelector(IAvaloniaStyle? Style, Selector? Selector, IAvaloniaStyle? SelectorStyle) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                if (SelectorStyle != null)
                {
                    var selectorStyle = SelectorStyle.BuildStyle(owner, new Style() { Selector = Selector });
                    style.Add(selectorStyle);
                }

                Style?.BuildStyle(owner, style);

                return style;
            }
        }
        public record struct StyleOrientation(IAvaloniaStyle? Style, Orientation Orientation) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "Orientation", Orientation);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleOrientation SetOrientation(IAvaloniaStyle? style, Orientation orientation) =>
             new StyleOrientation(style, orientation);

        public record struct StyleBackground(IAvaloniaStyle? Style, IBrush? Brush) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "Background", Brush);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleBackground SetBackground(IAvaloniaStyle? style, IBrush? brush) =>
            new StyleBackground(style, brush);

        public record struct StyleForeground(IAvaloniaStyle? Style, IBrush? Brush) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "Foreground", Brush);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleForeground SetForeground(IAvaloniaStyle? style, IBrush? brush) =>
            new StyleForeground(style, brush);

        #region StyleStroke
        public record struct StyleStroke(IAvaloniaStyle? Style, IBrush? Brush) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "Stroke", Brush);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleStroke SetStroke(IAvaloniaStyle? style, IBrush? brush) =>
           new StyleStroke(style, brush);

        public record struct StyleStrokeDashArray(IAvaloniaStyle? Style, Spread<float> DashArray) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "StrokeDashArray", DashArray.Select(x => (double)x).ToArray());
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleStrokeDashArray SetStrokeDashArray(IAvaloniaStyle? style, Spread<float> dashArray) =>
           new StyleStrokeDashArray(style, dashArray);

        public record struct StyleStrokeDashOffset(IAvaloniaStyle? Style, float DashOffset) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "StrokeDashOffset", (double)DashOffset);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleStrokeDashOffset SetStrokeDashOffset(IAvaloniaStyle? style, float dashOffset) =>
         new StyleStrokeDashOffset(style, dashOffset);

        public record struct StyleStrokeJoint(IAvaloniaStyle? Style, PenLineJoin? Join) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "StrokeLineJoin", Join);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleStrokeJoint SetStrokeJoint(IAvaloniaStyle? style, PenLineJoin? join) =>
            new StyleStrokeJoint(style, join);

        public record struct StyleStrokeCap(IAvaloniaStyle? Style, PenLineCap? Cap) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "StrokeLineCap", Cap);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleStrokeCap SetStrokeCap(IAvaloniaStyle? style, PenLineCap? cap) =>
            new StyleStrokeCap(style, cap);

        public record struct StyleStrokeThickness(IAvaloniaStyle? Style, float Thickness) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "StrokeThickness", (double)Thickness);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleStrokeThickness SetStrokeThickness(IAvaloniaStyle? style, float thickness) =>
            new StyleStrokeThickness(style, thickness);
        #endregion

        public record struct StyleSpacing(IAvaloniaStyle? Style, float Spacing) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "Spacing", (double)Spacing);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleSpacing SetSpacing(IAvaloniaStyle? style, float spacing) =>
             new StyleSpacing(style, spacing);

        public record struct StyleHorizontalAlignment(IAvaloniaStyle? Style, HorizontalAlignment Alignment) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "HorizontalAlignment", Alignment);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleHorizontalAlignment SetHorizontalAlignment(IAvaloniaStyle? style, HorizontalAlignment alignment) =>
            new StyleHorizontalAlignment(style, alignment);

        public record struct StyleVerticalAlignment(IAvaloniaStyle? Style, VerticalAlignment Alignment) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "VerticalAlignment", Alignment);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleVerticalAlignment SetVerticalAlignment(IAvaloniaStyle? style, VerticalAlignment alignment) =>
            new StyleVerticalAlignment(style, alignment);

        public record struct StyleHorizontalContentAlignment(IAvaloniaStyle? Style, HorizontalAlignment Alignment) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "HorizontalContentAlignment", Alignment);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleHorizontalContentAlignment SetHorizontalContentAlignment(IAvaloniaStyle? style, HorizontalAlignment alignment) =>
            new StyleHorizontalContentAlignment(style, alignment);

        public record struct StyleVerticalContentAlignment(IAvaloniaStyle? Style, VerticalAlignment Alignment) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "VerticalContentAlignment", Alignment);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleVerticalContentAlignment SetVerticalContentAlignment(IAvaloniaStyle? style, VerticalAlignment alignment) =>
            new StyleVerticalContentAlignment(style, alignment);

        public record struct StyleWidth(IAvaloniaStyle? Style, float Width) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "Width", (double)Width);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleWidth SetWidth(IAvaloniaStyle? style, float width) =>
            new StyleWidth(style, width);

        public record struct StyleHeight(IAvaloniaStyle? Style, float Height) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "Height", (double)Height);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleHeight SetHeight(IAvaloniaStyle? style, float height) =>
         new StyleHeight(style, height);

        #region FontStyles

        public record struct StyleFontFamily(IAvaloniaStyle? Style, string FontFamily) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "FontFamily", new FontFamily(FontFamily));
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleFontFamily SetFontFamily(IAvaloniaStyle? style, FontFamily fontFamily) =>
            new StyleFontFamily(style, fontFamily.Name);

        public static StyleFontFamily SetFontFamily(IAvaloniaStyle? style, string fontFamily) =>
            new StyleFontFamily(style, fontFamily);

        public record struct StyleFontSize(IAvaloniaStyle? Style, float FontSize) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "FontSize", (double)FontSize);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleFontSize SetFontSize(IAvaloniaStyle? style, float fontSize) =>
            new StyleFontSize(style, fontSize);

        public record struct StyleFontStyle(IAvaloniaStyle? Style, FontStyle FontStyle) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "FontStyle", FontStyle);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleFontStyle SetFontStyle(IAvaloniaStyle? style, FontStyle fontStyle) =>
            new StyleFontStyle(style, fontStyle);

        public record struct StyleFontWeight(IAvaloniaStyle? Style, FontWeight FontWeight) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "FontWeight", FontWeight);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleFontWeight SetFontWeight(IAvaloniaStyle? style, FontWeight fontWeight) =>
            new StyleFontWeight(style, fontWeight);

        public record struct StyleFontStretch(IAvaloniaStyle? Style, FontStretch FontStretch) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "FontStretch", FontStretch);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleFontStretch SetFontStretch(IAvaloniaStyle? style, FontStretch fontStretch) =>
            new StyleFontStretch(style, fontStretch);

        public record struct StyleBaselineOffset(IAvaloniaStyle? Style, float BaselineOffset) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "BaselineOffset", (double)BaselineOffset);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleBaselineOffset SetBaselineOffset(IAvaloniaStyle? style, float baselineOffset) =>
            new StyleBaselineOffset(style, baselineOffset);

        public record struct StyleLineHeight(IAvaloniaStyle? Style, float LineHeight) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "LineHeight", (double)LineHeight);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleLineHeight SetLineHeight(IAvaloniaStyle? style, float lineHeight) =>
            new StyleLineHeight(style, lineHeight);

        public record struct StyleLineSpacing(IAvaloniaStyle? Style, float LineSpacing) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "LineSpacing", (double)LineSpacing);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleLineSpacing SetLineSpacing(IAvaloniaStyle? style, float lineSpacing) =>
            new StyleLineSpacing(style, lineSpacing);

        public record struct StyleLetterSpacing(IAvaloniaStyle? Style, float LetterSpacing) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "LetterSpacing", (double)LetterSpacing);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleLetterSpacing SetLetterSpacing(IAvaloniaStyle? style, float letterSpacing) =>
            new StyleLetterSpacing(style, letterSpacing);

        public record struct StyleMaxLines(IAvaloniaStyle? Style, int MaxLines) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "MaxLines", MaxLines);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleMaxLines SetMaxLines(IAvaloniaStyle? style, int maxLines) =>
            new StyleMaxLines(style, maxLines);

        public record struct StyleTextAlignment(IAvaloniaStyle? Style, TextAlignment TextAlignment) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "TextAlignment", TextAlignment);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleTextAlignment SetTextAlignment(IAvaloniaStyle? style, TextAlignment textAlignment) =>
            new StyleTextAlignment(style, textAlignment);

        public record struct StyleTextWrapping(IAvaloniaStyle? Style, TextWrapping TextWrapping) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "TextWrapping", TextWrapping);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleTextWrapping SetTextWrapping(IAvaloniaStyle? style, TextWrapping textWrapping) =>
            new StyleTextWrapping(style, textWrapping);

        // TODO: IMPLEMENT
        //public record struct StyleTextTrimming(IAvaloniaStyle? Style, TextTrimming TextTrimming) : IAvaloniaStyle
        //{
        //    public Style BuildStyle(StyledElement owner, Style style)
        //    {
        //        style.TryAddSetter(owner, "TextTrimming", TextTrimming);
        //        Style?.BuildStyle(owner, style);
        //        return style;
        //    }
        //}

        // TODO: IMPLEMENT
        //public record struct StyleTextDecorations(IAvaloniaStyle? Style, TextDecorationCollection? TextDecorations) : IAvaloniaStyle
        //{
        //    public Style BuildStyle(StyledElement owner, Style style)
        //    {
        //        style.TryAddSetter(owner, "TextDecorations", TextDecorations);
        //        Style?.BuildStyle(owner, style);
        //        return style;
        //    }
        //}

        // TODO: IMPLEMENT
        //public record struct StyleFontFeatures(IAvaloniaStyle? Style, FontFeatureCollection? FontFeatures) : IAvaloniaStyle
        //{
        //    public Style BuildStyle(StyledElement owner, Style style)
        //    {
        //        style.TryAddSetter(owner, "FontFeature", FontFeatures);
        //        Style?.BuildStyle(owner, style);
        //        return style;
        //    }
        //}

        #endregion

        #region Border Styles
        public record struct StyleBorderBrush(IAvaloniaStyle? Style, IBrush? Brush) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "BorderBrush", Brush);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }
        public static StyleBorderBrush SetBorderBrush(IAvaloniaStyle? style, IBrush? brush) =>
            new StyleBorderBrush(style, brush);

        public record struct StyleBorderThickness(IAvaloniaStyle? Style, Thickness Thickness) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "BorderThickness", Thickness);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleBorderThickness SetBorderThickness(IAvaloniaStyle? style, Thickness thickness) =>
            new StyleBorderThickness(style, thickness);

        public static StyleBorderThickness SetBorderThickness(IAvaloniaStyle? style, float thickness) =>
            new StyleBorderThickness(style, new Thickness(thickness));

        public static StyleBorderThickness SetBorderThickness(IAvaloniaStyle? style, float left, float top, float right, float bottom) =>
            new StyleBorderThickness(style, new Thickness(left, top, right, bottom));

        public static StyleBorderThickness SetBorderThickness(IAvaloniaStyle? style, float horizontal, float vertical) =>
            new StyleBorderThickness(style, new Thickness(horizontal, vertical));

        public record struct StyleCornerRadius(IAvaloniaStyle? Style, CornerRadius CornerRadius) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "CornerRadius", CornerRadius);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleCornerRadius SetCornerRadius(IAvaloniaStyle? style, CornerRadius cornerRadius) =>
            new StyleCornerRadius(style, cornerRadius);

        public static StyleCornerRadius SetCornerRadius(IAvaloniaStyle? style, float cornerRadius) =>
            new StyleCornerRadius(style, new CornerRadius(cornerRadius));

        public static StyleCornerRadius SetCornerRadius(IAvaloniaStyle? style, float topLeft, float topRight, float bottomRight, float bottomLeft) =>
            new StyleCornerRadius(style, new CornerRadius(topLeft, topRight, bottomRight, bottomLeft));

        public static StyleCornerRadius SetCornerRadius(IAvaloniaStyle? style, float top, float bottom) =>
            new StyleCornerRadius(style, new CornerRadius(top, bottom));
        #endregion

        #region Box Shadows
        public record struct StyleBoxShadows(IAvaloniaStyle? Style, BoxShadows BoxShadows) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "BoxShadow", BoxShadows);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleBoxShadows SetBoxShadows(IAvaloniaStyle? style, BoxShadows boxShadows) =>
            new StyleBoxShadows(style, boxShadows);

        public static StyleBoxShadows SetBoxShadows(IAvaloniaStyle? style, BoxShadow boxShadow) =>
            new StyleBoxShadows(style, new BoxShadows(boxShadow));

        public static StyleBoxShadows SetBoxShadows(IAvaloniaStyle? style, Vector2 Offset, float Blur, float Spread, Color Color, bool IsInSet) =>
            new StyleBoxShadows(style, new BoxShadows(new BoxShadow() { OffsetX = Offset.X, OffsetY = Offset.Y, Blur = Blur, Spread = Spread, Color = Color, IsInset = IsInSet }));

        public static StyleBoxShadows SetBoxShadows(IAvaloniaStyle? style, Spread<BoxShadow> boxShadows)
        {
            var first = boxShadows.FirstOrDefault();
            var rest = boxShadows.Skip(1).ToArray();
            return new StyleBoxShadows(style, new BoxShadows(first, rest));
        }
        #endregion

        #region Padding
        public record struct StylePadding(IAvaloniaStyle? Style, Thickness Padding) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "Padding", Padding);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StylePadding SetPadding(IAvaloniaStyle? style, Thickness padding) =>
            new StylePadding(style, padding);
        public static StylePadding SetPadding(IAvaloniaStyle? style, float padding) =>
            new StylePadding(style, new Thickness(padding));
        public static StylePadding SetPadding(IAvaloniaStyle? style, float left, float top, float right, float bottom) =>
            new StylePadding(style, new Thickness(left, top, right, bottom));
        public static StylePadding SetPadding(IAvaloniaStyle? style, float horizontal, float vertical) =>
            new StylePadding(style, new Thickness(horizontal, vertical));

        #endregion

        #region Margin
        public record struct StyleMargin(IAvaloniaStyle? Style, Thickness Margin) : IAvaloniaStyle
        {
            public Style BuildStyle(StyledElement owner, Style style)
            {
                style.TryAddSetter(owner, "Margin", Margin);
                Style?.BuildStyle(owner, style);
                return style;
            }
        }

        public static StyleMargin SetMargin(IAvaloniaStyle? style, Thickness margin) =>
            new StyleMargin(style, margin);

        public static StyleMargin SetMargin(IAvaloniaStyle? style, float margin) =>
            new StyleMargin(style, new Thickness(margin));

        public static StyleMargin SetMargin(IAvaloniaStyle? style, float left, float top, float right, float bottom) =>
            new StyleMargin(style, new Thickness(left, top, right, bottom));

        public static StyleMargin SetMargin(IAvaloniaStyle? style, float horizontal, float vertical) =>
            new StyleMargin(style, new Thickness(horizontal, vertical));
        #endregion

        #endregion
    }
}