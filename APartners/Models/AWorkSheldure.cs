using AitukCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Models
{
    public class AWorkSheldure : ViewModelBase
    {
        public AWorkDay? Monday { get => GetValue<AWorkDay>(nameof(Monday)); set => SetValue(value, nameof(Monday)); }
        public AWorkDay? Tuesday { get => GetValue<AWorkDay>(nameof(Tuesday)); set => SetValue(value, nameof(Tuesday)); }
        public AWorkDay? Wednesday { get => GetValue<AWorkDay>(nameof(Wednesday)); set => SetValue(value, nameof(Wednesday)); }
        public AWorkDay? Thursday { get => GetValue<AWorkDay>(nameof(Thursday)); set => SetValue(value, nameof(Thursday)); }
        public AWorkDay? Friday { get => GetValue<AWorkDay>(nameof(Friday)); set => SetValue(value, nameof(Friday)); }
        public AWorkDay? Saturday { get => GetValue<AWorkDay>(nameof(Saturday)); set => SetValue(value, nameof(Saturday)); }
        public AWorkDay? Sunday { get => GetValue<AWorkDay>(nameof(Sunday)); set => SetValue(value, nameof(Sunday)); }

        public AWorkSheldure()
        {
            InitalizeWorkSheldure();
        }
        private void InitalizeWorkSheldure()
        {
            Monday = new AWorkDay(DayOfWeek.Monday, false);
            Tuesday = new AWorkDay(DayOfWeek.Tuesday, false);
            Wednesday = new AWorkDay(DayOfWeek.Wednesday, false);
            Thursday = new AWorkDay(DayOfWeek.Thursday, false);
            Friday = new AWorkDay(DayOfWeek.Friday, false);
            Saturday = new AWorkDay(DayOfWeek.Saturday, false);
            Sunday = new AWorkDay(DayOfWeek.Sunday, false);
        }

        public AWorkSheldure(WorkSheldureContract contract)
        {
            if (contract == null)
            {
                InitalizeWorkSheldure();
                return;
            }
            Monday = contract.Monday != null ? new AWorkDay(contract.Monday) : null;
            Tuesday = contract.Tuesday != null ? new AWorkDay(contract.Tuesday) : null;
            Wednesday = contract.Wednesday != null ? new AWorkDay(contract.Wednesday) : null;
            Thursday = contract.Thursday != null ? new AWorkDay(contract.Thursday) : null;
            Friday = contract.Friday != null ? new AWorkDay(contract.Friday) : null;
            Saturday = contract.Saturday != null ? new AWorkDay(contract.Saturday) : null;
            Sunday = contract.Sunday != null ? new AWorkDay(contract.Sunday) : null;
        }

        public WorkSheldureContract ToContract()
        {
            return new WorkSheldureContract
            {
                Monday = Monday != null ? Monday.ToContract() : null,
                Tuesday = Tuesday != null ? Tuesday.ToContract() : null,
                Wednesday = Wednesday != null ? Wednesday.ToContract() : null,
                Thursday = Thursday != null ? Thursday.ToContract() : null,
                Friday = Friday != null ? Friday.ToContract() : null,
                Saturday = Saturday != null ? Saturday.ToContract() : null,
                Sunday = Sunday != null ? Sunday.ToContract() : null
            };
        }
    }
}
