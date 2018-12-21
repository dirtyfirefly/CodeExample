using exam.Class;
using System.Collections.Generic;

//–	сортирует информацию по убыванию возраста с использованием класса, реализующего интерфейс IСomparer; 

namespace exam.Helpers
{
    class CompararSportsnamAge : IComparer<Sportsman>
    {
        public int Compare(Sportsman x, Sportsman y)
        {
            if (x == null)
                return 1;
            else if (y == null)
                return -1;
            else if(x == null && y == null)
                return 0;

            return -x.Age.CompareTo(y.Age);
        }
    }
}
