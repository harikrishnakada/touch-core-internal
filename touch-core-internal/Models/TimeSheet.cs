using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace touch_core_internal.Models
{
    public class TimeSheet
    {
        public Employee Employee { get; set; }

        public Guid EmployeeId { get; set; }

        public DateTime FromDateTime { get; set; }

        public Guid TimeSheetId { get; set; } = Guid.NewGuid();

        public DateTime ToDateTime { get; set; }
    }
}