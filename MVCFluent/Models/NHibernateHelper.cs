using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

//@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-MVCFluent-20171024125445.mdf;Initial Catalog=aspnet-MVCFluent-20171024125445;Integrated Security=True"
//@"Data Source=DESKTOP-0QUBDNJ\SQLEXPRESS;Initial Catalog=nhdb;Integrated Security=True"

namespace MVCFluent.Models
{
    public class NHibernateHelper
    {
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
               .Database(MsSqlConfiguration.MsSql2012
                .ConnectionString(@"Data Source=DESKTOP-0QUBDNJ\SQLEXPRESS;Initial Catalog=nhdb;Integrated Security=True")
                .ShowSql())
               .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<Employee>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }

        private static void BuildSchema(Configuration config)
        {
            var schema = new SchemaExport(config);
            schema.Create(false, false);
        }
    }
}