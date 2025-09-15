using Ardalis.Specification;
using cleanArchitecture.Domain.Models.Carts;

namespace cleanArchitecture.Application.Features.Carts.Specifications;

public class CartItemByCartAndProductSpec : Specification<CartItem>
{
    public CartItemByCartAndProductSpec(Guid cartId, Guid productId)
    {
        Query.Where(ci => ci.CartId == cartId && ci.ProductId == productId);
    }
}
