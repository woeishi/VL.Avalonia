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
                var b = new Button();
                b.Content = "haha";
                this.Window.Content = b;
                desktop.MainWindow = this.Window;

            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
