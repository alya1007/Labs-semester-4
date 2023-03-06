using System;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            // List<string> nonTerminalSymbols = new List<string> { "q0", "q1", "q2" };
            // List<string> terminalSymbols = new List<string> { "a", "b" };
            // string startingSymbol = "q0";
            // List<Production> productions = new List<Production>
            // {
            //     new Production(new []{"q0"}, new []{"a", "q0"}),
            //     new Production(new []{"q0"}, new []{"b", "q1"}),
            //     new Production(new []{"q1"}, new []{"b", "q1"}),
            //     new Production(new []{"q1"}, new []{"b", "q2"}),
            //     new Production(new []{"q1"}, new []{"a", "q0"}),
            //     new Production(new []{"q2"}, new []{"b", "q1"}),
            //     new Production(new []{"q2"}, new []{""})
            // };

            // Grammar grammar = new Grammar(nonTerminalSymbols, terminalSymbols, startingSymbol, productions);
            // Console.WriteLine(grammar.GetChomskyType());

            List<string> states = new List<string> { "q0", "q1", "q2" };
            List<string> alphabet = new List<string> { "a", "b" };
            List<Transition> transitions = new List<Transition>
            {
                new Transition("q0", "a", "q0"),
                new Transition("q0", "b", "q1"),
                new Transition("q1", "b", "q1"),
                new Transition("q1", "b", "q2"),
                new Transition("q1", "a", "q0"),
                new Transition("q2", "b", "q1"),
                new Transition("q2", "", "q2")
            };
            string startState = "q0";
            List<string> finalStates = new List<string> { "q2" };
            FiniteAutomaton finiteAutomaton = new FiniteAutomaton(states, alphabet, transitions, startState, finalStates);
            Grammar grammar = finiteAutomaton.ToGrammar();
            Console.WriteLine(grammar.GetChomskyType());
            Console.WriteLine(grammar);
            // write each rule on a new line in the form "A -> B C"
            foreach (var production in grammar.Rules)
            {
                Console.WriteLine($"{production.LeftSide[0]} -> {production.RightSide[0]} {production.RightSide[1]}");
            }
        }
    }
}