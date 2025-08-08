using AitukCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Models
{
    public class AWorkDay : ViewModelBase
    {
        /// <summary>
        /// День недели
        /// </summary>
        public DayOfWeek Day { get => GetValue<DayOfWeek>(nameof(Day)); set => SetValue(value, nameof(Day)); }

        /// <summary>
        /// Время начала работы
        /// </summary>
        public TimeSpan? StartTime { get => GetValue<TimeSpan?>(nameof(StartTime)); set => SetValue(value, nameof(StartTime)); }

        /// <summary>
        /// Время окончания работы
        /// </summary>
        public TimeSpan? EndTime { get => GetValue<TimeSpan?>(nameof(EndTime)); set => SetValue(value, nameof(EndTime)); }

        /// <summary>
        /// Является ли день рабочим
        /// </summary>
        public bool IsWorkingDay { get => GetValue<bool>(nameof(IsWorkingDay)); set => SetValue(value, nameof(IsWorkingDay)); }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public AWorkDay() { }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        public AWorkDay(DayOfWeek day, bool isWorkingDay, TimeSpan? startTime = null, TimeSpan? endTime = null)
        {
            Day = day;
            IsWorkingDay = isWorkingDay;
            StartTime = startTime;
            EndTime = endTime;
        }
        
        public AWorkDay(WorkDayContract contract)
        {
            Day = contract.Day;
            IsWorkingDay = contract.IsWorkingDay;
            StartTime = contract.StartTime;
            EndTime = contract.EndTime;
        }

        public WorkDayContract ToContract()
        {
            return new WorkDayContract
            {
                Day = Day,
                StartTime = StartTime,
                EndTime = EndTime,
                IsWorkingDay = IsWorkingDay
            };
        }

        /// <summary>
        /// Удобное текстовое представление
        /// </summary>
        public override string ToString()
        {
            return IsWorkingDay
                ? $"{Day}: с {StartTime?.ToString(@"hh\:mm")} до {EndTime?.ToString(@"hh\:mm")}"
                : $"{Day}: выходной";
        }
    }
}
