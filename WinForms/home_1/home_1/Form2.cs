using home_1.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace home_1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            dataGridView1.ColumnCount = 1;
        }

        public void ShowDialog(MyArray arr, string str, int res)
        {
            dataGridView1.RowCount = arr.isLength;

            for (int i = 0; i < arr.isLength; i++)
                dataGridView1[0, i].Value = arr[i];

            label1.Text = str;
            textBox1.Text = res.ToString();

            ShowDialog();
        }
    }
}
