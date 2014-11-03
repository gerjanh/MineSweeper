using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    class StaticCodeConverter
    {
        public static void decode(int combination, out int x, out int y,out int number ){
            x = Math.Abs(combination % 100);
            y = Math.Abs(combination / 100 % 100);
            number = combination / 10000;
        }

        public static void decode(int combination, out int x, out int y)
        {
            int number;
            decode( combination, out  x, out  y,out  number );
        }

        public static void encode(int x, int y, int number, out int combination)
        {
            combination = (((number * 100) + y) * 100) + x;
        }

        public static void encode(int x, int y, out int combination)
        {
            int number = 0;
            encode(x, y, number, out  combination);
        }

        public static int ConvertStringToInt(string text)
        {
            int number = 0;

            number = int.Parse(text);
           
            return number;
        }

    }
}
