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


        public record struct StyleBackground(IAvaloniaStyle? Style, IBrush? brush) : IAvaloniaStyle
        {
            public void ApplyStyle(object owner)
            {
                owner.GetType().GetProperty("Background")?.SetValue(owner, brush);

                Style?.ApplyStyle(owner);
            }
        }
        #endregion

        #region StyleNodes
        public static IAvaloniaStyle SetOrientation(IAvaloniaStyle? style, Orientation orientation) =>
            new StyleOrientation(style, orientation);

        public static IAvaloniaStyle SetBackground(IAvaloniaStyle? style, IBrush? brush) =>
            new StyleBackground(style, brush);
        #endregion
    }
}
