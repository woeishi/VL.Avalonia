using Avalonia.Styling;
using VL.Core;

namespace VL.Avalonia.Themes
{
    public enum AvaloniaDefaultThemeVariant
    {
        Default,
        Light,
        Dark
    }

    public class Variants
    {
        public static ThemeVariant DefaultTheme => ThemeVariant.Default;
        public static ThemeVariant LightTheme => ThemeVariant.Light;
        public static ThemeVariant DarkTheme => ThemeVariant.Dark;

        public static ThemeVariant DefaultThemeVariant(Optional<AvaloniaDefaultThemeVariant> variant) =>
            variant.HasValue ? variant.Value switch
            {
                AvaloniaDefaultThemeVariant.Default => ThemeVariant.Default,
                AvaloniaDefaultThemeVariant.Light => ThemeVariant.Light,
                AvaloniaDefaultThemeVariant.Dark => ThemeVariant.Dark,
                _ => ThemeVariant.Default,
            } : ThemeVariant.Default;
    }
}
