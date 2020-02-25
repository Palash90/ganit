using System;
using System.Collections.Generic;

namespace ganit
{
    public class ShuntingYard
    {
        public static void Parse(List<Token> tokens)
        {
            foreach (var token in tokens)
            {
                Console.WriteLine("{0} {1} {2}", token.token, token.line, token.column);
            }
        }
    }
}