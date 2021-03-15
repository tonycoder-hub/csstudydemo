using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormAppDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            double a = Convert.ToDouble(textBox1.Text);
            double b = Convert.ToDouble(textBox2.Text);
            char c = GetCheckedRadioButton(radioButton1, radioButton2, radioButton3, radioButton4, radioButton5);
            double res = Calculate(a, b, c);
            label5.Text = Convert.ToString(res);
        }

        private char GetCheckedRadioButton(RadioButton radioButton1, RadioButton radioButton2, RadioButton radioButton3, RadioButton radioButton4, RadioButton radioButton5)
        {
            if (radioButton1.Checked)
            {
                return '+';
            }
            else if (radioButton2.Checked)
            {
                return '-';
            }
            else if (radioButton3.Checked)
            {
                return '*';
            }
            else if (radioButton4.Checked)
            {
                return '/';
            }
            else if (radioButton5.Checked)
            {
                return '%';
            }
            else
            {
                return 'x';
            }
        }

        public double Calculate(double a, double b, char c)
        {
            switch (c)
            {
                case '+':
                    {
                        return a + b;
                    }
                case '-':
                    {
                        return a - b;
                    }
                case '*':
                    {
                        return a * b;
                       
                    }
                case '/':
                    {
                        return a / b;
                        
                    }
                case '%':
                    {
                        return a % b;
                        
                    }
                default:
                    {
                        throw new DataException("error occurs");
                    }
            }
        }

    }
}
