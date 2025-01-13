//main class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //    { var t1 = new Thread(() =>
            //     {
            //         while (true)
            //         {
            //             System.Console.WriteLine("Thread 1");
            //             Thread.Sleep(1000);
            //         }
            //     });

            //     var t2 = new Thread(() =>
            //     {
            //         while (true)
            //         {
            //             System.Console.WriteLine("Thread 2");
            //             Thread.Sleep(2000);
            //         }
            //     });}

            var t1 = new Thread(new ParameterizedThreadStart(Print));
            var t2 = new Thread(new ParameterizedThreadStart(Print));

            t1.IsBackground = true;
            t2.IsBackground = true;

            t1.Start("Thread 1");
            t2.Start("Thread 2");

            Console.ReadLine();
        }

        static void Print(object? obj)
        {
            while (true)
            {
                System.Console.WriteLine(obj);
                Thread.Sleep(1000);
            }
        }
    }
}