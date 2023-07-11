using Avalonia;

namespace VL.Avalonia
{
    public static class AppBuilderExtensions
    {
        public static AppBuilder RunSetup(this AppBuilder appBuilder, Action<AppBuilder> setup)
        {
            if (appBuilder != null && setup != null)
            {
                //var completion = new TaskCompletionSource();
                var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
                Task
                    .Run(() => { /* give the host time to resume its mainloop */})
                    .ContinueWith(
               (x) => {
                   //appBuilder = appBuilder.AfterSetup((ab) => {
                   //    completion.SetResult();
                   //});
                   setup.Invoke(appBuilder); 
               }
               , scheduler);
                Console.WriteLine("Dispatched");
                // something like this, but not blocking to await the setup
                //completion.Task.Wait();
            }
            return appBuilder;
        }

        public static AppBuilder ConfigureDefault() => AppBuilder.Configure<Application>();

        public static AppBuilder PassThrough(this AppBuilder appBuilder)
        {
            Console.WriteLine("here we are");
            return appBuilder;
        }
       
    }
}
