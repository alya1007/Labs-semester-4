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
            return Transitions.All(
                transition => Transitions.Count(
                    transition2 => transition.CurrentState == transition2.CurrentState && transition.Symbol == transition2.Symbol && transition.NextState != transition2.NextState
                ) == 0
            );
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

        public FiniteAutomaton ToDFA()
        {
            if (isDeterministic())
            {
                // already a DFA
                return this;
            }

            // create initial DFA state
            var initialDFAState = new HashSet<string>(new[] { StartState });
            var dfaStates = new List<HashSet<string>> { initialDFAState };
            var dfaTransitions = new List<Transition>();

            // repeat until no new states are created
            for (int i = 0; i < dfaStates.Count; i++)
            {
                var dfaState = dfaStates[i];

                // create a transition for each symbol in the alphabet
                foreach (var symbol in Alphabet)
                {
                    // find the set of NFA states that the DFA state represents
                    var nfaStates = new HashSet<string>();
                    foreach (var state in dfaState)
                    {
                        nfaStates.UnionWith(
                            Transitions
                                .Where(t => t.CurrentState == state && t.Symbol == symbol)
                                .Select(t => t.NextState)
                        );
                    }

                    if (nfaStates.Count == 0)
                    {
                        // no transition for this symbol
                        continue;
                    }

                    // check if the set of NFA states is already a DFA state
                    var existingDFAState = dfaStates.FirstOrDefault(ds => ds.SetEquals(nfaStates));
                    if (existingDFAState != null)
                    {
                        // use existing DFA state
                        dfaTransitions.Add(new Transition(
                            String.Join("", dfaState),
                            symbol,
                            String.Join("", existingDFAState)
                        ));
                    }
                    else
                    {
                        // create new DFA state
                        var newDFAState = nfaStates;
                        dfaStates.Add(newDFAState);
                        dfaTransitions.Add(new Transition(
                            String.Join("", dfaState),
                            symbol,
                            String.Join("", newDFAState)
                        ));
                    }
                }
            }

            // determine final states of the DFA
            var finalDFAStates = dfaStates.Where(ds => ds.Overlaps(FinalStates)).ToList();

            // create new DFA
            return new FiniteAutomaton(
                dfaStates.Select(ds => String.Join("", ds)).ToList(),
                Alphabet,
                dfaTransitions,
                String.Join("", initialDFAState),
                finalDFAStates.Select(ds => String.Join("", ds)).ToList()
            );
        }

        public void WriteToFile(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("digraph finite_state_machine {");
                writer.WriteLine("\tfontname=\"Helvetica,Arial,sans-serif\"");
                writer.WriteLine("\tnode [fontname=\"Helvetica,Arial,sans-serif\"]");
                writer.WriteLine("\tedge [fontname=\"Helvetica,Arial,sans-serif\"]");
                writer.WriteLine("\trankdir=LR;");
                writer.WriteLine("\tnode [shape = doublecircle]; " + string.Join(" ", FinalStates));
                writer.WriteLine("\tnode [shape = circle];");

                foreach (var transition in Transitions)
                {
                    if (transition.Symbol == "")
                    {
                        continue; // Skip empty string transitions
                    }

                    writer.WriteLine($"\t{transition.CurrentState} -> {transition.NextState} [label = \"{transition.Symbol}\"];");
                }

                writer.WriteLine("}");
            }
        }

    }
}