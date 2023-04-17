using System;
using System.Collections.Generic;
using System.Text;

namespace src
{
    class Grammar
    {
        public List<string> NonTerminalSymbols { get; set; }
        public List<string> TerminalSymbols { get; set; }
        public string StartingSymbol { get; set; }
        public List<Production> Rules { get; set; }
        public Grammar(List<string> nonTerminalSymbols, List<string> terminalSymbols, string startingSymbol, List<Production> rules)
        {
            NonTerminalSymbols = nonTerminalSymbols;
            TerminalSymbols = terminalSymbols;
            StartingSymbol = startingSymbol;
            Rules = rules;
        }
        public string GetChomskyType()
        {
            bool hasUnitLeftSide = true;
            bool type3 = true;
            bool type2 = false;
            bool type1 = true;
            foreach (Production production in Rules)
            {
                if (production.LeftSide.Length > 1 || !(NonTerminalSymbols.Contains(production.LeftSide[0])))
                {
                    hasUnitLeftSide = false;
                }
            }

            // if has unit left side
            // the grammar is type 3 or type 2
            if (hasUnitLeftSide)
            {
                // check for type 3
                foreach (Production production in Rules)
                {
                    int i = 0;
                    foreach (string symbol in production.RightSide)
                    {
                        // count the number of non terminal symbols in the right side
                        if (NonTerminalSymbols.Contains(symbol))
                        {
                            i++;
                        }
                    }
                    // type 3 only if there is one non terminal symbol in the right side
                    if (i == 1)
                    {
                        // type 3 only if the non terminal symbol is the first or the last symbol in the right side
                        if (!(NonTerminalSymbols.Contains(production.RightSide[0])) && !(NonTerminalSymbols.Contains(production.RightSide[production.RightSide.Length - 1])))
                        {
                            type3 = false;
                        }
                    }
                    else if (i > 1)
                    {
                        type3 = false;
                    }
                }
                // if none of the conditions above are met, the grammar is type 2
                if (type3 == false)
                {
                    type2 = true;
                }
            }

            // if more symbols on the left side
            // the grammar is type 1 or type 0
            else
            {
                type3 = false;
                foreach (Production production in Rules)
                {
                    // type 1 only if the right side has only terminal symbols
                    foreach (string symbol in production.LeftSide)
                    {
                        if (!(NonTerminalSymbols.Contains(symbol)))
                        {
                            type1 = false;
                        }
                    }
                }
            }
            if (type3)
            {
                return "Type 3";
            }
            else if (type2)
            {
                return "Type 2";
            }
            else if (type1)
            {
                return "Type 1";
            }
            else
            {
                return "Type 0";
            }
        }
        public string GenerateRandomString()
        {
            // Create a random number generator
            Random rand = new Random();

            // Start with the starting symbol
            string currentSymbol = StartingSymbol;

            // Keep track of the generated string
            StringBuilder sb = new StringBuilder();

            // Loop until we have only terminal symbols
            while (NonTerminalSymbols.Contains(currentSymbol))
            {
                // Find all productions that have the current symbol on the left side
                List<Production> productions = Rules.FindAll(p => p.LeftSide.Contains(currentSymbol));

                // Pick a random production from the list
                Production chosenProduction = productions[rand.Next(productions.Count)];

                // Add the right side of the chosen production to the generated string
                foreach (string symbol in chosenProduction.RightSide)
                {
                    sb.Append(symbol);
                }

                // Set the current symbol to the first symbol in the generated string
                currentSymbol = sb.ToString().Substring(0, 1);

                // Clear the generated string
                sb.Clear();
            }

            // Return the generated string
            return sb.ToString();
        }

        public FiniteAutomaton ToFiniteAutomaton()
        {
            var states = new List<string>();
            var alphabet = new List<string>();
            var transitions = new List<Transition>();
            var startState = StartingSymbol;
            var finalStates = new List<string>();

            // Add a new start state
            var newStartState = GetUnusedStateName(states);
            states.Add(newStartState);
            transitions.Add(new Transition(newStartState, "", StartingSymbol));

            // Add new final states
            foreach (var production in Rules.Where(p => p.RightSide.Length == 0))
            {
                var newFinalState = GetUnusedStateName(states);
                states.Add(newFinalState);
                finalStates.Add(newFinalState);
                transitions.Add(new Transition(production.LeftSide[0], "", newFinalState));
            }

            // Add transitions for all productions
            foreach (var production in Rules.Where(p => p.RightSide.Length > 0))
            {
                var currentState = production.LeftSide;
                var nextState = "";
                foreach (var symbol in production.RightSide)
                {
                    if (!alphabet.Contains(symbol))
                    {
                        alphabet.Add(symbol);
                    }

                    nextState = GetUnusedStateName(states);
                    states.Add(nextState);
                    transitions.Add(new Transition(currentState[0], symbol, nextState));
                    currentState = new[] { nextState };
                }

                transitions.Add(new Transition(currentState[0], "", ""));
            }

            return new FiniteAutomaton(states, alphabet, transitions, newStartState, finalStates);
        }

        private string GetUnusedStateName(List<string> existingStates)
        {
            var i = 0;
            while (true)
            {
                var stateName = $"q{i++}";
                if (!existingStates.Contains(stateName))
                {
                    return stateName;
                }
            }
        }

        public NormalizedGrammar Normalize()
        {
            var nonterminals = NonTerminalSymbols;
            var terminals = TerminalSymbols;
            var start = StartingSymbol;
            var rules = Rules;
            var normalizedGrammar = new NormalizedGrammar(nonterminals, terminals, start, rules);
            normalizedGrammar.NormalizeGrammar();
            return normalizedGrammar;
        }

        public void PrintAllProductions()
        {
            foreach (var production in Rules)
            {
                Console.WriteLine(production.LeftSide[0] + " -> " + string.Join(" ", production.RightSide));
            }
        }
    }
}
