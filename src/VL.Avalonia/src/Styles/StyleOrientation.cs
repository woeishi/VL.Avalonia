using Avalonia.Layout;

namespace VL.Avalonia.Styles
{
    public record struct StyleOrientation<T>(IAvaloniaStyle<T>? Style, Orientation Orientation) where T : class, IAvaloniaStyle<T>
    {
        public void ApplyStyle(T owner)
        {
            var property = typeof(T).GetProperty("Orientation");

            if (property != null)
            {
                property.SetValue(owner, Orientation);
            }

            Style?.ApplyStyle(owner);
        }
    }

    public static class Styles
    {
        public static StyleOrientation<T> SetOrientation<T>(IAvaloniaStyle<T>? style, Orientation orientation) where T : class, IAvaloniaStyle<T>
        {
            return new StyleOrientation<T>(style, orientation);
        }
    }
}
