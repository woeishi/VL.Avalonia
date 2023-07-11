using Avalonia.Controls;
using Avalonia.Themes.Fluent;

namespace Sandbox
{
    public static class WindowUtil
    {
        public static void SpawnWindow()
        {
            var window = new Window();
            window.Styles.Add(new FluentTheme());
            window.Show();

            var panel = new StackPanel();
            for (int i = 0; i < 10; i++)
            {
                var b = new Button();
                b.Content = "Button " + i;
                panel.Children.Add(b);
            }
            window.Content = panel;
        }
    }
}
