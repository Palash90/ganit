using System;
using System.Collections.Generic;

namespace ganit
{
    public class ShuntingYard
    {
        public static void Parse(List<Token> tokens)
        {
            int index = 1;
            foreach (var token in tokens)
            {
                if (token.line > index)
                {
                    Console.WriteLine("\n");
                    index++;
                }
                Console.Write("{0} {1} {2} ", token.value, token.type, token.meaning);
                //Console.Write("{0} {1}", token.line, token.column);
                Console.Write("\t");
            }
            Console.WriteLine();
        }
    }
}