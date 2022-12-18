using System;
using System.Linq;

namespace calculator.Classes.Functions
{
    internal class Cos : BaseFunction
    {
        public Cos(string text)
        {
            if (text.ToLower().Contains("cos"))
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
            return (Math.Cos(double.Parse(Input) / (180 / Math.PI))).ToString();
        }
        public override string GetInputRadian()
        {
            return (Math.Cos(double.Parse(Input))).ToString();
        }
    }
}
