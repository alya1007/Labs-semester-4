using System;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> nonTerminalSymbols = new List<string> { "q0", "q1", "q2" };
            List<string> terminalSymbols = new List<string> { "a", "b" };
            string startingSymbol = "q0";
            List<Production> productions = new List<Production>
            {
                new Production(new []{"q0"}, new []{"a", "q0"}),
                new Production(new []{"q0"}, new []{"b", "q1"}),
                new Production(new []{"q1"}, new []{"b", "q1"}),
                new Production(new []{"q1"}, new []{"b", "q2"}),
                new Production(new []{"q1"}, new []{"a", "q0"}),
                new Production(new []{"q2"}, new []{"b", "q1"}),
                new Production(new []{"q2"}, new []{""})
            };

            Grammar grammar = new Grammar(nonTerminalSymbols, terminalSymbols, startingSymbol, productions);
            Console.WriteLine(grammar.GetChomskyType());
        }
    }
}