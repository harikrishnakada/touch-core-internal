using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using touch_core_internal.Model;

namespace touch_core_internal.ViewModel
{
    public class EmployeeViewModel : IViewModel<Employee>
    {
        public virtual string Designation { get; set; }

        public virtual string Email { get; set; }

        public virtual string Identifier { get; set; }

        public virtual string Name { get; set; }

        public virtual byte[] Photo { get; set; }

        public virtual void Write(Employee entity)
        {
            entity.Identifier = this.Identifier;
            entity.Name = this.Name;
            entity.Email = this.Email;
            entity.Designation = this.Designation;
            entity.Photo = this.Photo;
        }
    }
}