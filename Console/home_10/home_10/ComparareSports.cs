using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_10
{
    class ComparareSports : IComparer<Sportsman>
    {
        public int Compare(Sportsman x, Sportsman y)
        {
            return x.Sport.CompareTo(y.Sport);
        }
    }
}
