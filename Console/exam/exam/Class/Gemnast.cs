using System;

//Класс гимнастов должен содержать дополнительные поля оценки за упражнения на кольцах, на брусьях, за опорный прыжок, конструктор с параметрами, реализацию метода для определения лучшего результата соревнований, операции < и  > для сравнения гимнастов по результатам.

namespace exam.Class
{
    [Serializable]
    public class Gemnast : Sportsman
    {
        public double ratCircl;        //кольца
        public double ratBrus;         //брусья
        public double ratJamp;         //опорный прыжок

        //--методы-----------------------------------------------------------------------------------------------------
        public override double TopRes()
        {
            double tmp = ratCircl > ratBrus ? ratCircl : ratBrus;
            return tmp > ratJamp ? tmp : ratJamp;
        }
        
        //--переобпределения операторов
        public static bool operator <(Gemnast obj1, Gemnast obj2)
        {
            return obj1.TopRes() < obj2.TopRes();
        }
        public static bool operator >(Gemnast obj1, Gemnast obj2)
        {
            return !(obj1 < obj2);
        }

        //--конструкторы-----------------------------------------------------------------------------------------------
        public Gemnast(string surname, int age, string typeSport, double ratCircl, double ratBrus, double ratJamp) : base(surname, age, typeSport)
        {
            this.ratCircl = ratCircl;
            this.ratBrus = ratBrus;
            this.ratJamp = ratJamp;
        }
        public Gemnast()
        {

        }
    }
}
