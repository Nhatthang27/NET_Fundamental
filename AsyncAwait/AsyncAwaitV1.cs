namespace AsyncAwait
{
    internal class AsyncAwaitV1
    {
        private static void CountAndChangeColor(int second, string msg, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine($"Start: {msg}");
            for (int i = 0; i < second; i++)
            {
                Console.ForegroundColor = consoleColor;
                Console.WriteLine($"{msg} {i} second");
                Thread.Sleep(1000);
            }
            Console.WriteLine($"End: {msg}");
            Console.ResetColor();
        }

        //không mong muốn vì phải chờ hết task này xong thì task khác mới được chạy
        static Task MyTask(int second, string msg, ConsoleColor consoleColor)
        {
            Task t = new Task(() => CountAndChangeColor(second, msg, consoleColor));
            t.Start();
            t.Wait();
            Console.WriteLine($"{msg} done");
            return t;
        }


        static async Task MyTaskAsync(int second, string msg, ConsoleColor consoleColor)
        {
            Task t = new Task(() => CountAndChangeColor(second, msg, consoleColor));
            t.Start();
            await t;
            Console.WriteLine($"{msg} done");
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Start tasks");
            //Task t1 = MyTask(5, "Task 1", ConsoleColor.Red);
            //Task t2 = MyTask(5, "Task 2", ConsoleColor.Green);
            //Task t3 = new Task((object obj) => CountAndChangeColor(5, obj as string, ConsoleColor.Blue), "Task 3");
            //t3.Start();

            Task t1 = MyTaskAsync(3, "Task 1", ConsoleColor.Red);
            Task t2 = MyTaskAsync(3, "Task 2", ConsoleColor.Green);
            Task t3 = MyTaskAsync(3, "Task 3", ConsoleColor.Blue);

            await Task.WhenAll(t1, t2, t3);
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
