using src;

namespace ASTNodes
{
    class RootNode : ASTNode
    {
        public RootNode(string? value) : base(TokenType.Command, value, "command")
        {
            var bodyToken = new ASTNode(TokenType.Command, null, "body");
            Children.Add(bodyToken);
        }
    }
}