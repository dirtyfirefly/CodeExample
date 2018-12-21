using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Dol
{
    public partial class Form1 : Form
    {
        //Объявляем приложение
        Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();

        //-- value --
        Form2 f2 = new Form2();

        decimal rate = 0.00m;
        DateTime date = DateTime.Now;
        string path = "";

        const decimal oneHour = 3600000;
        const decimal delim = 10000;
        
        bool flag = false;
        decimal timeWait = 1;
        
        //-- form --
        public Form1()
        {
            InitializeComponent();

            WindowState = FormWindowState.Minimized;
            LoadOption();
            numericUpDown1.Value = timeWait;
            timer1.Interval = Convert.ToInt32(timeWait * oneHour);
            dateTimePicker1.Value = date;
            notifyIcon1.Text = date.ToShortTimeString() + "\n" + rate.ToString("F2");
            if(numericUpDown1.Enabled)
                timer1.Start();
            path = Application.StartupPath;

            DateWWW();
            RateDol();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Visible = false;
            WindowState = FormWindowState.Minimized;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            date = dateTimePicker1.Value;
            Visible = false;
            WindowState = FormWindowState.Minimized;
            RateDol();
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timeWait = numericUpDown1.Value;
            timer1.Interval = Convert.ToInt32(timeWait * oneHour);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveOption();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateWWW();
            RateDol();
        }
        private void optionsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
        }
        private void descardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();
                ex.Workbooks.Open(path + "\\1.xlsx",
                                  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                  Type.Missing, Type.Missing);
                ex.Visible = true;
            }
            catch { }
        }
        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Close();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                timer1.Stop();
                numericUpDown1.Enabled = false;
            }
            else
            {
                timer1.Start();
                numericUpDown1.Enabled = true;
            }
        }
        private void diagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f2.ShowDialog();
        }

        //-- timeWait --
        private void SaveOption()
        {
            using (StreamWriter sw = new StreamWriter("time.data"))
            {
                sw.WriteLine(timeWait);
                sw.WriteLine((checkBox1.Checked).ToString());
            }
        }
        private void LoadOption()
        {
            using (StreamReader sr = new StreamReader("time.data"))
            {
                timeWait = Convert.ToDecimal(sr.ReadLine());
                checkBox1.Checked = Convert.ToBoolean(sr.ReadLine());
                if (checkBox1.Checked)
                    numericUpDown1.Enabled = false;
                else
                    numericUpDown1.Enabled = true;
            }
        }

        //-- html --
        private void DateWWW()
        {
            string Responce = "";
            try
            {
                WebClient wc = new WebClient();
                Responce = wc.DownloadString("http://bilet.pp.ru/calculator_rus/tochnoe_moskovskoe_vremia.php");
            }
            catch
            {
                MessageBox.Show("no internet connection");
                date = DateTime.Now;
            }

            try
            {
                string tim = Regex.Match(Responce, "<b>(\\d\\d:\\d\\d:\\d\\d)</b>").Groups[1].Value;
                string dat = Regex.Match(Responce, "Дата сегодня:<br>\r\n<b>(.+)</b></h1>").Groups[1].Value;
                dat = dat.Replace('-', '.');
                date = Convert.ToDateTime(dat + tim);
            }
            catch { }
        }
        public void RateDol()
        {
            string Responce = "";
            try
            {
                WebClient wc = new WebClient();
                Responce = wc.DownloadString("http://val.ru/valhistory.asp?tool=840");
            }
            catch
            {
                MessageBox.Show("no internet connection");
            }
            
            try
            {
                string Rate = Regex.Match(Responce, date.ToShortDateString() + "</td><td width=\\d+>\\d</td><td width=\\d+>(\\d+.\\d+)</td>").Groups[1].Value;
                Rate = Rate.Remove(2, 1);
                rate = Convert.ToDecimal(Rate) / delim;
                notifyIcon1.Text = date.ToShortTimeString() + "\n" + rate.ToString("F2");
                flag = true;
            }
            catch
            {
                MessageBox.Show("date not found");
                rate = 0.00m;
                notifyIcon1.Text = date.ToShortTimeString() + "\n" + rate.ToString("F2");
                flag = false;
            }

            if (flag)
                InExcel();
        }

        //-- Excel --
        public void InExcel()
        {
            try
            {
                //Объявляем приложение
                //Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();
                ex.Workbooks.Open(path + "\\1.xlsx",
                                  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                  Type.Missing, Type.Missing);

                //Получаем первый лист документа (счет начинается с 1)
                Excel.Worksheet sheet = (Excel.Worksheet)ex.Worksheets.get_Item(3);
                sheet.Cells[2, 3] = date.ToShortDateString();
                sheet.Cells[3, 3] = rate;
				ex.Close();
            }
            catch { }
        }
    }
}
