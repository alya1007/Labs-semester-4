using System;
using System.Text.RegularExpressions;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            var nonTerminalSymbols = new List<string> { "S", "A", "B", "C", "E" };
            var terminalSymbols = new List<string> { "a", "b" };
            var startingSymbol = "S";
            var rules = new List<Production> {
                new Production(new string[] { "S" }, new string[] { "a", "B" }),
                new Production(new string[] { "S" }, new string[] { "A", "C" }),
                new Production(new string[] { "A" }, new string[] { "a" }),
                new Production(new string[] { "A" }, new string[] { "A", "C", "S", "C" }),
                new Production(new string[] { "A" }, new string[] { "B", "C" }),
                new Production(new string[] { "B" }, new string[] { "b" }),
                new Production(new string[] { "B" }, new string[] { "a", "A" }),
                new Production(new string[] { "C" }, new string[] { "" }),
                new Production(new string[] { "C" }, new string[] { "B", "A" }),
                new Production(new string[] { "E" }, new string[] { "b", "B" }),
            };
            var grammar = new Grammar(nonTerminalSymbols, terminalSymbols, startingSymbol, rules);
            grammar.Normalize();
            grammar.PrintAllProductions();
        }
    }
}