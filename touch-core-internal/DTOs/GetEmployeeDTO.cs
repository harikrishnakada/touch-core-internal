using System;
using System.Collections.Generic;

namespace touch_core_internal.DTOs
{
    public class GetEmployeeDTO
    {
        public virtual string Designation { get; set; }

        public virtual string Email { get; set; }

        public virtual Guid EmployeeId { get; set; }

        public virtual string Identifier { get; set; }

        public virtual string Name { get; set; }

        public IList<GetEmployeeToTimesheetDTO> TimeSheets { get; set; }
    }

    public class GetTimeSheetToEmployeeDTO
    {
        public virtual string Designation { get; set; }

        public virtual string Email { get; set; }

        public virtual Guid EmployeeId { get; set; }

        public virtual string Identifier { get; set; }

        public virtual string Name { get; set; }
    }
}