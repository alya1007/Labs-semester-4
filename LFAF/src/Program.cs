using System;

namespace src
{
    class Program
    {

        static public Dictionary<(string, char), string> GetTransitions(Dictionary<char, List<string>> rules)
        {
            var transitions = new Dictionary<(string, char), string>();
            var visited = new HashSet<char>();
            var epsilon = "X";

            foreach (var rule in rules)
            {
                var nonterminal = rule.Key;
                visited.Add(nonterminal);
                var productions = rule.Value;

                foreach (var production in productions)
                {
                    var symbols = production.ToCharArray();

                    for (int i = 0; i < symbols.Length; i++)
                    {
                        var symbol = symbols[i];

                        if (rules.ContainsKey(symbol))
                        {
                            // If this is the first visit to the nonterminal, add transition to the first symbol in the production
                            if (!visited.Contains(symbol))
                            {
                                visited.Add(symbol);
                                transitions.Add((nonterminal.ToString(), symbol), production.Substring(i));
                                break;
                            }
                            // If we've already visited this nonterminal, add epsilon transitions to its first set of productions
                            else
                            {
                                var firstProductions = rules[symbol];
                                foreach (var firstProduction in firstProductions)
                                {
                                    // If the first symbol in the production is a nonterminal, add epsilon transition
                                    if (rules.ContainsKey(firstProduction[0]))
                                    {
                                        transitions.Add((nonterminal.ToString(), firstProduction[0]), epsilon);
                                    }
                                    // Otherwise, add transition to the first symbol
                                    else
                                    {
                                        transitions.Add((nonterminal.ToString(), firstProduction[0]), firstProduction);
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Add transition for nonterminal to terminal symbol
                            transitions.Add((nonterminal.ToString(), symbol), production.Substring(i + 1));
                            break;
                        }
                    }
                }

                // If the nonterminal can produce the empty string, add an epsilon transition
                if (productions.Contains(""))
                {
                    transitions.Add((nonterminal.ToString(), epsilon[0]), nonterminal.ToString());
                }
            }

            return transitions;
        }



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

            var states = new List<string> { "S", "B", "C", "FINAL" };
            string initialState = "S";
            var transitions = new Dictionary<(string, char), string> {
            {("S", 'a'), "B"},
            {("B", 'a'), "C"},
            {("B", 'b'), "B"},
            {("C", 'b'), "B"},
            {("C", 'c'), "FINAL"},
            {("C", 'a'), "S"}
            };

            var transitionss = GetTransitions(rules);
            foreach (var transition in transitionss)
            {
                Console.WriteLine($"({transition.Key.Item1}, {transition.Key.Item2}) -> {transition.Value}");
            }


            var grammar = new Grammar(nonTerminalSymbols, terminalSymbols, startingSymbol, rules);

            // for (int i = 0; i < 5; i++)
            // {
            //     Console.WriteLine(grammar.GenerateString());
            // }

            // var automaton = grammar.ConvertToFiniteAutomaton();

            // Console.WriteLine(automaton.StartState);

            // foreach (var transition in automaton.Transitions)
            // {
            //     Console.WriteLine($"({transition.Key.Item1}, {transition.Key.Item2}) -> {transition.Value}");
            // }

            // Test the CanGenerateString method for some strings
            // var inputs = new List<string> { "ab", "aab", "abb", "aabb", "abbac", "aaaaabaaabac" };
            // foreach (var input in inputs)
            // {
            //     var canGenerate = automaton.CanGenerateString(input);
            //     Console.WriteLine($"Input '{input}': {canGenerate}");
            // }
        }
    }
}