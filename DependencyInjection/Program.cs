using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    interface IClassB
    {
        public void ActionB();
    }
    interface IClassC
    {
        public void ActionC();
    }
    class ClassC : IClassC
    {
        public ClassC() => Console.WriteLine("ClassC is created");
        public void ActionC() => Console.WriteLine("Action in ClassC");
    }
    class ClassB : IClassB
    {
        IClassC c_dependency;
        public ClassB(IClassC classc)
        {
            c_dependency = classc;
            Console.WriteLine("ClassB is created");
        }
        public void ActionB()
        {
            Console.WriteLine("Action in ClassB");
            c_dependency.ActionC();
        }
    }
    class ClassA
    {
        IClassB b_dependency;
        public ClassA(IClassB classb)
        {
            b_dependency = classb;
            Console.WriteLine("ClassA is created");
        }
        public void ActionA()
        {
            Console.WriteLine("Action in ClassA");
            b_dependency.ActionB();
        }
    }
    class ClassC1 : IClassC
    {
        public ClassC1() => Console.WriteLine("ClassC1 is created");
        public void ActionC()
        {
            Console.WriteLine("Action in C1");
        }
    }
    class ClassB1 : IClassB
    {
        IClassC c_dependency;
        public ClassB1(IClassC classc)
        {
            c_dependency = classc;
            Console.WriteLine("ClassB1 is created");
        }
        public void ActionB()
        {
            Console.WriteLine("Action in B1");
            c_dependency.ActionC();
        }
    }
    class ClassB2 : IClassB
    {
        IClassC c_dependency;
        string message;
        public ClassB2(IClassC classc, string mgs)
        {
            c_dependency = classc;
            message = mgs;
            Console.WriteLine("ClassB2 is created");
        }
        public void ActionB()
        {
            Console.WriteLine(message);
            c_dependency.ActionC();
        }
    }

    internal class Program
    {
        // Factory nhận tham số là IServiceProvider và trả về đối tượng địch vụ cần tạo
        public static ClassB2 CreateB2Factory(IServiceProvider serviceprovider)
        {
            var service_c = serviceprovider.GetService<IClassC>();
            var sv = new ClassB2(service_c, "Thực hiện trong ClassB2");
            return sv;
        }
        static void ServiceCollectionV1()
        {
            var services = new ServiceCollection();

            var provider = services.BuildServiceProvider();

            services.AddSingleton<IClassC, ClassC1>();

            for (int i = 0; i < 5; i++)
            {
                IClassC classC = provider.GetService<IClassC>();
                Console.WriteLine(classC.GetHashCode);
            }
        }

        static void DIHandMade()
        {
            IClassC objectC = new ClassC1();            // new ClassC();
            IClassB objectB = new ClassB(objectC);     // new ClassB();
            ClassA objectA = new ClassA(objectB);

            objectA.ActionA();
        }

        static void DILibrary()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IClassC, ClassC1>();
            services.AddSingleton<IClassB, ClassB>();
            services.AddSingleton<ClassA, ClassA>();

            IServiceProvider provider = services.BuildServiceProvider();

            var objectA = provider.GetService<ClassA>();
            objectA?.ActionA();
        }

        static void DelegateFactory()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IClassC, ClassC1>();
            services.AddSingleton<IClassB, ClassB2>((IServiceProvider provider) =>
            {
                return new ClassB2(provider.GetService<IClassC>(), "Hello from ClassB2");
            });

            services.AddSingleton<IClassB, ClassB2>(CreateB2Factory);

            var provider = services.BuildServiceProvider();

            var objectB = provider.GetService<IClassB>();
            objectB?.ActionB();
        }
        static void Main(string[] args)
        {
            DelegateFactory();
            Console.WriteLine("------------------");
        }
    }
}
