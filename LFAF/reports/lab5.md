# Parser & Building an Abstract Syntax Tree.

## Course: Formal Languages & Finite Automata

## Author: Konjevic Alexandra FAF-213

<br>

## Introduction

Parsing and building an Abstract Syntax Tree (AST) are integral parts of programming language processing and compiler design. These techniques provide a deeper understanding of the structure and semantics of source code, enabling efficient analysis, transformation, and execution of programs. In this report, we explore the concepts, techniques, and benefits associated with parsing and constructing an AST.

Parsing involves analyzing a sequence of symbols according to a defined grammar or set of rules. It verifies the syntactic correctness of the input and extracts meaningful information from it. The parsed output serves as the foundation for subsequent stages of language processing.

The Abstract Syntax Tree is a hierarchical representation of the parsed source code. It captures the structure of the program in a concise and abstract manner, removing unnecessary details while preserving essential semantic information. Each node in the AST represents a language construct, and the relationships between nodes depict the program's structure.

<br>

## Objectives

- Get familiar with parsing, what it is and how it can be programmed.
- Get familiar with the concept of AST.
- In addition to what has been done in the 3rd lab work do the following:
    - In case there wasn't a type that denotes the possible types of tokens:
        - Create a type TokenType (like an enum) that can be used in the lexical analysis to categorize the tokens.
        - Use regular expressions to identify the type of the token.
    - Implement the necessary data structures for an AST that could be used for the text processed in the 3rd lab work.
    - Implement a simple parser program that could extract the syntactic information from the input text.

<br>

## Implementation Description

First of all, in the third laboratory work I implemented a lexer for a general command line interface, whereas in this laboratory work I created a parser for specific commands: git commands.

### Token class
I changed the Token class to include some additional types of tokens: `Git` (as every git command starts with the word `git`), `Command` and `Argument`.

### ASTNode class
I created a class for the nodes of the AST. Each node has a type, a value, a name and a list of children. The type of the node is the type of the token, the value is the value of the token and the children are the arguments of the command.
```
public ASTNode(TokenType tokenType, string? value, string? name)
{
    TokenType = tokenType;
    Value = value;
    Name = name;
    Children = new List<ASTNode>();
}
```
ASTNode class also has a method for adding children to the node:
```
public void AddChild(ASTNode child)
{
    Children?.Add(child);
}
```
And methods to convert the AST to a json object (to make the output more readable):
```
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
```

### RootNode class
I created a class for the root node of the AST:
```
public RootNode(string? value) : base(TokenType.Command, value, "command")
{
    var bodyToken = new ASTNode(TokenType.Command, null, "body");
    Children.Add(bodyToken);
}
```

### GitCommandParser class
I created a class for the parser. It is instantiated with a lexer and the constructor takes the input string and tokenizes it:
```
public GitCommandParser(string input)
{
    _lexer = new Lexer(input);
    _tokens = _lexer.Tokenize();
    _currentTokenIndex = 0;
}
```
#### Parse() method
Next, there is a `Parse()` method, that calls the `ParseCommand()` method and returns the root node of the AST:
```
public ASTNode Parse()
{
    return ParseCommand();
}
```
#### ParseCommand() method
The `ParseCommand()` method is responsible for parsing a command in a Git command sequence. The method starts with checking if the current token matches the type `Identifier` and if its value is `git`. This condition ensures that the command sequence starts with the `git` keyword. If the condition is true, the method proceeds with parsing the command. Otherwise, it throws an exception. If the condition is true, the `git` token is consumed.

A new `ASTNode` called `rootNode` is created with the type `TokenType.Command` and the value of the consumed `gitToken`. This node represents the root of the AST for the command.

The method then checks if there are more tokens and if the next token is an identifier (another command in the sequence). If this condition is true, the method proceeds to parse the next command.

The next command token is consumed and a new `ASTNode` called `newNode` is created to represent this command. This `newNode` is added as a child to the `rootNode` representing the initial `git` command.
```
private ASTNode ParseCommand()
{
    if (Match(TokenType.Identifier) && _tokens[_currentTokenIndex].Value == "git")
    {
        var gitToken = Consume(TokenType.Identifier);
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
```
#### ParseBody() method
First of all, there is initialized the `bodyNode` which will represent the body of the command. Then, there is a while loop that continues as long as there are more tokens to be parsed. Inside the loop, program checks if the current token matches the `TokenType.Option`. If it does, there is created an `optionNode`, and the value is obtained by consuming the token.

If the current token is an option, we proceed to consume one or two arguments that follow the option. The first argument is consumed and an `argumentNode` is created to represent it. The same process is repeated for the second argument or string literal, if it exists.  Both argument nodes are added as children to the `optionNode`. Finally, the `optionNode` is added as a child to the `bodyNode`.

```
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
    //...
}
```
If the current token is an argument and not an option, it is consumed and an `argumentNode` to represent it is created and added as a child to the `bodyNode`.
```
else if (Match(TokenType.Argument))
{
    // Consume additional arguments
    var argumentNode = new ASTNode(TokenType.Argument, Consume(TokenType.Argument).Value, "argument");
    bodyNode.AddChild(argumentNode);
}
```
If the current token does not match TokenType.Option or TokenType.Argument, it is considered an invalid token, and an exception is thrown.
```
else
{
    throw new Exception("Invalid token");
}
```
Finally, the `bodyNode` is returned.

#### Match() method
The `Match()` method checks if the current token matches the given type. If it does, it returns true, otherwise it returns false.
```
private bool Match(TokenType type)
{
    if (_currentTokenIndex >= _tokens.Count)
    {
        return false;
    }
    return _tokens[_currentTokenIndex].Type.Equals(type);
}
```

#### Consume() method
The `Consume()` method checks if the current token matches the given type. If it does, it returns the token and increments the current token index. Otherwise, it throws an exception.
```
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
```

## Results
Taking in account that git command start always with the word `git` and are followed by another command (we cannot have a prompt consisting only of word `git`), in the resulting AST there will always be a root node with the type `command` and the value `git`, with another command as a child, and only after that there will be the `body` node with the options and arguments.

Input: `git config --global user.name \"firstname lastname\"`

Output:

```
{
  "value": "git",
  "name": "command",
  "children": [
    {
      "value": "config",
      "name": "command"
    },
    {
      "name": "body",
      "children": [
        {
          "value": "--global",
          "name": "option",
          "children": [
            {
              "value": "user.name",
              "name": "argument"
            },
            {
              "value": "\u0022firstname lastname\u0022",
              "name": "string literal"
            }
          ]
        }
      ]
    }
  ]
}
```

Input: `git init`

Output:

```
{
  "value": "git",
  "name": "command",
  "children": [
    {
      "value": "init",
      "name": "command"
    }
  ]
}
```

Input: `git commit -m \"[descriptive message]\"`

Output:

```
{
  "value": "git",
  "name": "command",
  "children": [
    {
      "value": "commit",
      "name": "command"
    },
    {
      "name": "body",
      "children": [
        {
          "value": "-m",
          "name": "option",
          "children": [
            {
              "value": "\u0022[descriptive message]\u0022",
              "name": "string literal"
            }
          ]
        }
      ]
    }
  ]
}
```

Input: `git stash list`

Output:

```
{
  "value": "git",
  "name": "command",
  "children": [
    {
      "value": "stash",
      "name": "command"
    },
    {
      "name": "body",
      "children": [
        {
          "value": "list",
          "name": "argument"
        }
      ]
    }
  ]
}
```

Input: `git log --stat -M`

Output:

```
{
  "value": "git",
  "name": "command",
  "children": [
    {
      "value": "log",
      "name": "command"
    },
    {
      "name": "body",
      "children": [
        {
          "value": "--stat",
          "name": "option"
        },
        {
          "value": "-M",
          "name": "option"
        }
      ]
    }
  ]
}
```
Input: `git invalid-command`

Output: `Unhandled exception. System.Exception: Invalid token`

Input: `git branch feature branch-name`

Output: `Unhandled exception. System.Exception: Invalid token`

Input: `git push origin branch-name/file.txt`

Output: `Unhandled exception. System.Exception: Invalid token`

## Conclusion

In conclusion, in this laboratory work I focused on implementing a parser for git commands in C#, resulting in the generation of an Abstract Syntax Tree (AST) representing the structure of the token streams. The parser was designed to handle a specific set of rules for git commands, ensuring that the input commands adhere to the expected syntax.

Throughout the implementation process, I utilized various techniques such as token matching, consuming tokens, and constructing AST nodes. The parser methodically traversed the token streams, validating the presence of the "git" command as the initial keyword and then parsing the subsequent arguments and options based on the specified rules.

By constructing the AST, I gained a structured representation of the parsed git commands, enabling further analysis, manipulation, and interpretation of the command structures. The AST provided a clear hierarchical view of the command components, such as the command itself, followed by the body containing arguments and options.

In summary, this laboratory work has provided valuable insights into the process of building a parser for git commands, showcasing the significance of constructing an AST for representing the parsed structures. The implemented parser offers a solid starting point for further exploration and utilization in the realm of parsing and related applications.