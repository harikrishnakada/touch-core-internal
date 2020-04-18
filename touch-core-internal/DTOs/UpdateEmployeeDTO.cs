using System;

namespace touch_core_internal.DTOs
{
    public class UpdateEmployeeDTO
    {
        public virtual string Designation { get; set; }

        public virtual string Email { get; set; }

        public Guid EmployeeId { get; set; }

        public virtual string Identifier { get; set; }

        public virtual string Name { get; set; }
    }
}