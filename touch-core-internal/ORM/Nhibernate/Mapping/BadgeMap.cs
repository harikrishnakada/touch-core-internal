using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using touch_core_internal.Model;

namespace touch_core_internal.ORM.Nhibernate.Mapping
{
    public class BadgeMap : ClassMapping<Badge>
    {
        public BadgeMap()
        {
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.GuidComb);
                x.Type(NHibernateUtil.Guid);
                //x.UnsavedValue(null);
            });
            this.Property(x => x.BadgeName);
            this.Property(x => x.Category);
            this.Property(x => x.Score);
            Table("Badges");
        }
    }
}