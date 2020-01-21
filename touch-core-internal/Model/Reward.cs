using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace touch_core_internal.Model
{
    public class Reward
    {
        public virtual Badge Badge { get; set; }

        public virtual Guid BadgeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Guid EmployeeId { get; set; }

        public virtual Guid Id { get; set; }
    }
}