using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace Sandbox
{
    public class MinimalApp : Application
    {
        public Window? Window { get; set; }

        public override void Initialize()
        {
            //this.RequestedThemeVariant = Avalonia.Styling.ThemeVariant.Default;
            
            this.Styles.Add(new Avalonia.Themes.Simple.SimpleTheme());
            base.Initialize();
        }
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                //desktop.MainWindow = new MainWindow();
                this.Window = new Avalonia.Controls.Window();
                var panel = new StackPanel();

                var b = new Button();
                b.Content = "haha";
                b.Click += (s, e) => {
                    for (int i= 0; i < 10; i++)
                    {
                        var _b = new Button();
                        _b.Content = "hoho " + i.ToString();
                        panel.Children.Add(_b);
                    }
                };
                panel.Children.Add(b);
                this.Window.Content = panel;
                desktop.MainWindow = this.Window;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
