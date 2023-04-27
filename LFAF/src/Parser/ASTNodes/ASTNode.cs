using src;

namespace ASTNodes
{
    class ASTNode
    {
        public TokenType TokenType { get; set; }
        public string? Value { get; set; }
        public List<ASTNode> Children { get; set; }
        public string? Name { get; set; }

        public ASTNode(TokenType tokenType, string? value, string? name)
        {
            TokenType = tokenType;
            Value = value;
            Name = name;
            Children = new List<ASTNode>();
        }

        public void AddChild(ASTNode child)
        {
            Children?.Add(child);
        }
    }
}