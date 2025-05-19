using Avalonia.Threading;
using VL.Core;
using VL.Lib.Animation;
using SysTimer = System.Threading.Timer;

namespace VL.Skia.Avalonia
{
    internal class GammaDispatcherImpl : IDispatcherImpl
    {
        private readonly IClock _clock = AppHost.Current.Services.GetService(typeof(IClock)) as IClock;

        private readonly Thread _mainThread;
        private readonly SysTimer _timer;
        private readonly SendOrPostCallback _invokeSignaled; // cached delegate
        private readonly SendOrPostCallback _invokeTimer;  // cached delegate

        public long Now
        {
            get
            {
                // TODO: DO IT PROPERLY
                var now = (long)_clock.Time.Seconds * 1000; // (long)Time.GetTicksMsec();
                return now;
            }
        }

        public bool CurrentThreadIsLoopThread
            => _mainThread == Thread.CurrentThread;

        public event Action? Signaled;

        public event Action? Timer;

        public GammaDispatcherImpl(Thread mainThread)
        {
            _mainThread = mainThread;
            _invokeSignaled = InvokeSignaled;
            _invokeTimer = InvokeTimer;
            _timer = new(OnTimerTick, this, Timeout.Infinite, Timeout.Infinite);
        }

        public void UpdateTimer(long? dueTimeInMs)
        {
            var interval = dueTimeInMs is { } value
                ? Math.Clamp(value - Now, 0L, 0xFFFFFFFEL)
                : Timeout.Infinite;

            _timer.Change(interval, Timeout.Infinite);
        }

        //  => GdDispatcher.SynchronizationContext.Post(_invokeTimer, null);
        private void OnTimerTick(object? state)
        {
            try
            {
                AppHost.Current.SynchronizationContext.Post(_invokeTimer, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //  => GdDispatcher.SynchronizationContext.Post(_invokeSignaled, this);
        public void Signal()
        {
            // THIS NEEDS REWORK
            // written by copilot
            // source: https://github.com/MrJul/Estragonia/blob/main/src/JLeb.Estragonia/GodotDispatcherImpl.cs
            try
            {
                Console.WriteLine("Signal called");


                var syncContext = AppHost.Current?.SynchronizationContext;
                if (syncContext == null)
                {
                    Console.WriteLine("Warning: SynchronizationContext is null");
                    Signaled?.Invoke();
                    return;
                }

                syncContext.Post(_invokeSignaled, this);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Signal: {ex.Message}");

                try { Signaled?.Invoke(); } catch { }
            }
        }

        private void InvokeSignaled(object? state)
            => Signaled?.Invoke();

        private void InvokeTimer(object? state)
            => Timer?.Invoke();
    }
}
