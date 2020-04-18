using System;
using System.Collections.Generic;

namespace touch_core_internal.Models
{
    public class Employee
    {
        public virtual string Designation { get; set; }

        public virtual string Email { get; set; }

        public virtual Guid EmployeeId { get; set; } = Guid.NewGuid();

        public virtual string Identifier { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<Reward> Rewards { get; set; }

        public virtual IList<TimeSheet> TimeSheets { get; set; }
    }
}