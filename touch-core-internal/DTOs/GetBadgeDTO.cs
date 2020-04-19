using System;

namespace touch_core_internal.DTOs
{
    public class GetBadgeDTO
    {
        public Guid BadgeId { get; set; }

        public GetCategoryDTO Category { get; set; }

        public string Name { get; set; }
    }
}