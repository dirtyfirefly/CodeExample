using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace home_2
{
    public partial class Form1 : Form
    {
        //-- value --
        Rabotnik[] arr;
        int yungStrNum  = 0;
        Form2 f2        = new Form2();

        //-- form --
        public Form1()
        {
            InitializeComponent();

            saveFileDialog1.InitialDirectory    = Application.StartupPath;
            openFileDialog1.InitialDirectory    = Application.StartupPath;
        }
        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (panel1.Visible)
            {
                dataGridView1.RowCount      = 1;
                dataGridView1[0, 0].Value   = "";
                dataGridView1[1, 0].Value   = "";
                dataGridView1[2, 0].Value   = "";
                dataGridView1[3, 0].Value   = "";
                dataGridView1[4, 0].Value   = "";
                dataGridView1[5, 0].Value   = "";
                dataGridView1[6, 0].Value   = "";
                dataGridView1[7, 0].Value   = "";
                dataGridView1[8, 0].Value   = "";
                arr                         = null;
            }
            else
            {
                panel1.Visible                      = true;
                обработкаToolStripMenuItem.Enabled  = true;
                сохранитьToolStripMenuItem.Enabled  = true;
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void самыйМолодойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();

            const int amtColumn     = 9;

            for (int i = 0; i < amtColumn; i++)
                dataGridView1[i, yungStrNum].Style.BackColor    = Color.White;

            yungStrNum = YungEmployee();

            for (int i = 0; i < amtColumn; i++)
                dataGridView1[i, yungStrNum].Style.BackColor    = Color.Red;

        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                const int numCell     = 9;

                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                {
                    for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                    {
                        sw.Write(   dataGridView1[0, i].Value + ";" +
                                    dataGridView1[1, i].Value + ";" +
                                    dataGridView1[2, i].Value
                                    );
                        for (int j = 3; j < numCell; j++)
                        {
                            sw.Write(";");

                            try
                            {
                                sw.Write(Convert.ToDecimal(dataGridView1[j, i].Value));
                            }
                            catch
                            {
                                sw.Write("0");
                            }
                        }
                        sw.WriteLine();
                    }
                }
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 2 && !Regex.Match(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString(), @"([0-2][1-9]|3[0-1])([.])(0[0-9]|1[0-2])\2([0-9][0-9][0-9][0-9])").Success)
            {
                dataGridView1[e.ColumnIndex, e.RowIndex].Value = "";
                MessageBox.Show("Некоректный ввод даты\nНеобходим формат: дд.мм.гггг");
            }
        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                panel1.Visible                          = true;
                обработкаToolStripMenuItem.Enabled      = true;
                сохранитьToolStripMenuItem.Enabled      = true;

                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        int i   = 0;

                        while (!sr.EndOfStream)
                        {
                            dataGridView1.RowCount      = i + 2;
                            string[] str = sr.ReadLine().Split(';');

                            dataGridView1[0, i].Value   = str[0];
                            dataGridView1[1, i].Value   = str[1];
                            dataGridView1[2, i].Value   = str[2];
                            dataGridView1[3, i].Value   = str[3];
                            dataGridView1[4, i].Value   = str[4];
                            dataGridView1[5, i].Value   = str[5];
                            dataGridView1[6, i].Value   = str[6];
                            dataGridView1[7, i].Value   = str[7];
                            dataGridView1[8, i].Value   = str[8];

                            ++i;
                        }
                    }
                }
                catch { }
            }
        }
        private void средняяЗарплатаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InArr();
            f2.ShowDialog(arr.ToList());
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                средняяЗарплатаToolStripMenuItem_Click(null, null);
            else
                самыйМолодойToolStripMenuItem_Click(null, null);
        }

        //-- metods --
        private void InArr()
        {
            const int months    = 6;
            const int bais      = 3;

            arr = new Rabotnik[dataGridView1.RowCount - 1];
            
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                decimal[] tmp = new decimal[months];
                for (int j = 0; j < months; j++)
                {
                    try
                    {
                        tmp[j]  = Convert.ToDecimal(dataGridView1[j + bais, i].Value);
                    }
                    catch
                    {
                        tmp[j]  = 0;
                    }
                }

                arr[i] = new Rabotnik(  Convert.ToString(dataGridView1[0, i].Value),
                                        Convert.ToString(dataGridView1[1, i].Value),
                                        Convert.ToString(dataGridView1[2, i].Value),
                                        tmp);
            }
        }
        private void InGrid()
        {
            const int amtColumn             = 9;
            const int amtMounth             = 6;

            dataGridView1.RowCount          = arr.Length;
            dataGridView1.ColumnCount       = amtColumn;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1[i, 0].Value   = arr[i].Company;
                dataGridView1[i, 1].Value   = arr[i].Surname;
                dataGridView1[i, 2].Value   = arr[i].DateB;

                for (int j = 0; j < amtMounth; j++)
                {
                    dataGridView1[i, 3].Value   = arr[i][j];
                    dataGridView1[i, 4].Value   = arr[i][j];
                    dataGridView1[i, 5].Value   = arr[i][j];
                    dataGridView1[i, 6].Value   = arr[i][j];
                    dataGridView1[i, 7].Value   = arr[i][j];
                    dataGridView1[i, 8].Value   = arr[i][j];
                }
            }
        }
        private int YungEmployee()
        {
            InArr();
            int min     = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[min].Age > arr[i].Age)
                    min     = i;
            }
            return min;
        }
    }
}
