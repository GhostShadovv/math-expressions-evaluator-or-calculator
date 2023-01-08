namespace calculator.Classes
{
    internal class Operator
    {
        public int priority;
        public double Left { get; set; }
        public double Right { get; set; }
        public int Position { get; set; }

        public virtual double Calculate()
        {
            return 0;
        }
    }
}
