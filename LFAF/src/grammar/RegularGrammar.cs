using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.Linq;
namespace src
{
    class RegularGrammar
    {
        public List<string> NonTerminalSymbols { get; set; }
        public List<char> TerminalSymbols { get; set; }
        public char StartingSymbol { get; set; }
        public Dictionary<string, List<string>> Rules { get; set; }
        public RegularGrammar(List<string> nonTerminalSymbols, List<char> terminalSymbols, char startingSymbol, Dictionary<string, List<string>> rules)
        {
            NonTerminalSymbols = nonTerminalSymbols;
            TerminalSymbols = terminalSymbols;
            StartingSymbol = startingSymbol;
            Rules = rules;
        }
        public string GenerateString()
        {
            var random = new Random();
            var generatedString = "";
            var stack = new Stack<char>();
            stack.Push(StartingSymbol);

            while (stack.Count > 0)
            {
                var currentSymbol = stack.Pop();

                if (TerminalSymbols.Contains(currentSymbol))
                {
                    generatedString += currentSymbol;
                }
                else
                {
                    var possibleRules = Rules[currentSymbol.ToString()];
                    var rule = possibleRules[random.Next(possibleRules.Count)];

                    for (int i = rule.Length - 1; i >= 0; i--)
                    {
                        stack.Push(rule[i]);
                    }
                }
            }

            return generatedString;
        }

        // Method to convert a grammar rules into transitions for a finite automaton
        static public Dictionary<(string, char), string> GetTransitions(Dictionary<string, List<string>> rules)
        {
            var transitions = new Dictionary<(string, char), string>();
            var visited = new HashSet<string>();
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

                        if (rules.ContainsKey(symbol.ToString()))
                        {
                            // If this is the first visit to the nonterminal, add transition to the first symbol in the production
                            if (!visited.Contains(symbol.ToString()))
                            {
                                visited.Add(symbol.ToString());
                                transitions.Add((nonterminal.ToString(), symbol), production.Substring(i));
                                break;
                            }
                            // If we've already visited this nonterminal, add epsilon transitions to its first set of productions
                            else
                            {
                                var firstProductions = rules[symbol.ToString()];
                                foreach (var firstProduction in firstProductions)
                                {
                                    // If the first symbol in the production is a nonterminal, add epsilon transition
                                    if (rules.ContainsKey(firstProduction[0].ToString()))
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
        public Automaton ConvertToAutomaton()
        {
            var states = NonTerminalSymbols;
            var alphabet = TerminalSymbols;
            string initialState = "S";
            var transitions = GetTransitions(Rules);
            var finalStates = "";

            // Convert states and final states to string
            // because the Automaton class constructor expects strings
            List<string> stringStates = states.Select(c => c.ToString()).ToList();
            List<string> stringFinalStates = new List<string> { finalStates };
            return new Automaton(stringStates, alphabet, transitions, initialState, stringFinalStates);
        }
    }
}