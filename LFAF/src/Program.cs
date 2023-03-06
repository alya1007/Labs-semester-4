using System;
namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            var nonTerminalSymbols = new List<char> { 'S', 'B', 'C' };
            var terminalSymbols = new List<char> { 'a', 'b', 'c' };
            var startingSymbol = 'S';
            var rules = new Dictionary<string, List<string>> {
            {"S", new List<string> {"aB"}},
            {"B", new List<string> {"aC", "bB"}},
            {"C", new List<string> {"bB", "c", "aS"}}
            };

            var grammar = new Grammar(nonTerminalSymbols, terminalSymbols, startingSymbol, rules);

        }
    }
}