using System;

namespace touch_core_internal.DTOs
{
    public class GetEmployeeToTimesheetDTO
    {
        public Guid EmployeeId { get; set; }

        public DateTime FromDateTime { get; set; }

        public Guid TimeSheetId { get; set; }

        public DateTime ToDateTime { get; set; }
    }

    public class GetTimeSheetDTO
    {
        public GetTimeSheetToEmployeeDTO Employee { get; set; }

        public DateTime FromDateTime { get; set; }

        public Guid TimeSheetId { get; set; }

        public DateTime ToDateTime { get; set; }
    }
}