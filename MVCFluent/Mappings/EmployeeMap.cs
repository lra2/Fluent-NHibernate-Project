using FluentNHibernate.Mapping;
using MVCFluent.Models;

namespace MVCFluent.Mappings
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            SchemaAction.None();
            Table("Employee");
            Id(x => x.Id);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Designation);
        }
    }
}