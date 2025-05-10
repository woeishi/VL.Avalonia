using Avalonia;
using Avalonia.Themes.Fluent;

namespace VL.Avalonia.Themes;

public static class AppBuilderExtensions
{
    public static AppBuilder UseThemeFluent(this AppBuilder builder, Action<FluentTheme>? configureTheme)
    {
        var theme = new FluentTheme();

        configureTheme?.Invoke(theme);

        builder.Instance?.Styles.Add(theme);

        return builder;
    }
}
