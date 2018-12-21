using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Написать программу, выполняющую следующие функции:
//- Считывание из текстового файла информации о сотрудниках фирмы, записанных в формате csv(Фамилия, должность, отдел, возраст)
//- Формирование коллекции объектов класса Сотрудник

//- Считывание из текстового файла информации о зарплатах, записанных в формате csv(должность, размер зарплаты)
//- Формирование коллекции объектов класса Зарплата

namespace lab_LINQ
{
    class Program
    {

        static void Main(string[] args)
        {
            List<ZP> listZp = new List<ZP>();
            List<Worker> listW = new List<Worker>();
            string[] tmp;

            using (StreamReader read = new StreamReader("Salaries.dat", Encoding.Default))
            {
                while (!read.EndOfStream)
                {
                    tmp = read.ReadLine().Split(';');
                    listZp.Add(new ZP() { Work = tmp[0], Zp = Convert.ToDecimal(tmp[1]) });
                }
            }

            using (StreamReader read = new StreamReader("employees.dat", Encoding.Default))
            {
                while (!read.EndOfStream)
                {
                    tmp = read.ReadLine().Split(';');
                    listW.Add(new Worker() { Surname = tmp[0], Work = tmp[1], Department = tmp[2], Age = Convert.ToInt32(tmp[3])});
                }
            }

            Console.WriteLine("Zp");
            listZp.ForEach(f => Console.WriteLine(f.Work + " " + f.Zp));
            Console.WriteLine();
            Console.WriteLine("Worker");
            listW.ForEach(f => Console.WriteLine(f.Surname + " " + f.Work + " " + f.Department + " " + f.Age));
            Console.WriteLine();

            //- Вывод списка сотрудников моложе 30 лет в алфавитном порядке с указанием возраста
            Console.WriteLine("*** Вывод списка сотрудников моложе 30 лет в алфавитном порядке с указанием возраста ***");
            var yungQuery = listW.Where(f => f.Age < 30).OrderBy(f => f.Surname).ToList();
            yungQuery.ForEach(f => Console.WriteLine(f.Surname + " " + f.Work + " " + f.Department + " " + f.Age));
            Console.WriteLine();
            //- Вывод списка отделов(без повторений)
            Console.WriteLine("*** Вывод списка отделов(без повторений) ***");
            var dipQuery = listW.GroupBy(f => f.Department).ToList();
            dipQuery.ForEach(f => Console.WriteLine(f.Key));
            Console.WriteLine();
            //- Определение среднего возраста сотрудников для каждого отдела.Выводить название отдела и средний возраст в порядке убывания возраста.
            Console.WriteLine("*** Определение среднего возраста сотрудников для каждого отдела в порядке убывания возраста ***");
            var midlAgeQuery = listW.GroupBy(f => f.Department).Select(f => new { f.Key, MidleAge = f.Average(a => a.Age) }).OrderByDescending(f => f.MidleAge).ToList();
            midlAgeQuery.ForEach(f => Console.WriteLine(f.Key + " " + f.MidleAge));
            Console.WriteLine();
            //- Вывод списка сотрудников заданного отдела с указанием зарплаты и должности
            Console.WriteLine("*** Вывод списка сотрудников заданного отдела с указанием зарплаты и должности ***");
            var worQuery = listW.Where(w => w.Department == "АСУ").Join(listZp, w => w.Work, z => z.Work, (w, z) => new { w.Surname, z.Work, z.Zp}).ToList();
            worQuery.ForEach(w => Console.WriteLine(w.Surname + " " + w.Zp + " " + w.Work));
            Console.WriteLine();
            //- Определение отдела с максимальной средней зарплатой
            Console.WriteLine("*** Определение отдела с максимальной средней зарплатой ***");
            var maxZpQuery = listW.Join(listZp, w => w.Work, z => z.Work, (w, z) => new { w.Department, z.Zp }).GroupBy(g => g.Department).Select(s => new { s.Key, midlZp = s.Average(d => d.Zp) }).ToList();
            decimal max = maxZpQuery.Max(c => c.midlZp);
            var res = maxZpQuery.Where(v => v.midlZp == max).ToList();
            res.ForEach(c => Console.WriteLine(c.Key));
            Console.WriteLine();
            //- Определение количества сотрудников в каждом отделе
            Console.WriteLine("*** Определение количества сотрудников в каждом отделе ***");
            var countWorkerQuery = listW.GroupBy(c => c.Department).Select(c => new { c.Key, Count = c.Count() }).ToList();
            countWorkerQuery.ForEach(c => Console.WriteLine(c.Key + " " + c.Count));
            Console.WriteLine();
            //- Определение минимального возраста для каждой должности
            Console.WriteLine("*** Определение минимального возраста для каждой должности ***");
            var minAgeQuery = listW.GroupBy(c => c.Work).Select(c => new { c.Key, minAge = c.Min(g => g.Age) }).ToList();
            minAgeQuery.ForEach(f => Console.WriteLine(f.Key + " " + f.minAge));

            Console.ReadKey();
        }
    }
}
