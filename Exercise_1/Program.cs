using System;
using System.Collections.Generic;
using System.IO;

namespace Exercise_1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;

            var listManager = new ProgramLogic();

            do
            {
                string command;

                ConsoleEx.Write(ConsoleColor.Green, "Wpisz komendę (add, remove, show, save, load, exit): ");
                command = Console.ReadLine();

                switch (command)
                {
                    case "add":
                        ConsoleEx.WriteLine(ConsoleColor.Yellow, "--- DODAWANIE ZADANIA ---");
                        ConsoleEx.WriteLine(ConsoleColor.Yellow, "--- ----------------- ---");

                        var desc = AskForString("Podaj nazwe zadania");

                        var isImportant = AskForBool("Czy zadanie ma byc oznaczone jako wazne? (T/N): ");
                        var isAllDayTask = AskForBool("Czy zadanie ma byc calodniowe? (T/N): ");

                        DateTime from;
                        DateTime? to = null;

                        if (isAllDayTask)
                        {
                            from = AskForDate("Data zadania");
                        }
                        else
                        {
                            from = AskForDate("Data rozpoczecia zadania");
                            to = AskForDate("Data zakonczenia zadania");
                        }

                        listManager.AddTask(desc, from, to, isImportant);
                        break;
                    case "remove":
                        ConsoleEx.WriteLine(ConsoleColor.Yellow, "--- USUWANIE ZADANIA ---");
                        ConsoleEx.WriteLine(ConsoleColor.Yellow, "--- ---------------- ---");

                        if (listManager.TaskCount == 0)
                        {
                            ConsoleEx.WriteLine(ConsoleColor.Red, "Brak zadan!");
                            break;
                        }

                        int numberOfTask = AskForNumberOfTask("Podaj nr zadania", listManager.TaskCount);

                        listManager.RemoveTask(numberOfTask);
                        break;
                    case "show":
                        listManager.ShowTasks();
                        break;
                    case "save":
                        listManager.SaveTasks();
                        break;
                    case "load":
                        listManager.LoadTasks();
                        break;
                    case "exit":
                        isRunning = false;
                        break;

                    default:
                        break;
                }

            } while (isRunning);

        }

        private static DateTime AskForDate(string dateName)
        {
            ConsoleEx.Write(ConsoleColor.Green, "{0} (rrrr-mm-dd): ", dateName);
            var dateString = Console.ReadLine();
            var dateStringParseOk = DateTime.TryParse(dateString, out var date);
            if (!dateStringParseOk)
            {
                ConsoleEx.WriteLine(ConsoleColor.Red, "Podana data {0} jest nieprawidlowa. Sprobuj jeszcze raz stosujac format rrrr-mm-dd!", dateString);
                AskForDate(dateName);
            }

            return date;
        }

        private static bool AskForBool(string question)
        {
            ConsoleEx.Write(ConsoleColor.Green, "{0} (T/N): ", question);
            var answer = Console.ReadLine().ToLower();

            if (answer != "t" && answer != "n")
            {
                ConsoleEx.WriteLine(ConsoleColor.Red, "Odpowiadaj tylko 'T' lub 'N'!");
                AskForBool(question);
            }

            return answer == "t";
        }

        private static string AskForString(string question)
        {
            ConsoleEx.Write(ConsoleColor.Green, "{0}: ", question);
            var result = Console.ReadLine();

            return result;
        }

        private static int AskForNumberOfTask(string question, int count)
        {
            var input = AskForString(question);
            var parsedSuccesfully = int.TryParse(input, out int i);
            if (!parsedSuccesfully)
            {
                ConsoleEx.WriteLine(ConsoleColor.Red, "Podana wartosc nie jest liczba calkowita!");
                i = AskForNumberOfTask(question, count);
            }

            if (i < 0 || i > count)
            {
                ConsoleEx.WriteLine(ConsoleColor.Red, "Podales {0}, a lista zawiera elementy 1-{1}", count);
                i = AskForNumberOfTask(question, count);
            }

            return i;
        }
    }
}