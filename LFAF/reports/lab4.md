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

### Convert to Chomsky normal form

For method `ToChomskyNormalForm` I used several helping methods:

- `GetNewSymbol` - creates a new non-terminal symbol:

```
var newSymbol = "Z" + _symbolIndex;
_symbolIndex++;
return newSymbol;
```

- `SplitProduction` - splits a production until is has no more than 2 symbols:

```
var newProductions = new List<Production>();
var rightSide = production.RightSide;
while (rightSide.Length > 2)
{
    var leftSide = rightSide.Take(2).ToArray();
    rightSide = new[] { newSymbol }.Concat(rightSide.Skip(2)).ToArray();
    var newProduction = new Production(leftSide, rightSide);
    newProductions.Add(newProduction);
}
var lastProduction = new Production(new[] { newSymbol }, rightSide);
newProductions.Add(lastProduction);
return newProductions;
```

- `IsTerminal` - boolean method for checking if the symbol is terminal.

- `ConvertTerminalProduction` - takes a single Production object and a string representing a new non-terminal symbol, and returns a list of two Production objects that are equivalent to the original production, but with any terminal symbols on the right-hand side replaced by the new non-terminal symbol:

```
var newProductions = new List<Production>();
var leftSide = production.RightSide[0];
var rightSide = production.RightSide[1];
if (IsTerminal(leftSide))
{
    var temp = leftSide;
    leftSide = rightSide;
    rightSide = temp;
}
var newProduction1 = new Production(production.LeftSide, new[] { leftSide, newSymbol });
var newProduction2 = new Production(new[] { newSymbol }, new[] { rightSide });
newProductions.Add(newProduction1);
newProductions.Add(newProduction2);
return newProductions;
```

- `RemoveDuplicateNormalForm` - removes the useless new production when converting to normal form, for example, for productions X0 -> a, X1 -> b, removes the symbol X1 and replaces it everywhere with X0:

```
var rules = new List<Production>(Rules);
var newRulesList = rules.Where(p => p.LeftSide[0][0] == 'Z');
for (int i = 0; i < newRulesList.Count(); i++)
{
    for (int j = i + 1; j < newRulesList.Count(); j++)
    {
        if (newRulesList.ElementAt(i).RightSide[0] == newRulesList.ElementAt(j).RightSide[0])
        {
            // remove production at index j
            Rules.Remove(newRulesList.ElementAt(j));
            var symbolToReplace = newRulesList.ElementAt(j).LeftSide[0];
            // in all the production from rules that contain in right side symbolToReplace replace it with the symbol from production at index i
            foreach (Production production in Rules)
            {
                for (int k = 0; k < production.RightSide.Length; k++)
                {
                    if (production.RightSide[k] == symbolToReplace)
                    {
                        production.RightSide[k] = newRulesList.ElementAt(i).LeftSide[0];
                    }
                }
            }
        }
    }
}
```

## Results

My variant, 22:

- Vn = {S, A, B, C, E}
- Vt = {a, b}
- S = {S}
- P =
  {
  1.S->aB
  2.S->AC
  3.A->a
  4.A->ACSC
  5.A->BC
  6.B->b
  7.B->aA
  8.C->epsilon
  9.C->BA
  10.E->bB
  }

I will show the results from each of the five steps.

### Remove epsilon productions

Output:

```
S -> a B
S -> A C
A -> a
A -> A C S C
A -> B C
B -> b
B -> a A
C -> B A
E -> b B
S -> A
A -> A S C
A -> A S
A -> A C S
A -> B
```

### Remove unit productions

Output:

```
S -> a B
S -> A C
A -> a
A -> A C S C
A -> B C
B -> b
B -> a A
C -> B A
E -> b B
A -> A S C
A -> A S
A -> A C S
S -> a
S -> A C S C
S -> B C
S -> A S C
S -> A S
S -> A C S
A -> b
A -> a A
S -> b
S -> a A
```

### Remove non productive symbols

Output:

```
S -> a B
A -> a
B -> b
B -> a A
A -> A S
S -> a
S -> A S
A -> b
A -> a A
S -> b
S -> a A
```

### Remove unreachable symbols

Output:

```
S -> a B
A -> a
B -> b
B -> a A
A -> A S
S -> a
S -> A S
A -> b
A -> a A
S -> b
S -> a A
```

### Convert to Chomsky normal form

Output:

```
A -> a
B -> b
A -> A S
S -> a
S -> A S
A -> b
S -> b
S -> B Z0
Z0 -> a
B -> A Z0
A -> A Z0
S -> A Z0
Z0 -> A
```

## Conclusion

In conclusion, this report has focused on the topic of simplifying context-free grammars (CFGs) and converting them to Chomsky normal form. The process of simplification involves removing unproductive and unreachable symbols from the grammar, as well as eliminating epsilon productions and unit productions. These steps result in a more concise and easily manageable grammar that can be used for parsing and other applications.

Furthermore, the report discussed the conversion of CFGs to Chomsky normal form, which is a standardized form that makes the grammar more amenable to parsing algorithms. This form requires the elimination of all non-binary productions, including unit productions and epsilon productions, and the introduction of new non-terminals to represent pairs of terminals.

The implementation of these processes in C# was also described in detail, with the functionality for simplifying CFGs and converting them to Chomsky normal form being demonstrated through code snippets and examples.

In conclusion, the development of the C# implementation for simplifying CFGs and converting them to Chomsky normal form provides a useful tool for those working with grammars in natural language processing, compiler design, and related fields. The techniques discussed in this report can serve as a foundation for further research and development in the area of CFG manipulation and parsing.
