using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace home_2_2
{
    public partial class Form1 : Form
    {
        //-- values --
        List<int> arr1              = new List<int>();
        List<int> arr2              = new List<int>();
        Form2 f2                    = new Form2();
        
        //-- form --
        public Form1()
        {
            InitializeComponent();

            dataGridView1.RowCount      = 1;
            dataGridView2.RowCount      = 1;
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.RowCount      = (int)numericUpDown1.Value;
            dataGridView2.RowCount      = (int)numericUpDown1.Value;
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount   = (int)numericUpDown2.Value;
            dataGridView2.ColumnCount   = (int)numericUpDown2.Value;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            InList();
            NoNegativ();

            if (checkBox1.Checked)
            {
                dataGridView2.Visible   = false;
                f2.ShowDialog(dataGridView1.ColumnCount, dataGridView1.RowCount, arr1, arr2, InGrid);
            }
            else
            {
                InGrid(arr2, dataGridView2);
                dataGridView2.Visible   = true;
            }
        }

        //-- methods --
        private void NoNegativ()
        {
            int amtPosi         = arr2.Count(x => x >= 0);
            for (int i = 0; i < arr2.Count; i++)
            {
                if (arr2[i] < 0)
                    arr2[i]     = amtPosi;
            }
        }
        private void InList()
        {
            arr1.Clear();
            arr2.Clear();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    int tmp     = 0;
                    try
                    {
                        tmp     = Convert.ToInt32(dataGridView1[j, i].Value);
                    }
                    catch
                    {
                        tmp     = 0;
                    }
                    
                    arr1.Add(tmp);
                    arr2.Add(tmp);
                }
            }
        }
        private void InGrid(List<int> a, DataGridView g)
        {
            int l   = 0;
            for (int i = 0; i < g.RowCount; i++)
            {
                for (int j = 0; j < g.ColumnCount; j++)
                {
                    g[j, i].Value   = a[l++];
                }
            }
        }
    }
}
