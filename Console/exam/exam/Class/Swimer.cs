using System;

//Класс пловцов должен содержать дополнительное поле-массив с результатами заплывов, реализацию метода для определения лучшего результата соревнований, конструктор с параметрами, метод с переменным числом параметров, возвращающий средний результат за указанные заплывы(например, srednee(1,3) – средний результат за 1-й и 3-й заплывы, srednee(1) – время в 1-м заплыве и т.д.).

namespace exam.Class
{
    [Serializable]
    public class Swimer : Sportsman
    {
        public double[] rat;       //массив отценок 

        //--свойства----------------------------------------------------------------------
        public double this[int i] => rat[i];

        //--методы------------------------------------------------------------------------
        public override double TopRes()
        {
            double tmp = rat[0];
            for (int i = 1; i < rat.Length; i++)
               tmp = tmp > rat[i] ? rat[i] : tmp;
            return tmp;
        }
        public double Srednee(params int[] arr)
        {
            double res = 0;
            int amt = arr.Length;

            for (int i = 0; i < amt; i++)
                res += rat[arr[i]];

            return res / amt;
        }

        //--конструкторы------------------------------------------------------------------
        public Swimer(string surname, int age, string typeSport, params double[] arr) : base(surname, age, typeSport)
        {
            rat = arr;
        }
        public Swimer()
        {
            rat = new double[0];
        }
    }
}
