using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace touch_core_internal.DTOs
{
    public class AddRewardDTO
    {
        public Guid BadgeId { get; set; }

        public Guid EmployeeId { get; set; }
    }
}