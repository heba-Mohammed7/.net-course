using Ardalis.Specification;
using cleanArchitecture.Domain.Models.Carts;

namespace cleanArchitecture.Application.Features.Carts.Specifications;

public class CartItemsByCartIdSpec : Specification<CartItem>
{
    public CartItemsByCartIdSpec(Guid cartId, bool includeProduct = false)
    {
        Query.Where(ci => ci.CartId == cartId);
        
        if (includeProduct)
        {
            Query.Include(ci => ci.Product);
        }
    }
}
