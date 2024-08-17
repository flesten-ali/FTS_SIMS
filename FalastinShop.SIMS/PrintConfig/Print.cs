using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalastinShop.Simple_Inventory_Management_System.PrintConfig
{
    public class Print
    {
        public static void Log(string msg)
        {
            Console.WriteLine(msg);
        }
        public static void ConfigConsoleColor(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Log(msg);
            Console.ResetColor();
        }
        public static void ConfigSuccessConsole(string msg)
        {
            ConfigConsoleColor(msg , ConsoleColor.Green);


        }


        public static void ConfigErrorConsole(string msg)
        {
           ConfigConsoleColor(msg, ConsoleColor.Red);

        }






    }
}
