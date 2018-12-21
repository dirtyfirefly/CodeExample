using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Описать структуру для представления информации о спортсменах, содержащую следующие элементы:  поля для хранения фамилии, года рождения, вида спорта и разряда.Написать метод-расширение для структуры Спортсмен, который определяет возраст.

namespace home_10
{
    struct Sportsman : IComparable<Sportsman>
    {
        public string Surname { get; private set; }
        public DateTime YearBirth { get; private set; }
        public string Sport { get; set; }
        public string Class { get; set; }

        public int CompareTo(Sportsman other)
        {
            return YearBirth.CompareTo(other.YearBirth);
        }
        public override string ToString()
        {
            return Sport;
        }

        public Sportsman(string surname, string date, string sport, string Class)
        {
            Surname = surname;
            try
            {
                YearBirth = Convert.ToDateTime(date);
            }
            catch
            {
                YearBirth = DateTime.Now;
            }
            Sport = sport;
            this.Class = Class;
        }
    }
}
