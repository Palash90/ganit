using System.Collections.Generic;
using System;
using System.Linq;

namespace ganit
{
    public class Tokenizer
    {
        public List<Token> Tokenize(string text)
        {
            List<Token> tokens = new List<Token>();
            int line = 1;
            int column = 0;
            int index = 0;
            int tokenStartColumn = 0;

            string tokenPart = "";

            while (index < text.Length)
            {
                char currChar = text[index];
                column++;

                if (currChar == '\n')
                {
                    column = 0;
                }
                if (IsSeparator(currChar) || IsOperator(currChar))
                {
                    Token token = new Token
                    {
                        token = tokenPart,
                        line = line,
                        column = tokenStartColumn + 1
                    };
                    tokenPart = "";
                    tokenStartColumn = column;
                    tokens.Add(token);

                    if (IsOperator(currChar))
                    {
                        Token opearatorToken = new Token
                        {
                            token = currChar.ToString(),
                            line = line,
                            column = column
                        };
                        tokens.Add(opearatorToken);
                    }
                    if (currChar == '\n')
                    {
                        line++;
                    }
                }
                else
                {
                    tokenPart = tokenPart + currChar;
                }

                index++;
            }
            tokens = tokens.Where(t => !String.IsNullOrEmpty(t.token)).ToList();
            return tokens;
        }

        private static bool IsSeparator(char currChar)
        {
            return Array.Exists(Token.Separators, element => element == currChar);
        }

        private static bool IsOperator(char currChar)
        {
            return Array.Exists(Token.Operators, element => element == currChar);
        }
    }
}