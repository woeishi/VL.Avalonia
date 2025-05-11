using Avalonia.Layout;
using Avalonia.Media;

namespace VL.Avalonia.Styles
{
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

    public record struct StyleBackground(IAvaloniaStyle? Style, IBrush? brush) : IAvaloniaStyle
    {
        public void ApplyStyle(object owner)
        {
            owner.GetType().GetProperty("Background")?.SetValue(owner, brush);

            Style?.ApplyStyle(owner);
        }
    }

    public static class Styles
    {
        public static IAvaloniaStyle SetOrientation(IAvaloniaStyle? style, Orientation orientation)
        {
            return new StyleOrientation(style, orientation);
        }

        public static IAvaloniaStyle SetBackground(IAvaloniaStyle? style, IBrush? brush)
            => new StyleBackground(style, brush);
    }
}
