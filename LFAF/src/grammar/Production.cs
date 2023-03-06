namespace src
{
    class Production
    {
        public String[] LeftSide { get; set; }
        public String[] RightSide { get; set; }
        public Production(String[] leftSide, String[] rightSide)
        {
            this.LeftSide = leftSide;
            this.RightSide = rightSide;
        }
    }
}