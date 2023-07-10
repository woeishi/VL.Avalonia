using Avalonia;
using Avalonia.Win32;
using Avalonia.Direct2D1;
using Avalonia.Headless;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace VL.Avalonia
{
    public static class VLAppBuilder
    {
        public static AppBuilder BuildVLApp()
        {

            var builder = AppBuilder.Configure<VLApplication>();
            builder = builder.UsePlatformDetect();
            //builder = builder.UseWin32();
            //builder = builder.UseSkia();
            //builder = builder.UseDirect2D1();
            //builder = builder.LogToTrace();
            return builder;
        }

        public static void Run(AppBuilder builder, ShutdownMode shutdownMode)  {
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(() =>
            {
                builder.StartWithClassicDesktopLifetime(new string[] { }, shutdownMode);
            }, new CancellationToken(), TaskCreationOptions.None, scheduler);
            System.Diagnostics.Debug.WriteLine("app started");
            //if (builder != null)
            //{
            //    builder.Start(AppMain, new string[] { });
            //}
        }

        static void AppMain(Application app, string[] args)
        {
            // A cancellation token source that will be 
            // used to stop the main loop
            var cts = new CancellationTokenSource();

            // Do you startup code here
            new Window().Show();
            // Start the main loop
            app.Run(cts.Token);
        }
    }

   
}
