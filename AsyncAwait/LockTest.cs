namespace AsyncAwait
{
    public class Counter
    {
        private int _count = 0;
        private readonly object _lock = new object();

        public void Increment()
        {
            //lock (_lock)
            //{
            _count++;
            Console.WriteLine($"Count: {_count}");
            //}
        }
    }

    internal class LockTest
    {
        private static Counter _counter = new Counter();

        //public static void Main()
        //{
        //    Thread t1 = new Thread(() => DoWork());
        //    Thread t2 = new Thread(() => DoWork());

        //    t1.Start();
        //    t2.Start();

        //    t1.Join();
        //    t2.Join();

        //    Console.WriteLine("Both threads have finished.");
        //}

        private static void DoWork()
        {
            for (int i = 0; i < 10; i++)
            {
                _counter.Increment();
            }
        }
    }
}
