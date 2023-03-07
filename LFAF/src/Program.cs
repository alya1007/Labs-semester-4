using System;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
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

            // convert to grammar and determine its type
            Grammar grammar = finiteAutomaton.ToGrammar();
            Console.WriteLine(grammar.GetChomskyType());

            // determine if fa is deterministic
            Console.WriteLine(finiteAutomaton.isDeterministic());

            // convert nfa to dfa and print transitions
            FiniteAutomaton dfa = finiteAutomaton.ToDFA();
            foreach (var transition in dfa.Transitions)
            {
                Console.WriteLine(transition.CurrentState + " " + transition.Symbol + " " + transition.NextState);
            }

            Console.WriteLine(dfa.isDeterministic());

            // write dfa transitions to file
            finiteAutomaton.WriteToFile("fa.dot");
            dfa.WriteToFile("dfa.dot");

        }
    }
}