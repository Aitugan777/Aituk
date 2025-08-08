using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AitukCore.Contracts
{
    public class WorkSheldureContract
    {
        public WorkDayContract? Monday { get; set; }
        public WorkDayContract? Tuesday { get; set; }
        public WorkDayContract? Wednesday { get; set; }
        public WorkDayContract? Thursday { get; set; }
        public WorkDayContract? Friday { get; set; }
        public WorkDayContract? Saturday { get; set; }
        public WorkDayContract? Sunday { get; set; }
    }
}
