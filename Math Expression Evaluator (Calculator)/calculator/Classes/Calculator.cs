using calculator.Classes.Functions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace calculator.Classes
{
    internal class Calculator
    {
        List<Operator> operatorList;
        List<BaseFunction> functionList;

        public Calculator()
        {
            operatorList = new List<Operator>();
            functionList = new List<BaseFunction>();
        }
        public double Calculate(string input)
        {
            List<Operator> opOrdered = operatorList.OrderByDescending(op => op.priority).ToList();
            while (operatorList.Count > 1)
            {
                int originalLocation = operatorList.IndexOf(opOrdered.FirstOrDefault());
                if (originalLocation.Equals(0))
                {
                    operatorList[1].Left = opOrdered.FirstOrDefault().Calculate();
                }
                if (originalLocation > 0 && originalLocation < operatorList.Count - 1)
                {
                    double tempRez = opOrdered.FirstOrDefault().Calculate();
                    operatorList[originalLocation - 1].Right = tempRez;
                    operatorList[originalLocation + 1].Left = tempRez;
                }
                if (originalLocation.Equals(operatorList.Count - 1))
                {
                    operatorList[originalLocation - 1].Right = opOrdered.FirstOrDefault().Calculate();
                }
                opOrdered.Remove(opOrdered.FirstOrDefault());
                operatorList.Remove(operatorList[originalLocation]);
            }
            return operatorList.FirstOrDefault().Calculate();
        }

        public bool AddOperators(string input)
        {
            string[] tempSplitted = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tempSplitted.Length; i++)
            {
                string itemSplitted = tempSplitted[i];
                switch (itemSplitted)
                {
                    case "+":
                        AddOperator addOperator = new AddOperator()
                        {
                            Left = double.Parse(tempSplitted[i - 1]),
                            Right = double.Parse(tempSplitted[i + 1]),
                            Position = operatorList.Count + 1
                        };
                        operatorList.Add(addOperator);
                        break;
                    case "-":
                        SubOperator subOperator = new SubOperator()
                        {
                            Left = double.Parse(tempSplitted[i - 1]),
                            Right = double.Parse(tempSplitted[i + 1]),
                            Position = operatorList.Count + 1
                        };
                        operatorList.Add(subOperator);
                        break;
                    case "x":
                        MulOperator mulOperator = new MulOperator()
                        {
                            Left = double.Parse(tempSplitted[i - 1]),
                            Right = double.Parse(tempSplitted[i + 1]),
                            Position = operatorList.Count + 1
                        };
                        operatorList.Add(mulOperator);
                        break;
                    case "÷":
                        DivOperator divOperator = new DivOperator()
                        {
                            Left = double.Parse(tempSplitted[i - 1]),
                            Right = double.Parse(tempSplitted[i + 1]),
                            Position = operatorList.Count + 1
                        };
                        operatorList.Add(divOperator);
                        break;
                    default:
                        break;
                }
            }
            return operatorList.Count>0;

        }

        internal string AddFunctions(string Input)
        {
            string tempInput = Input.ToLower();
            while (tempInput.Contains("sin"))
            {
                int sinBegin = tempInput.IndexOf("sin");
                int sinEnd = tempInput.IndexOf(')', sinBegin) + 1;
                string sinText = tempInput.Substring(sinBegin, sinEnd - sinBegin);
                functionList.Add(new Sin(sinText));

                tempInput = tempInput.Replace(sinText, functionList.Last().GetInputRadian());
            }
            while (tempInput.Contains("cos"))
            {
                int sinBegin = tempInput.IndexOf("cos");
                int sinEnd = tempInput.IndexOf(')', sinBegin) + 1;
                string sinText = tempInput.Substring(sinBegin, sinEnd - sinBegin);
                functionList.Add(new Cos(sinText));

                tempInput = tempInput.Replace(sinText, functionList.Last().GetInputRadian());
            }
            return tempInput;
        }

        internal double GetFunctionResult(string text)
        {
            double rez = 0;
            foreach (var item in functionList)
            {
                rez += double.Parse(item.GetInputRadian());
            }
            return rez;
        }
    }
}
