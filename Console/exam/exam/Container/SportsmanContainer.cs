using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

//Создать класс-коллекцию(generic) с необходимой функциональностью.Создать в этом классе метод для поиска информации по заданному критерию(критерий передавать через параметр-делегат: стандартный или созданный, результат – объект этого класса). Предусмотреть метод для сериализации объекта класса в двоичном формате(параметры – имя файла, форматер). Перегрузить этот метод для сериализации объекта класса в формате XML.

namespace exam.Container
{
    class SportsmanContainer<T>
    {
        List<T> list;       //колекция объектов

        //--свойства--------------------------------------------------------------
        public int isLeght => list.Count;
        public T this[int i] => list[i];

        //--методы----------------------------------------------------------------
        public void Add(T obj) => list.Add(obj);
        public SportsmanContainer<T> SearchOne(Predicate<T> func)
        {
            SportsmanContainer<T> con = new SportsmanContainer<T>();

            for (int i = 0; i < list.Count; i++)
            {
                if (func(list[i]))
                    con.Add(list[i]);
            }
            return con;
        }
        public void SerializeCon(BinaryFormatter bf, string path = "1B.data")
        {
            using (FileStream fs = File.Create(path))
            {
                bf.Serialize(fs, list);
            }
        }
        public void SerializeCon(XmlSerializer xml, string path = "1X.xml")
        {
            using (FileStream fs = File.Create(path))
            {
                xml.Serialize(fs, list);
            }
        }
        public List<T> GetCopy()
        {
            T[] t = new T[list.Count];
            list.CopyTo(t);
            return t.ToList<T>();
        }
        public void SortWithComparare(IComparer<T> c) => list.Sort(c);

        //--конструкторы----------------------------------------------------------
        public SportsmanContainer(List<T> list)
        {
            this.list = list;
        }
        public SportsmanContainer()
        {
            list = new List<T>();
        }
    }
}
