using System;
using System.Collections.Generic;

namespace ganit
{
    public class ShuntingYard
    {
        public static void Parse(List<Token> tokens)
        {
            var operatorStack = new Stack<Token>();
            var outputQueue = new Queue<Token>();

            foreach (var token in tokens)
            {
                if (token.type == Type.INT_LITERAL || token.type == Type.DOUBLE_LITERAL)
                {
                    outputQueue.Enqueue(token);
                }
                if (token.type == Type.FUNC_INVOKE)
                {
                    operatorStack.Push(token);
                }
                
            }
        }
        public static void Print(List<Token> tokens)
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
                Console.Write("{0} {1}", token.line, token.column);
                Console.Write("\t");
            }
            Console.WriteLine();
        }
    }
}