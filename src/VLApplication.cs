using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Fluent = Avalonia.Themes.Fluent;
using Simple = Avalonia.Themes.Simple;

namespace VL.Avalonia
{
    //https://github.com/AvaloniaUI/avalonia-dotnet-templates/blob/master/templates/csharp/app/Program.cs
    public class VLApplication : Application, IDisposable
    {
        public void Dispose()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow?.Close();
            }
        }

        public override void Initialize()
        {
            //this.Styles.Add(new Fluent.FluentTheme());
            base.Initialize();
        }
        public override void OnFrameworkInitializationCompleted()
        {
            
            //if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            //{
            //    desktop.MainWindow = new Window();
            //    System.Diagnostics.Debug.WriteLine("Avalonia window created");

            //} else
            //{
            //    System.Diagnostics.Debug.WriteLine("no desktop lifetime found");
            //}

            base.OnFrameworkInitializationCompleted();
        }

        

    }
}
