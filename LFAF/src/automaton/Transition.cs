namespace src
{
    class Transition
    {
        public string CurrentState { get; set; }
        public string Symbol { get; set; }
        public string NextState { get; set; }
        public Transition(string currentState, string symbol, string nextState)
        {
            CurrentState = currentState;
            Symbol = symbol;
            NextState = nextState;
        }
    }
}