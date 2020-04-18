using System;

namespace touch_core_internal.DTOs
{
    public class AddTimeSheetDTO
    {
        public Guid EmployeeId { get; set; }

        public DateTime FromDateTime { get; set; }

        public DateTime ToDateTime { get; set; }
    }
}