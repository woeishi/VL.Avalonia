using Avalonia.Layout;

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

    public static class Styles
    {
        public static IAvaloniaStyle SetOrientation(IAvaloniaStyle? style, Orientation orientation)
        {
            return new StyleOrientation(style, orientation);
        }
    }
}
