namespace src
{
    class NormalizedGrammar : Grammar
    {
        public NormalizedGrammar(List<string> nonTerminalSymbols, List<string> terminalSymbols, string startingSymbol, List<Production> rules) : base(nonTerminalSymbols, terminalSymbols, startingSymbol, rules)
        {
        }
        public void RemoveEpsilonProductions()
        {
            List<Production> epsilonProductions = EpsilonProductionList();
            foreach (Production production in epsilonProductions)
            {
                Rules.Remove(production);
            }
            List<Production> rulesContainingEpsilon = new List<Production>();
            foreach (Production production in epsilonProductions)
            {
                foreach (Production rule in Rules)
                {
                    if (rule.RightSide.Contains(production.LeftSide[0]) && !rulesContainingEpsilon.Contains(rule))
                    {
                        rulesContainingEpsilon.Add(rule);
                    }
                }
            }
            AddProductions(rulesContainingEpsilon, epsilonProductions);
        }

        private List<Production> AddProductions(List<Production> rulesContainingEpsilon, List<Production> epsilonProductions)
        {
            List<Production> newProductions = new List<Production>();
            foreach (Production rule in rulesContainingEpsilon)
            {
                string leftSymbol = rule.LeftSide[0];
                foreach (Production production in epsilonProductions)
                {
                    List<string[]> newRightSides = GenerateStringArrays(production.LeftSide[0], rule.RightSide);
                    foreach (string[] newRightSide in newRightSides)
                    {
                        Production newProduction = new Production(new string[] { leftSymbol }, newRightSide);
                        Rules.Add(newProduction);
                        newProductions.Add(newProduction);
                    }
                }
            }
            return newProductions;
        }

        private List<Production> EpsilonProductionList()
        {
            List<Production> epsilonProductions = new List<Production>();
            foreach (Production production in Rules)
            {
                if (production.LeftSide.Length == 1 && production.RightSide.Length == 1 && production.RightSide[0] == "")
                {
                    epsilonProductions.Add(production);
                }
            }
            return epsilonProductions;
        }

        private List<string[]> GenerateStringArrays(string charToReplace, string[] inputArray)
        {
            List<string[]> outputList = new List<string[]>();
            // Loop through each element of the input array
            for (int i = 0; i < inputArray.Length; i++)
            {
                // If the element contains the char to replace
                if (inputArray[i].Contains(charToReplace[0]))
                {
                    // Create a new string array with the same length as the input array
                    string[] newStringArray = new string[inputArray.Length - 1];
                    // Copy all the elements of the input array to the new array, except the element with the char to replace
                    for (int j = 0, k = 0; j < inputArray.Length; j++)
                    {
                        if (j != i)
                        {
                            newStringArray[k] = inputArray[j];
                            k++;
                        }
                    }
                    if (!outputList.Contains(newStringArray))
                    {
                        // Add the new string array to the output list
                        outputList.Add(newStringArray);
                    }
                    // Recursively call the same method with the new string array as input
                    outputList.AddRange(GenerateStringArrays(charToReplace[0].ToString(), newStringArray));
                }
            }
            return RemoveDuplicateArrays(outputList);
        }

        private List<string[]> RemoveDuplicateArrays(List<string[]> inputList)
        {
            HashSet<string> set = new HashSet<string>();
            List<string[]> outputList = new List<string[]>();
            foreach (string[] arr in inputList)
            {
                // Check if the current array's contents are already in the set
                string arrString = string.Join(",", arr);
                if (!set.Contains(arrString))
                {
                    // If not, add the array to the output list and the set
                    outputList.Add(arr);
                    set.Add(arrString);
                }
            }
            return outputList;
        }

        public void RemoveUnitProductions()
        {
            List<Production> unitProductions = FindUnitProductions();
            foreach (Production unitProduction in unitProductions)
            {
                string rightSymbol = unitProduction.RightSide[0];
                string leftSymbol = unitProduction.LeftSide[0];
                var newProductions = Rules.Where(p => p.LeftSide[0] == rightSymbol)
                                        .Select(p => new Production(new[] { leftSymbol }, p.RightSide))
                                        .ToList();
                Rules.AddRange(newProductions);
                Rules.Remove(unitProduction);
            }
            // check if there are any more unit productions in Rules and if they are remove them
            unitProductions = FindUnitProductions();
            if (unitProductions.Count > 0)
            {
                RemoveUnitProductions();
            }
        }

        private List<Production> FindUnitProductions()
        {
            var unitProductions = new List<Production>();
            foreach (Production production in Rules)
            {
                if (production.LeftSide.Length == 1 && NonTerminalSymbols.Contains(production.LeftSide[0]) && production.RightSide.Length == 1 && NonTerminalSymbols.Contains(production.RightSide[0]))
                {
                    unitProductions.Add(production);
                }
            }
            return unitProductions;
        }

        public void RemoveNonProductiveSymbols()
        {
            // foreach production containing a non productive symbol
            // remove from rules
            List<string> nonProductiveSymbols = FindNonProductiveSymbols();
            foreach (string symbol in nonProductiveSymbols)
            {
                var symbolProductions = Rules.Where(p => p.RightSide.Contains(symbol) || p.LeftSide.Contains(symbol)).ToList();
                foreach (Production production in symbolProductions)
                {
                    Rules.Remove(production);
                }
            }
        }

        public List<string> FindNonProductiveSymbols()
        {
            var nonProductiveSymbols = NonTerminalSymbols.ToList(); // create a copy of the collection
            foreach (string symbol in NonTerminalSymbols)
            {
                var symbolProductions = Rules.Where(p => p.LeftSide[0] == symbol);
                foreach (Production production in symbolProductions)
                {
                    // convert the right side of the production to a list
                    var rightSide = production.RightSide.ToList();
                    // if the right side contains only terminal symbols
                    if (rightSide.All(s => TerminalSymbols.Contains(s)))
                    {
                        // remove the symbol from the non productive symbols list
                        nonProductiveSymbols.Remove(symbol);
                    }
                }
            }
            return nonProductiveSymbols;
        }

        public void RemoveUnreachableSymbols()
        {
            var nonReachableSymbols = NonTerminalSymbols.ToList();
            foreach (string nonterminal in NonTerminalSymbols)
            {
                foreach (Production production in Rules)
                {
                    if (production.RightSide.Contains(nonterminal))
                    {
                        nonReachableSymbols.Remove(nonterminal);
                    }
                }
            }
            foreach (string symbol in nonReachableSymbols)
            {
                // remove productions containing the symbol
                var symbolProductions = Rules.Where(p => p.RightSide.Contains(symbol) || p.LeftSide.Contains(symbol)).ToList();
                foreach (Production production in symbolProductions)
                {
                    Rules.Remove(production);
                }
            }
        }
    }
}