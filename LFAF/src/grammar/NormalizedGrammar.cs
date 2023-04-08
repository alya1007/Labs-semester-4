namespace src
{
    class NormalizedGrammar : Grammar
    {
        public NormalizedGrammar(List<string> nonTerminalSymbols, List<string> terminalSymbols, string startingSymbol, List<Production> rules) : base(nonTerminalSymbols, terminalSymbols, startingSymbol, rules)
        {
        }

        public void EliminateEpsilonProductions()
        {
            var toRemove = new List<Production>();
            foreach (var prod in Rules)
            {
                if (prod.RightSide.Length == 1 && prod.RightSide[0] == "")
                {
                    toRemove.Add(prod);
                }
            }
            foreach (var prod in toRemove)
            {
                Rules.Remove(prod);
            }
        }

    }
}