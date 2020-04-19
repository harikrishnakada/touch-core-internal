using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using touch_core_internal.Model;

namespace touch_core_internal.ORM.Nhibernate.Mapping
{
    public class TimeSheetMap: ClassMapping<TimeSheet>
    {
        public TimeSheetMap()
        {
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.GuidComb);
                x.Type(NHibernateUtil.Guid);
                //x.UnsavedValue(null);
            });

            this.ManyToOne(x => x.Employee, map => map.Column("EmployeeId"));
            this.Property(x => x.EmployeeId, map => { map.Insert(false); map.Update(false); map.Column("EmployeeId"); });

            this.Property(x => x.FromDateTime);
            this.Property(x => x.ToDateTime);
            this.Property(x => x.Comments);

            Table("TimeSheet");
        }
    }
}
