using System;

namespace touch_core_internal.Models
{
    public class Reward
    {
        public Badge Badge { get; set; }

        public Guid BadgeId { get; set; }

        public Employee Employee { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid RewardId { get; set; } = Guid.NewGuid();
    }
}