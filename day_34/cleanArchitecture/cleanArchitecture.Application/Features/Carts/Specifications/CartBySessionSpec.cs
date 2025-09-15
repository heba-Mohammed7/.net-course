using Ardalis.Specification;
using cleanArchitecture.Domain.Models.Carts;

namespace cleanArchitecture.Application.Features.Carts.Specifications;

public class CartBySessionSpec : Specification<Cart>
{
    public CartBySessionSpec(string sessionId)
    {
        Query.Where(c => c.SessionId == sessionId);
    }
}
