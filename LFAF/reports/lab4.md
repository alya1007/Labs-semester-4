# Chomsky Normal Form.

## Course: Formal Languages & Finite Automata

## Author: Konjevic Alexandra FAF-213

## Variant: 22

<br>

## Introduction

Context-free grammars (CFG) are widely used in computer science to describe the syntax of programming languages, natural languages, and other formal languages. However, CFGs can sometimes become complex and difficult to manipulate, hindering their practical applications. Therefore, simplification and normalization techniques are often applied to CFGs to improve their efficiency and ease of use.

Simplification of CFGs involves transforming them into simpler and more manageable forms without changing the language they generate. The simplification process can involve removing useless or unreachable symbols, merging equivalent or similar rules, eliminating recursion or left recursion, and converting a grammar into Chomsky normal form or Greibach normal form. By simplifying CFGs, we can reduce the complexity of parsers and improve the performance of algorithms that operate on these grammars.

Grammar normalization is another technique used to simplify CFGs. The goal of normalization is to transform a CFG into a standard form that allows for easier analysis and manipulation. Chomsky normal form requires that all production rules be either of the form A → BC or A → a, where A, B, and C are non-terminal symbols, and a is a terminal symbol.

<br>

## Objectives

- Learn about Chomsky Normal Form (CNF).
- Get familiar with the approaches of normalizing a grammar.
- Implement a method for normalizing an input grammar by the rules of CNF (bonus points for implementing methods for general-case grammars and for having unit test)

<br>

## Implementation Description

First of all, to normalize the grammar, I created the method `Normalize` in the class `Grammar`, that converts a grammar into a `NormalizedGrammar` object. Therefore, I created a class `NormalizedGrammar`, which has the main method `NormalizeGrammar`. This method goes through all the steps needed to bring a grammar to Chomsky normal form:

- Remove epsilon productions
- Remove unit productions
- Remove non-productive productions
- Remove unreachable symbols
- Convert to Chomsky normal form

```
public void NormalizeGrammar()
{
    RemoveEpsilonProductions();
    RemoveUnitProductions();
    RemoveNonProductiveSymbols();
    RemoveUnreachableSymbols();
    ToChomskyNormalForm();
}
```

### Remove epsilon productions

The method `RemoveEpsilonProductions` uses several other helping methods to remove all the epsilon productions from the set of rules. First of all, I created a list of all the epsilon productions, using the method `EpsilonProductionList`, which checks every rule for an empty right side of the production:

```
foreach (Production production in Rules)
{
    if (production.LeftSide.Length == 1 && production.RightSide.Length == 1 && production.RightSide[0] == "")
    {
        epsilonProductions.Add(production);
    }
}
```

Then, this rules are removed.
Next, I selected all the productions that contained the left symbol of the epsilon production, and added the needed productions, with the method `AddProductions`. This method creates new rules that must be added, with the helper method `GenerateStringArrays`, that generates all the possible combinations of a production. For example, for productions

```
S -> A | AbA
A -> epsilon
```

It would generate the productions:

```
S -> Ab
S -> bA
S -> b
```

`GenerateStringArrays` method:

```
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
```

The duplicates from the output list are removed via the method `RemoveDuplicateArrays`.

### Remove unit productions

To remove the unit productions, I firstly created the method `FindUnitProductions` that returns a list of unit production from the `Rules`.

```
if (production.LeftSide.Length == 1 && NonTerminalSymbols.Contains(production.LeftSide[0]) && production.RightSide.Length == 1 && NonTerminalSymbols.Contains(production.RightSide[0]))
{
    unitProductions.Add(production);
}
```

Then, each of the unit production is replaced with the according productions.

```
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
```

If there are any unit productions left, the method is called recursively.

```
unitProductions = FindUnitProductions();
if (unitProductions.Count > 0)
{
    RemoveUnitProductions();
}
```

### Remove non productive symbols

Like in the steps above, I first created a method `FindNonProductiveSymbols` that finds the non-terminal symbols, that don't have at least one production that has only terminals on the right side.

```
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
```

Then, in the method `RemoveNonProductiveSymbols`, are removed all productions that contain the non-productive symbols.

```
List<string> nonProductiveSymbols = FindNonProductiveSymbols();
foreach (string symbol in nonProductiveSymbols)
{
    var symbolProductions = Rules.Where(p => p.RightSide.Contains(symbol) || p.LeftSide.Contains(symbol)).ToList();
    foreach (Production production in symbolProductions)
    {
        Rules.Remove(production);
    }
}
```

### Remove non productive symbols

In this method, I first created a list, `nonReachableSymbols`, that has all the terminal symbols. Then, for each of the production in rules, is checked if there is at least one production that has the non-terminal symbol on the right side, then the symbol is removed from `nonReachableSymbols`.

```
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
```

Next, there are removed the productions that have the unreachable symbols.

```
foreach (string symbol in nonReachableSymbols)
{
    // remove productions containing the symbol
    var symbolProductions = Rules.Where(p => p.RightSide.Contains(symbol) || p.LeftSide.Contains(symbol)).ToList();
    foreach (Production production in symbolProductions)
    {
        Rules.Remove(production);
    }
}
```

## Results

## Conclusion

```

```

```

```
