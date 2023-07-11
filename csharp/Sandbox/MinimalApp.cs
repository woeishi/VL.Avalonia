using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox
{
    public class MinimalApp : Application
    {
        public Window? Window { get; set; }

        public override void Initialize()
        {
            //this.RequestedThemeVariant = Avalonia.Styling.ThemeVariant.Default;
            
            this.Styles.Add(new Avalonia.Themes.Fluent.FluentTheme());
            base.Initialize();
        }
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                //desktop.MainWindow = new MainWindow();
                this.Window = new Avalonia.Controls.Window();
                
                var b = new Button();
                var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
                Task.Run(() =>
                {
                    Thread.Sleep(5000);

                }).ContinueWith((x) =>
                {
                    var panel = new StackPanel();
                    for (int i = 0; i < 10; i++)
                    {
                        var _b = new Button();
                        _b.Content = "hoho " + i.ToString();
                        panel.Children.Add(_b);
                    }
                    
                    this.Window.Content = panel;
                }, scheduler);
               
                //this.Window.Content = panel;
                desktop.MainWindow = this.Window;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
