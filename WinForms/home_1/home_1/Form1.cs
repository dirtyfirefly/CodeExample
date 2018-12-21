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
    public partial class Form1 : Form
    {
        Form2 f = new Form2();
        Form3 a = new Form3();
        MyArray arr;

        public Form1()
        {
            InitializeComponent();

            comboBox1.SelectedIndex  = 0;
            dataGridView1.RowCount   = 1;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = Convert.ToInt32(numericUpDown1.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            arr = new MyArray(dataGridView1.ColumnCount);

            for (int i = 0; i < arr.isLength; i++)
            {
                try
                {
                    arr[i] = Convert.ToInt32(dataGridView1[i, 0].Value);
                }
                catch
                {
                    MessageBox.Show("Возможно неверное\nвычисление произведения.\nВведена буква или знак препинания\nтам где ожидалась цифра!\nЗамена на 0.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (comboBox1.SelectedIndex == 0)
                f.ShowDialog(arr, "Сумма = ", arr.ArraySum());
            else
                f.ShowDialog(arr, "Произведение = ", arr.ArrayMulty());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            a.ShowDialog();
        }
    }
}
