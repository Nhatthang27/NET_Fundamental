namespace AsyncAwait
{
    internal class Program
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
        static void Main1(string[] args)
        {
            Console.WriteLine("hehe");
            Task t1 = new Task(() => CountAndChangeColor(5, "Task 1", ConsoleColor.Red));
            Task t2 = new Task(() => CountAndChangeColor(5, "Task 2", ConsoleColor.Green));
            Task t3 = new Task((object obj) => CountAndChangeColor(5, obj as string, ConsoleColor.Blue), "Task 3");
            //Task (Action<object> action, object state)

            t1.Start();
            t2.Start();
            t3.Start();

            //t1.Wait();
            //t2.Wait();
            //t3.Wait();

            Task.WaitAll(t1, t2, t3); //chờ tất cả các task hoàn thành
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
