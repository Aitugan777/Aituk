using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AitukCore.Contracts
{
    public class WorkDayContract
    {
        /// <summary>
        /// День недели
        /// </summary>
        public DayOfWeek Day { get; set; }

        /// <summary>
        /// Время начала работы
        /// </summary>
        public TimeSpan? StartTime { get; set; }

        /// <summary>
        /// Время окончания работы
        /// </summary>
        public TimeSpan? EndTime { get; set; }

        /// <summary>
        /// Является ли день рабочим
        /// </summary>
        public bool IsWorkingDay { get; set; }
    }

}
