using ASTNodes;
using src;

namespace ParserNamespace
{
    class Parser
    {
        private readonly List<Token> tokens;
        private int position;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            position = 0;
        }

        private bool IsAtEnd()
        {
            return position >= tokens.Count;
        }

        private Token Peek()
        {
            return tokens[position];
        }

        private Token Advance()
        {
            if (!IsAtEnd())
            {
                position++;
            }
            return tokens[position - 1];
        }

        private bool Check(TokenType type)
        {
            if (IsAtEnd())
            {
                return false;
            }
            return tokens[position].Type == type;
        }

        private Token Consume(TokenType type, string? message = null)
        {
            if (Check(type))
            {
                return Advance();
            }
            throw new Exception(message);
        }

        private ASTNode CreateNode(Token token, string? name = null)
        {
            var nodeName = name ?? token.Type.ToString();
            return new ASTNode(token.Type, token.Value, nodeName);
        }

        public void Parse()
        {
            if (tokens.Count == 0)
            {
                throw new Exception("No tokens to parse.");
            }
            if (tokens.Count > 5)
            {
                throw new Exception("Too many tokens to parse.");
            }
            ASTNode root = new RootNode(tokens.Select(token => token.Value).Aggregate((acc, token) => acc + " " + token));
            ASTNode body = root.Children[0];
            if (tokens[0].Value != "git")
            {
                throw new Exception($"Invalid command \"{tokens[0].Value}\". Expected: git.");
            }
            if (tokens.Count == 1)
            {
                throw new Exception("No command to parse.");
            }
            body.AddChild(CreateNode(tokens[0], "program"));
            position++;
            body.AddChild(CreateNode(Consume(TokenType.Identifier, $"Expected command instead of \"{tokens[position].Value}\"")));
            if (tokens[position].Type == TokenType.Argument)
            {
                body.AddChild(CreateNode(Consume(TokenType.Argument)));
                if (!IsAtEnd())
                {
                    if (tokens[position].Type == TokenType.Identifier)
                    {
                        throw new Exception($"Invalid argument \"{tokens[position].Value}\". Expected: option/argument/string literal.");
                    }
                    body.AddChild(CreateNode(Consume(tokens[position].Type)));
                }
                if (!IsAtEnd())
                {
                    if (tokens[position - 1].Type != TokenType.Option)
                    {
                        throw new Exception($"Invalid argument \"{tokens[position - 1].Value}\". Expected option.");
                    }
                    else
                    {
                        body.AddChild(CreateNode(Consume(TokenType.Argument, $"Expected argument instead of \"{tokens[position].Value}\"")));
                    }
                }

            }
            else if (tokens[position].Type == TokenType.Option)
            {
                body.AddChild(CreateNode(Consume(TokenType.Option)));
            }
            // output the children of the root node
            foreach (var child in root.Children)
            {
                Console.WriteLine(child.Name);
                foreach (var grandchild in child.Children)
                {
                    Console.WriteLine(grandchild.Name);
                }
            }
        }
    }
}