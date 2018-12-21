using System;

namespace home_10
{
    class Table
    {
        readonly int amtColHead;               //количество колонок
        readonly int[] lCellHead;              //размер ячеек
        int count = 1;

        //-- методы -----------------------------------------------------------------------
        //печатает заголовок
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
        //печатает строку таблицы
        public void showTable(Sportsman s)
        {   
            Console.Write("║");
            Console.Write("{0," + (lCellHead[0] - 1) + "} ║", count);
            Console.Write("{0," + (lCellHead[1] - 1) + "} ║", s.Surname);
            Console.Write("{0," + (lCellHead[2] - 1) + "} ║", s.Sport);
            Console.Write("{0," + (lCellHead[3] - 1) + "} ║", s.Class);
            Console.Write("{0," + (lCellHead[4] - 1) + "} ║", s.YearBirth.ToShortDateString());

            Console.Write("\n");
            count++;
        }
        //закрывает таблицу
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
