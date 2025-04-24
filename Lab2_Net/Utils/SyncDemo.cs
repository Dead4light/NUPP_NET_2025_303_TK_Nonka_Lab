using System;
using System.Threading;

namespace Lab2App.Utils
{
    public class SyncDemo
    {
        private static readonly object _lock = new object();
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private static AutoResetEvent _autoResetEvent = new AutoResetEvent(false);

        public static void DemoLock()
        {
            lock (_lock)
            {
                Console.WriteLine("Critical section (lock) executed");
            }
        }

        public static async Task DemoSemaphore()
        {
            await _semaphore.WaitAsync();
            try
            {
                Console.WriteLine("Critical section (SemaphoreSlim) executed");
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public static void DemoAutoResetEvent()
        {
            _autoResetEvent.WaitOne();
            Console.WriteLine("Critical section (AutoResetEvent) executed");
            _autoResetEvent.Set();
        }
    }
}

