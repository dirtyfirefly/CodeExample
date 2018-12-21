using home_1_2.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace home_1_2
{
    public partial class Form1 : Form
    {
        Matrix m;

        int colMin = 0;
        int rowMin = 0;
        int colMax = 0;
        int rowMax = 0;

        public Form1()
        {
            InitializeComponent();

            dataGridView1.RowCount = 1;
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = (int)numericUpDown1.Value;

            if (colMax >= (int)numericUpDown1.Value)
                colMax = (int)numericUpDown1.Value - 1;
            if (colMin >= (int)numericUpDown1.Value)
                colMin = (int)numericUpDown1.Value - 1;
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.RowCount = (int)numericUpDown2.Value;

            if (rowMax >= (int)numericUpDown2.Value)
                rowMax = (int)numericUpDown2.Value - 1;
            if (rowMin >= (int)numericUpDown2.Value)
                rowMin = (int)numericUpDown2.Value - 1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            m = new Matrix((int)numericUpDown1.Value, (int)numericUpDown2.Value);

            for (int i = 0; i < (int)numericUpDown1.Value; i++)
            {
                for (int j = 0; j < (int)numericUpDown2.Value; j++)
                {
                    try
                    {
                        m[i, j] = Convert.ToInt32(dataGridView1[i, j].Value);
                    }
                    catch { }
                }
            }

            if (radioButton1.Checked)
            {
                if (colMax != colMin || rowMax != rowMin)
                    dataGridView1[colMin, rowMin].Style.BackColor = Color.White;
                m.CaseMinValue(ref colMin, ref rowMin);
                dataGridView1[colMin, rowMin].Style.BackColor = Color.Blue;
            }
            else
            {
                if(colMin != colMax || rowMin != rowMax)
                    dataGridView1[colMax, rowMax].Style.BackColor = Color.White;
                m.CaseMaxValue(ref colMax, ref rowMax);
                dataGridView1[colMax, rowMax].Style.BackColor = Color.Green;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            for (int i = 0; i < m.isWidth; i++)
            {
                for (int j = 0; j < m.isHeight; j++)
                {
                    if (m[i, j] > 0)
                        listBox1.Items.Add(m[i, j]);
                }
            }
        }
    }
}
