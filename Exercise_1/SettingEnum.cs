using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise_1
{
    [Flags]
    public enum SettingEnum : int
    {
        IsAllDayTask =1,
        IsImportant = 2
    }
}
