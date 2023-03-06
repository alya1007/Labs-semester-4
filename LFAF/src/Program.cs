using System;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            var nonTerminalSymbols = new List<string> { "S", "B", "C" };
            var terminalSymbols = new List<char> { 'a', 'b', 'c' };
            var startingSymbol = 'S';
            var rules = new Dictionary<string, List<string>> {
            {"S", new List<string> {"aB"}},
            {"B", new List<string> {"aC", "bB"}},
            {"C", new List<string> {"bB", "c", "aS"}}
            };

            var grammar = new RegularGrammar(nonTerminalSymbols, terminalSymbols, startingSymbol, rules);
            // generate 5 strings
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(grammar.GenerateString());
            }

            var automaton = grammar.ConvertToFiniteAutomaton();
            Console.WriteLine(automaton.CanGenerateString("aac"));
        }
    }
}