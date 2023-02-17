namespace src
{
    class FiniteAutomaton
    {
        public List<string> States { get; set; }
        public List<char> Alphabet { get; set; }
        public Dictionary<(string, char), string> Transitions { get; set; }
        public string StartState { get; set; }
        public List<string> FinalStates { get; set; }

        public FiniteAutomaton(List<string> states, List<char> alphabet, Dictionary<(string, char), string> transitions, string startState, List<string> finalStates)
        {
            States = states;
            Alphabet = alphabet;
            Transitions = transitions;
            StartState = startState;
            FinalStates = finalStates;
        }

        public bool CanGenerateString(string input)
        {
            var currentState = StartState;

            foreach (var symbol in input)
            {
                if (!Alphabet.Contains(symbol))
                {
                    return false; // Reject input if it contains a symbol not in the alphabet
                }

                if (!Transitions.ContainsKey((currentState, symbol)))
                {
                    return false; // Reject input if there is no transition from the current state for the current symbol
                }

                currentState = Transitions[(currentState, symbol)];
            }

            return FinalStates.Contains(currentState); // Accept input if the final state is in the list of final states
        }
    }
}