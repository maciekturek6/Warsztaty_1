using System;
using System.Diagnostics;

namespace Exercise_1
{
    class Program
    {
        static void Main(string[] args)
        {
          // var obj = new TaskModel();
           // obj.Setting =  SettingEnum.IsAllDayTask |  SettingEnum.IsImportant;
            //All  import
            // 0    0       0
            // 0    1       1
            // 1    0       2
            // 1    1       3 
            bool IsRunning = true;
            do
            {
                string command;
                ConsoleEx.WriteLine(ConsoleColor.Cyan, "Wpisz komendę: ");
                command = Console.ReadLine();

                switch (command)
                {
                    case "exit":
                        IsRunning = false;
                        break;
                    default:
                        break;
                    
                }

                
            } while (IsRunning);
            


        }
    }
}
