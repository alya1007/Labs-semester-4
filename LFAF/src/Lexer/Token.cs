namespace src
{
    public enum TokenType
    {
        Command,
        Identifier,
        Option,
        Argument,
        StringLiteral,
        Git // Added the "git" command

    }
    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}