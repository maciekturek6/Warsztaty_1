using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise_1
{
    class ConsoleEx
    {
        public static void Write(ConsoleColor color, string line, string [] args = null)
        {
            //var temp = Console.ForegroundColor;
            Console.ForegroundColor = color;
            if(args !=null) Console.Write(line,args);
            Console.WriteLine(line);
        }

        public static void WriteLine(ConsoleColor color, string line, string[] args = null)
        {
            Console.ForegroundColor = color;
            if (args != null) Console.Write(line, args);
            Console.WriteLine(line);
        }
    }
}
