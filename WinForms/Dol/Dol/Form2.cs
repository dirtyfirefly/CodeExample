using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dol
{
    public partial class Form2 : Form
    {
        //-- values --
        const decimal delim     = 10000;
        List<Data> data = new List<Data>();

        //-- form --
        public Form2()
        {
            InitializeComponent();

            DataForDiagram();
            chart1.DataSource = data;
            chart1.Series[0].XValueMember = "Date";
            chart1.Series[0].YValueMembers = "Rates";
            chart1.DataBind();
        }

        //-- methods --
        private void DataForDiagram()
        {
            string Responce     = "";
            try
            {
                WebClient wc    = new WebClient();
                Responce        = wc.DownloadString("http://val.ru/valhistory.asp?tool=840");
            }
            catch{ }

            try
            {
                MatchCollection Rate = Regex.Matches(Responce, "(([0-2][1-9]|3[0-1])([.])(0[0-9]|1[0-2])\\3([0-9][0-9][0-9][0-9]))</td><td width=\\d+>\\d</td><td width=\\d+>(\\d+.\\d+)</td>");
                for (int i = 0; i < 10; i++)
                {
                    data.Add(new Data() { Rates = Convert.ToDecimal(Rate[i].Groups[6].Value.Remove(2, 1)) / delim, Date = Rate[i].Groups[1].Value });
                }
                data.Reverse();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
