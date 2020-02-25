using System;
using System.IO;
using System.Collections.Generic;

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
                    List<Token> tokens = Tokenizer.Tokenize(text + " ");
                    ShuntingYard.Parse(tokens);
                }
                catch (FileNotFoundException f)
                {
                    Console.WriteLine(f.Message);
                }
            }
        }
    }
}
