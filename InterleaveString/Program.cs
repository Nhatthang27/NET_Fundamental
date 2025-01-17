using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        string A = "ancd";
        string B = "oeinc";

        string result = InterleaveStrings(A, B);
        Console.WriteLine(result); // Kết quả: aonecidnc
    }

    public static string InterleaveStrings(string a, string b)
    {
        int len_a = a.Length;
        int len_b = b.Length;
        int max_len = Math.Max(len_a, len_b);
        var newString = new StringBuilder();
        for (int i = 0; i < max_len; i++)
        {
            if (i < len_a) newString.Append(a[i]);
            if (i < len_b) newString.Append(b[i]);
        }
        return newString.ToString();
    }
}
