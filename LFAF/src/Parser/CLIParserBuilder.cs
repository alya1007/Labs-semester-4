namespace ParserNamespace
{
    public class CLIParserBuilder
    {
        private readonly CLIParser parser;

        public CLIParserBuilder()
        {
            parser = new CLIParser();
        }

        public CLIParserBuilder WithOption(string option)
        {
            parser.Option = option;
            return this;
        }

        public CLIParserBuilder WithFlag(string flag)
        {
            parser.Flag = flag;
            return this;
        }

        public CLIParserBuilder WithArgument(string argument)
        {
            parser.Argument = argument;
            return this;
        }

        public CLIParserBuilder WithIdentifier(string identifier)
        {
            parser.Identifier = identifier;
            return this;
        }

        public CLIParserBuilder WithStringLiteral(string stringLiteral)
        {
            parser.StringLiteral = stringLiteral;
            return this;
        }

        public CLIParser Build()
        {
            return parser;
        }
    }
}
