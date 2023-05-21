using System.Text.Json;
using src;

namespace ASTNodes
{
    public class ASTNode
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

        public string ToJsonString()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            return JsonSerializer.Serialize(ToJsonObject(), options);
        }

        private object ToJsonObject()
        {
            if (Children?.Any() == true)
            {
                var jsonObject = new Dictionary<string, object?>();
                if (!string.IsNullOrEmpty(Value))
                {
                    jsonObject.Add("value", Value);
                }

                if (!string.IsNullOrEmpty(Name))
                {
                    jsonObject.Add("name", Name);
                }

                var childObjects = Children.Select(child => child.ToJsonObject());
                jsonObject.Add("children", childObjects);

                return jsonObject;
            }
            else
            {
                return new
                {
                    value = Value,
                    name = Name
                };
            }
        }
    }
}
