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
        public SettingEnum Setting { get; set; }

    }
}
