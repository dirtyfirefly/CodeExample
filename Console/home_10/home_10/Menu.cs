using System;

namespace home_10
{
    class Menu
    {
        //-- световое меню
        public int LigthMenu(string[] arr, int poz)
        {
            int strNum = poz;
            bool end = false;
            int amtOption = arr.Length;
            ConsoleKeyInfo UpDown;

            do
            {
                Console.Clear();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (strNum == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(arr[i]);
                        Console.ResetColor();
                    }
                    else
                        Console.WriteLine(arr[i]);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("*** ^ v - стрелки управления для выбора ****");
                Console.ResetColor();

                UpDown = Console.ReadKey();

                if (UpDown.Key == ConsoleKey.UpArrow)
                    strNum -= 1;
                else if (UpDown.Key == ConsoleKey.DownArrow)
                    strNum += 1;
                else if (UpDown.Key == ConsoleKey.Enter)
                    return strNum;

                if (strNum < 0)
                    strNum = amtOption - 1;
                if (strNum > amtOption - 1)
                    strNum = 0;

            } while (!end);
            return 0;
        }
    }
}
