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

        public record struct StyleSpacing(IAvaloniaStyle? Style, double Spacing) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("Spacing")?.SetValue(owner, Spacing);
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

        public record struct StyleWidth(IAvaloniaStyle? Style, double Width) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("Width")?.SetValue(owner, Width);
                Style?.ApplyStyle(owner);
            }
        }

        public record struct StyleHeight(IAvaloniaStyle? Style, double Height) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("Height")?.SetValue(owner, Height);
                Style?.ApplyStyle(owner);
            }
        }

        #endregion

        #region StyleNodes
        public static StyleOrientation SetOrientation(IAvaloniaStyle? style, Orientation orientation) =>
            new StyleOrientation(style, orientation);
        public static StyleBackground SetBackground(IAvaloniaStyle? style, IBrush? brush) =>
            new StyleBackground(style, brush);
        public static StyleForeground SetForeground(IAvaloniaStyle? style, IBrush? brush) =>
    new StyleForeground(style, brush);
        public static StyleSpacing SetSpacing(IAvaloniaStyle? style, double spacing) =>
             new StyleSpacing(style, spacing);
        public static StyleHorizontalAlignment SetHorizontalAlignment(IAvaloniaStyle? style, HorizontalAlignment alignment) =>
            new StyleHorizontalAlignment(style, alignment);
        public static StyleVerticalAlignment SetVerticalAlignment(IAvaloniaStyle? style, VerticalAlignment alignment) =>
            new StyleVerticalAlignment(style, alignment);
        public static StyleWidth SetWidth(IAvaloniaStyle? style, double width) =>
            new StyleWidth(style, width);
        public static StyleHeight SetHeight(IAvaloniaStyle? style, double height) =>
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

        #endregion
    }
}
