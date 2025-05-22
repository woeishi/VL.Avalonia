using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox
{
    public class MinimalApp : Application
    {

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                //desktop.MainWindow = new MainWindow();
                var windowA = new Avalonia.Controls.Window();
                windowA.Styles.Add(new Avalonia.Themes.Fluent.FluentTheme());
                var windowB = new Avalonia.Controls.Window();
                windowB.Styles.Add(new Avalonia.Themes.Simple.SimpleTheme());
                windowA.Show();
                windowB.Show();

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
                    
                    windowA.Content = panel;
                }, scheduler).ContinueWith((x) =>
                {
                    var bb = new Button();
                    bb.Content = "hahahah";
                    var sp = new StackPanel();
                    sp.Children.Add(bb);
                    windowB.Content = sp;
                    System.Diagnostics.Debug.WriteLine(desktop.Args);

                }, scheduler);

                //this.Window.Content = panel;
                
                
                //desktop.MainWindow = this.Window;

                desktop.ShutdownRequested += Desktop_ShutdownRequested;
            }
            
            base.OnFrameworkInitializationCompleted();
        }

        private void Desktop_ShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("shutting down after the last window");
        }
    }
}
