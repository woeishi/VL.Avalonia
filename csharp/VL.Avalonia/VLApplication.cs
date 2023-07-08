using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Fluent = Avalonia.Themes.Fluent;

namespace VL.Avalonia
{
    //https://github.com/AvaloniaUI/avalonia-dotnet-templates/blob/master/templates/csharp/app/Program.cs
    public class VLApplication : Application, IDisposable
    {
        public Window? Window { get; set; }

        public string WindowContent => this.Window?.Content?.ToString() ?? "'No Content'";

        public void Dispose()
        {
            this.Window?.Close();
        }

        public override void Initialize()
        {
            
            this.Styles.Add(new Fluent.FluentTheme());
            base.Initialize();
        }
        public override void OnFrameworkInitializationCompleted()
        {
            
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new Window();
                this.Window = desktop.MainWindow;
                
            } else
            {
                System.Diagnostics.Debug.WriteLine("no desktop found");
            }

            base.OnFrameworkInitializationCompleted();
        }

        

    }
}
