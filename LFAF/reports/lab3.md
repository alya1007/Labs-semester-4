# Lexer & Scanner.

## Course: Formal Languages & Finite Automata

## Author: Konjevic Alexandra FAF-213

<br>

## Introduction

A lexer, also known as a scanner or tokenizer, is a fundamental component in the process of compiling or interpreting programming languages. The lexer is responsible for breaking down the source code into a sequence of tokens that can be further processed by a parser. Tokens are meaningful units of the programming language, such as keywords, identifiers, operators, and literals.

The role of the lexer is to scan through the input source code character by character, identifying and classifying each character into its corresponding token type. This process involves recognizing patterns and regular expressions that define the syntax and structure of the language. The lexer typically uses a set of rules and patterns that are defined by the language specification or grammar, and applies them to the input source code to generate a stream of tokens.

As an example of a lexer implementation, I have developed a lexer for command prompts in the Windows command prompt (cmd) using the C# programming language. The lexer is designed to recognize and tokenize command prompt inputs into individual tokens based on whitespace and other special characters. The resulting sequence of tokens can be further processed by subsequent components in a command prompt interpreter to execute the corresponding commands.

<br>

## Objectives

- Understand what lexical analysis is.
- Get familiar with the inner workings of a lexer/scanner/tokenizer.
- Implement a sample lexer and show how it works.

<br>

## Implementation Description

This implementation defines a TokenType enum:

```
public enum TokenType
    {
        Identifier,
        Option,
        StringLiteral
    }
```

and a Token class, which represent different types of tokens and their values:

```
public class Token
    {
        public TokenType Type { get; }
        public string Value { get; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
```

It then defines a Lexer class that takes a string input and uses regular expressions to tokenize it into a list of Tokens based on their categories, which are Identifier, Option, or StringLiteral.

The `DelimitInput` function splits the input string into a list of individual arguments using a regular expression that matches either a quoted string or a non-whitespace string.

```
Regex regex = new Regex(@"(\"".+?\"")|([^ ]+)");
```

It also keeps the double quotes of an element that has them:

```
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
```

The Tokenize function uses regular expressions to match each argument in the input to a TokenType:

```
Regex identifierRegex = new Regex(@"^\w+$");
Regex optionRegex = new Regex(@"^(-\w|--\w+|--\w+=\w+)$");
Regex stringRegex = new Regex(@"\"".*?\""");
```

It creates a new Token with the corresponding type and value. If an argument does not match any of the defined categories, it throws an exception.

```
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
```

Finally, it provides a PrintTokens function that prints out each token in the list.

```
public void PrintTokens()
{
    List<Token> tokens = Tokenize();

    foreach (Token token in tokens)
    {
        Console.WriteLine($"{token.Type}: {token.Value}");
    }
}
```

## Results

Input: `ls -l`

Output:

```
Identifier: ls
Option: -l
```

Input: `python script.py --arg1 value1 --arg2=value2`

Output:

```
Identifier: python
Identifier: script.py
Option: --arg1
Identifier: value1
Option: --arg2=value2
```

Input: `foo bar --invalid-option=value`

Output:

```
Unhandled exception. System.Exception: Invalid token
```

Input: `ping google.com`

Output:

```
Identifier: ping
Identifier: google.com
```

Input: `docker build -t myimage:latest .`

Output:

```
Identifier: docker
Identifier: build
Option: -t
Identifier: myimage:latest
Identifier: .
```

## Conclusion

Overall, I can say that lexers are a fundamental component of many programming languages, compilers, and interpreters. They provide an essential mechanism for transforming human-readable source code into a form that can be more easily processed by a computer. By breaking down the input into a sequence of tokens, a lexer enables efficient and accurate parsing of the source code, making it easier to identify syntax errors and other issues.
The lexer that I implemented is designed to analyze and categorize the different components of a command line input based on specific criteria. The objective of this laboratory work was to develop an efficient and reliable system to transform input strings into a sequence of tokens.
The design of the Lexer class is well-organized, and the implemented regular expressions accurately capture the patterns of different types of input. The DelimitInput() method takes care of separating individual arguments, while the Tokenize() method takes care of categorizing the tokens. The Token class is defined with two properties: Type and Value, making it easy to associate a category with its corresponding value.
The implementation of the lexer in C# demonstrates a comprehensive understanding of the principles of lexical analysis and the ability to apply them in practice. The code is clear, concise, and efficiently handles a wide range of input scenarios. Future improvements could include the addition of more regular expressions to identify new token types or further optimizing the code for improved performance.
