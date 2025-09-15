
using cleanArchitecture.Domain.Models.Base;
using cleanArchitecture.Domain.Models.Categories;

namespace cleanArchitecture.Domain.Models.Products
{
    public class Product : Entity, IAuditableEntity, ISoftDeletableEntity
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}