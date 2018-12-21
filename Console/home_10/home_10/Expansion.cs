using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//метод расширение для определения возраста

namespace home_10
{
    static class Expansion
    {
        public static int GetAge(this Sportsman sm)
        {
            return DateTime.Now.Year - sm.YearBirth.Year;
        }
    }
}
