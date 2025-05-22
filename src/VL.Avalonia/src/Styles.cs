using Avalonia.Layout;
using Avalonia.Media;

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

        public record struct StyleSpacing(IAvaloniaStyle? Style, double Spacing) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("Spacing")?.SetValue(owner, Spacing);

                Style?.ApplyStyle(owner);
            }
        }

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

        #endregion
    }
}
