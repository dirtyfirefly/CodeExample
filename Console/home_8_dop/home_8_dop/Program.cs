using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//Файл Program.cs содержит корректный C# код. Написать программу, которая удаляет все комментарии из исходного текста и выводит измененный текст на экран. Не забыть, что комментарии бывают двух видов. Если в строковой константе встречаются символы комментариев, строковая константа должна оставаться без изменений. В программе должны обрабатываться все исключения
//...now not all...

namespace home_8_dop
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = @"//переменные
int foo;                // ащщ gjgjgk
double cru = 134;       // скг

/*ghghgj gjgj nfnf 48449 kfk
* kkgkgkg
* gkgkgkg'
* gkgkkg
* jvmvmvm*/
//jgjg kfkfl f
string str = ""dfjvnel  sdlms ld;dsd  sdl m;sdlm a asdl;a"";
string str = ""ghgjg // kfkfkf  fkfkfkf /*fkfkfk */fkfkffmfmfm aasda;s "";        // gjgjgnfjnnd
/*gjgjgj fnfj mdslmdls */";

            Console.WriteLine("-- Данный код ------------------------------------------");
            Console.WriteLine(input);
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine();

            string patternString = "\"(.|[\n\r])*?\"";
            string patternLongComent = "(/\\*)(.|[\n\r])*?(\\*/)";
            string patternShortComent = "//([\\s,.!?]*\\w*)*[\\n\\r]";

            Regex regString = new Regex(patternString);
            Regex regLongC = new Regex(patternLongComent);
            Regex regShortC = new Regex(patternShortComent);
            Regex regMyClone = new Regex(@"(\.\.\.\.\.)");

            MatchCollection mc = regString.Matches(input);
            string[] arrTmp = new string[mc.Count];
            mcInString(mc, arrTmp);
            string tmp = regString.Replace(input, ".....");
            tmp = regShortC.Replace(tmp, "\n\r");
            tmp = regLongC.Replace(tmp, "");
            for(int i = 0; i < arrTmp.Length; i++)
            {
                tmp = regMyClone.Replace(tmp, arrTmp[i], 1);
            }

            Console.WriteLine("-- Получившийся код ------------------------------------------");
            Console.WriteLine(tmp);
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine();

            Console.ReadKey();
        }

        static void mcInString(MatchCollection mc, string[] tmp)
        {
            for (int i = 0; i < mc.Count; i++)
            {
                Match t = mc[i];
                tmp[i] = t.Groups[0].Value;
            }
        }
    }
}
