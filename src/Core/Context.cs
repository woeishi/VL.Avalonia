using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

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
        // (empty) task gives vl(?) time to resume
        // resuming on current thread starts avalonia on main thread
        // without task, vl blocks
        // without continuation on main thread, keyboard is not received...
        var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        Task.Run(() => { }).ContinueWith(
            (x) =>
            {
                //System.Diagnostics.Debug.WriteLine("inside " + Thread.CurrentThread.ManagedThreadId);
                if (this.Lifetime is ClassicDesktopStyleApplicationLifetime)
                    (this.Lifetime as ClassicDesktopStyleApplicationLifetime)?.Start([]);
            }, scheduler);
        //Task.Run(() =>
        //{
        //    if (this.Lifetime is ClassicDesktopStyleApplicationLifetime)
        //        (this.Lifetime as ClassicDesktopStyleApplicationLifetime)?.Start([]);
        //});
    }


    public void Dispose()
    {
        if (this.Lifetime != null && this.Lifetime is ClassicDesktopStyleApplicationLifetime)
        {
            (this.Lifetime as ClassicDesktopStyleApplicationLifetime)?.Shutdown(0);
        }
    }
}

