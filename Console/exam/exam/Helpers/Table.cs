using exam.Class;
using System;

namespace exam.Helpers
{
    class Table
    {
        readonly int amtColHead;               //количество колонок
        readonly int[] lCellHead;              //размер ячеек
        int count = 1;                         //счётчик строк в таблице

        //-- методы -----------------------------------------------------------------------
        public void showHeads(string[] heads)
        {
            if (heads.Length != amtColHead)
                throw new Exception("Количество заголовков не соответствует количеству колонок");
            //верхняя линия заголовков
            for (int j = 0; j < amtColHead; j++)
            {
                if (j == 0)
                    Console.Write("╔");

                for (int i = 0; i < lCellHead[j]; i++)
                {
                    Console.Write("═");
                }

                if (j == amtColHead - 1)
                    Console.WriteLine("╗");
                else
                    Console.Write("╦");
            }
            //текст заголовков
            Console.Write("║");
            for (int j = 0; j < heads.Length; j++)
            {
                string tmp = heads[j];
                for (int i = 0; i < lCellHead[j]; i++)
                {
                    if (i < tmp.Length)
                    {
                        Console.Write(tmp[i]);
                    }
                    else
                        Console.Write(" ");
                }
                Console.Write("║");
            }
            Console.Write("\n");
            //нижняя диния заголовка
            for (int j = 0; j < amtColHead; j++)
            {
                if (j == 0)
                    Console.Write("╠");

                for (int i = 0; i < lCellHead[j]; i++)
                {
                    Console.Write("═");
                }

                if (j == amtColHead - 1)
                    Console.WriteLine("╣");
                else
                    Console.Write("╬");
            }
        }
        public void showTable(Sportsman s)
        {
            //Фамилия	Возраст  Вид спорта	 Лучший результат
            Console.Write("║");
            Console.Write("{0," + (lCellHead[0] - 1) + "} ║", count);
            Console.Write("{0," + (lCellHead[1] - 1) + "} ║", s.Surname);
            Console.Write("{0," + (lCellHead[2] - 1) + "} ║", s.Age);
            Console.Write("{0," + (lCellHead[3] - 1) + "} ║", s.TypeSport);
            if(s is Gemnast)
                Console.Write("{0," + (lCellHead[4] - 1) + "} ║", ((Gemnast)s).TopRes());
            else if (s is Swimer)
                Console.Write("{0," + (lCellHead[4] - 1) + "} ║", ((Swimer)s).TopRes());

            Console.Write("\n");
            count++;
        }
        public void showTable(Swimer s)
        {
            //с указанием среднего результата в 1 - м, 2 - м и 5 - м заплывах;
            Console.Write("║");
            Console.Write("{0," + (lCellHead[0] - 1) + "} ║", count);
            Console.Write("{0," + (lCellHead[1] - 1) + "} ║", s.Surname);
            Console.Write("{0," + (lCellHead[2] - 1) + "} ║", s.Age);
            Console.Write("{0," + (lCellHead[3] - 1) + "} ║", s.Srednee(0,1,4));
            Console.WriteLine();
        }
        public void showEnd()
        {
            for (int j = 0; j < amtColHead; j++)
            {
                if (j == 0)
                    Console.Write("╚");

                for (int i = 0; i < lCellHead[j]; i++)
                {
                    Console.Write("═");
                }

                if (j == amtColHead - 1)
                    Console.WriteLine("╝");
                else
                    Console.Write("╩");
            }
            count = 1;
        }

        //-- конструкторы -----------------------------------------------------------------
        public Table(int amtColHead, int[] lCellHead)
        {
            this.amtColHead = amtColHead;
            this.lCellHead = lCellHead;
        }
    }
}
