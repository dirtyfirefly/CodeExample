using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//закрытые поля фамилия, название фирмы, зарплата за полугодие(массив), дата рождения, метод для определения средней зарплаты, свойство для определения возраста.

namespace home_2
{
    public class Rabotnik
    {
        decimal[] pay;
        public decimal this[int i]
        {
            get      => pay[i];
            set      => pay[i] = value > 0 ? value : pay[i];
        }

        public string DateB         { get; set; }
        public string Surname       { get; set; }
        public string Company       { get; set; }

        public decimal MidlePay()   => pay.Average();
        public int Age              => DateTime.Now.Year - Convert.ToDateTime(DateB + " 00:00:00").Year;

        public Rabotnik(string company, string surname, string dateBirth, params decimal[] amt)
        {
            Company     = company;
            Surname     = surname;
            DateB       = dateBirth;
            pay         = amt;
        }
    }
}
