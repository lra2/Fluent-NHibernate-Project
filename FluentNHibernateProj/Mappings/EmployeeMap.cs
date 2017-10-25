using FluentNHibernate.Mapping;
using FluentNHibernateProj.Entities;

namespace FluentNHibernateProj.Mappings
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            SchemaAction.None(); //evita dar erro do tipo "tabela já existe no DB"
            Id(x => x.Id);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            References(x => x.Store);
        }
    }
}
