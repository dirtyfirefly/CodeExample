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
    public partial class Form2 : Form
    {
        //-- form --
        public Form2()
        {
            InitializeComponent();

        }

        //-- methods --
        public void ShowDialog(int column, int row,List<int> a1, List<int> a2, Action<List<int>, DataGridView> funk)
        {
            dataGridView1.ColumnCount   = column;
            dataGridView1.RowCount      = row;

            dataGridView2.ColumnCount   = column;
            dataGridView2.RowCount      = row;

            funk(a1, dataGridView1);
            funk(a2, dataGridView2);

            ShowDialog();
        }
    }
}
