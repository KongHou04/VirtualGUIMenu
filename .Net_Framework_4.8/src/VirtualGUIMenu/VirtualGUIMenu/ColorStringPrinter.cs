using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualGUIMenu
{
    public static class ColorStringPrinter
    {
        // Custom your color here

        /// <summary>
        ///     Print a color string with color number you choice
        /// </summary>
        /// <param name="str"></param>
        /// <param name="colorNum"></param>
        public static void PintWithColor(this string str, int colorNum)
        {
            switch (colorNum)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    //Console.ForegroundColor = ConsoleColor.Yellow
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case 9:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case 10:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 11:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case 12:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 13:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 14:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case 15:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.Write(str);
            Console.ResetColor();
        }
    }
}
