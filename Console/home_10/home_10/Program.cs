using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Написать приложение, выполняющее следующие функции:
//•	Создание объекта с информацией о спортсменах.
//•	Вывод информации в виде таблицы.
//•	Добавление данных о спортсмене.
//•	Сортировку по возрастанию года рождения или по виду спорта (по выбору пользователя).
//•	Формирование и вывод списка спортсменов моложе 20 лет, имеющих I разряд(использовать стандартную коллекцию List<>).
//•	Формирование и вывод коллекции Dictionary<>, содержащей информацию с количество спортсменов по каждому виду спорта(ключ – вид спорта, значение – количество спортсменов).

namespace home_10
{
    class Program
    {
        static void Main(string[] args)
        {
            Container<Sportsman> con = new Container<Sportsman>(new Sportsman[]
            {
                new Sportsman("Duudor", "12.05.1999", "Golf", "master"),
                new Sportsman("Кувыркин", "05.05.2005", "Борьба", "I"),
                new Sportsman("Truno", "12.05.1977", "Golf", "super"),
                new Sportsman("КувыркинII", "05.05.2005", "Тенис", "I")
            });
            List<Sportsman> listYung;
            Dictionary<string, int> dicSports;
            Table t = new Table(5, new int[] { 5, 20, 13, 15, 13});
            Menu menu = new Menu();
            IComparer<Sportsman> c = new ComparareSports();
            int option = 0;
            bool end = false;

            do
            {
                option = menu.LigthMenu(new string[] { "Вывод таблицы", "Добовление спортсмена", "Сортировка по возрастанию года рождения", "Сотрировака по виду спорта", "Вывод списка спортсменов моложе 20 лет, имеющих I разряд", "Kоличество спортсменов по каждому виду спорта", "Выход" }, option);
                switch (option)
                {
                    case 0:
                        showTable(t, con);
                        break;
                    case 1:
                        addSports(con);
                        break;
                    case 2:
                        con.Sort();
                        showTable(t, con);
                        break;
                    case 3:
                        con.Sort(c);
                        showTable(t, con);
                        break;
                    case 4:
                        listYung = con.GetList(x => x.GetAge() < 20 && x.Class == "I");
                        Console.WriteLine();
                        listYung.ForEach(u => Console.WriteLine(u.Surname));
                        break;
                    case 5:
                        dicSports = con.GetDictionary();
                        foreach (var i in dicSports)
                            Console.WriteLine(i.Key + " " + i.Value);
                        break;
                    case 6:
                        Console.WriteLine("До свидания!");
                        end = true;
                        break;
                }
                Console.ReadKey();
            } while(!end);
            
        }

        static void showTable(Table t, Container<Sportsman> con)
        {
            t.showHeads(new string[] { "№", "Фамилия", "Вид спорта", "Разряд", "Дата рождения" });
            for (int i = 0; i < con.Lenght; i++)
                t.showTable(con[i]);
            t.showEnd();
        }
        static void addSports(Container<Sportsman> c)
        {
            Console.Write("Введите фамилию: ");
            string surname = Console.ReadLine();
            Console.Write("Введите дату (дд.мм.гггг): ");
            string date = Console.ReadLine();
            Console.Write("Введите вид спорта: ");
            string sport = Console.ReadLine();
            Console.WriteLine("Введите разряд: ");
            string Class = Console.ReadLine();
            
            Sportsman tmp = new Sportsman(surname, date, sport, Class);
            c.Add(tmp);
        }
    }
}
