using System;

namespace Wile.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.Write("Source: ");
                    var source = Console.ReadLine();
                    var scanner = new Scanner(source);
                    var tokens = scanner.ScanTokens();

                    foreach (var token in tokens)
                    {
                        Console.WriteLine(token);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
