using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using touch_core_internal.Model;

namespace touch_core_internal.ORM.Nhibernate.Mapping
{
    public class RewardMap : ClassMapping<Reward>
    {
        public RewardMap()
        {
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.GuidComb);
                x.Type(NHibernateUtil.Guid);
                //x.UnsavedValue(null);
            });

            this.ManyToOne(x => x.Badge, map => map.Column("BadgeId"));
            this.Property(x => x.BadgeId, map => { map.Insert(false); map.Update(false); map.Column("BadgeId"); });

            this.ManyToOne(x => x.Employee, map => map.Column("EmployeeId"));
            this.Property(x => x.EmployeeId, map => { map.Insert(false); map.Update(false); map.Column("EmployeeId"); });

            Table("Rewards");
        }
    }
}