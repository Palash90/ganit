using System;
using System.IO;

namespace ganit
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: ganit <ganit-source-file>");
            }
            else
            {
                try
                {
                    string text = System.IO.File.ReadAllText(args[0]);
                    Tokenizer tokenizer = new Tokenizer();
                    foreach (var token in tokenizer.Tokenize(text+" ")){
                        Console.WriteLine("{0} {1} {2}",token.token, token.line, token.column);
                    }
                }
                catch (FileNotFoundException f)
                {
                    Console.WriteLine(f.Message);
                }
            }
        }
    }
}
