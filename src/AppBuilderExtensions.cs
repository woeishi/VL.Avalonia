using Avalonia;

namespace VL.Avalonia
{
    public static class AppBuilderExtensions
    {
        public static void RunSetup(this AppBuilder appBuilder, Action<AppBuilder> setup)
        {
            if (appBuilder != null && setup != null)
            {
                var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
                Task
                    .Run(() => { /* give the host time to resume its mainloop */})
                    .ContinueWith(
               (x) => { setup.Invoke(appBuilder); }
               , scheduler);
            }
        }
    }
}
