using System;
using System.Collections.Generic;
using ASTNodes;
using src;

namespace ParserNamespace
{
    public class GitCommandParser
    {
        private readonly Lexer _lexer;
        private readonly List<Token> _tokens;
        private int _currentTokenIndex;

        public GitCommandParser(string input)
        {
            _lexer = new Lexer(input);
            _tokens = _lexer.Tokenize();
            _currentTokenIndex = 0;
        }

        public ASTNode Parse()
        {
            return ParseCommand();
        }

        private ASTNode ParseCommand()
        {
            if (Match(TokenType.Identifier) && _tokens[_currentTokenIndex].Value == "git")
            {
                var gitToken = Consume(TokenType.Identifier); // Consume "git" command
                var rootNode = new ASTNode(TokenType.Command, gitToken.Value, "command");

                if (_currentTokenIndex < _tokens.Count && Match(TokenType.Identifier))
                {
                    var commandToken = Consume(TokenType.Identifier);
                    var newNode = new ASTNode(TokenType.Command, commandToken.Value, "command");
                    rootNode.AddChild(newNode);

                    if (_currentTokenIndex < _tokens.Count)
                    {
                        var bodyNode = ParseBody();
                        rootNode.AddChild(bodyNode);
                    }

                    return rootNode;
                }

                throw new Exception("Invalid command");
            }

            throw new Exception("Invalid command");
        }

        private ASTNode ParseBody()
        {
            var bodyNode = new ASTNode(TokenType.Command, null, "body");

            while (_currentTokenIndex < _tokens.Count)
            {
                if (Match(TokenType.Option))
                {
                    var optionNode = new ASTNode(TokenType.Option, Consume(TokenType.Option).Value, "option");

                    // Consume one or two arguments after the option
                    if (Match(TokenType.Argument))
                    {
                        var argumentNode = new ASTNode(TokenType.Argument, Consume(TokenType.Argument).Value, "argument");
                        optionNode.AddChild(argumentNode);
                    }

                    if (Match(TokenType.Argument))
                    {
                        var argumentNode = new ASTNode(TokenType.Argument, Consume(TokenType.Argument).Value, "argument");
                        optionNode.AddChild(argumentNode);
                    }

                    if (Match(TokenType.StringLiteral))
                    {
                        var stringLiteralNode = new ASTNode(TokenType.StringLiteral, Consume(TokenType.StringLiteral).Value, "string literal");
                        optionNode.AddChild(stringLiteralNode);
                    }

                    bodyNode.AddChild(optionNode);
                }
                else if (Match(TokenType.Argument))
                {
                    // Consume additional arguments
                    var argumentNode = new ASTNode(TokenType.Argument, Consume(TokenType.Argument).Value, "argument");
                    bodyNode.AddChild(argumentNode);
                }
                else
                {
                    throw new Exception("Invalid token");
                }
            }

            return bodyNode;
        }

        private bool Match(TokenType type)
        {
            if (_currentTokenIndex >= _tokens.Count)
            {
                return false;
            }

            return _tokens[_currentTokenIndex].Type.Equals(type);
        }


        private Token Consume(TokenType type)
        {
            if (Match(type))
            {
                var token = _tokens[_currentTokenIndex];
                _currentTokenIndex++;
                return token;
            }

            throw new Exception($"Unexpected token. Expected {type}");
        }
    }
}
