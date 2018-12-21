using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace home_2
{
    public partial class Form2 : Form
    {
        //-- values --
        List<Rabotnik> list;

        //-- form --
        public Form2()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<Rabotnik> tmp          = list.FindAll(x => x.Company.Equals(textBox1.Text));
            dataGridView1.RowCount      = tmp.Count;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1[0, i].Value   = tmp[i].Surname;
                dataGridView1[1, i].Value   = tmp[i].MidlePay().ToString("f2");
            }
        }

        //-- methods --
        public void ShowDialog(List<Rabotnik> arr)
        {
            list = arr;
            ShowDialog();
        }
    }
}
