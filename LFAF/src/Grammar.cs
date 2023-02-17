using System;
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

        // public FiniteAutomaton ConvertToFiniteAutomaton()
        // {
        //     var states = NonTerminalSymbols;
        //     states.Add('FINAL');
        //     string initialState = "S";

        // }
    }
}