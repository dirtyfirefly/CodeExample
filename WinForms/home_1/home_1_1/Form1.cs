using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace home_1_1
{
    public partial class Form1 : Form
    {
        Form2 f = new Form2();
        bool end = false;

        public Form1()
        {
            InitializeComponent();

            Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
            timer2.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool good = true;
            double x = 0;
            double y = 0;
            try
            {
                x = Convert.ToDouble(textBox2.Text);
                y = Convert.ToDouble(textBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
                textBox2.Text = "";
                good = false;
            }

            if(good)
                textBox3.Text = (Math.Pow((Math.Pow(Math.Sin(x), 2) + 1), 3) - (Math.Sqrt(Math.Abs(y - 3)) / 3.01)).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Text = DateTime.Now.ToLongTimeString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            end = true;
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(end)
                MessageBox.Show("Bye-Bye", "Bye-Bye", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }
}
