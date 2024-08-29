namespace CS009_Event
{
    class DataInput : EventArgs
    {
        public int Number { get; set; }
        public DataInput(int number)
        {
            Number = number;
        }
    }
    class UserInput
    {
        public event Action<int> NumberInputAction; //void delegate
        public event EventHandler NumberInputEvent; //void delegate
                                                    //public delegate void EventHandler(object sender, EventArgs e);
        public void Input()
        {
            while (true)
            {
                string s = Console.ReadLine();
                int i = int.Parse(s);
                //NumberInputAction?.Invoke(i);
                NumberInputEvent?.Invoke(this, new DataInput(i));
            }
        }
    }

    class Sum
    {

        public void Sub(UserInput userInput)
        {
            //userInput.NumberInputAction += PrintSum;
            userInput.NumberInputEvent += (sender, e) => PrintSum((e as DataInput).Number);
        }
        public void PrintSum(int i)
        {
            Console.WriteLine((i + 1) * i / 2);
        }
    }

    class Square
    {
        public void Sub(UserInput userInput)
        {
            //userInput.NumberInputAction += PrintSquare;
            userInput.NumberInputEvent += (sender, e) => PrintSquare((e as DataInput).Number);
        }
        public void PrintSquare(int i)
        {
            Console.WriteLine(i * i);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            UserInput userInput = new(); //publisher

            Sum sum = new(); //subcriber
            sum.Sub(userInput);

            Square square = new(); //subcriber
            square.Sub(userInput);

            userInput.Input();
        }
    }
}
