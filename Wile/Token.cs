namespace Wile
{
    public class Token
    {
        public Token(TokenType type, string lexeme, object literal, int line)
        {
            Lexeme = lexeme;
            Type = type;
            Literal = literal;
            Line = line;
        }

        public string Lexeme { get; }
        public TokenType Type { get; }
        public object Literal { get; }
        public int Line { get; }

        public override string ToString()
        {
            return $"{Type} {Lexeme} {Literal}";
        }
    }
}
