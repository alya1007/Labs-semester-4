using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace src
{
    class Grammar
    {
        public List<char> NonTerminalSymbols { get; set; }
        public List<char> TerminalSymbols { get; set; }
        public char StartingSymbol { get; set; }
        public Dictionary<char, List<string>> Rules { get; set; }

        public Grammar(List<char> nonTerminalSymbols, List<char> terminalSymbols, char startingSymbol, Dictionary<char, List<string>> rules)
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
                    var possibleRules = Rules[currentSymbol];
                    var rule = possibleRules[random.Next(possibleRules.Count)];

                    for (int i = rule.Length - 1; i >= 0; i--)
                    {
                        stack.Push(rule[i]);
                    }
                }
            }

            return generatedString;
        }
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
        public FiniteAutomaton ConvertToFiniteAutomaton()
        {
            var states = NonTerminalSymbols;
            var alphabet = TerminalSymbols;
            string initialState = "S";
            var transitions = GetTransitions(Rules);
            var finalStates = "";
            List<string> stringStates = states.Select(c => c.ToString()).ToList();
            List<string> stringFinalStates = new List<string> { finalStates };
            return new FiniteAutomaton(stringStates, alphabet, transitions, initialState, stringFinalStates);

        }
    }
}