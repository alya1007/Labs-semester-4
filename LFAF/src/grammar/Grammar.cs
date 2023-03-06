using System;
using System.Collections.Generic;
using System.Text;


namespace src2
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
    }
}
