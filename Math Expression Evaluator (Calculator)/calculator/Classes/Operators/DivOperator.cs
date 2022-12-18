namespace calculator.Classes
{
    internal class DivOperator : Operator
    {

        public DivOperator()
        {
            priority = 2;
        }
        public override double Calculate() => Left / Right;
    }
}
