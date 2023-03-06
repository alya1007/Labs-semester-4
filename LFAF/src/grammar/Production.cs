namespace src2
{
    class Production
    {
        public String leftSide { get; set; }
        public String rightSide { get; set; }
        public Production(String leftSide, String rightSide)
        {
            this.leftSide = leftSide;
            this.rightSide = rightSide;
        }
    }
}