using cleanArchitecture.Domain.Models.Base;

namespace cleanArchitecture.Domain.Models.Carts
{
    public class Cart : Entity, IAuditableEntity, ISoftDeletableEntity
    {
        public string SessionId { get; set; } 
        public decimal TotalAmount { get; set; }
        public int ItemCount { get; set; }
        public virtual List<CartItem> CartItems { get; set; } = new();
    }
}
