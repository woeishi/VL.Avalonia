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

public static class ApplicationExtensions
{
    public static void UseThemeFluent(this Application app, Action<FluentTheme>? configureTheme)
    {
        var theme = new FluentTheme();
        configureTheme?.Invoke(theme);
        app.Styles.Add(theme);
    }
}
