using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppMar19th
{
    public partial class Form1 : Form
    {
       
        int count,num;
        
        public Form1()
        {
            InitializeComponent();
    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            num =Convert.ToInt32( this.textBox1.Text);
            this.timer1.Interval = 1000;
            timer1.Start();
            this.label2.Text = "starts....";
        }
       
    private void timer1_Tick(object sender, EventArgs e)
        {

            count++;
            this.label2.Text = (num-count).ToString()+" second(s) left.";
            if (count == num)
            {
                this.label2.Text = "Ring Ring Ring~~~";
                timer1.Stop();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.label2.Text = "Refreshed.";
            timer1.Stop();
        }

        
       
    }
}
