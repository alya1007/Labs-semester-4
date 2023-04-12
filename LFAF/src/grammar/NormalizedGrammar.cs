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
    }
}