namespace calculator.Classes
{
    internal class MulOperator : Operator
    {

        public  MulOperator()
        {
            priority = 2;
        }
        public override double Calculate()
        {
            return Left * Right;
        }
    }
}
