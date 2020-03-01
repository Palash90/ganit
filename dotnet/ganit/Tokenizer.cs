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
                        value = tokenPart,
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
                            value = currChar.ToString(),
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
            tokens = tokens.Where(t => !String.IsNullOrEmpty(t.value)).ToList();
            return Analyzer();
        }

        private List<Token> Analyzer()
        {
            List<Token> analyzedTokens = new List<Token>();

            while (Peek().type != Type.EOF)
            {
                Token currToken = Peek();
                if (currToken.value == "=")
                {
                    Token tempToken = Peek();
                    tempToken.type = Type.OPERATOR;
                    Consume();
                    currToken = Peek();

                    if (currToken.value == "=")
                    {
                        Consume();
                        tempToken.value = "==";
                        tempToken.type = Type.OPERATOR;
                    }
                    analyzedTokens.Add(tempToken);
                }
                else if (currToken.value == "!")
                {
                    Token tempToken = Peek();
                    tempToken.value = "!";
                    tempToken.type = Type.OPERATOR;
                    Consume();
                    currToken = Peek();

                    if (currToken.value == "=")
                    {
                        Consume();
                        tempToken.value = "!=";
                        tempToken.type = Type.OPERATOR;
                    }
                    analyzedTokens.Add(tempToken);
                }
                else if (currToken.value == ">")
                {
                    Token tempToken = Peek();
                    tempToken.type = Type.OPERATOR;
                    Consume();
                    currToken = Peek();

                    if (currToken.value == "=")
                    {
                        Consume();
                        tempToken.value = ">=";
                        tempToken.type = Type.OPERATOR;
                    }
                    analyzedTokens.Add(tempToken);
                }
                else if (currToken.value == "<")
                {
                    Token tempToken = Peek();
                    tempToken.type = Type.OPERATOR;
                    Consume();
                    currToken = Peek();

                    if (currToken.value == "=")
                    {
                        Consume();
                        tempToken.value = ">=";
                        tempToken.type = Type.OPERATOR;
                    }
                    analyzedTokens.Add(tempToken);
                }
                else if (currToken.value == "-")
                {
                    Token tempToken = Peek();
                    Consume();
                    currToken = Peek();

                    if (currToken.value == "-")
                    {
                        Consume();
                        tempToken.value = "+";
                        tempToken.type = Type.OPERATOR;
                    }
                    else if (PeekLast(analyzedTokens).value == ")" || PeekLast(analyzedTokens).type == Type.VARIABLE)
                    {
                        Consume();
                        tempToken.value = "-";
                        tempToken.meaning = Meaning.BINARY_MINUS;
                        tempToken.type = Type.VARIABLE;
                    }
                    else if (PeekLast(analyzedTokens).type == Type.OPERATOR || PeekLast(analyzedTokens).value == "(" || PeekLast(analyzedTokens).type == Type.START)
                    {
                        tempToken.value = "-";
                        tempToken.type = Type.OPERATOR;
                        tempToken.meaning = Meaning.UNARY_MINUS;
                    }
                    analyzedTokens.Add(tempToken);
                }
                else if (IsOperator(currToken.value.ToCharArray()[0]))
                {
                    Token token = Peek();
                    token.type = Type.OPERATOR;
                    analyzedTokens.Add(token);
                    Consume();
                }
                else
                {
                    Token token = Peek();
                    token.type = Type.VARIABLE;
                    analyzedTokens.Add(token);
                    Consume();
                }

            }
            return analyzedTokens;
        }

        private Token PeekLast(List<Token> tokens)
        {
            if (tokens.Count > 0)
                return tokens[tokens.Count - 1];
            else
                return new Token { type = Type.START };
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
                    type = Type.EOF
                };
            }
        }

        private void Consume() => pointer++;

        private bool IsSeparator(char currChar) => Array.Exists(LanguageConstruct.Separators, element => element == currChar);

        private bool IsOperator(char currChar) => Array.Exists(LanguageConstruct.Operators, element => element == currChar);
    }
}