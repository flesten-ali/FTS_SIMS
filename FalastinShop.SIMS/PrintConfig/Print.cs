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
        public static void ConfigSuccessConsole(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Log(msg);
            Console.ResetColor();
        }
    }
}
