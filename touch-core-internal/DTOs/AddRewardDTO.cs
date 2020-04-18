using System;

namespace touch_core_internal.DTOs
{
    public class AddRewardDTO
    {
        public Guid BadgeId { get; set; }

        public Guid EmployeeId { get; set; }
    }
}