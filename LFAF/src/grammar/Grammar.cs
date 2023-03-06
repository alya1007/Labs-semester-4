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
                if (production.LeftSide.Length > 1)
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
    }
}
