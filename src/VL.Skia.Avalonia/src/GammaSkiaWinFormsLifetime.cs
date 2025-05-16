using Avalonia.Controls.ApplicationLifetimes;

namespace VL.Skia.Avalonia
{
    sealed class GammaSkiaWinFormsLifetime : IControlledApplicationLifetime
    {
        public event EventHandler<ControlledApplicationLifetimeStartupEventArgs>? Startup;
        public event EventHandler<ControlledApplicationLifetimeExitEventArgs>? Exit;

        public GammaSkiaWinFormsLifetime()
        {
            Application.ApplicationExit += (s, a) => Shutdown();
        }

        public void Shutdown(int exitCode = 0)
        {
            Exit?.Invoke(this, new ControlledApplicationLifetimeExitEventArgs(exitCode));
        }
    }
}
