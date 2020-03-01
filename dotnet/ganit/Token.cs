namespace ganit
{
    public class LanguageConstruct {
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
        public string token { get; set; }
        public int line { get; set; }
        public int column { get; set; }

        public string type {get; set;}
    }
}