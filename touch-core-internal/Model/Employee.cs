using System;

namespace touch_core_internal.Model
{
    public class Employee
    {
        public virtual string Designation { get; set; }

        public virtual string Email { get; set; }

        public virtual Guid Id { get; set; }

        public virtual string Identifier { get; set; }

        public virtual string Name { get; set; }

        public virtual byte[] Photo { get; set; }

        //public virtual ISet<Recognition> Recognitions { get; set; } = new HashSet<Recognition>();
    }
}