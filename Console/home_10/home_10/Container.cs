using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//      Создать класс-прототип для хранения и обработки экземпляров структур, наложить ограничение на параметр типа данных: элементы коллекции должны быть значимого типа, тип-аргумент должен реализовывать интерфейс IComparable.В классе предусмотреть метод для сортировки коллекции, метод для формирования списка объектов, удовлетворяющих заданному условию (условие поиска передавать в метод через делегат), остальные элементы на свое усмотрение.

namespace home_10
{
    class Container<T> where T : struct, IComparable<T>
    {
        List<T> arr = new List<T>();

        public T this[int i]
        {
            get => arr[i];
            set { arr[i] = value; }
        }
        public int Lenght => arr.Count;

        public void Sort()
        {
            arr.Sort();
        }
        public void Sort(IComparer<T> c)
        {
            arr.Sort(c);
        }
        public void Add(T obj) => arr.Add(obj);

        public List<T> GetList( Predicate<T> func)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < arr.Count; i++)
            {
                if (func(arr[i]))
                    list.Add(arr[i]);
            }
            return list;
        }
        public Dictionary<string, int> GetDictionary()
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            for (int i = 0; i < arr.Count; i++)
            {
                if (map.ContainsKey(arr[i].ToString()))
                    map[arr[i].ToString()] += 1;
                else
                    map.Add(arr[i].ToString(), 1);
            }
            return map;
        }

        public Container(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                arr.Add(array[i]);
            }
        }
    }
}
