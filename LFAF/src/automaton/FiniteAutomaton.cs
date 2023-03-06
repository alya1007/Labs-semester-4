namespace src
{
    class FiniteAutomaton
    {
        public List<string> States { get; set; }
        public List<string> Alphabet { get; set; }
        public List<Transition> Transitions { get; set; }
        public string StartState { get; set; }
        public List<string> FinalStates { get; set; }
        public FiniteAutomaton(List<string> states, List<string> alphabet, List<Transition> transitions, string startState, List<string> finalStates)
        {
            States = states;
            Alphabet = alphabet;
            Transitions = transitions;
            StartState = startState;
            FinalStates = finalStates;
        }

        public bool isDeterministic()
        {
            foreach (var transition in Transitions)
            {
                foreach (var transition2 in Transitions)
                {
                    // If the current state and the symbol are the same,
                    // but the next state is different, then it is not deterministic
                    if (transition.CurrentState == transition2.CurrentState && transition.Symbol == transition2.Symbol && transition.NextState != transition2.NextState)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Production TransitionToProduction(Transition transition)
        {
            return new Production(new[] { transition.CurrentState }, new[] { transition.Symbol, transition.NextState });
        }
        public Grammar ToGrammar()
        {
            var nonTerminalSymbols = States;
            var terminalSymbols = Alphabet;
            var startingSymbol = StartState;
            var productions = new List<Production>();
            foreach (var transition in Transitions)
            {
                productions.Add(TransitionToProduction(transition));
            }
            return new Grammar(nonTerminalSymbols, terminalSymbols, startingSymbol, productions);
        }
    }
}