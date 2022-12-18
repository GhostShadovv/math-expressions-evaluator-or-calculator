using System;
using System.Linq;

namespace calculator.Classes.Functions
{
    internal class Sin : BaseFunction
    {
        public Sin(string text)
        {
            if (text.ToLower().Contains("sin"))
            {
                Input = text.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries).Last();
            }
            else
            {
                Input = text;
            }
        }


        public override string GetInputDegree()
        {
            return (Math.Sin(double.Parse(Input) / (180 / Math.PI))).ToString();
        }
        public override string GetInputRadian()
        {
            return (Math.Sin(double.Parse(Input))).ToString();
        }
    }
}
