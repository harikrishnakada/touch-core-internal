using System;

namespace touch_core_internal.DTOs
{
    public class GetCategoryDTO
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public int Points { get; set; }
    }
}