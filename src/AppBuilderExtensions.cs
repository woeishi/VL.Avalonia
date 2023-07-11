using Avalonia;

namespace VL.Avalonia
{
    public static class AppBuilderExtensions
    {
        public static void RunSetup(this AppBuilder appBuilder, Action<AppBuilder> setup)
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
                // something like this, but not blocking to await the setup
                //completion.Task.Wait();
            }
           
        }

        public static AppBuilder ConfigureDefault() => AppBuilder.Configure<Application>();

        public static AppBuilder PassThrough(this AppBuilder appBuilder)
        {
            Console.WriteLine("here we are");
            return appBuilder;
        }
       
    }
}
