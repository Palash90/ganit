namespace ganit
{
    public class Token
    {
        public string token { get; set; }
        public int line { get; set; }
        public int column { get; set; }

        public static char[] Separators = { ' ', '\n' };
        public static char[] Operators = { '+', '-', '*', '\\' };
    }
}