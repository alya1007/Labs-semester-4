using System;
using System.Text.RegularExpressions;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            // string input = "docker build -t myimage:latest .";
            // Lexer lexer = new Lexer(input);
            // lexer.PrintTokens();

            // initialize the grammar
            var nonTerminalSymbols = new List<string> { "S", "A", "B", "C" };
            var terminalSymbols = new List<string> { "a", "b", "c" };
            var startingSymbol = "S";
            var rules = new List<Production> {
                new Production(new string[] { "S" }, new string[] { "A", "B", "C", "D", "A" }),
                new Production(new string[] { "A" }, new string[] { "" }),
                new Production(new string[] { "B" }, new string[] { "b" }),
                new Production(new string[] { "C" }, new string[] { "c" }),
                new Production(new string[] { "C" }, new string[] { "" }),
                new Production(new string[] { "B" }, new string[] { "" }),
            };
            var normalizedGrammar = new NormalizedGrammar(nonTerminalSymbols, terminalSymbols, startingSymbol, rules);
            var list = normalizedGrammar.GenerateStringArrays(normalizedGrammar.Rules[1].LeftSide[0], normalizedGrammar.Rules[0].RightSide);
            var listWithoutDuplicates = normalizedGrammar.RemoveDuplicateArrays(list);
            // print all elements of list
            foreach (var item in list)
            {
                foreach (var element in item)
                {
                    Console.Write(element + " ");
                }
                Console.WriteLine();
            }
        }
    }
}