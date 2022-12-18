namespace calculator.Classes
{
    internal class SubOperator : Operator
    {

        public SubOperator()
        {
            priority = 1;
        }
        public override double Calculate() => Left - Right;
    }
}
