using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Classes.Operators
{
    internal class ParenthesisOperator
    {
        Stack<Operator> OperatorsStack;

        public ParenthesisOperator()
        {
            OperatorsStack = new Stack<Operator>();
        }
        public double Calculate()
        {
            return 0;
        }

        public void AddOperators(string input, int pozBegin)
        {
        }

    }
}
