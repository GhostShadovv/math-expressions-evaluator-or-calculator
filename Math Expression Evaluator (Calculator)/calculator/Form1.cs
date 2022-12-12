using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class Form1 : Form
    {
        private bool clearOutput;
        private bool separatorActive;
        private bool functionActive;
        private bool operatorActive;
        public Form1()
        {
            InitializeComponent();
            tbInput.ResetText();
            tbOutput.ResetText();
            clearOutput = false;
            separatorActive = false;
            functionActive = false;
            operatorActive = false;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            AddOutputText("0");
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            AddOutputText("1");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            AddOutputText("2");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            AddOutputText("3");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            AddOutputText("4");
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            AddOutputText("5");
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            AddOutputText("6");
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            AddOutputText("7");
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            AddOutputText("8");
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            AddOutputText("9");
        }

        private void btnSeparator_Click(object sender, EventArgs e)
        {
            if (!separatorActive)
            {
                AddOutputText(",");
            }
            separatorActive = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddOperatorText("+");
        }


        private void btnSub_Click(object sender, EventArgs e)
        {
            AddOperatorText("-");

        }

        private void btnMul_Click(object sender, EventArgs e)
        {
            AddOperatorText("x");
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            AddOperatorText("÷");
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Clear:
                    break;
                case Keys.Enter:
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                    btn0.PerformClick();
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    btn1.PerformClick();
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    btn2.PerformClick();
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    btn3.PerformClick();
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    btn4.PerformClick();
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    btn5.PerformClick();
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    btn6.PerformClick();
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    btn7.PerformClick();
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    btn8.PerformClick();
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    btn9.PerformClick();
                    break;
                case Keys.Decimal:
                case Keys.Oemcomma:
                    btnSeparator.PerformClick();
                    break;
                case Keys.Add:
                    btnAdd.PerformClick();
                    break;
                case Keys.Subtract:
                    btnSub.PerformClick();
                    break;
                case Keys.Multiply:
                    btnMul.PerformClick();
                    break;
                case Keys.Divide:
                    btnDiv.PerformClick();
                    break;
                default:
                    break;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (tbOutput.Text.First().Equals('-'))
            {
                tbOutput.Text = tbOutput.Text.Substring(1);
            }
            else
            {
                tbOutput.Text = "-" + tbOutput.Text;
            }
        }

        private void btnSin_Click(object sender, EventArgs e)
        {
            AddFunctionTest("sin");
        }
        private void btnCos_Click(object sender, EventArgs e)
        {
            AddFunctionTest("cos");
        }


        #region  Helpers
        private void AddFunctionTest(string function)
        {
            if (operatorActive)
            {
                tbInput.Text += $"{function}({tbOutput.Text})";
                tbOutput.ResetText();
                tbOutput.Text = "0";
                operatorActive = false;
            }
            else
            {
                tbInput.Text = $"{function}({tbOutput.Text})";
                switch (function)
                {
                    case "sin":
                        tbOutput.Text = Math.Sin(double.Parse(tbOutput.Text)).ToString(); break;
                    case "cos":
                        tbOutput.Text = Math.Cos(double.Parse(tbOutput.Text)).ToString(); break;
                    default:
                        break;
                }
                clearOutput = true;
            }
            functionActive = true;

        }
        private void AddOperatorText(string operatorText)
        {
            if (functionActive)
            {
                tbInput.Text += $" {operatorText} ";
            }
            else
            {
                tbInput.Text += $"{tbOutput.Text} {operatorText} ";
            }
            operatorActive = true;
            separatorActive = false;
            functionActive = false;
            clearOutput = true;
        }
        private void AddOutputText(string text)
        {
            if (clearOutput)
            {
                tbOutput.ResetText();
            }
            tbOutput.Text += text;
            if (text != "0" && tbOutput.Text.StartsWith("0"))
            {
                tbOutput.Text = tbOutput.Text.Substring(1);
            }
            if (text.Equals("0") && tbOutput.Text.Equals("00"))
            {
                tbOutput.Text = "0";
            }
            clearOutput = false;
        }
        #endregion



        private void btnClear_Click(object sender, EventArgs e)
        {
            tbOutput.ResetText();
            tbInput.ResetText();
            clearOutput = false;
            separatorActive = false;
            functionActive = false;
            operatorActive = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (tbOutput.Text.Length > 0)
            {
                if (tbOutput.Text.Last().Equals(','))
                {
                    separatorActive = false;
                }
                tbOutput.Text = tbOutput.Text.Substring(0, tbOutput.Text.Length - 1);
                if (string.IsNullOrEmpty(tbOutput.Text))
                {
                    tbOutput.Text = "0";
                }
            }
        }

        private void btnBeginParanthesis_Click(object sender, EventArgs e)
        {
            if(operatorActive)
            {
                tbInput.Text += "(";
            }
            else
            {
                if(tbInput.Text.Length > 0)
                {
                    tbInput.Text += " x (";
                }
                else
                {
                    tbInput.Text += "(";
                }
            }
        }

        private void btnEndParanthesis_Click(object sender, EventArgs e)
        {
            tbInput.Text += $"{tbOutput.Text})";
            tbOutput.Text = "0";
            functionActive = true;
        }
    }
}
