using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace touch_core_internal.DTOs
{
    public class GetRewardDTO
    {
        public GetBadgeDTO Badge { get; set; }

        public GetEmployeeDTO Employee { get; set; }

        public Guid RewardId { get; set; }
    }
}