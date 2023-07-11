using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace Sandbox;

public partial class App : Application
{
    public override void Initialize()
    {
        //AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            //desktop.MainWindow = new MainWindow();
            desktop.MainWindow = new Avalonia.Controls.Window();
        }

        base.OnFrameworkInitializationCompleted();
    }
}