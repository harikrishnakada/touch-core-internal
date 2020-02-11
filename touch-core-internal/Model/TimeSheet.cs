using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace touch_core_internal.Model
{
    public class TimeSheet
    {
        public virtual Guid Id { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Guid EmployeeId { get; set; }
        public virtual DateTime FromDateTime { get; set; }
        public virtual DateTime ToDateTime { get; set; }
        public virtual string Comments { get; set; }
    }
}
