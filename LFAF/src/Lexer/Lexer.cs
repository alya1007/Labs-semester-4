using System.Text.RegularExpressions;

namespace src
{
    public class Lexer
    {
        private readonly string _input;

        public Lexer(string input)
        {
            _input = input;
        }

        private List<string> DelimitInput(string input)
        {
            List<string> result = new List<string>();
            Regex regex = new Regex(@"(\"".+?\"")|([^ ]+)");

            foreach (Match match in regex.Matches(input))
            {
                string value = match.Groups[1].Success ? match.Groups[1].Value : match.Groups[2].Value;
                if (match.Groups[1].Success)
                {
                    result.Add(value);
                }
                else
                {
                    result.Add(value.Trim('"'));
                }
            }


            return result;
        }

        public List<Token> Tokenize()
        {
            Regex identifierRegex = new Regex(@"^(?:[a-zA-Z][\w.:]*[a-zA-Z\d]|[.])$");
            Regex optionRegex = new Regex(@"^(-\w|--\w+|--\w+=\w+)$");
            Regex stringRegex = new Regex(@"\"".*?\""");

            // Split the input string into a list of arguments
            List<string> inputs = DelimitInput(_input);

            List<Token> tokens = new List<Token>();

            // Loop through each argument in the command line input
            foreach (string arg in inputs)
            {
                // Determine the category of the argument using regular expressions
                if (identifierRegex.IsMatch(arg))
                {
                    tokens.Add(new Token(TokenType.Identifier, arg));
                }
                else if (optionRegex.IsMatch(arg))
                {
                    tokens.Add(new Token(TokenType.Option, arg));
                }
                else if (stringRegex.IsMatch(arg))
                {
                    tokens.Add(new Token(TokenType.StringLiteral, arg));
                }
                else
                {
                    throw new Exception("Invalid token");
                }

            }

            return tokens;
        }

        public void PrintTokens()
        {
            List<Token> tokens = Tokenize();

            foreach (Token token in tokens)
            {
                Console.WriteLine($"{token.Type}: {token.Value}");
            }
        }
    }
}