using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_1_2.Classes
{
    class Matrix
    {
        int[,] arr;
        int width = 0;
        int height = 0;

        public int isWidth => width;
        public int isHeight => height;
        public int this[int i, int j]
        {
            get => arr[i, j];
            set => arr[i, j] = value;
        }

        public void CaseMinValue(ref int x, ref int y)
        {
            x = 0;
            y = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if(arr[x, y] > arr[i, j])
                    {
                        x = i;
                        y = j;
                    }
                }
            }
        }
        public void CaseMaxValue(ref int x, ref int y)
        {
            x = 0;
            y = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (arr[x, y] < arr[i, j])
                    {
                        x = i;
                        y = j;
                    }
                }
            }
        }

        public Matrix(int width, int height)
        {
            this.width = width;
            this.height = height;

            arr = new int[width, height];
        }
    }
}
