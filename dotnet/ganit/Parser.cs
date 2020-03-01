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
                Console.Write("{0} {1} {2} {3} \t", token.token, token.type, token.line, token.column);
            }
            Console.WriteLine();
        }
    }
}