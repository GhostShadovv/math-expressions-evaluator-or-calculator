using calculator.Classes.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;

namespace calculator.Classes
{
    internal class Calculator
    {
        List<Operator> operatorList;
        List<BaseFunction> functionList;
        bool _degree;

        public Calculator(bool degree)
        {
            _degree = degree;
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
            double ret = operatorList.FirstOrDefault().Calculate();
            operatorList.Clear();
            functionList.Clear();
            return ret;
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
            return operatorList.Count > 0;

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

                if (_degree)
                {
                    tempInput = tempInput.Replace(sinText, functionList.Last().GetInputDegree());
                }
                else
                {
                    tempInput = tempInput.Replace(sinText, functionList.Last().GetInputRadian());
                }
            }
            while (tempInput.Contains("cos"))
            {
                int cosBegin = tempInput.IndexOf("cos");
                int cosEnd = tempInput.IndexOf(')', cosBegin) + 1;
                string cosText = tempInput.Substring(cosBegin, cosEnd - cosBegin);
                functionList.Add(new Cos(cosText));

                if (_degree)
                {
                    tempInput = tempInput.Replace(cosText, functionList.Last().GetInputDegree());
                }
                else
                {
                    tempInput = tempInput.Replace(cosText, functionList.Last().GetInputRadian());
                }
            }
            return tempInput;
        }

        internal double GetFunctionResult(string text)
        {
            double rez = 0;
            foreach (var item in functionList)
            {
                if (_degree)
                {
                    rez += double.Parse(item.GetInputDegree());
                }
                else
                {
                    rez += double.Parse(item.GetInputRadian());
                }
            }
            return rez;
        }

        internal string SolveParanthesis(string txtInput)
        {
            string tmpInput = txtInput;
            string tmpInputNoParanthesis = tmpInput.Substring(1, tmpInput.Length - 2);
            if (IsFunction(tmpInputNoParanthesis))
            {
                AddFunctions(tmpInputNoParanthesis);
                return GetFunctionResult(tmpInputNoParanthesis).ToString();
            }
            tmpInput = "(" + ReplaceFunctions(tmpInputNoParanthesis) + ")";
            while (tmpInput.Contains("("))
            {
                int openPoz = tmpInput.LastIndexOf("(");
                int closePoz = tmpInput.IndexOf(")", openPoz) + 1;
                string content = tmpInput.Substring(openPoz, closePoz - openPoz);
                string contentNoParanthesis = content.Substring(1, content.Length - 2);
                AddOperators(contentNoParanthesis);
                double rez = Calculate(contentNoParanthesis);
                tmpInput = tmpInput.Replace(content, rez.ToString());
            }

            //AddOperators(AddFunctions(txtInput);
            return tmpInput;
        }

        private string ReplaceFunctions(string input)
        {
            string temp = input;

            while (temp.Contains("sin") || temp.Contains("cos"))
            {
                int funcStartPoz = temp.IndexOf("sin(");
                int intermediar = temp.IndexOf("(", funcStartPoz + 4);
                int funcStopPoz = temp.IndexOf(")", funcStartPoz);

                if (intermediar < funcStopPoz && intermediar != -1)
                {
                    MessageBox.Show("Complex argument inside functions not supported");
                    return "0";
                }
                string funcTxt = temp.Substring(funcStartPoz, funcStopPoz - funcStartPoz + 1);
                temp = temp.Replace(funcTxt, AddFunctions(funcTxt));
                functionList.Clear();
            }

            return temp;
        }

        private bool IsFunction(string txtInput)
        {
            if (txtInput.Count(a => a.Equals(' ')).Equals(0))
            {
                return true;
            }
            return false;
        }
    }
}
