
using System.Diagnostics;
using NFind;

public class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("FIND: Param format not correct");
            return;
        }
        var findOption = BuildOption(args);

        if (findOption.StringToFind != null)
        {
            System.Console.WriteLine("String to find: " + findOption.StringToFind);
        }

        var sources = LineSourceFactory.CreateInstance(findOption.Path);

        foreach (var source in sources)
        {
            ProcessSource(source, findOption);
        }
    }

    private static void ProcessSource(ILineSource source, FindOption findOption)
    {
        source = new FilterLineSource(source,
            line => findOption.IsCaseSensitive ?
                line.Text.Contains(findOption.StringToFind) :
                line.Text.ToLower().Contains(findOption.StringToFind.ToLower()));
        source.Open();
        var line = source.ReadLine();
        while (line != null)
        {
            Print(line);
            line = source.ReadLine();
        }
    }

    private static void Print(Line line)
    {
        Console.WriteLine($"[{line.LineNumber}]: {line.Text}");
    }

    public static FindOption BuildOption(string[] args)
    {
        var option = new FindOption();
        foreach (var arg in args)
        {
            switch (arg)
            {
                case "/v":
                    option.FindDontContain = true;
                    break;
                case "/c":
                    option.CountMode = true;
                    break;
                case "/n":
                    option.ShowLineNumbers = true;
                    break;
                case "/i":
                    option.IsCaseSensitive = false;
                    break;
                case "/?":
                    option.HelpMode = true;
                    break;
                default:
                    {
                        if (string.IsNullOrEmpty(option.StringToFind))
                        {
                            option.StringToFind = arg;
                        }
                        else if (string.IsNullOrEmpty(option.Path))
                        {
                            option.Path = arg;
                        }
                        else
                        {
                            throw new ArgumentException("Invalid argument: " + arg);
                        }
                        break;
                    }
            }
        }
        return option;
    }
}
