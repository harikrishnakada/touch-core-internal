using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

using touch_core_internal.Model;

namespace touch_core_internal.ORM.Nhibernate.Maping
{
    public class EmployeeMap : ClassMapping<Employee>
    {
        public EmployeeMap()
        {
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.GuidComb);
                x.Type(NHibernateUtil.Guid);
                //x.UnsavedValue(null);
            });

            this.Property(x => x.Identifier);
            this.Property(x => x.Name);
            this.Property(x => x.Email);
            this.Property(x => x.Designation);
            this.Property(x => x.Photo);

            //Uncomment this when recognitions is implemented
            //this.Set(x => x.Recognitions, map => { map.Key(k => k.Column("EmployeeId")); }, action => action.OneToMany());

            Table("Employees");
        }
    }
}