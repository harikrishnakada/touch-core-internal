using System;

namespace touch_core_internal.DTOs
{
    public class GetRewardDTO
    {
        public GetBadgeDTO Badge { get; set; }

        public GetEmployeeDTO Employee { get; set; }

        public Guid RewardId { get; set; }
    }
}