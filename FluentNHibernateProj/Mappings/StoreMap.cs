using FluentNHibernate.Mapping;
using FluentNHibernateProj.Entities;

namespace FluentNHibernateProj.Mappings
{
    public class StoreMap : ClassMap<Store>
    {
        public StoreMap()
        {
            SchemaAction.None(); //evita dar erro do tipo "tabela já existe no DB"
            Id(x => x.Id);
            Map(x => x.Name);
            HasMany(x => x.Staff)
              .Inverse()
              .Cascade.All();
            HasManyToMany(x => x.Products)
             .Cascade.All()
             .Table("StoreProduct");
        }
    }
}
