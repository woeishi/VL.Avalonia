using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;

namespace VL.Avalonia;

[ProcessNode(HasStateOutput = true)]

public class Context:IDisposable
{
    //public Context Output => this;
    public Application Application { get; private set; }
    public IApplicationLifetime Lifetime { get; private set; }

    public Context(AppBuilder? appBuilder, IApplicationLifetime lifetime)
    {
        
        if (lifetime == null)
        {
            var _lifetime = new ClassicDesktopStyleApplicationLifetime();
            _lifetime.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            this.Lifetime = _lifetime;
        } else
        {
            this.Lifetime = lifetime;
        }
        if (appBuilder == null)
        {
            //appBuilder = AppBuilder.Configure<Application>().UseWin32().UseSkia();
            appBuilder = AppBuilder.Configure<Application>().UsePlatformDetect();
        }
        appBuilder.SetupWithLifetime(this.Lifetime);
        
        Application = appBuilder.Instance;

        // HACK: 
        // ILifetime.Start needs to be called on the UI thread
        // it starts the avalonias mainloop / messagepump
        // the call will block (it is usually used in the main function/entry point
        // since that would block the vl runtime / mainloop, we have to somehow resume after starting avalonia
        var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        Task.Factory.StartNew(() =>
        {
            if (this.Lifetime is ClassicDesktopStyleApplicationLifetime)
                (this.Lifetime as ClassicDesktopStyleApplicationLifetime)?.Start([]);

        }, new CancellationToken(), TaskCreationOptions.None, scheduler);
    }

    public void Dispose()
    {
        if (this.Lifetime != null && this.Lifetime is ClassicDesktopStyleApplicationLifetime)
        {
            (this.Lifetime as ClassicDesktopStyleApplicationLifetime)?.Shutdown(0);
        }
    }
}

