using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Type;

namespace touch_core_internal.ORM.Nhibernate
{
    public static class NhibernateExtensions
    {
        public static ISessionFactory SessionFactory { get; private set; }

        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
       {
            var mapper = new ModelMapper();

            mapper.AddMappings(typeof(NhibernateExtensions).Assembly.ExportedTypes);
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new NHibernate.Cfg.Configuration();
            configuration.DataBaseIntegration(c =>
            {
                c.Dialect<MsSql2012Dialect>();
                c.ConnectionString = connectionString;
                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                // c.SchemaAction = SchemaAutoAction.Validate;
                c.LogFormattedSql = true;
                c.LogSqlInConsole = true;
            });

            configuration.AddMapping(domainMapping);

            SessionFactory = configuration.BuildSessionFactory();

            return services;
        }
    }
}