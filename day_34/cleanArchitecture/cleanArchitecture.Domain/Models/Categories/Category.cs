using cleanArchitecture.Domain.Models.Base;
using cleanArchitecture.Domain.Models.Products;

namespace cleanArchitecture.Domain.Models.Categories
{
    public class Category : Entity,  IAuditableEntity, ISoftDeletableEntity
    {
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
