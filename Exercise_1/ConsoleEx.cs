using System;

namespace Exercise_1
{
    public static class ConsoleEx
    {
        public static void Write(ConsoleColor color, string line, params object[] args)
        {
            var currentColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.Write(line, args);

            Console.ForegroundColor = currentColor;
        }

        public static void WriteLine(ConsoleColor color, string line, params object[] args)
        {
            var currentColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(line, args);

            Console.ForegroundColor = currentColor;
        }
    }
}
