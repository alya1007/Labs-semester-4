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
            var rules = new Dictionary<char, List<string>> {
            {'S', new List<string> {"aB"}},
            {'B', new List<string> {"aC", "bB"}},
            {'C', new List<string> {"bB", "c", "aS"}}
            };

            var grammar = new Grammar(nonTerminalSymbols, terminalSymbols, startingSymbol, rules);

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(grammar.GenerateString());
            }

            var automaton = grammar.ConvertToFiniteAutomaton();

            // Print the transitions of the automaton

            // foreach (var transition in automaton.Transitions)
            // {
            //     Console.WriteLine($"({transition.Key.Item1}, {transition.Key.Item2}) -> {transition.Value}");
            // }

            // Test the CanGenerateString method for some strings
            var inputs = new List<string> { "abbabaaaaaabbbaaaaaabbbbaaaaaabbaaabbac", "aac", "abac", "asdss", "", "abb" };
            foreach (var input in inputs)
            {
                var canGenerate = automaton.CanGenerateString(input);
                Console.WriteLine($"Input '{input}': {canGenerate}");
            }
        }
    }
}