using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Создать класс «Одномерный массив», в котором описать следующие элементы: закрытое поле – массив целых чисел, свойство для определения длины массива, индексатор для доступа к элементам поля-массива,  конструктор с параметрами, метод для вычисления суммы элементов массива, метод для вычисления произведения элементов массива.

namespace home_1.Classes
{
    public class MyArray
    {
        int[] arr;

        public int isLength => arr.Length;
        public int this[int i]
        {
            get => arr[i];
            set => arr[i] = value;
        }

        public int ArraySum() => arr.Sum();
        public int ArrayMulty()
        {
            int res = 1;
            foreach (int i in arr)
                res *= i;

            return res;
        }

        public MyArray(int amt)
        {
            arr = new int[amt];
        }
    }
}
