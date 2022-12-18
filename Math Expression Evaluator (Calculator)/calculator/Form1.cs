using calculator.Classes;
using System;
using System.Linq;
using System.Windows.Forms;

namespace calculator
{
    public partial class Form1 : Form
    {
        private bool flClearOutput;
        private bool vlSeparatorActive;
        private bool flFunctionActive;
        private bool flOperatorActive;
        private bool flClearInput;
        private bool degree;

        public Form1()
        {
            InitializeComponent();
            tbInput.ResetText();
            tbOutput.ResetText();
            flClearOutput = false;
            flClearInput = false;
            vlSeparatorActive = false;
            flFunctionActive = false;
            flOperatorActive = false;
            degree = false;
        }
        private void btnSeparator_Click(object sender, EventArgs e)
        {
            if (!vlSeparatorActive)
            {
                AddOutputText(",");
            }
            vlSeparatorActive = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Clear:
                    btnClear.PerformClick();
                    break;
                case Keys.Enter:
                    btnEqual.PerformClick();
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
            if (tbOutput.Text.StartsWith("-"))
            {
                tbOutput.Text = tbOutput.Text.Substring(1);
            }
            else
            {
                tbOutput.Text = "-" + tbOutput.Text;
            }
        }


        #region  Helpers
        private void AddFunctionTest(string function)
        {
            flFunctionActive = true;
            if (flOperatorActive)
            {
                tbInput.Text += $"{function}({tbOutput.Text})";
                tbOutput.ResetText();
                tbOutput.Text = "0";
                flOperatorActive = false;
            }
            else
            {
                tbInput.Text = $"{function}({tbOutput.Text})";
                btnEqual.PerformClick();
                flClearOutput = true;
            }

        }
        private void AddOperatorText(string operatorText)
        {
            if (flFunctionActive)
            {
                tbInput.Text += $" {operatorText} ";
            }
            else
            {
                tbInput.Text += $"{tbOutput.Text} {operatorText} ";
            }
            flOperatorActive = true;
            vlSeparatorActive = false;
            flFunctionActive = false;
            flClearOutput = true;
            flClearInput = false;
        }
        private void AddOutputText(string text)
        {
            if (flClearOutput)
            {
                tbOutput.ResetText();
            }
            if (flClearInput)
            {
                tbInput.ResetText();
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
            flClearOutput = false;
            flClearInput = false;
        }
        #endregion



        private void btnClear_Click(object sender, EventArgs e)
        {
            tbOutput.ResetText();
            tbInput.ResetText();
            flClearOutput = false;
            vlSeparatorActive = false;
            flFunctionActive = false;
            flOperatorActive = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (tbOutput.Text.Length > 0)
            {
                if (tbOutput.Text.Last().Equals(','))
                {
                    vlSeparatorActive = false;
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
            if (flOperatorActive)
            {
                tbInput.Text += tbOutput.Text + " x (";
                flClearOutput = true;
            }
            else
            {
                if (tbOutput.Text.Length > 0)
                {
                    tbInput.Text += tbOutput.Text + " x (";
                    flClearOutput = true;
                }
                else
                {
                    tbInput.Text += "(";
                    flClearOutput = true;
                }
            }
        }

        private void btnEndParanthesis_Click(object sender, EventArgs e)
        {
            tbInput.Text += $"{tbOutput.Text})";
            tbOutput.Text = "0";
            flFunctionActive = true;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (!flFunctionActive)
            {
                tbInput.Text += tbOutput.Text;
            }
            Calculator calculator = new Calculator();
            if (calculator.AddOperators(calculator.AddFunctions(tbInput.Text)))
            {
                tbOutput.Text = calculator.Calculate(tbInput.Text).ToString();
            }
            else
            {
                tbOutput.Text = calculator.GetFunctionResult(tbInput.Text).ToString();
            }
            flClearOutput = true;
            flClearInput = true; 
            flFunctionActive = false;
            flOperatorActive = false;
        }

        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            btnFocus.Focus();
        }

        private void btnTrig_Click(object sender, EventArgs e)
        {
            if (btnTrig.Text.Equals("RAD"))
            {
                btnTrig.Text = "DEG";
                degree = true;
                if(tbOutput.Text.Length > 0)
                {
                    double val = double.Parse(tbOutput.Text) * (180 / Math.PI);
                    tbOutput.Text = val.ToString();
                    flClearOutput = true;
                    
                }
            }
            else
            {
                btnTrig.Text = "RAD";
                degree = false;
                if (tbOutput.Text.Length > 0)
                {
                    double val = double.Parse(tbOutput.Text) / (180 / Math.PI);
                    tbOutput.Text = val.ToString();
                    flClearOutput = true;
                }

            }
        }

        private void BtnNrClick(object sender, EventArgs e)
        {
            AddOutputText((sender as Button).Text);
        }

        private void BtnOperatorClick(object sender, EventArgs e)
        {
            AddOperatorText((sender as Button).Text);
        }

        private void btnFunctionClick(object sender, EventArgs e)
        {
            AddFunctionTest((sender as Button).Text);
        }
    }
}
