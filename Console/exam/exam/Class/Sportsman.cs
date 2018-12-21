using System;
using System.Xml.Serialization;

//Класс «Спортсмен» должен быть абстрактным и содержать следующие элементы: поле-фамилия, поле - возраст, поле вид спорта(гимнастика, бокс, плавание и т.д.), конструктор с параметрами, свойства для чтения полей класса, абстрактный метод для определения лучшего результата соревнований.

namespace exam.Class
{
    [Serializable]
    [XmlInclude(typeof(Gemnast))]
    [XmlInclude(typeof(Swimer))]
    public abstract class Sportsman
    {
        public string surname;     //фамилия
        public int age;            //возраст
        public string typeSport;   //вид спорта

        //--свойства---------------------------------------------------
        public string Surname => surname;
        public int Age => age;
        public string TypeSport => typeSport;

        //--методы-----------------------------------------------------
        abstract public double TopRes();

        //--конструкторы-----------------------------------------------
        public Sportsman(string surname, int age, string typeSport)
        {
            this.surname = surname;
            this.age = age;
            this.typeSport = typeSport;
        }
        public Sportsman()
        {

        }
    }
}
