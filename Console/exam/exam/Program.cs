using exam.Class;
using exam.Container;
using exam.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

//–	сортирует информацию по убыванию возраста с использованием класса, реализующего интерфейс IСomparer; +
//–	сортирует информацию по убыванию возраста с использованием класса, реализующего интерфейс IСomparer; +
//–	Сериализует коллекцию в двоичном формате; +
//–	выводит на экран информацию о пловцах моложе 20 лет с указанием среднего результата в 1-м, 2-м и 5-м заплывах; +
//–	сравнивает двух указанных гимнастов по результатам; +
//–	сериализует информацию  о гимнастах в формате  XML; +
//–	выводит на экран фамилию слушателя, текущую дату и время. +

namespace exam
{
    class Program
    {
        static void Main(string[] args)
        {
            Table table = new Table(5, new int[] { 3, 15, 8, 20, 20});
            //1
            Console.WriteLine("*** Чтаем из файла \"input.txt\", создаём контейнер дженерик и выводим в виде таблицы ***");
            SportsmanContainer<Sportsman> container = new SportsmanContainer<Sportsman>(ReadSportsmanFile("input.txt"));
            ShowTable(table, container);
            //2
            Console.WriteLine("*** Сортируем по убыванию возраста ***");
            container.SortWithComparare(new CompararSportsnamAge());
            ShowTable(table, container);
            //3
            Console.WriteLine("*** Бинарно сериализуем в файл \"1В.data\" ***");
            container.SerializeCon(new BinaryFormatter());
            Console.WriteLine("Готово");
            //4
            Console.WriteLine("*** Ищем молодых плавцов и выводим их средний результат по заплывам № 1, 2, 5 ***");
            SportsmanContainer<Sportsman> yungSwim = container.SearchOne(x => x.Age < 20 && x is Swimer);
            ShowYungSwim(yungSwim);
            //5
            Console.WriteLine("*** Сравниваем двух гимнастов ***");
            try { Console.WriteLine(CompareGemnast(container)); }
            catch (Exception e) { Console.WriteLine(e.Message); }
            //6
            Console.WriteLine("*** xml сериализация в файл \"1Х.xml\" ***");
            container.SerializeCon(new XmlSerializer(typeof(List<Sportsman>)));
            Console.WriteLine("Готово");
            //7
            Console.WriteLine("*** Самый сложный пункт ***");
            Console.WriteLine("Гринь " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

            Console.ReadKey();
        }

        //------------------------------
        static List<Sportsman> ReadSportsmanFile(string path)
        {
            string[] tmp = null;
            List<Sportsman> fil = new List<Sportsman>();
            Sportsman f = null;
            int tmpAmt;
            double tmpOne;
            double tmpCirc;
            double tmpBrus;
            double tmpJamp;

            using (FileStream fs = File.OpenRead(path))
            {
                StreamReader sr = new StreamReader(fs);

                while (!sr.EndOfStream)
                {
                    tmp = sr.ReadLine().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine(tmp[0] + " " + tmp[1] + " " + tmp[2] + " " + tmp[3]);

                    if (tmp[2].Equals("Плавание"))
                    {
                        try
                        {
                            tmpAmt = Convert.ToInt32(tmp[1]);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            tmpAmt = 0;
                        }

                        double[] arrRat = new double[tmp.Length - 3];
                        for (int i = 3, j = 0; i < tmp.Length; i++, j++)
                        {
                            try
                            {
                                tmpOne = Convert.ToDouble(tmp[i]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                tmpOne = 0;
                            }
                            arrRat[j] = tmpOne;
                        }

                        f = new Swimer(tmp[0], tmpAmt, tmp[2], arrRat);
                    }

                    else if (tmp[2].Equals("Гимнастика"))
                    {
                        try
                        {
                            tmpAmt = Convert.ToInt32(tmp[1]);
                            tmpCirc = Convert.ToDouble(tmp[3]);
                            tmpBrus = Convert.ToDouble(tmp[4]);
                            tmpJamp = Convert.ToDouble(tmp[5]);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            tmpAmt = 0;
                            tmpCirc = 0;
                            tmpBrus = 0;
                            tmpJamp = 0;
                        }
                        f = new Gemnast(tmp[0], tmpAmt, tmp[2], tmpCirc, tmpBrus, tmpJamp);
                    }

                    fil.Add(f);
                }
            }
            return fil;
        }
        //--
        static void ShowTable(Table t, SportsmanContainer<Sportsman> container)
        {
            t.showHeads(new string[] { "№", "Фамилия", "Возраст", "Вид спорта", "Лучший результат" });
            for (int i = 0; i < container.isLeght; i++)
            {
                t.showTable(container[i]);
            }
            t.showEnd();
        }
        //--
        static void ShowYungSwim(SportsmanContainer<Sportsman> con)
        {
            Table t = new Table(4, new int[] { 3, 15, 8, 20});

            t.showHeads(new string[] { "№", "Фамилия", "Возраст", "Средний результат" });

            for (int i = 0; i < con.isLeght; i++)
                t.showTable(con[i] as Swimer);

            t.showEnd();
        }
        //--
        static string CompareGemnast(SportsmanContainer<Sportsman> con)
        {
            string str = "Лучший результат у ";

            Console.Write("Введите фамилию гимнаста 1:");
            string spm1 = Console.ReadLine();
            Console.Write("Введите фамилию гимнаста 2:");
            string spm2 = Console.ReadLine();
            List<Sportsman> tmp = con.GetCopy();

            Gemnast g1 = tmp.FirstOrDefault(x => x.Surname.Equals(spm1) && x is Gemnast) as Gemnast;
            Gemnast g2 = tmp.FirstOrDefault(x => x.Surname.Equals(spm2) && x is Gemnast) as Gemnast;

            if (g1 == null || g2 == null)
                throw new Exception("Проверьте правильность ввод гимнастов.");

            if (g1 > g2)
                return (str + g1.Surname);
            else if (g1 < g2)
                return (str + g2.Surname);
            else
                return "Гимнасты равны";
        }
    }
}
