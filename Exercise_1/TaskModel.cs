using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise_1
{
    public class TaskModel
    {
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsImportant { get; set; }

        public bool IsAllDayTask { get; set; }

        public TaskModel(string description, DateTime from, DateTime? to, bool isImportant)
        {
            Description = description;
            StartDate = from;
            EndDate = to;
            IsImportant = isImportant;

            if (to.HasValue)
            {
                IsAllDayTask = true;
            }
        }
    }
}
