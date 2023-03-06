namespace src
{
    class Transition
    {
        public string CurrentState { get; set; }
        public string NextState { get; set; }
        public string Symbol { get; set; }
        public Transition(string currentState, string nextState, string symbol)
        {
            CurrentState = currentState;
            NextState = nextState;
            Symbol = symbol;
        }
    }
}