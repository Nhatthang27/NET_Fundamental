using System.Diagnostics;

namespace TaskSample
{
    internal class Program
    {
        /// <summary>
        /// Hàm async nếu muốn chạy bất đồng bộ thì gọi ngay. Nếu không gọi thì hàm sẽ chạy đồng bộ trên thread chính
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            Console.WriteLine($"Delay Main ... Thread ID = {Environment.CurrentManagedThreadId}");
            stopwatch.Start();
            /*Task t = Task.Run(NomarlMethod);
            var t1 = Delay1();
            var t2 = Delay2();
            var t3 = Delay3();

            await t1;
            await t2;
            await t3;*/
            //Method1();
            //Method2();
            await Method1Async();
            Console.WriteLine($"Delay Main after method 1... Thread ID = {Environment.CurrentManagedThreadId}");
            await Method2Async();
            Console.WriteLine($"Delay Main after method 2... Thread ID = {Environment.CurrentManagedThreadId}");
            stopwatch.Stop();
            Console.WriteLine($"Elapsed {stopwatch.Elapsed}");
            Console.WriteLine($"Delay Main after all... Thread ID = {Environment.CurrentManagedThreadId}");
        }

        static void Method1()
        {
            Console.WriteLine($"Method 1...Thread ID = {Environment.CurrentManagedThreadId}");
            Task.Delay(1000).Wait();
        }

        static void Method2()
        {
            Console.WriteLine($"Method 2...Thread ID = {Environment.CurrentManagedThreadId}");
            Task.Delay(2000).Wait();
        }

        static async Task Method1Async()
        {
            await Task.Delay(1000);
            Console.WriteLine($"Method 1 Async...Thread ID = {Environment.CurrentManagedThreadId}");
        }

        static async Task Method2Async()
        {
            await Task.Delay(2000);
            Console.WriteLine($"Method 2 Async...Thread ID = {Environment.CurrentManagedThreadId}");
        }

        static void NomarlMethod()
        {
            Console.WriteLine($"NomarlMethod...Thread ID = {Environment.CurrentManagedThreadId}");
        }

        static async Task Delay1()
        {
            System.Console.WriteLine($"Delay1...before Thread ID = {Environment.CurrentManagedThreadId}");
            await Task.Delay(1000);
            System.Console.WriteLine($"Delay1...after Thread ID = {Environment.CurrentManagedThreadId}");

        }

        static async Task Delay2()
        {
            System.Console.WriteLine($"Delay2...before Thread ID = {Environment.CurrentManagedThreadId}");
            await Task.Delay(1000);
            System.Console.WriteLine($"Delay2...after Thread ID = {Environment.CurrentManagedThreadId}");
        }

        static async Task Delay3()
        {
            System.Console.WriteLine($"Delay3...before Thread ID = {Environment.CurrentManagedThreadId}");
            await Task.Delay(1000);
            System.Console.WriteLine($"Delay2...after Thread ID = {Environment.CurrentManagedThreadId}");
        }

        /* static async Task Main(string[] args)
         {
             Console.WriteLine($"Main - 1 - Thread ID = {Environment.CurrentManagedThreadId}");
             Task t = Task.Run(AsyncFunc1);
             AsyncFunc2();
             Console.WriteLine($"Main - 2 - Thread ID = {Environment.CurrentManagedThreadId}");
             t.Wait();
         }

         private static int AsyncFunc1()
         {
             Console.WriteLine($"AsyncFunc1 - Thread ID = {Environment.CurrentManagedThreadId}");
             return 0;

         }

         private static async Task<int> AsyncFunc2()
         {
             Console.WriteLine($"Async2 - 1 - Thread ID = {Environment.CurrentManagedThreadId}");
             var r = (await File.ReadAllTextAsync("sample.txt")).Length;
             Console.WriteLine($"Async2 - 2 - Thread ID = {Environment.CurrentManagedThreadId}");
             return r;
         }*/
    }
}
