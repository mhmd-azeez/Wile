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


                    var parser = new Parser(tokens);
                    var expr = parser.Parse();

                    var printer = new PrintVisitor();
                    var printed = expr.Accept(printer);

                    Console.WriteLine(printed);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
