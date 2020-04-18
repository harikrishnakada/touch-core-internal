using System;

namespace touch_core_internal.DTOs
{
    public class AddBadgeDTO
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }
    }
}