using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Exercise_1
{
    public class ProgramLogic
    {
        private List<TaskModel> TaskModelList = new List<TaskModel>();

        private const string _path = @"C:\tmp\data.csv";

        public int TaskCount
        {
            get
            {
                return TaskModelList.Count;
            }
        }

        public void AddTask(string description, DateTime from, DateTime? to, bool isImportant)
        {
            var task = new TaskModel(description, from, to, isImportant);

            TaskModelList.Add(task);
        }

        public void RemoveTask(int taskNumber)
        {
            int index = taskNumber - 1;

            TaskModelList.Sort((x, y) => { return x.StartDate.CompareTo(y.StartDate); });

            TaskModelList.RemoveAt(index);
        }

        public void ShowTasks()
        {
            ConsoleEx.WriteLine(ConsoleColor.Yellow, "------------------------------ LISTA ZADAN -------------------------------");

            TaskModelList.Sort((x, y) => { return x.StartDate.CompareTo(y.StartDate); });

            if (TaskModelList.Count == 0)
            {
                ConsoleEx.WriteLine(ConsoleColor.Green, "Brak zadan do wykonania!");
                return;
            }

            ConsoleEx.WriteLine(ConsoleColor.Green, "| {0} | {1} | {2} | {3} | {4} |",
                    "#".PadLeft(4),
                    "Nazwa".PadRight(30),
                    "Data od".PadRight(10),
                    "Data do".PadRight(10),
                    "Priorytet"
                    );

            for (var i = 0; i < TaskModelList.Count; i++)
            {
                var tm = TaskModelList[i];

                var desc = tm.Description;
                if (desc.Length > 28) desc = $"{desc.Substring(0, 25)}...";

                ConsoleEx.WriteLine(ConsoleColor.Green, "| {0} | {1} | {2} | {3} | {4} |",
                    (i + 1).ToString().PadLeft(4),
                    desc.PadRight(30),
                    tm.StartDate.ToString("yyyy-MM-dd"),
                    tm.EndDate.HasValue ? tm.EndDate.Value.ToString("yyyy-MM-dd") : "c/d".PadRight(10),
                    tm.IsImportant ? "(!)".PadRight(9) : "".PadRight(9)
                    );
            }

        }

        public void SaveTasks()
        {
            TaskModelList.Sort((x, y) => { return x.StartDate.CompareTo(y.StartDate); });

            if (TaskModelList.Count == 0)
            {
                ConsoleEx.WriteLine(ConsoleColor.Green, "Brak zadan do zapisania!");
                return;
            }

            using (var writer = new StreamWriter(Path.GetFullPath(_path), false))
            {
                foreach (var task in TaskModelList)
                {
                    writer.WriteLine("{0},{1},{2},{3},{4}",
                        task.Description,
                        task.StartDate.ToString("yyyy-MM-dd"),
                        task.EndDate.HasValue ? task.EndDate.Value.ToString("yyyy-MM-dd") : "",
                        task.IsImportant ? "T" : "N",
                        task.IsAllDayTask ? "T" : "N"
                    );
                }
            }

            ConsoleEx.WriteLine(ConsoleColor.DarkGreen, "Zapisano do pliku!");
        }

        public void LoadTasks()
        {
            if (File.Exists(_path))
            {
                ConsoleEx.WriteLine(ConsoleColor.Yellow, "(!) Obecna baza zostanie nadpisana!");
            }

            int recordLoaded = 0;

            using (var reader = new StreamReader(_path))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var record = line.Split(',');

                    if (record.Length != 5)
                    {
                        ConsoleEx.WriteLine(ConsoleColor.Red, "(!) Blad struktury pliku!");
                        TaskModelList.Clear();
                        break;
                    }

                    bool fromParsed = DateTime.TryParse(record[1], out var from);
                    if (!fromParsed)
                    {
                        ConsoleEx.WriteLine(ConsoleColor.Yellow, "(!) Blad struktury rekordu (Data od) !");
                        ConsoleEx.WriteLine(ConsoleColor.Gray, "POMIJAM: {0}", record);
                        continue;
                    }

                    DateTime? to = null;
                    if (record[2] != string.Empty)
                    {
                        bool toParsed = DateTime.TryParse(record[2], out var toNullable);
                        if (!fromParsed)
                        {
                            ConsoleEx.WriteLine(ConsoleColor.Yellow, "(!) Blad struktury rekordu (Data od) !");
                            ConsoleEx.WriteLine(ConsoleColor.Gray, "POMIJAM: {0}", record);
                            continue;
                        }

                        to = toNullable;
                    }

                    bool isImportant = record[3] == "T" ? true : false;
                    bool isAllDay = record[4] == "T" ? true : false;

                    var task = new TaskModel(record[0], from, to, isImportant);

                    TaskModelList.Add(task);
                    recordLoaded++;
                }
            }

            ConsoleEx.WriteLine(ConsoleColor.Green, "Wczytano {0} rekordow do pamieci operacyjnej", recordLoaded);
            ConsoleEx.WriteLine(ConsoleColor.Green, "W pamieci jest {0} rekordow", TaskCount);
        }
    }
}
