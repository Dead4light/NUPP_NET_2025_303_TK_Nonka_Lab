using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zoo.Common
{
    public static class SyncExamples
    {
        // Приклад використання lock
        private static readonly object _lockObject = new object();

        public static void LockExample()
        {
            lock (_lockObject)
            {
                Console.WriteLine($"Lock started by thread: {Thread.CurrentThread.ManagedThreadId}");
                // Імітуємо роботу
                Thread.Sleep(1000);
                Console.WriteLine($"Lock ended by thread: {Thread.CurrentThread.ManagedThreadId}");
            }
        }

        // Приклад використання Semaphore
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(3);  // Дозволяє до 3 потоків одночасно

        public static void SemaphoreExample()
        {
            var tasks = new Task[10];

            for (int i = 0; i < 10; i++)
            {
                tasks[i] = Task.Run(() => PerformSemaphoreTask(i));
            }

            Task.WhenAll(tasks).Wait();
        }

        private static async Task PerformSemaphoreTask(int id)
        {
            await _semaphore.WaitAsync();
            try
            {
                Console.WriteLine($"Semaphore started by task {id}");
                // Імітуємо роботу
                await Task.Delay(1000);
                Console.WriteLine($"Semaphore ended by task {id}");
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
