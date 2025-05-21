using Avalonia.Threading;
using VL.Lib.Animation;
using SysTimer = System.Threading.Timer;

namespace VL.Skia.Avalonia
{
    internal class GammaDispatcherImpl : IDispatcherImpl
    {
        private readonly IClock _clock;
        private readonly SynchronizationContext _synchronizationContext;
        private readonly Thread _mainThread;

        private readonly SysTimer _timer;
        private readonly SendOrPostCallback _invokeSignaled; // cached delegate
        private readonly SendOrPostCallback _invokeTimer;  // cached delegate

        // TODO: Is there a better way to get the current milliseconds?
        public long Now => (long)_clock.Time.Seconds * 1000;

        public bool CurrentThreadIsLoopThread
            => _mainThread == Thread.CurrentThread;

        public event Action? Signaled;

        public event Action? Timer;

        public GammaDispatcherImpl(Thread mainThread, IClock clock, SynchronizationContext synchronizationContext)
        {
            _mainThread = mainThread;
            _clock = clock;
            _synchronizationContext = synchronizationContext;

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

        private void OnTimerTick(object? state)
            => _synchronizationContext.Post(_invokeTimer, null);


        public void Signal() =>
            _synchronizationContext.Post(_invokeSignaled, null);

        private void InvokeSignaled(object? state)
            => Signaled?.Invoke();

        private void InvokeTimer(object? state)
            => Timer?.Invoke();
    }
}
