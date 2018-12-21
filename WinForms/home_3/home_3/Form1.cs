using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace home_3
{
    public partial class Form1 : Form
    {
        //-- values --
        List<Results> res       = new List<Results>();
        BindingSource source    = new BindingSource();

        double xBegin       = 0;
        double xEnd         = 0;
        double xStep        = 0.1;
        bool updateBox      = true;

        //-- form --
        public Form1()
        {
            InitializeComponent();

            source.DataSource       = res;
            XYMembers(chart1);
            XYMembers(chart2);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CheckChartVisible(chart1, chart2);
            WorkOut(chart1);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            CheckChartVisible(chart2, chart1);
            WorkOut(chart2);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            updateBox       = true;
        }

        //-- methods --
        private void CheckChartVisible(Chart chartMain, Chart chart)
        {
            if (chartMain.Visible == false)
            {
                chartMain.Visible   = true;
                chart.Visible       = false;
                updateBox           = true;
            }
        }
        private void XYMembers(Chart chart)
        {
            chart.DataSource                   = source;
            chart.Series[0].XValueMember       = "Step";
            chart.Series[0].YValueMembers      = "Sin";
            chart.Series[1].XValueMember       = "Step";
            chart.Series[1].YValueMembers      = "Cos";
        }
        private bool SetData(TextBox box, ref double d, bool zero = true)
        {
            double tmp  = d;

            if (!double.TryParse(box.Text, out d))
            {
                d   = 0;
                box.Text = "0";
            }

            if (!zero && d <= 0)
            {
                d   = 0.1;
                box.Text = "0,1";
            }

            return tmp == d ? false : true;
        }
        private void Calculation()
        {
            double tmp  = xBegin;

            while(tmp <= xEnd)
            {
                source.Add(new Results(){ Sin   = Math.Sin(tmp),
                                          Cos   = Math.Cos(tmp) * 2,
                                          Step  = tmp });

                tmp     += xStep;
            }
        }
        private void Analysis(Chart chart)
        {
            if (!checkBox1.Checked)
                chart.Series[0].Enabled    = false;
            else
                chart.Series[0].Enabled    = true;

            if (!checkBox2.Checked)
                chart.Series[1].Enabled    = false;
            else
                chart.Series[1].Enabled    = true;
        }
        private void WorkOut(Chart chart)
        {
            bool update1    = SetData(textBox1, ref xBegin);
            bool update2    = SetData(textBox2, ref xEnd);
            bool update3    = SetData(textBox3, ref xStep, false);

            if (update1 || update2 || update3 || updateBox)
            {
                Calculation();
                Analysis(chart);
                chart.DataBind();
                source.Clear();
                updateBox       = false;
            }
        }
    }
}
