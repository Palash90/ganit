namespace ganit
{
    public enum Type
    {
        OPERATOR, VARIABLE, KEYWORD, START, EOF
    }

    public enum Meaning
    {
        NA = 0,
        PLUS, BINARY_MINUS, UNARY_MINUS, MUL, DIV, POW, MODULO,
        ASSIGNMENT, EQ, INV, NEQ, GT, GE, LT, LE

    }

    public class LanguageConstruct
    {

        public static char[] Separators = { ' ', '\n' };
        public static char[] Operators = {
                            '+', '-', '*', '/', '%', '^',
                            '=', '(', ')', '{', '}',
                            '[', ']', ',', ';',
                            '<', '>', '!'
                            };
    }
    public class Token
    {
        public string value { get; set; }
        public int line { get; set; }
        public int column { get; set; }
        public Type type { get; set; }

        public Meaning meaning { get; set; }
    }
}