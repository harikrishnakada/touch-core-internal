using System;

namespace touch_core_internal.Models
{
    public class Badge
    {
        public Guid BadgeId { get; set; } = Guid.NewGuid();

        public Category Category { get; set; }

        public Guid CategoryId { get; set; }

        public string Name { get; set; }
    }
}