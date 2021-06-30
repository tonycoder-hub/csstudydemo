using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppHomework06
{
    public partial class Form2 : Form
    {
       
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.service.addOrder(new Order( int.Parse(this.textBox1.Text),
                new OrderDetails(this.textBox2.Text,this.textBox3.Text,double.Parse(this.textBox4.Text)
                )));
      
            Form2.ActiveForm.Close();
            
        }
    }
}
