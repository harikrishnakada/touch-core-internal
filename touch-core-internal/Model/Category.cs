using System;

namespace touch_core_internal.Model
{
    public class Category
    {
        public Guid CategoryId { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public int Points { get; set; }
    }
}