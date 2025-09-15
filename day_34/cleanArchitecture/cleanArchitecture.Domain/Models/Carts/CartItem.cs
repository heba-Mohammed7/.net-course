using cleanArchitecture.Domain.Models.Base;
using cleanArchitecture.Domain.Models.Products;

namespace cleanArchitecture.Domain.Models.Carts
{
    public class CartItem : Entity, IAuditableEntity, ISoftDeletableEntity
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        
        public virtual Cart Cart { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
