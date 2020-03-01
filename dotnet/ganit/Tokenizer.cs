using System.Collections.Generic;
using System;
using System.Linq;

namespace ganit
{
    public class Tokenizer
    {
        List<Token> tokens = new List<Token>();
        int pointer = 0;

        public List<Token> Tokenize(string text)
        {
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
            return Analyzer();
        }

        private List<Token> Analyzer()
        {
            List<Token> analyzedTokens = new List<Token>();

            while (Peek().type != "EOF")
            {
                Token currToken = Peek();
                if (currToken.token == "=")
                {
                    Token tempToken = Peek();
                    tempToken.type = "ASSIGNMENT";
                    Consume();
                    currToken = Peek();

                    if (currToken.token == "=")
                    {
                        Consume();
                        tempToken.token = "==";
                        tempToken.type = "EQ";
                    }
                    analyzedTokens.Add(tempToken);
                }
                else if (currToken.token == "!")
                {
                    Token tempToken = Peek();
                    tempToken.type = "Negation";
                    Consume();
                    currToken = Peek();

                    if (currToken.token == "=")
                    {
                        Consume();
                        tempToken.token = "!=";
                        tempToken.type = "NEQ";
                    }
                    analyzedTokens.Add(tempToken);
                }
                else if (currToken.token == ">")
                {
                    Token tempToken = Peek();
                    tempToken.type = "GT";
                    Consume();
                    currToken = Peek();

                    if (currToken.token == "=")
                    {
                        Consume();
                        tempToken.token = ">=";
                        tempToken.type = "GE";
                    }
                    analyzedTokens.Add(tempToken);
                }
                else if (currToken.token == "<")
                {
                    Token tempToken = Peek();
                    tempToken.type = "LT";
                    Consume();
                    currToken = Peek();

                    if (currToken.token == "=")
                    {
                        Consume();
                        tempToken.token = "<=";
                        tempToken.type = "LE";
                    }
                    analyzedTokens.Add(tempToken);
                }
                else if (currToken.token == "-")
                {
                    Token tempToken = Peek();
                    Consume();
                    currToken = Peek();

                    if (currToken.token == "-")
                    {
                        Consume();
                        tempToken.token = "+";
                        tempToken.type = "Operator";
                    }
                    else if (PeekLastToken(analyzedTokens).token == ")" || PeekLastToken(analyzedTokens).type == "Var")
                    {
                        Consume();
                        tempToken.token = "-";
                        tempToken.type = "Binary Minus";
                    }
                    else if (PeekLastToken(analyzedTokens).type == "Operator" || PeekLastToken(analyzedTokens).token == "(" || PeekLastToken(analyzedTokens).type == "START")
                    {
                        tempToken.token = "-";
                        tempToken.type = "Unary Minus";
                    }
                    analyzedTokens.Add(tempToken);
                }
                else if (IsOperator(currToken.token.ToCharArray()[0]))
                {
                    Token token = Peek();
                    token.type = "Operator";
                    analyzedTokens.Add(token);
                    Consume();
                }
                else
                {
                    Token token = Peek();
                    token.type = "Var";
                    analyzedTokens.Add(token);
                    Consume();
                }

            }
            return analyzedTokens;
        }

        private Token PeekLastToken(List<Token> tokens)
        {
            if (tokens.Count > 0)
                return tokens[tokens.Count - 1];
            else
                return new Token { type = "START" };
        }
        private Token PeekLast()
        {
            if (pointer > 0)
            {
                return tokens[pointer - 1];
            }
            else
            {
                return new Token
                {
                    type = "START"
                };
            }
        }

        private Token Peek()
        {
            if (pointer < tokens.Count)
            {
                return tokens[pointer];
            }
            else
            {
                return new Token
                {
                    type = "EOF"
                };
            }
        }
        private void Consume()
        {
            pointer++;
        }

        private bool IsSeparator(char currChar)
        {
            return Array.Exists(LanguageConstruct.Separators, element => element == currChar);
        }

        private bool IsOperator(char currChar)
        {
            return Array.Exists(LanguageConstruct.Operators, element => element == currChar);
        }
    }
}