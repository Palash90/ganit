using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ganit
{
    public class ParseException : Exception
    {
        public ParseException(string msg) : base(msg) { }
    }
    public class Tokenizer
    {
        List<Token> tokens = new List<Token>();
        int pointer = 0;

        #region Tokenize the input
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

                    if (IsOperator(currChar) || currChar == ',')
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
        #endregion
        private List<Token> Analyzer()
        {
            List<Token> analyzedTokens = new List<Token>();

            while (Peek().type != Type.EOF)
            {
                Token currToken = Peek();
                if (currToken.value == "=")
                {
                    currToken = CheckSign(analyzedTokens, "==");
                }
                else if (currToken.value == "!")
                {
                    currToken = CheckSign(analyzedTokens, "!=");
                }
                else if (currToken.value == ">")
                {
                    currToken = CheckSign(analyzedTokens, ">=");
                }
                else if (currToken.value == "<")
                {
                    currToken = CheckSign(analyzedTokens, "<=");
                }
                else if (currToken.value == "-")
                {
                    currToken = CheckSign(analyzedTokens);
                }
                else if (IsOperator(currToken.value.ToCharArray()[0]))
                {
                    Token token = Peek();
                    token.type = Type.OPERATOR;
                    analyzedTokens.Add(token);
                    Consume();
                }
                else if (IsSeparator(currToken.value.ToCharArray()[0]))
                {
                    Token token = Peek();
                    token.type = Type.SEPARATOR;
                    analyzedTokens.Add(token);
                    Consume();
                }
                else if (IsKeyword(currToken.value))
                {
                    Token token = Peek();
                    token.type = Type.KEYWORD;
                    analyzedTokens.Add(token);
                    Consume();
                }
                else
                {
                    CheckVariableAndLiterals(analyzedTokens);
                }
            }
            return analyzedTokens;
        }

        #region Check for multi-character operators

        private Token CheckSign(List<Token> analyzedTokens, String newValue)
        {
            Token currToken;
            Token tempToken = Peek();
            tempToken.type = Type.OPERATOR;
            Consume();
            currToken = Peek();

            if (currToken.value == "=")
            {
                Consume();
                tempToken.value = newValue;
                tempToken.type = Type.OPERATOR;
            }
            analyzedTokens.Add(tempToken);
            return currToken;
        }
        private Token CheckSign(List<Token> analyzedTokens)
        {
            Token currToken;
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
            return currToken;
        }
        #endregion
        private void CheckVariableAndLiterals(List<Token> analyzedTokens)
        {
            Token token = Peek();

            var regex = "^[a-zA-Z_$][a-zA-Z_$0-9]*$";
            var match = Regex.Match(token.value, regex);

            long intLiteral;
            double doubleLiteral;


            if (Int64.TryParse(token.value, out intLiteral))
            {

                token.doubleValue = intLiteral;
                token.type = Type.INT_LITERAL;
            }
            else if (Double.TryParse(token.value, out doubleLiteral))
            {

                token.doubleValue = doubleLiteral;
                token.type = Type.DOUBLE_LITERAL;
            }
            else if (match.Success)
            {
                token.type = Type.VARIABLE;
            }
            else
            {
                Token last = PeekLast(analyzedTokens);
                if (last.value == "function")
                {
                    throw new ParseException(String.Format("Syntax error in function name line {0}, column {1}: {2}",
                                                        token.line, token.column, token.value));

                }
                else
                {
                    throw new ParseException(String.Format("Syntax error in variable name line {0}, column {1}: {2}",
                                                                            token.line, token.column, token.value));
                }

            }
            analyzedTokens.Add(token);
            Consume();
        }

        #region Token Stream Navigation
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
        #endregion

        #region Determine token type
        private bool IsSeparator(char value) => Array.Exists(LanguageConstruct.Separators, element => element == value);

        private bool IsOperator(char value) => Array.Exists(LanguageConstruct.Operators, element => element == value);

        private bool IsKeyword(string value) => Array.Exists(LanguageConstruct.Keywords, element => element == value);
        #endregion
    }
}