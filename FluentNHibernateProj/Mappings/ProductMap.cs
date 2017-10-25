using FluentNHibernate.Mapping;
using FluentNHibernateProj.Entities;

namespace FluentNHibernateProj.Mappings
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            SchemaAction.None(); //evita dar erro do tipo "tabela já existe no DB"
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Price);
            HasManyToMany(x => x.StoresStockedIn)
              .Cascade.All()
              .Inverse()
              .Table("StoreProduct");
        }
    }
}
