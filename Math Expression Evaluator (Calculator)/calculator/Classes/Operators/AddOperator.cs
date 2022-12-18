namespace calculator.Classes
{
    internal class AddOperator : Operator
    {
        public AddOperator()
        {
            priority = 1;
        }
        public override double Calculate() => Left + Right;
    }
}
